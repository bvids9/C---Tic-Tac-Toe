using System.Drawing;
using System.Diagnostics;
namespace GameLogic
{
    public class Board
    {
        /// <summary>
        /// 
        /// Backend for the game, includes board functions and definitions
        /// 
        /// </summary>

        // Board grid definitions
        // Board can be re-sized
        public int[,] Grid{ get; set;}
        public int Row { get; set; }
        public int Col { get; set; }

        public enum Symbol { X, O, Empty };

        public Board(int row, int col)
        {
            Grid = new int[row, col];
            Row = row;
            Col = col;
            // Grid size set to 3, 3
            Grid = new int[row, col];
            // initialise the Grid
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Grid[i, j] = 0;
                }
            }
        }

        // Symbol Functions
        public int getIntSymbol(Symbol symbol)
        {
            // Convert Symbol to Integer
            if (symbol == Symbol.O)
            {
                return 2;
            }
            else if(symbol == Symbol.X)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Board.Symbol getSymbol(int i)
        {
            // Convert integer to Symbol
            if (i == 2)
            {
                return Symbol.O;
            }
            else if (i == 2)
            {
                return Symbol.X;
            }
            else
            {
                return Symbol.Empty;
            }
        }
        public int getOpponent(Symbol symbol)
        {
            // Return opposite symbol integer
            // Otherwise return null
            if (symbol == Symbol.O)
            {
                return 1;
            }
            else if (symbol == Symbol.X)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        // Move and board state functions
        public void updatePosition(Point position, Symbol symbol)
        {
            // Update the board position based on player input
            // Update position
            int symbolNum = getIntSymbol(symbol);
            Point point = position;
            Grid[point.X, point.Y] = symbolNum;

        }
        public int checkForWinner()
        {
            
            // Check rows
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (Grid[i, j] == 0)
                    {
                        break;
                    }
                    else if(Grid[i, 0] == Grid[i, 1] && Grid[i, 1] == Grid[i, 2])
                    {
                        return Grid[i, j];
                    }
                }
            }

            // Check columns

            for (int j = 0; j < Col; j++)
            {
                for (int i = 0; i < Row; i++)
                {
                    if (Grid[j, i] == 0)
                    {
                        break;
                    }
                    else if(Grid[0, i] == Grid[1, i] && Grid[1, i] == Grid[2, i])
                    {
                        return Grid[j, i];
                    }
                }
            }

            // Check Two Diagonals

            if (Grid[0, 0] == Grid[1, 1] && Grid[1,1] == Grid[2, 2])
            {
                return Grid[0, 0];
            }

            if (Grid[0, 2] == Grid[1,1] && Grid[1,1] == Grid[2, 0])
            {
                return Grid[0, 2];
            }
            return 0;
        }

        public bool IsBoardEmpty()
        {
            // Check if board is empty
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Grid[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public bool IsBoardFull()
        {
            // Check if board is full
            // assume board is full until otherwise proven.
            bool isFull = true;


            for (int i = 0; i< Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (Grid[i, j] == 0)
                    {
                        isFull = false;
                    }
                }
            }
            return isFull;
        }

        public List<Point> emptyPositions()

        {
            // Generate a list of all empty positions on the board
            // Used for AI
            List<Point> emptyPos = new List<Point>();

            for(int i = 0; i < Row; i++)
            {
                for(int j = 0; j < Col; j++)
                {
                    if(Grid[i, j] == 0)
                    {
                        emptyPos.Add(new Point(i, j));
                    }
                }
            }
            return emptyPos;
        }

        // Board utility functions
        public bool Equals(Board board, Board copyBoard)
        {
            // Determine if two boards are alike.
            for (int i = 0; i < board.Row; i++)
            {
                for (int j = 0; j < board.Col; j++)
                {
                    if (board.Grid[i, j] != copyBoard.Grid[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void printBoard()
        {
            // Console Function to Print the Board in ASCII
            // Used for troubleshooting and Console prototype.
            for (int i = 0; i < Row ; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (Grid[i, j] == 0)
                    {
                        Console.Write(".");
                        Debug.Write(".");
                    }
                    if (Grid[i, j] == 1)
                    {
                        Console.Write("X");
                        Debug.Write("X");
                    }
                    if (Grid[i, j] == 2)
                    {
                        Console.Write("O");
                        Debug.Write("O");
                    }

                    // Check if lines need to be printed (Horizontally)

                    if (j == 2)
                    {
                        Console.WriteLine();
                        Debug.WriteLine("");
                    }
                }
            }
        }

    }
}