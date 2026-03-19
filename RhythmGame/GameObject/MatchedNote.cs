using Framework.Engine;
using System;

class MathcedNote : GameObject
{
    private const int k_MatchedLineY = 21;
    private int _matchedKey;
    //private const float k_KeySpeed = 0.1f;
    //private float _inputTimer;
    
    public int MatchedKey { get { return _matchedKey; } }
    public MathcedNote(Scene scene) : base(scene)
    {

    }
    /*
    public int CalculateMatching(int lane, int currentTime, int targetTime)
    {
        int scale = targetTime - currentTime;
        if (Input.IsKey(ConsoleKey.D))
        {
            if (lane == 0)
            {
                return Math.Abs(scale);
            }
        }
        if (Input.IsKey(ConsoleKey.F))
        {
            if (lane == 1)
            {
                return Math.Abs(scale); 
            }
        }
        if (Input.IsKey(ConsoleKey.G))
        {
            if (lane == 2)
            {
                return Math.Abs(scale);
            }
        }
        if (Input.IsKey(ConsoleKey.H))
        {
            if (lane == 3)
            {
                return Math.Abs(scale);
            }
        }
        return -1;
        
    }
    */
    public override void Update(float deltaTime)
    { 
        _matchedKey = -1;

        if (Input.IsKey(ConsoleKey.D))
        {
            _matchedKey = 0;
        }
        if (Input.IsKey(ConsoleKey.F))
        {
            _matchedKey = 1;
        }
        if (Input.IsKey(ConsoleKey.G))
        {
            _matchedKey = 2;
        }
        if (Input.IsKey(ConsoleKey.H))
        {
            _matchedKey = 3;
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
       
        buffer.DrawHLine(1, k_MatchedLineY, 39, '-', ConsoleColor.White);
       
        if (_matchedKey != -1)
        {
            int x = _matchedKey * 10 + 1;
            buffer.DrawHLine(x, k_MatchedLineY, 9, '-', ConsoleColor.Magenta);
            buffer.FillRect(x, k_MatchedLineY, 9, 1, 'бс', ConsoleColor.Magenta);
        }
    }
}