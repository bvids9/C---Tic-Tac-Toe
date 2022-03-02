using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameLogic
{
    public class TicTacToeGame
    {
        /// <summary>
        /// 
        /// Graphical User Interface for Tic Tac Toe program
        /// Provides visuals for all of the Board and Player Logic (stored under GameLogic)
        /// 
        /// </summary>
        /// 

        // Board Dimensions
        int row = 3;
        int col = 3;

        // Board Backend
        Board game = new Board(3, 3);


        // Players
        Player.HumanPlayer player1 = new Player.HumanPlayer(Board.Symbol.X);

        // Player 2 is declared as ComputerPlayer (can become AI)
        // But can still behave as Human with settings
        Player.ComputerPlayer player2 = new Player.ComputerPlayer(Board.Symbol.O);

        // Game Session Variables
        public bool hardMode = false; // AI Difficulty

        // Redo_Undo Variables
        public Stack<Point> move_list = new Stack<Point>(); // move_list for undo moves
        public Stack<Point> redo_list = new Stack<Point>(); // move_list for redo moves
        public Point lastElement = new Point(); // Placeholder for the undo/redo move stacks

        // Turn tracking
        public int turn_flag = 1; // Player turn, 1 = Player1, 2 = Player2
        public bool vsAI = true; // If true, game is played against an AI player.
        public bool paused = false;

        // Human moves
        public void humanMove(Point move)
        {
            if (turn_flag == 1)
            {
                // Make move and end turn
                player1.makeMove(game, move);
                turn_flag = 2;
            }
            // If vsAI is disabled, then second player is human
            else if (turn_flag == 2 && !vsAI)
            {
                // Make move and end turn
                player2.makeMove(game, move);
                turn_flag = 1;
            }
            // Record move in the Queue
            move_list.Push(move);
        }

        // AI Methods
        public void computerMove()
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
                }
                else
                {
                    // replace with hardmode moves.
                    move = player2.bestMove(game);
                }
                // Update grid and add move to the stack
                player2.makeMove(game, move);

                move_list.Push(move);
                // End Turns
                turn_flag = 1;

            }
        }

        // Access board states
        public int checkForWinner()
        {
            return game.checkForWinner();
        }

        public bool IsBoardFull()
        {
            return game.IsBoardFull();
        }

        public Board getBoard()
        {
            // Access the board of the gamestate
            return game;
        }

        // New game

        public void newGame()
        {
            // Generate a new board
            game = new Board(3, 3);

            // Clear the move_list 
            move_list.Clear();

            // Reset the turn flag
            turn_flag = 1;
        }

        // Save and Load Game Functions

        // Save Game

        public void saveGame()
        {
            // Call the save game function
            saveBoard(game, turn_flag, "board.txt");
            saveMoveList(move_list, "move_list.txt");
            saveMoveList(redo_list, "redo_list.txt");
        }
        private string[] stringPoint(Point[] pointArray)
        {
            // Convert point array to string arrays
            Point[] pList = pointArray.ToArray();
            string[] pString = new string[pList.Length];

            int index = 0;

            foreach (Point p in pList)
            {
                pString[index] = $"{p.X.ToString()}, {p.Y.ToString()}";
                index++;
            }

            return pString;
        }
        private void saveBoard(Board board, int turn_flag, string file)
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
            File.Delete(file);
            using (StreamWriter sw = File.AppendText(file))
            {
                for (int i = 0; i < boardSave.Length; i++)
                {
                    sw.WriteLine(boardSave[i]);
                }
            }
        }
        private void saveMoveList(Stack<Point> move_list, string file)
        {

            // Turn flag will be 10th entry
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(turn_flag);
                sw.Close();
            }

            // Convert Stacks to String Arrays
            string[] strMoveList = stringPoint(move_list.ToArray());

            // Appending movelists
            File.Delete(file);
            using (StreamWriter sw = File.AppendText(file))
            {
                for (int i = 0; i < strMoveList.Length; i++)
                {
                    sw.WriteLine(strMoveList[i].ToString());
                }
                sw.Close();
            }
        }

        // Load Game functions
        public void loadGame(string file)
        {
            // Call the board load function
            game = loadBoard(file);
        }
        private Board loadBoard(string boardfile)
        {
            // Function reads the board from the array file, including the current turn
            // The first n entries (row * col) is the board as a 1D array.

            Board board = new Board(row, col);
            string[] board_array = new string[row * col];
            int index = 0;

            // Read text and update turn_flag
            string[] saveText = readBoardfile(boardfile);

            // Update turn_flag
            turn_flag = loadTurn_flag(boardfile);

            // Generate a board from save file data

            // Load the board array (ensure rest of file is skipped).
            for (int i = 0; i < (row * col); i++)
            {
                board_array[i] = saveText[i];
            }
            // Convert 1D array to 2D
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    board.Grid[i, j] = Int32.Parse(board_array[index]);
                    index++;
                }
            }

            return board;
        }
        private string[] readBoardfile(string boardfile)
        {
            string[] boardSave = new string[9];
            int index = 0;
            // Read boards
            using (StreamReader sr = new StreamReader(boardfile))
            {
                for (int i = 0; i < 9; i++)
                {
                    string line = sr.ReadLine();
                    boardSave[index] = line;
                    index++;
                }
                sr.Close();
            }

            return boardSave;
        }
        private int loadTurn_flag(string boardfile)
        {
            // Turn_flag will be the last entry in 'board.txt'
            string turn = File.ReadAllLines(boardfile).Last();

            return int.Parse(turn);
        }

        // Load move lists
        private Stack<Point> loadMoveList(Stack<Point> mlist, string file)
        {
            // Function for reading the move lists
            mlist = readMove(file);
            return mlist;
        }

        public void loadUndoList(string file)
        {
            // Call the load functions for the move list
            move_list = loadMoveList(move_list, file);
        }
        public void loadRedoList(string file)
        {
            // Call the load function for the redo list
            redo_list = loadMoveList(redo_list, file);
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
                    if (line != null)
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

        // Utility methods
        private Stack<Point> rev(Stack<Point> stack)
        {
            // Utility function to reverse a stack
            // used for stacks read from text.
            Stack<Point> revStack = new Stack<Point>();
            while (stack.Count != 0)
            {
                revStack.Push(stack.Pop());
            }
            return revStack;
        }

        // Undo and redo methods
        public void undoMove()
        {
            // Undo the last move on the board
            lastElement = move_list.Pop();
            game.Grid[lastElement.X, lastElement.Y] = 0;

            // Add the move to the redo stack
            redo_list.Push(lastElement);

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

        }

        public void redoMove()
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
        }

    }
}
