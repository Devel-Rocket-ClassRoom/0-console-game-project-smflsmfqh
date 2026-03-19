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

    private Queue<string> combos;
    private string _lastJudge = "";

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
        combos = new Queue<string>();
    }

    public void CalculateCombo(int scale)
    {
        if (scale > 120) 
        { 
            _miss++;
            combos.Enqueue("Miss!");
            return; 
        }
        if (scale > 80) 
        {
            _bad++; 
            _score++;
            combos.Enqueue("Bad!");
            return; 
        }
        if (scale >= 40) 
        {
            _good++; 
            _score++;
            combos.Enqueue("Good!");
            return; 
        }
        if (scale == -1)
        {
            return;
        }

        _perfect++; 
        _score++;
        combos.Enqueue("Perfect!!");   
        return; 
    }

    public override void Update(float deltaTime)
    {
        if (combos.Count != 0)
        {
            _lastJudge = combos.Dequeue();
        }
        
    }
    public override void Draw(ScreenBuffer buffer)
    {
        ConsoleColor fg = ConsoleColor.Cyan;
        
        if (_lastJudge == "Miss") { fg = ConsoleColor.Red; }
        buffer.WriteTextCentered(24, _lastJudge, fg);
           
    }
}