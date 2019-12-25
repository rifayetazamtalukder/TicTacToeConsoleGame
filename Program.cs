using System;


namespace TicTacToeConsoleGame
{
    class Program
    {
        // Define the global tic tac toe board
        private static string[,] ticTacToeBoard = new string[3,3];

        static int[] takenInput = new int[12];
        static int position = 0;
        static bool end = false;
        static string player1 = "O";
        static string player2 = "X";
        static void Main(string[] args)
        {
            // set background and forgorund color
            // Set Background and Foreground Color
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            // Start the game
            StartGame();
            // PrintTicTacToeBoard();

            bool play = true;
            while (play)
            {
                int InputCount = 1;
                // Loop Through while not end;
                while (!end)
                {
                    // Print Tic Toe Board
                    PrintTicTacToeBoard();

                    #region Player 1 Input

                    UserOneInput:
                    Console.Write("Player 1 ==> Choose Your Field: ");
                    var input1 = Console.ReadLine();

                    var playerOneInput = SetValueForPlayer(input1, player1);

                    if (playerOneInput == -1)
                    {
                        // Invalid Input. Take value agian for user
                        goto UserOneInput;
                    }
                    else
                    {
                        // Console.ForegroundColor = ConsoleColor.Green;
                        // Console.WriteLine(" Player One Valid Input. Value SET Successful.");
                        // Console.ForegroundColor = ConsoleColor.Black;

                        InputCount++; // Console.WriteLine($"Input Count value = {InputCount}"); Console.ReadKey();


                        // Check If Player Won the Game
                        if (WinOrDraw(player1))
                        {
                            Console.Clear();
                            PrintTicTacToeBoard();
                            Console.WriteLine("\nPlayer 1(O) Won The Game.");

                            // get out from the loop
                            end = true;
                            break;
                        }
                    }

                    // clear the console
                    Console.Clear();
                    #endregion



                    // Print Tic Toe Board
                    PrintTicTacToeBoard();
                    #region Player 2 Input

                    UserTwoInput:
                    Console.Write("Player 2 ==> Choose Your Field: ");
                    var input2 = Console.ReadLine();

                    var playerTwoInput = SetValueForPlayer(input2, player2);

                    if (playerTwoInput == -1)
                    {
                        // Invalid Input. Take value agian for user
                        goto UserTwoInput;
                    }
                    else
                    {
                        // Console.ForegroundColor = ConsoleColor.Green;
                        // Console.WriteLine(" Player Two Valid Input. Value SET Successful.");
                        // Console.ForegroundColor = ConsoleColor.Black;

                        InputCount++; // Console.WriteLine($"Input Count value = {InputCount}"); Console.ReadKey();

                        // Check If Player Won the Game
                        if (WinOrDraw(player2))
                        {
                            Console.Clear();
                            PrintTicTacToeBoard();
                            Console.WriteLine("Player 2(X) Won The Game.");
                            // get out from the loop
                            end = true;
                            break;
                        }
                    }

                    // clear the console
                    Console.Clear();
                    #endregion

                    
                    if (InputCount >= 9)
                    {
                        PrintTicTacToeBoard();
                        Console.WriteLine("Match Drawn");
                        end = true;
                        break;
                    }
                    
                }

                Console.WriteLine("\n\nDo you want to play again? (Y/N): ");
                var choice = Console.ReadLine();
                if(choice.ToUpper() == "Y" || choice.ToUpper() == "YES")
                {
                    play = true;
                    StartGame();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Thank You For Playing. Press any key to exit.");
                    play = false;
                    break;
                }
            }
            

            // End of Code
            Console.ReadKey();
        }

        /// <summary>
        ///     Takes an string as input and validates the input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static int SetValueForPlayer(string input, string player)
        {
            int playerInput;
            try
            {
                playerInput = int.Parse(input);
                
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Please Enter a Number");
                Console.ForegroundColor = ConsoleColor.Black;

                // return to the function call in Main
                return -1;
            }

            // if valid Integer number
            // Check if the integer number is in between 1 to 9

            if(playerInput < 1 || playerInput > 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Number Should be inBetween 1 to 9");
                Console.ForegroundColor = ConsoleColor.Black;

                // return to the function call in Main
                return -1;
            }
            else
            {
                // Validate If value already taken
                // ValueTaken(int playerInput)
                if (ValueTaken(playerInput))
                {
                    // Show Error When value taken and return -1
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" {playerInput} already taken ");
                    Console.ForegroundColor = ConsoleColor.Black;

                    // return to the function call
                    return -1;
                }
                else
                {
                    // Valid Input.
                    // Validation Complete

                    // Set Value
                    int rowLength = ticTacToeBoard.GetLength(0);
                    int colLength = ticTacToeBoard.GetLength(1);

                    /*foreach(var val in ticTacToeBoard)
                    {
                        Console.Write($"{val} ");
                    }*/

                    for (int i = 0; i < rowLength; i++)
                    {
                        for (int j = 0; j < colLength; j++)
                        {
                            // set number
                            if (ticTacToeBoard[i, j] == input)
                            {
                                if(player == player1)
                                {
                                    ticTacToeBoard[i, j] = "O";
                                }
                                else if(player == player2)
                                {
                                    ticTacToeBoard[i, j] = "X";
                                }
                            }
                        }
                        // do nothing here
                    }

                    // End
                    return playerInput;
                }
            }

        }

