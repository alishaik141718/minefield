using MinefieldGameConsole.Interfaces;

namespace MinefieldGameConsole.Squares
{
    public abstract class Square
    {

        public string Name
        {
            get
            {
                return XPositionName + YPositionName;
            }
        }

        public Position SquarePosition { get; init; }

        public string XPositionName { get; init; }
        public string YPositionName { get; init; }



        public Square(int xPosition, int yPosition, string? xPositionName = null, string? yPositionName = null)
        {

            SquarePosition = new Position(xPosition, yPosition);

            XPositionName = xPositionName ?? xPosition.ToString();
            YPositionName = yPositionName ?? (yPosition + 1).ToString();

        }


        public abstract bool IsItAMine();

        public abstract void VisitSquare(IGamePlayer player, IConsoleRenderer renderer);


    }
}

