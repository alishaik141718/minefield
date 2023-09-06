namespace MinefieldGameConsole
{
    public record Position(int XPosition, int YPosition);
    public record Dimensions(int BoardWidth, int BoardHeight);
    public record Randomise(bool StartPosition, bool TargetPosition);
    public record BoardConfiguration(Dimensions Dimensions, int NumberOfMines, Randomise RandomPositions, Position StartPosition);
    public record PlayerConfiguration(int MaxLives);
    public record Configuration(BoardConfiguration Board, PlayerConfiguration Player);

}

