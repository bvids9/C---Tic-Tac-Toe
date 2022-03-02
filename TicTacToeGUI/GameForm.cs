using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.IO;
using GameLogic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TicTacToeGUI
{
    public partial class GameForm : Form
    {
        /// <summary>
        /// 
        /// Graphical User Interface for Tic Tac Toe program
        /// Provides visuals for all of the Board and Player Logic (stored under GameLogic)
        /// 
        /// </summary>

        // Grid class using buttons
        Button[,] buttons = new Button[3, 3];

        // Game Session
        TicTacToeGame gameSession = new TicTacToeGame();
        public GameForm()
        {
            InitializeComponent();
            GenerateGrid();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateBoard();
        }

        // Grid visual generation
        private void GenerateGrid()
        {
            Board gameBoard = gameSession.getBoard();
            for (int i = 0; i < gameBoard.Row; i++)
            {
                for (int j = 0; j < gameBoard.Col; j++)
                {

                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(150, 150);
                    buttons[i, j].Location = new Point(i * 150, j * 150);
                    buttons[i, j].FlatStyle = FlatStyle.Flat;
                    buttons[i, j].Font = new Font(DefaultFont.FontFamily, 80, FontStyle.Bold);

                    panel1.Controls.Add(buttons[i, j]);

                    // Click event for each button
                    buttons[i, j].Click += handleButtonClick;

                    // Assign the clicked button an identifying coordinate (Point)
                    buttons[i, j].Tag = new Point(i, j);
                }
            }
        }

        // Board update
        private void updateBoard()
        {
            Board gameBoard = gameSession.getBoard();
            // Function is for the visual update of the board and surrounds
            // Assign X or O to each button
            for (int i = 0; i < gameBoard.Row; i++)
            {
                for (int j = 0; j < gameBoard.Col; j++)
                {
                    if (gameBoard.Grid[i, j] == 0)
                    {
                        buttons[i, j].Text = "";
                        buttons[i, j].Enabled = true;
                    }
                    else if (gameBoard.Grid[i, j] == 1)
                    {
                        buttons[i, j].Text = "X";
                        buttons[i, j].Enabled = false;
                    }
                    else if (gameBoard.Grid[i, j] == 2)
                    {
                        buttons[i, j].Text = "O";
                        buttons[i, j].Enabled = false;
                    }
                }
            }
        }

        // Caption bar update
        private void updateCaption()
        {
            // Update State caption
            // If the game is running, display which player's turn it is
            if (gameSession.turn_flag == 1 && gameSession.paused == false)
            {
                stateCaption.Text = "Player 1 Turn...";
            }
            if (gameSession.turn_flag == 2 && gameSession.paused == false)
            {
                stateCaption.Text = "Player 2 Turn...";
            }
            if (gameSession.paused == true)
            {
                stateCaption.Text = "Game paused...";
            }
            // Check wins and draw
            if (gameSession.checkForWinner() == 1)
            {
                stateCaption.Text = "Player 1 wins!";
            }

            if (gameSession.checkForWinner() == 2)
            {
                stateCaption.Text = "Player 2 wins!";
            }
            if (gameSession.IsBoardFull() && gameSession.checkForWinner() == 0)
            {
                stateCaption.Text = "Draw...";
            }
        }

        // Grid Button Event Function
        private void handleButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Retrieve the position of the clicked button
            Point pos = (Point)clickedButton.Tag;

            // Human makes move

            gameSession.humanMove(pos);


            // Update the board

            updateBoard();
            updateCaption();

            // Check board states
            if (gameSession.checkForWinner() == 1)
            {
                MessageBox.Show("Player 1 wins!");
                disableGrid();
            }
            else if (gameSession.IsBoardFull() == true && gameSession.checkForWinner() == 0)
            {
                MessageBox.Show("The board is full. The game is a draw.");
                disableGrid();
            }
            else
            {
                // Computer moves
                gameSession.computerMove();
                updateBoard();
                updateCaption();
                // Check if computer made a winning move
                if (gameSession.checkForWinner() == 2)
                {
                    MessageBox.Show("Player 2 wins!");
                    disableGrid();
                }
                
            }

        }

        // Grid Control
        private void disableGrid()
        {
            Board gameBoard = gameSession.getBoard();
            gameSession.paused = true;
            for (int i = 0; i < gameBoard.Row; i++)
            {
                for (int j = 0; j < gameBoard.Col; j++)
                {
                    buttons[i, j].Enabled = false;
                }
            }
        }

        private void enableGrid()
        {
            Board gameBoard = gameSession.getBoard();
            gameSession.paused = false;
            for (int i = 0; i < gameBoard.Row; i++)
            {
                for (int j = 0; j < gameBoard.Col; j++)
                {
                    buttons[i, j].Enabled = true;
                }
            }
            updateBoard();
        }

        // New Game Button
        private void btn_newGame_Click(object sender, EventArgs e)
        {
            // Function to generate a new game
            // Reinitialise the board
            gameSession.newGame();
            enableGrid();
            updateCaption();
            updateBoard();

        }

        // AI Difficulty
        private void btnHuman_CheckedChanged(object sender, EventArgs e)
        {
            // Set the vsAI flag to false
            gameSession.vsAI = false;
            if (btnHuman.Checked == true)
            {
                MessageBox.Show("Player versus Player selected.");
            }
        }

        private void btn_easyMode_CheckedChanged(object sender, EventArgs e)
        {
            // Radio button control for difficulty selection - easy mode
            // Used to flag if the AI will behave randomly or deliberately.
            gameSession.vsAI = true;
            if (btn_easyMode.Checked == true)
            {
                gameSession.hardMode = false;
                MessageBox.Show($"Easy mode selected. Computer will play randomly.");
            }

        }

        private void btn_hardMode_CheckedChanged(object sender, EventArgs e)
        {
            // Radio button control for difficulty selection - hard mode
            // Used to flag if the AI will behave randomly or deliberately.
            gameSession.vsAI = true;
            if (btn_hardMode.Checked == true)
            {
                gameSession.hardMode = true;
                MessageBox.Show($"Hard mode selected. Computer will apply heuristics.");
            }

        }

        // Redo and Undo Methods and Buttons
        private void btn_undo_Click(object sender, EventArgs e)
        {
            // Undo Button
            // Pauses the game

            // Check if the Stack has values in it, otherwise will generate error.

            if (gameSession.move_list.Count != 0)
            {
                gameSession.undoMove();
            }
            else
            {
                MessageBox.Show("No moves yet.");
            }

            updateBoard();
            disableGrid();
            updateCaption();

        }
        private void btn_Resume_Click(object sender, EventArgs e)
        {
            // Check if the game has been won or is a draw
            // If it has, do not resume.
            if (gameSession.checkForWinner() == 0 || gameSession.IsBoardFull() == false)
            {
                // Re-enable the grid
                // Check if it's the computer's turn and let them make a move.
                enableGrid();
                if (gameSession.turn_flag == 2)
                {
                    gameSession.computerMove();
                }
            }
            else
            {
                MessageBox.Show("Too late, the game has been completed.");
            }
            updateCaption();
        }
        private void btn_redo_Click(object sender, EventArgs e)
        {
            // Redo button event
            // Call the redo method and update visuals
            if (gameSession.redo_list.Count != 0)
            {
                gameSession.redoMove();
            }
            else
            {
                MessageBox.Show("No more moves.");
            }

            updateBoard();
            disableGrid();
            updateCaption();

        }

        // Save and Load Buttons
        private void btn_saveGame_Click(object sender, EventArgs e)
        {
            // Save Game
            gameSession.saveGame();
        }
        private void btn_loadGame_Click(object sender, EventArgs e)
        {
            // Load Game and update state
            gameSession.loadGame("board.txt");

            // Redo and undo lists
            gameSession.loadUndoList("move_list.txt");
            gameSession.loadRedoList("redo_list.txt");
            
            // Set variables

            updateBoard();
            updateCaption();
        }
    }
}
