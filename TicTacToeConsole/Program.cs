using System;
using GameLogic;
using System.Drawing;

namespace TicTacToe
{
    class Program
    {
        /// <summary>
        /// Console program of Tic Tac Toe Board
        /// Uses same classes as GUI
        /// Not part of final project
        /// </summary>
        // Board is the data store
        static Board game = new Board(3, 3);

        // Players
        static Player.HumanPlayer player1 = new Player.HumanPlayer(Board.Symbol.X);
        static Player.ComputerPlayer player2 = new Player.ComputerPlayer(Board.Symbol.O);


        static void Main(string[] args)
        {


            Point userTurn = new Point(-1, -1);
            Point computerTurn = new Point();
            Stack<Point> move_list = new Stack<Point>();


        Random rand = new Random();

            while (game.checkForWinner() == 0)
            {
                // Check for valid moves for human player
                while (userTurn.X == -1 && userTurn.Y == -1  || game.Grid[userTurn.X, userTurn.Y] != 0)
                {
                    Console.WriteLine("Please enter a row between 0 and 2");
                    userTurn.X = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter a column between 0 and 2");
                    userTurn.Y = int.Parse(Console.ReadLine());
                    Console.WriteLine($"You have selected position {userTurn.X}, {userTurn.Y}.");
                }
                game.updatePosition(userTurn, player1.PlayerSymbol);
                move_list.Push(userTurn);

                if (game.IsBoardFull() || game.checkForWinner() == 1)
                {
                    printBoard(game);
                    break;
                }

                // Computer generates a move
                computerTurn = player2.bestMove(game);

                game.updatePosition(computerTurn, player2.PlayerSymbol);
                move_list.Push(computerTurn);

                if (game.IsBoardFull() || game.checkForWinner() == 2)
                {
                    printBoard(game);
                    break;
                }
                

                printBoard(game);
                if(game.checkForWinner() != 0)
                {
                    printBoard(game);
                    break;
                }
                Console.WriteLine(game.checkForWinner());
            }

            Console.WriteLine($"Player {game.checkForWinner()} won the game.");

            Board newBoard = (Board)game.Clone();
            Console.WriteLine("New Board:....");
            printBoard(newBoard);
            Console.ReadLine();

        }
        private static void printBoard(Board game)
        {
            // Console Function to Print the Board in ASCII
            for (int i = 0; i < game.Row; i++)
            {
                for (int j = 0; j < game.Col; j++)
                {
                    if (game.Grid[i, j] == 0)
                    {
                        Console.Write(".");
                    }
                    if (game.Grid[i, j] == 1)
                    {
                        Console.Write("X");
                    }
                    if (game.Grid[i,j] == 2)
                    {
                        Console.Write("O");
                    }

                    // Check if lines need to be printed (Horizontally)

                    if (j == 2)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

    }
}