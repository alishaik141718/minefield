using MinefieldGameConsole.Interfaces;

namespace MinefieldGameConsole
{

    public class GameEngine : IGameEngine
    {
        private readonly Configuration _configuration;

        public GameEngine(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void Run(IGameBoard gameBoard, IGamePlayer player)
        {
            gameBoard.Initialize();

            while (player.Alive() && !player.HasReachedTarget())
            {
                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            gameBoard.Initialize();
                            player.Reset();
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        {
                            player.GoUp();
                            break;
                        }

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        {
                            player.GoDown();
                            break;
                        }

                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        {
                            player.GoLeft();
                            break;
                        }

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        {
                            player.GoRight();
                            break;
                        }

                }
            }

            Stop(gameBoard, player);
        }

        private void Stop(IGameBoard gameBoard, IGamePlayer player)
        {
            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.Enter:
                    {
                        player.Reset();
                        Run(gameBoard, player);
                        
                        break;
                    }
                case ConsoleKey.Escape: { return; }
                default: { Stop(gameBoard, player); break; }
            }
        }
    }

}

