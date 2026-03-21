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
   
    private ComboEnum _lastJudge = ComboEnum.None;
    private int _displayTime = 0;
    private string[] perfect = {
    @"  ___          __        _   _ ",
    @" | _ \___ _ _ / _|___ __| |_| |",
    @" |  _/ -_) '_|  _/ -_) _|  _|_|",
    @" |_| \___|_| |_| \___\__|\__(_) ",
   };
    private string[] good = {
    @"   ___              _ _ ",
    @"  / __|___  ___  __| | |",
    @" | (_ / _ \/ _ \/ _` |_|",
    @"  \___\___/\___/\__,_(_)",
   };

    private string[] bad = {
    @" ___          _ _ ",
    @" | _ ) __ _ __| | |",
    @" | _ \/ _` / _` |_|",
    @" |___/\__,_\__,_(_)",
   };

    private string[] miss = {
    @"  __  __ _       _ ",
    @" |  \/  (_)_____| |",
    @" | |\/| | (_-<_-<_|",
    @" |_|  |_|_/__/__(_)",
    };

    public int Score { get { return _score; } private set { } }
    public int Perfect { get { return _perfect; } private set { } } 
    public int Good { get { return _good; } private set { } }   
    public int Bad { get { return _bad; } private set { } } 
    public int Miss { get { return _miss; } private set { } }   

    public Combo(Scene scene) : base(scene)
    {
        Name = "Combo";
    }

    private int ScaleScore()
    {
        if (_score < 0)
        {
            _score = 0;
        }
        return _score;
    }

    public void ReadyPritingCombo(ComboEnum combo)
    {
        _lastJudge = combo;
        _displayTime = 500;

        if (combo == ComboEnum.Miss)
        {
            _miss++;
            _score -= 7;
            ScaleScore();
            return;
        }
        if (combo == ComboEnum.Perfect)
        {
            _perfect++;
            _score += 7;
            ScaleScore();
            return;
        }
        if (combo == ComboEnum.Good)
        {
            _good++;
            _score += 4;
            ScaleScore();
            return;
        }
        if (combo == ComboEnum.Bad)
        {
            _bad++;
            _score += 2;
            ScaleScore();
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
        string[][] comboStrings = { perfect, good, bad, miss };

        if (_displayTime > 0)
        {
            for (int i = 0; i < comboStrings.Length; i++)
            {
                if ((int)_lastJudge == i)
                {
                    if (i == 3) { buffer.WriteLines(10, 23, comboStrings[i], ConsoleColor.Red); }
                    else if (i == 1) { buffer.WriteLines(8, 23, comboStrings[i], ConsoleColor.Cyan); }
                    else if (i == 0) { buffer.WriteLines(5, 23, comboStrings[i], ConsoleColor.Green); }
                    else { buffer.WriteLines(8, 23, comboStrings[i], ConsoleColor.Magenta); }
                }
            }
        }

        buffer.WriteText(42, 21, $"Score: {_score}", ConsoleColor.White);
        buffer.WriteText(42, 23, $"Perfect Combo: {_perfect}", ConsoleColor.White);
        buffer.WriteText(42, 24, $"Good Combo: {_good}", ConsoleColor.White);
        buffer.WriteText(42, 25, $"Bad Combo: {_bad}", ConsoleColor.White);
        buffer.WriteText(42, 26, $"Miss Combo: {_miss}", ConsoleColor.White);
           
    }
}
