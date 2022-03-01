using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
using System.Drawing;
using System.Collections;
using System.Diagnostics;

namespace GameLogic
{
    public abstract class Player
    {
        /// <summary>
        /// 
        /// Player classes:
        /// Base Player class has a symbol and can make a move on the board.
        /// Computer Player extends this and generates moves, based on chosen
        /// complexity.
        /// 
        /// </summary>

        // Abstraction of the player class
        public Board.Symbol PlayerSymbol { get; set; }
        
        // Move method
        public void makeMove(Board board, Point move)
        {
            board.updatePosition(move, PlayerSymbol);
        }

        // Base class
        public Player(Board.Symbol symbol)
        {
            this.PlayerSymbol = symbol;
        }
        public Board.Symbol opponentSymbol(Board.Symbol symbol)
        {
            // Return opposite symbol
            // Used to determine opponent symbol
            if (symbol == Board.Symbol.O)
            {
                return Board.Symbol.X;
            }
            else if (symbol == Board.Symbol.X)
            {
                return Board.Symbol.O;
            }
            else
            {
                return Board.Symbol.Empty;
            }

        }
        public class HumanPlayer : Player
        {
            public HumanPlayer(Board.Symbol symbol) : base(symbol)
            {
                this.PlayerSymbol = symbol;
            }
        }
        public class ComputerPlayer : Player
        {

            public ComputerPlayer(Board.Symbol symbol) : base(symbol)
            {
                this.PlayerSymbol = symbol;
            }
            private Point move = new Point();
            public Point genEasyMove(Board board)
            {

                // Generate an 'easy' move by using randomisation.
                // Computer generates a random number. Update grid to reflect choice.
                // Generate a random set of coordinates
                Random rand = new Random();

                while (move.X == -1 && move.Y == -1 || board.Grid[move.X, move.Y] != 0)
                {
                    move.X = rand.Next(3);
                    move.Y = rand.Next(3);
                    Console.WriteLine($"Computer chooses position {move.X}, {move.Y}.");
                    Debug.WriteLine($"Computer chooses position {move.X}, {move.Y}.");
                }

                return move;

            }

            public Point bestMove(Board board)
            {
                // Generates a move based on heuristics
                // Uses minimax algorithm to generate best move

                int bestScore = -1000;
                Point move = new Point();

                // Go through all cells, evaluate
                // minimax function and return cell with optimal value

                for (int i = 0; i < board.Row; i++)
                {
                    for (int j = 0; j < board.Col; j++)
                    {
                        Point p = new Point(i, j);

                        // Check if cell is empty
                        if(board.Grid[i, j] == 0)
                        {
                            board.updatePosition(p, Board.Symbol.O);

                            int score = minimax(board, 0, false);

                            board.updatePosition(p, Board.Symbol.Empty);

                            // If the returned cell is higher value than
                            // previous best cell
                            if(score > bestScore)
                            {
                                Console.WriteLine($"Best Score: {bestScore}, {move}.");
                                move.X = i;
                                move.Y = j;
                                bestScore = score;
                            }
                        }
                    }
                }

                Console.WriteLine($"Minimax Move: {bestScore}, {move}.");

                return move;

            }

            public int evalBoard(Board board)
            {
                // Scoring function for board states.
                // Supports minimax algorithm
                if(board.checkForWinner() == 2)
                {
                    return 10;
                }
                if(board.checkForWinner() == 1)
                {
                    return -10;
                }
                else
                {
                    return 0;
                }
            }
            public int minimax(Board board, int depth, bool isMax)
            {
                // Minimax algorithm to search board
                // Scoring the board
                int score = evalBoard(board);

                // If maximiser has won
                if (score == 10)
                {
                    return score - depth;
                }
                // If minimiser has won
                if (score == -10)
                {
                    return score - depth;
                }
                // If board is full
                if (board.IsBoardFull())
                {
                    return 0 - depth;
                }

                // If maximiser's move.
                // Maximiser is the AI player - they want to maximise their chance of victory.
                if (isMax)
                {
                    int best = -1000;

                    // Loop through all elements of the board
                    // Check for empty positions

                    for (int i = 0; i < board.Row; i++)
                    {
                        for (int j = 0; j < board.Col; j++)
                        {
                            // Find empty position
                            // Call minimax recursively
                            if(board.Grid[i, j] == 0)
                            {
                                // Make a move
                                Point p = new Point(i, j);
                                board.updatePosition(p, PlayerSymbol);

                                best = Math.Max(best, minimax(board, depth + 1, false));

                                board.updatePosition(p, Board.Symbol.Empty);
                            }
                        }
                    }
                    return best;
                }
                else
                {
                    int best = 1000;
                    for (int i = 0; i < board.Row; i++)
                    {
                        for (int j = 0; j < board.Col; j++)
                        {
                            if (board.Grid[i, j] == 0)
                            {
                                Point p = new Point(i, j);
                                board.updatePosition(p, opponentSymbol(PlayerSymbol));

                                best = Math.Min(best, minimax(board, depth + 1, true));

                                board.updatePosition(p, Board.Symbol.Empty);
                            }
                        }
                    }
                    return best;
                }

            }
        }
    }
}
