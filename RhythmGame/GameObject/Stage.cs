using Framework.Engine;
using System;

class Stage : GameObject
{
    public Stage(Scene scene) : base(scene)
    {
        Name = "Stage";
    }

    public override void Update(float deltaTime)
    {
        
    }
    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(0, 0, 54, 29, ConsoleColor.White);
        buffer.DrawHLine(1, 21, 39, '-', ConsoleColor.White);
        buffer.DrawVLine(40, 1, 27, '|', ConsoleColor.White);
        buffer.DrawVLine(10, 1, 20, '|', ConsoleColor.White);
        buffer.DrawVLine(20, 1, 20, '|', ConsoleColor.White);
        buffer.DrawVLine(30, 1, 20, '|', ConsoleColor.White);

    }
}