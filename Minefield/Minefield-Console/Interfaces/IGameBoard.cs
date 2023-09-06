using MinefieldGameConsole.Squares;

namespace MinefieldGameConsole.Interfaces
{
	public interface IGameBoard
	{

        void Initialize();

        
        bool ShiftSquareUp();
        bool ShiftSquareDown();
        bool ShiftSquareLeft();
        bool ShiftSquareRight();


        Square GetCurrentSquare();
        List<Square> GetTargetSquares();
        bool CheckDefaultTargets();

    }
}

