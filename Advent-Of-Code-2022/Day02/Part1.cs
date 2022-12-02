using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day02;

public sealed class Part1 : IAOCProject<int>
{
    private Strategy[] _strategies = Array.Empty<Strategy>();
    
    public void Init(in string input)
    {
        ReadOnlySpan<char> fileContents = File.ReadAllText(input);
        
        var parser = new InputParser(in fileContents, new CharacterSearcher('\n', true));
        _strategies = new Strategy [parser.NodeCount];
        for (var i = 0; i < parser.NodeCount; ++i)
        {
            var line = parser.GetValue(in fileContents, i);
            var entry = new InputParser(in line, new CharacterSearcher(' ', true));
            GameAction opponentAction = GameAction.Rock, yourAction = GameAction.Rock;
            opponentAction = entry.GetValue(in line, 0)[0] switch
            {
                'A' => GameAction.Rock,
                'B' => GameAction.Paper,
                'C' => GameAction.Scissors,
                _ => opponentAction
            };   
            yourAction = entry.GetValue(in line, 1)[0] switch
            {
                'X' => GameAction.Rock,
                'Y' => GameAction.Paper,
                'Z' => GameAction.Scissors,
                _ => yourAction
            };
            _strategies[i] = new Strategy(in opponentAction, in yourAction);
        }

    }

    public int Run()
    {
        var score = 0;
        for (var i = 0; i < _strategies.Length; ++i) 
        {
            var result = GetGameResult(in i);
            score += 3 * (int) result + (int)_strategies[i].YourMove;
        }
        return score;
    }

    private GameResult GetGameResult(in int index)
    {
        if (_strategies[index].OpponentMove == _strategies[index].YourMove)
            return GameResult.Draw;
        
        var yourMove = (int)_strategies[index].YourMove - 1;
        if (yourMove == 0)
            yourMove = 3;

        if (yourMove == (int) _strategies[index].OpponentMove)
            return GameResult.Win;

        return GameResult.Lose;
    }
}