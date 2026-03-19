using Framework.Engine;
using System;

class UserNote : GameObject
{
    private const int k_MatchedLineY = 21;
    private int _matchedKey;
    public int MatchedKey { get { return _matchedKey; } }
    public UserNote(Scene scene) : base(scene)
    {

    }
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