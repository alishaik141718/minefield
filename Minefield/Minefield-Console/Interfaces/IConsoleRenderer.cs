using MinefieldGameConsole.Squares;

namespace MinefieldGameConsole.Interfaces
{
	public interface IConsoleRenderer
	{
        void ClearConsole();
        void RenderBoardDimensionConfigError();
        void RenderMinesConfigError();
        void RenderPlayerMaxLivesConfigError();
        void RenderInstructions();
        void RenderGameBoard(Square[,] squares, Square playerLocation, List<Square> targetLocations, List<Square> mineHits);
        void RenderCurrentLocation(Square currentTile);
        void RenderMoves(int playerMoves);
        void RenderMineHit();
        void RenderLives(int livesLeft);
        void RenderResult(int moves);
        void RenderGameOver();
    }
}