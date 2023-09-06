using MinefieldGameConsole.Interfaces;
using MinefieldGameConsole.Squares;

namespace MinefieldGameConsole
{
    public class GameBoard : IGameBoard
    {

        private readonly Dictionary<int, string> SquareLables = new()
            {
                { 0, "A"}, { 1, "B"}, { 2, "C"}, { 3, "D"},
                { 4, "E"}, { 5, "F"}, { 6, "G"}, { 7, "H"}
            };

        private readonly Configuration _configuration;
        private readonly BoardConfiguration _boardConfig;
        private readonly IConsoleRenderer _consoleRenderer;

        private Square[,] _squares;
        private Square _currentLocation;
        private readonly List<Square> _targetLocations;
        private readonly List<Square> _mineHits;


        public GameBoard(Configuration configuration, IConsoleRenderer renderer)
        {
            _configuration = configuration;
            _consoleRenderer = renderer;


            _boardConfig = configuration.Board;
            _squares = new Square[_configuration.Board.Dimensions.BoardWidth, _boardConfig.Dimensions.BoardHeight];

            _targetLocations = new();
            _mineHits = new();
            _currentLocation = new SafeSquare(0, 0);
        }



        public void Initialize()
        {

            if (!ValidConfig()) return;

            _targetLocations.Clear();
            _mineHits.Clear();

            _squares = GenerateBoardSquares();

            var startPosition = GetPlayerStartingPosition();

            _currentLocation = _squares[startPosition.XPosition, startPosition.YPosition];

            PlaceMines(_squares, startPosition, _boardConfig);

            var result = GenerateTargets(_squares, _boardConfig);

            _squares = result.Item1;
            _targetLocations.AddRange(result.Item2);

            DisplayBoard();
        }

        public bool ShiftSquareLeft()
        {
            if (GetCurrentSquare().SquarePosition.XPosition > 0)
            {
                Shift(new Position(GetCurrentSquare().SquarePosition.XPosition - 1, GetCurrentSquare().SquarePosition.YPosition));

                return true;
            }
            return false;
        }

        public bool ShiftSquareRight()
        {
            if (GetCurrentSquare().SquarePosition.XPosition < _boardConfig.Dimensions.BoardWidth - 1)
            {
                Shift(new Position(GetCurrentSquare().SquarePosition.XPosition + 1, GetCurrentSquare().SquarePosition.YPosition));

                return true;
            }
            return false;
        }

        public bool ShiftSquareUp()
        {
            if (GetCurrentSquare().SquarePosition.YPosition < _boardConfig.Dimensions.BoardHeight - 1)
            {
                Shift(new Position(GetCurrentSquare().SquarePosition.XPosition, GetCurrentSquare().SquarePosition.YPosition + 1));

                return true;
            }
            return false;
        }

        public bool ShiftSquareDown()
        {
            if (_currentLocation.SquarePosition.YPosition > 0)
            {
                Shift(new Position(GetCurrentSquare().SquarePosition.XPosition, GetCurrentSquare().SquarePosition.YPosition - 1));

                return true;
            }
            return false;
        }


        public Square GetCurrentSquare()
        {
            return _currentLocation;
        }

        public void SetCurrentSquare(Square square)
        {
            _currentLocation = square;
        }

        public List<Square> GetTargetSquares()
        {
            return _targetLocations;
        }

        public bool CheckDefaultTargets()
        {
            return _boardConfig.RandomPositions.TargetPosition;
        }


        private bool ValidConfig()
        {
            var result = true;

            if (_boardConfig.Dimensions.BoardWidth == 0 || _boardConfig.Dimensions.BoardHeight == 0)
            {
                _consoleRenderer.RenderBoardDimensionConfigError();
                result = false;
            }


            if (_boardConfig.NumberOfMines == 0)
            {
                _consoleRenderer.RenderMinesConfigError();
                result = false;
            }
            return result;
        }

        private Position GetPlayerStartingPosition()
        {
            var startingXPosition = _boardConfig.RandomPositions.StartPosition
                ? Utils.GetRandomNumber(0, _boardConfig.Dimensions.BoardWidth)
                : _boardConfig.StartPosition.XPosition;

            return new Position(startingXPosition, 0);
        }

        private Square[,] GenerateBoardSquares()
        {
            var squares = new Square[_boardConfig.Dimensions.BoardWidth, _boardConfig.Dimensions.BoardHeight];


            for (var x = 0; x < _boardConfig.Dimensions.BoardWidth; x++)
            {
                for (var y = 0; y < _boardConfig.Dimensions.BoardHeight; y++)
                {
                    squares[x, y] = new SafeSquare(x, y, SquareLables[x]);
                }
            }

            return squares;
        }

        private Square[,] PlaceMines(Square[,] squares, Position playerStartPosition, BoardConfiguration boardConfig)
        {
            var height = boardConfig.RandomPositions.TargetPosition
                ? boardConfig.Dimensions.BoardHeight
                : boardConfig.Dimensions.BoardHeight - 1;

            for (int i = 0; i < boardConfig.NumberOfMines; i++)
            {
                int row, col;
                do
                {
                    row = Utils.GetRandomNumber(0, boardConfig.Dimensions.BoardWidth);
                    col = Utils.GetRandomNumber(0, height);
                }
                while (squares[row, col].IsItAMine()
                        || (playerStartPosition.XPosition == row
                            && playerStartPosition.YPosition == col));

                squares[row, col] = new MineSquare(row, col, SquareLables[row]);
            }

            return squares;
        }

        private Tuple<Square[,], IEnumerable<Square>> GenerateTargets(Square[,] squares, BoardConfiguration boardConfig)
        {

            int targetXPosition = Utils.GetRandomNumber(0, boardConfig.Dimensions.BoardWidth);
            var targetYPosition = boardConfig.Dimensions.BoardHeight - 1;
            List<Square> targetLocations = new();

            if (boardConfig.RandomPositions.TargetPosition)
            {
                var targetSquare = new TargetSquare(targetXPosition, boardConfig.Dimensions.BoardHeight - 1, SquareLables[targetXPosition]);
                targetLocations.Add(targetSquare);
                squares[targetXPosition, targetYPosition] = targetSquare;
            }
            else
            {
                for (int i = 0; i < boardConfig.Dimensions.BoardWidth; i++)
                {
                    var targetSquare = new TargetSquare(i, boardConfig.Dimensions.BoardHeight - 1, SquareLables[i]);
                    targetLocations.Add(targetSquare);
                    squares[i, targetYPosition] = targetSquare;
                }
            }

            return Tuple.Create<Square[,], IEnumerable<Square>>(squares, targetLocations); ;

        }

        private void DisplayBoard()
        {
            _consoleRenderer.ClearConsole();
            _consoleRenderer.RenderInstructions();
            _consoleRenderer.RenderGameBoard(_squares, _currentLocation, _targetLocations, _mineHits);
            _consoleRenderer.RenderCurrentLocation(_currentLocation);
        }

        private void AddMineHit(Square square)
        {
            _mineHits.Add(square);
        }

        private void Shift(Position newPosition)
        {
            SetCurrentSquare(_squares[newPosition.XPosition, newPosition.YPosition]);

            if (GetCurrentSquare().IsItAMine())
            {
                AddMineHit(GetCurrentSquare());
            }

            DisplayBoard();
        }
    }
}

