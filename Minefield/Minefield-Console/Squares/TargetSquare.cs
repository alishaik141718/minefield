using MinefieldGameConsole.Interfaces;

namespace MinefieldGameConsole.Squares
{
    public class TargetSquare : Square
    {
        public TargetSquare(int xLocation, int yLocation, string? xName = null, string? yName = null)
            : base(xLocation, yLocation, xName, yName)
        {
        }

        public override bool IsItAMine()
        {
            return false;
        }

        public override void VisitSquare(IGamePlayer player, IConsoleRenderer renderer)
        {
            renderer.RenderResult(player.GetTotalMoves());
        }
    }
}

