namespace Day02;

public readonly struct Strategy
{
    public GameAction OpponentMove { get; }

    public GameAction YourMove { get; }

    public Strategy(in GameAction opponentMove, in GameAction yourMove)
    {
        OpponentMove = opponentMove;
        YourMove = yourMove;
    }
}