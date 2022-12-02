using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day02;

public sealed class Part2 : IAOCProject<int>
{
    private StrategyPart2[] _strategies = Array.Empty<StrategyPart2>();
    
    public void Init(in string input)
    {
        ReadOnlySpan<char> fileContents = File.ReadAllText(input);
        
        var parser = new InputParser(in fileContents, new CharacterSearcher('\n', true));
        _strategies = new StrategyPart2 [parser.NodeCount];
        for (var i = 0; i < parser.NodeCount; ++i)
        {
            var line = parser.GetValue(in fileContents, i);
            var entry = new InputParser(in line, new CharacterSearcher(' ', true));
            var opponentAction = GameAction.Rock;
            var result = GameResult.Win;
            opponentAction = entry.GetValue(in line, 0)[0] switch
            {
                'A' => GameAction.Rock,
                'B' => GameAction.Paper,
                'C' => GameAction.Scissors,
                _ => opponentAction
            };   
            result = entry.GetValue(in line, 1)[0] switch
            {
                'X' => GameResult.Lose,
                'Y' => GameResult.Draw,
                'Z' => GameResult.Win,
                _ => result
            };
            _strategies[i] = new StrategyPart2(in opponentAction, in result);
        }

    }

    public int Run()
    {
        var score = 0;
        for (var i = 0; i < _strategies.Length; ++i)
        {
            var opponentMove = (int)_strategies[i].OpponentMove - 1;
            var yourMove = _strategies[i].Result switch
            {
                GameResult.Draw => opponentMove,
                GameResult.Win => (opponentMove + 1) % 3,
                GameResult.Lose => opponentMove == 0 ? 2 : opponentMove - 1,
                _ => 0
            };

            
            //var strategy = new Strategy(_strategies[i].OpponentMove, (GameAction)(yourMove + 1));
            
            //Console.WriteLine($"{_strategies[i].Result}, {_strategies[i].OpponentMove} vs {(GameAction)(yourMove + 1)}");
            //var result = GetGameResult(in strategy);
            score += 3 * (int) _strategies[i].Result + (yourMove + 1);
        }
        return score;
    }

    // private static GameResult GetGameResult(in Strategy strategy)
    // {
    //     if (strategy.OpponentMove == strategy.YourMove)
    //         return GameResult.Draw;
    //     
    //     var yourMove = (int)strategy.YourMove - 1;
    //     if (yourMove == 0)
    //         yourMove = 3;
    //
    //     if (yourMove == (int) strategy.OpponentMove)
    //         return GameResult.Win;
    //
    //     return GameResult.Lose;
    // }
}