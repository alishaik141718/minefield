using MinefieldGameConsole.Interfaces;

namespace MinefieldGameConsole
{
    public class GamePlayer : IGamePlayer
    {
        private readonly IGameBoard _board;
        private readonly IConsoleRenderer _renderer;

        private readonly int _maxLives;
        private int _movesTaken = 0;
        private int _livesLeft;

        public GamePlayer(IGameBoard board, IConsoleRenderer renderer, int maxLives)
        {
            _board = board;
            _renderer = renderer;
            _livesLeft = maxLives;
            _maxLives = maxLives;
        }

        public void GoUp()
        {
            if (_board.ShiftSquareUp())
            {
                Move();
            }
        }

        public void GoDown()
        {
            if (_board.ShiftSquareDown())
            {
                Move();
            }
        }

        public void GoLeft()
        {
            if (_board.ShiftSquareLeft())
            {
                Move();
            }
        }

        public void GoRight()
        {
            if (_board.ShiftSquareRight())
            {
                Move();
            }
        }

        public bool Alive()
        {
            if(_maxLives <= 0)
            {
                _renderer.RenderPlayerMaxLivesConfigError();
            }
            return _livesLeft > 0;
        }

        public bool HasReachedTarget()
        {
            return _board.GetTargetSquares().Any(t =>
            t.SquarePosition.XPosition == _board.GetCurrentSquare().SquarePosition.XPosition
            && t.SquarePosition.YPosition == _board.GetCurrentSquare().SquarePosition.YPosition);
        }


        public void ReduceLives(int numOfLives)
        {
            _livesLeft -= numOfLives;
        }

        public int GetTotalMoves()
        {
            return _movesTaken;
        }


        public void Reset()
        {
            _livesLeft = _maxLives;
            _movesTaken = 0;
        }


        private void Move()
        {
            _movesTaken += 1;

            _renderer.RenderMoves(_movesTaken);

            _board.GetCurrentSquare().VisitSquare(this, _renderer);

            if (!HasReachedTarget())
            {
                _renderer.RenderLives(_livesLeft);
            }

            if (_livesLeft == 0)
            {
                _renderer.RenderGameOver();
            }
        }
    }
}

