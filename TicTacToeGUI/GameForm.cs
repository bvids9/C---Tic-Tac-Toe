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
        // Board Backend
        Board game = new Board(3, 3);

        // Players
        Player.HumanPlayer player1 = new Player.HumanPlayer(Board.Symbol.X);

        // Player 2 is declared as ComputerPlayer (can become AI)
        // But can still behave as Human with settings
        Player.ComputerPlayer player2 = new Player.ComputerPlayer(Board.Symbol.O);

        // Grid class using buttons
        Button[,] buttons = new Button[3, 3];

        // Game Session Variables
        private bool hardMode = false; // AI Difficulty

        // Redo_Undo Variables
        private Stack<Point> move_list = new Stack<Point>(); // move_list for undo moves
        private Stack<Point> redo_list = new Stack<Point>(); // move_list for redo moves
        private Point lastElement = new Point(); // Placeholder for the undo/redo move stacks

        // Turn tracking
        private int turn_flag = 1; // Player turn, 1 = Player1, 2 = Player2
        private bool vsAI = true; // If true, game is played against an AI player.
        private bool paused = false;



        public GameForm()
        {
            InitializeComponent();
            game = new Board(3, 3);
            GenerateGrid();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateBoard();
        }

        private void GenerateGrid()
        {
            for (int i = 0; i < game.Row; i++)
            {
                for (int j = 0; j < game.Col; j++)
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

        private void updateBoard()
        {
            // Function is for the visual update of the board and surrounds
            // Assign X or O to each button
            for (int i = 0; i < game.Row; i++)
            {
                for (int j = 0; j < game.Col; j++)
                {
                    if (game.Grid[i, j] == 0)
                    {
                        buttons[i, j].Text = "";
                        buttons[i, j].Enabled = true;
                    }
                    else if (game.Grid[i, j] == 1)
                    {
                        buttons[i, j].Text = "X";
                        buttons[i, j].Enabled = false;
                    }
                    else if (game.Grid[i, j] == 2)
                    {
                        buttons[i, j].Text = "O";
                        buttons[i, j].Enabled = false;
                    }
                }
            }
        }

        private void updateCaption()
        {
            // Update State caption
            // If the game is running, display which player's turn it is
            if (turn_flag == 1 && paused == false)
            {
                stateCaption.Text = "Player 1 Turn...";
            }
            if (turn_flag == 2 && paused == false)
            {
                stateCaption.Text = "Player 2 Turn...";
            }
            if (paused == true)
            {
                stateCaption.Text = "Game paused...";
            }
            // Check wins and draw
            if (game.checkForWinner() == 1)
            {
                stateCaption.Text = "Player 1 wins!";
            }

            if (game.checkForWinner() == 2)
            {
                stateCaption.Text = "Player 2 wins!";
            }
            if (game.IsBoardFull() && game.checkForWinner() == 0)
            {
                stateCaption.Text = "Draw...";
            }
        }

        // Button functions
        private void handleButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Retrieve the position of the clicked button
            Point pos = (Point)clickedButton.Tag;

            if (turn_flag == 1)
            {
               // Make move and end turn
                player1.makeMove(game, pos);
                turn_flag = 2;
            }

            // If vsAI is disabled, then second player is human
            else if (turn_flag == 2 && !vsAI)
            {
                // Make move and end turn
                player2.makeMove(game, pos);
                turn_flag = 1;
            }


            // Record move in the Queue
            move_list.Push(pos);


            // Update the board

            updateBoard();
            updateCaption();

            // Check winning conditions
            if (game.checkForWinner() == 1)
            {
                MessageBox.Show("Player 1 wins!");
                disableGrid();
            }
            else if(game.checkForWinner() == 2)
            {
                MessageBox.Show("Player 2 wins!");
                disableGrid();
            }
            else if (game.IsBoardFull() == true && game.checkForWinner() == 0)
            {
                MessageBox.Show("The board is full. The game is a draw.");
                disableGrid();
            }
            else
            {
                // Computer moves
                computerMove();
            }


        }

        // AI Methods
        private void computerMove()
        {
            // Check if vsAI is true
            // If false do nothing.
            if (vsAI)
            {
                // Update the move based on the computers selected move
                Point move = new Point();
                if (hardMode == false)
                {
                    // EasyMode
                    move = player2.genEasyMove(game);
                    Debug.WriteLine($"Computer chooses: {move}.");
                }
                else
                {
                    // replace with hardmode moves.
                    move = player2.bestMove(game);
                    Debug.WriteLine($"Computer chooses: {move}.");
                }
                // Update grid and add move to the stack
                player2.makeMove(game, move);

                move_list.Push(move);
                // End Turns
                turn_flag = 1;
                //endTurn(player1, player2);

                // Visual updates
                updateBoard();
                updateCaption();

                // Check for a winner
                // Check if the board is full

                if (game.IsBoardFull() == true)
                {
                    MessageBox.Show("The board is full. The game is a draw.");
                    disableGrid();
                }

                else if (game.checkForWinner() == 2)
                {
                    MessageBox.Show("Player 2 is the winner!");
                    disableGrid();
                }
            }
        }

        // Grid Control
        private void disableGrid()
        {
            paused = true;
            for (int i = 0; i < game.Row; i++)
            {
                for (int j = 0; j < game.Col; j++)
                {
                    buttons[i, j].Enabled = false;
                }
            }
        }

        private void enableGrid()
        {
            paused = false;
            for (int i = 0; i < game.Row; i++)
            {
                for (int j = 0; j < game.Col; j++)
                {
                    buttons[i, j].Enabled = true;
                }
            }
            updateBoard();
        }

        // New Game, AI Diffuclty Setting
        private void btn_newGame_Click(object sender, EventArgs e)
        {
            // Function to generate a new game
            // Reinitialise the board
            game = new Board(3, 3);
            enableGrid();

            // Clear the move_list 
            move_list.Clear();

            // Reset the turn flag
            turn_flag = 1;
            updateCaption();
            updateBoard();

        }

        // Save and Load Game Functions

        private string[] stringPoint(Point[] pointArray)
        {
            // Convert point array to string arrays
            Point[] pList = pointArray.ToArray();
            string[] pString = new string[pList.Length];

            int index = 0;

            foreach(Point p in pList)
            {
                pString[index] = $"{p.X.ToString()}, {p.Y.ToString()}";
                index++;
            }

            return pString;
        }
        private void saveGame(Board board, Stack<Point> move_list, Stack<Point> redo_list, int turn_flag)
        {
            // Write board to file
            string[] boardSave = new string[9];
            int index = 0;

            for (int i = 0; i < board.Row; i++)
            {
                for (int j = 0; j < board.Col; j++)
                {
                    boardSave[index] = board.Grid[i, j].ToString();
                    index++;
                }
            }
            // Write board
            File.Delete("board.txt");
            using (StreamWriter sw = File.AppendText("board.txt"))
            {
                for (int i = 0; i < boardSave.Length; i++)
                {
                    sw.WriteLine(boardSave[i]);
                }
            }

            // Turn flag will be 10th entry
            using (StreamWriter sw = File.AppendText("board.txt"))
            {
                sw.WriteLine(turn_flag);
                sw.Close();
            }

            // Convert Stacks to String Arrays
            string[] strMoveList = stringPoint(move_list.ToArray());
            string[] strRedoList = stringPoint(redo_list.ToArray());

            // Appending movelists
            File.Delete("movelist.txt");
            using (StreamWriter sw = File.AppendText("movelist.txt"))
            {
                for (int i = 0; i < strMoveList.Length; i++)
                {
                    sw.WriteLine(strMoveList[i].ToString());
                }
                sw.Close();
            }
            File.Delete("redolist.txt");
            using (StreamWriter sw = File.AppendText("redolist.txt"))
            {
                for (int i = 0; i < strRedoList.Length; i++)
                {
                    sw.WriteLine(strRedoList[i].ToString());
                }
                sw.Close();
            }

        }

        // Load Game functions
        private string[] loadBoard(string boardfile)
        {
            string[] boardSave = new string[9];
            int index = 0;
            // Read boards
            using (StreamReader sr = new StreamReader(boardfile))
            {
                for(int i = 0; i < 9; i++)
                {
                    string line = sr.ReadLine();
                    boardSave[index] = line;
                    index++;
                }
                sr.Close();
            }

            return boardSave;
        }

        private Stack<Point> readMove(string file)
        {
            // Function takes saved moves and transforms into a stack
            // For reuse in the program.

            string moves = File.ReadAllText(file);
            int index = 0;
            Point[] points = new Point[moves.Length];
            Stack<Point> moveStack = new Stack<Point>();

            using (StreamReader sr = new StreamReader(file))
            {
                for (int i = 0; i < 9; i++)
                {
                    string line = sr.ReadLine();
                    if(line != null)
                    {
                        string[] coords = line.Split(',');
                        points[index] = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                        moveStack.Push(points[index]);
                    }
                }
                sr.Close();
            }
            // Reverse the stack before returning.
            return rev(moveStack);
        }

        private int loadTurn_flag(string boardfile)
        {
            // Turn_flag will be the last entry in 'board.txt'
            string turn = File.ReadAllLines(boardfile).Last();

            return int.Parse(turn);
        }
        private Board readBoard(string[] saveText, int row, int col)
        {
            // Function reads the board from the array file
            // The first n entries (row * col) is the board as a 1D array.

            Board board = new Board(row, col);
            string[] board_array = new string[row * col];
            int index = 0;
            // Generate a board from save file data

            // Load the board array (ensure rest of file is skipped).
            for(int i = 0; i < (row * col); i++)
            {
                board_array[i] = saveText[i];
            }
            // Convert 1D array to 2D
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Debug.WriteLine(board_array[index]);
                    board.Grid[i, j] = Int32.Parse(board_array[index]);
                    Debug.WriteLine($"{board.Grid[i, j]}");
                    index++;
                }
            }
            
            return board;
        }

        // Utility methods

        private Stack<Point> rev(Stack<Point> stack)
        {
            // Utility function to reverse a stack
            // used for stacks read from text.
            Stack<Point> revStack = new Stack<Point> ();
            while(stack.Count != 0)
            {
                revStack.Push(stack.Pop());
            }
            return revStack;
        }

        // Undo and redo methods
        private void undoMove()
        {
            // Undo the last move on the board
            lastElement = move_list.Pop();
            game.Grid[lastElement.X, lastElement.Y] = 0;

            // Add the move to the redo stack
            redo_list.Push(lastElement);

            updateBoard();

            // If we're undoing on the Player 1's turn, reset the clock
            // Undo function will effectively pause the game
            //endTurn(player1, player2);
            if (turn_flag == 1)
            {
                turn_flag = 2;
            }
            else if (turn_flag == 2)
            {
                turn_flag = 1;
            }

            disableGrid();

        }

        private void redoMove()
        {
            // Redo the last move on the board
            // Remove it from the redo_list and move it back to the move_list
            lastElement = redo_list.Pop();
            move_list.Push(lastElement);

            // Check which turn was 'undone'
            if (turn_flag == 1)
            {
                turn_flag = 2;
                game.Grid[lastElement.X, lastElement.Y] = 1;
            }
            else
            {
                turn_flag = 1;
                game.Grid[lastElement.X, lastElement.Y] = 2;
            }
            updateBoard();
            updateCaption();
            disableGrid();
        }

        // Button methods

        private void btn_easyMode_CheckedChanged(object sender, EventArgs e)
        {
            // Radio button control for difficulty selection - easy mode
            // Used to flag if the AI will behave randomly or deliberately.
            vsAI = true;
            if (btn_easyMode.Checked == true)
            {
                hardMode = false;
                MessageBox.Show($"Easy mode selected. Computer will play randomly.");
            }

        }

        private void btn_hardMode_CheckedChanged(object sender, EventArgs e)
        {
            // Radio button control for difficulty selection - hard mode
            // Used to flag if the AI will behave randomly or deliberately.
            vsAI = true;
            if (btn_hardMode.Checked == true)
            {
                hardMode = true;
                MessageBox.Show($"Hard mode selected. Computer will apply heuristics.");
            }

        }


        // Redo and Undo Methods and Buttons
        private void btn_undo_Click(object sender, EventArgs e)
        {
            // Prototype undo button
            // Pauses the game



            // Check if the Stack has values in it, otherwise will generate error.

            if (move_list.Count != 0)
            {
                undoMove();
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
            if (game.checkForWinner() == 0 || game.IsBoardFull() == false)
            {
                // Re-enable the grid
                // Check if it's the computer's turn and let them make a move.
                enableGrid();
                if (turn_flag == 2)
                {
                    computerMove();
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
            if (redo_list.Count != 0)
            {
                redoMove();
            }
            else
            {
                MessageBox.Show("No more moves.");
            }

            updateBoard();
            disableGrid();
            updateCaption();

        }
        private void btnHuman_CheckedChanged(object sender, EventArgs e)
        {
            // Set the vsAI flag to false
            vsAI = false;
            if(btnHuman.Checked == true)
            {
                MessageBox.Show("Player versus Player selected.");
            }
        }

        private void btn_saveGame_Click(object sender, EventArgs e)
        {
            // Save Game
            saveGame(game, move_list, redo_list, turn_flag);
        }

        private void btn_loadGame_Click(object sender, EventArgs e)
        {
            // Load Game and update state
            string[] strBoard = loadBoard("board.txt");

            // Update Board
            game = readBoard(strBoard, game.Row, game.Col);

            // Load turn flag
            turn_flag = loadTurn_flag("board.txt");

            // Redo and undo lists
            move_list = readMove("movelist.txt");
            redo_list = readMove("redolist.txt");
            

            // Set variables

            updateBoard();
            updateCaption();
        }
    }
}
