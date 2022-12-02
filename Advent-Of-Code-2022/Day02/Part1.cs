using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day02;

public sealed class Part1 : IAOCProject<int>
{
    private Strategy[] _strategies;
    
    public void Init(in string input)
    {
        var fileContents = File.ReadAllText(input);
        
        var parser = new InputParser(fileContents, new CharacterSearcher('\n', true));
        _strategies = new Strategy [parser.NodeCount];
        for (var i = 0; i < parser.NodeCount; ++i)
        {
            var value = parser.GetValue(i);
            var entry = new InputParser(value.ToString(), new CharacterSearcher(' ', true));
            GameAction opponentAction = GameAction.Rock, yourAction = GameAction.Rock;
            opponentAction = entry.GetValue(0)[0] switch
            {
                'A' => GameAction.Rock,
                'B' => GameAction.Paper,
                'C' => GameAction.Scissors,
                _ => opponentAction
            };   
            yourAction = entry.GetValue(1)[0] switch
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
            var roundScore = 3 * (int) result + (int) _strategies[i].YourMove;
            //Console.WriteLine($"\t{_strategies[i].OpponentMove} vs {_strategies[i].YourMove}::{result} -> {roundScore}=3*{(int)result}+{(int)_strategies[i].YourMove}");
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