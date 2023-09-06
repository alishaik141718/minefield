using MinefieldGameConsole.Interfaces;
using MinefieldGameConsole.Squares;

namespace MinefieldGameConsole
{
    public class ConsoleRenderer : IConsoleRenderer
    {

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void RenderBoardDimensionConfigError()
        {
            Console.WriteLine("Invalid dimensions! Width and height should not be 0.");
        }


        public void RenderMinesConfigError()
        {
            Console.WriteLine("Invalid number of mines! The value should be greater than 0.");
        }


        public void RenderPlayerMaxLivesConfigError()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid number of Player Lives! The value should be greater than 0.");
        }

        public void RenderInstructions()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Minefield!");
            Console.WriteLine("Your start position is 'P'.");
            Console.WriteLine("Try to reach 'X' while avoiding mines.");
            Console.WriteLine("Press Enter to restart, or Escape to exit");
            Console.WriteLine();
        }

        public void RenderGameBoard(Square[,] squares, Square playerLocation, List<Square> targetLocations, List<Square> mineHits)
        {
            Console.CursorVisible = false;

            var width = squares.GetLength(0);
            var height = squares.GetLength(1);


            for (var y = height - 1; y >= 0; y--)
            {
                Console.Write($"{squares[0, y].YPositionName} ");

                for (var x = 0; x < width; x++)
                {
                    var output = "_ ";

                    output = mineHits.Contains(squares[x, y])
                        ? "* " :
                        squares[x, y] == playerLocation
                        ? "P "
                        : targetLocations.Any(t => t.SquarePosition.XPosition == squares[x, y].SquarePosition.XPosition
                                                    && t.SquarePosition.YPosition == squares[x, y].SquarePosition.YPosition)
                        ? "X "
                        : output;

                    Console.Write(output);

                }
                Console.WriteLine();

            }
            Console.WriteLine();
            Console.Write("  ");

            for (var x = 0; x < width; x++)
            {
                Console.Write($"{squares[x, 0].XPositionName} ");
            }

            Console.Write("\n\n");
        }

        public void RenderCurrentLocation(Square currentTile)
        {
            Console.WriteLine($"Current Location: {currentTile.Name}");
        }

        public void RenderMoves(int playerMoves)
        {
            Console.WriteLine($"Moves taken: {playerMoves}");
        }

        public void RenderMineHit()
        {
            Console.WriteLine("Oops! You hit a mine.");
        }

        public void RenderLives(int livesLeft)
        {
            Console.WriteLine($"Lives left: {livesLeft}");
        }


        public void RenderResult(int moves)
        {
            Console.WriteLine();
            Console.WriteLine("Congratulations! You reached the other side!");
            Console.WriteLine($"Total moves: {moves}");
            Console.WriteLine("Press Enter to play again, or Escape to exit");
        }

        public void RenderGameOver()
        {
            Console.WriteLine();
            Console.WriteLine("Game over! You ran out of lives.");
            Console.WriteLine("Press Enter to play again, or Escape to exit");
        }


    }
}

