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
        j_scale = scale;

        if (scale == -1)
        {
            combos.Enqueue(" ");
            return;
        }

        if (scale > 150) 
        { 
            _miss++;
            combos.Enqueue("Miss!");
            return; 
        }
        
        if (scale > 110) 
        {
            _bad++; 
            _score++;
            combos.Enqueue("Bad!");
            return; 
        }
        
        if (scale >= 90) 
        {
            _good++; 
            _score++;
            combos.Enqueue("Good!");
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
        buffer.WriteTextCentered(10, j_scale.ToString(), ConsoleColor.White);
        if (_lastJudge == "Miss!")
        {
            buffer.WriteText(20, 24, _lastJudge, ConsoleColor.Red);

        }
        else
        {
            buffer.WriteText(20, 24, _lastJudge, ConsoleColor.Cyan);
        }
           
    }
}
