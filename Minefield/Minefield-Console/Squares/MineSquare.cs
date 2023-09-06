using MinefieldGameConsole.Interfaces;

namespace MinefieldGameConsole.Squares
{
	public class MineSquare: Square
    {
        public MineSquare(int xLocation, int yLocation, string? xName = null, string? yName = null)
            : base(xLocation, yLocation, xName, yName)
        {
        }

        public override bool IsItAMine()
        {
            return true;
        }

        public override void VisitSquare(IGamePlayer player, IConsoleRenderer renderer)
        {
            player.ReduceLives(1);
            renderer.RenderMineHit();
        }
    }
    
}