        /// <summary>
        ///     Takes player Input as integer and return whether the value is already taken or not
        /// </summary>
        /// <param name="playerInput"></param>
        /// <returns></returns>
        static bool ValueTaken(int playerInput)
        {
            foreach(var value in takenInput)
            {
                if(value == playerInput)
                {
                    // return to the function call
                    return true;
                }
            }

            // match not found. i.e. player input not taken
            takenInput[position] = playerInput;
            position++;
            // Console.ForegroundColor = ConsoleColor.DarkYellow;
            // Console.WriteLine($" Position Value: {position}");
            // Console.ForegroundColor = ConsoleColor.Black;

            // return false for not taken value
            return false;

        }

        /// <summary>
        ///     Prints Header Text and Tic Tac Toe Board
        /// </summary>
        static void PrintTicTacToeBoard()
        {
            int rowLength = ticTacToeBoard.GetLength(0);
            int colLength = ticTacToeBoard.GetLength(1);

            // print header text
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            CenterText("|-----------|");
            CenterText("|Tic Tac Toe|");
            CenterText("|-----------|");
            Console.ForegroundColor = ConsoleColor.Black;


            // print the board
            Console.WriteLine("-------------------");
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write($"|  ");
                    if (ticTacToeBoard[i,j] == "O")
                    {
                        // set a color for player 1
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"{ticTacToeBoard[i, j]}");
                    }
                    else if(ticTacToeBoard[i,j] == "X")
                    {
                        // set a color for player 2
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"{ticTacToeBoard[i, j]}");
                    }
                    else
                    {
                        // Predefine color: For initial print
                        Console.Write($"{ticTacToeBoard[i, j]}");
                    }
                    // reset color to black again
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"  ");

                    if (j == colLength-1)
                    {
                        Console.Write("|");
                    }

                    
                }
                Console.WriteLine("\n-------------------");
            }
            //Console.WriteLine("|     |     |     |");
        }

        /// <summary>
        ///     Prints Texts at Position Center. Takes the text as argument
        /// </summary>
        /// <param name="text">text</param>
        static void CenterText(string text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));

            Console.WriteLine($"{text}");

        }


        static bool WinOrDraw(string player)
        {
            // Check row, column, diagonal win
            
            if(RowWin(player) || ColumnWin(player) || DiagonalWin(player))
            {
                return true;
            }
            else
            {
                return false;
            }

            // check column win

            // check diagonal win
        }

        static bool RowWin(string player)
        {
            if( ticTacToeBoard[0,0] == player && ticTacToeBoard[0,1] == player && ticTacToeBoard[0,2] == player ||
                ticTacToeBoard[1, 0] == player && ticTacToeBoard[1, 1] == player && ticTacToeBoard[1, 2] == player ||
                ticTacToeBoard[2, 0] == player && ticTacToeBoard[2, 1] == player && ticTacToeBoard[2, 2] == player
              )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool ColumnWin(string player)
        {
            if ( ticTacToeBoard[0, 0] == player && ticTacToeBoard[1, 0] == player && ticTacToeBoard[2, 0] == player ||
                 ticTacToeBoard[0, 1] == player && ticTacToeBoard[1, 1] == player && ticTacToeBoard[2, 1] == player ||
                 ticTacToeBoard[0, 2] == player && ticTacToeBoard[1, 2] == player && ticTacToeBoard[2, 2] == player
               )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool DiagonalWin(string player)
        {
            if ( ticTacToeBoard[0, 0] == player && ticTacToeBoard[1, 1] == player && ticTacToeBoard[2, 2] == player ||
                 ticTacToeBoard[0, 2] == player && ticTacToeBoard[1, 1] == player && ticTacToeBoard[2, 0] == player
               )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static void StartGame()
        {
            int rowLength = ticTacToeBoard.GetLength(0);
            int colLength = ticTacToeBoard.GetLength(1);
            int val = 1;
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    ticTacToeBoard[i, j] = val.ToString();

                    val++;
                }
                // do nothing here
            }

            position = 0;
            end = false;

            for(int i = 0; i < takenInput.Length; i++)
            {
                takenInput[i] = -1;
            }
        }

    }

}