using Framework.Engine;
using System;
using System.Collections.Generic;

class Combo : GameObject
{
    private int _score;

    private int _perfect;
    private int _good;
    private int _bad;   
    private int _miss;
    private int j_scale;
    private ComboEnum _lastJudge = ComboEnum.None;
    private int _displayTime = 0;
    private string[] _pArt =
    {

    };

    private string[] _gArt =
    {

    };

    private string[] _bArt =
    {

    };

    private string[] _mArt =
    {

    };

    public Combo(Scene scene) : base(scene)
    {
        Name = "Combo";
    }

    public void ReadyPritingCombo(ComboEnum combo)
    {
        _lastJudge = combo;
        _displayTime = 500;

        if (combo == ComboEnum.Miss)
        {
            _miss++;
            return;
        }
        if (combo == ComboEnum.Perfect)
        {
            _perfect++;
            _score += 10;
            return;
        }
        if (combo == ComboEnum.Good)
        {
            _good++;
            _score += 5;
            return;
        }
        if (combo == ComboEnum.Bad)
        {
            _bad++;
            _score += 2;
            return; 
        }
    }

    public override void Update(float deltaTime)
    {
        if (_displayTime > 0)
        {
            _displayTime -= (int)deltaTime * 1000;
        }
        
    }
    public override void Draw(ScreenBuffer buffer)
    {
        if (_displayTime > 0)
        {
            if (_lastJudge == ComboEnum.Miss)
            {
                buffer.WriteText(18, 25, _lastJudge.ToString(), ConsoleColor.Red);
            }
            else buffer.WriteText(18, 25, _lastJudge.ToString(), ConsoleColor.Cyan);
        }

        buffer.WriteText(42, 3, $"Score: {_score}", ConsoleColor.White);
        buffer.WriteText(42, 5, $"Perfect Combo: {_perfect}", ConsoleColor.White);
        buffer.WriteText(42, 6, $"Good Combo: {_good}", ConsoleColor.White);
        buffer.WriteText(42, 7, $"Bad Combo: {_bad}", ConsoleColor.White);
           
    }
}
