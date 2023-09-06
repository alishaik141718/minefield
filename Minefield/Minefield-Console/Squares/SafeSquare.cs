using MinefieldGameConsole.Interfaces;

namespace MinefieldGameConsole.Squares
{
    public class SafeSquare : Square
    {


        public SafeSquare(int xPosition, int yPosition, string? xPositionName = null, string? yPositionName = null)
            : base(xPosition, yPosition, xPositionName, yPositionName)
        {

        }

        public override bool IsItAMine()
        {
            return false;
        }

        public override void VisitSquare(IGamePlayer player, IConsoleRenderer renderer)
        {
            
        }
    }
}

