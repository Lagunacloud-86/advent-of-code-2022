namespace Day02;

public readonly struct StrategyPart2
{
    public GameAction OpponentMove { get; }

    public GameResult Result { get; }

    public StrategyPart2(in GameAction opponentMove, in GameResult result)
    {
        OpponentMove = opponentMove;
        Result = result;
    }
}