using Framework.Engine;
using System;

// --- 게임 화면 출력 메서드 ---
// 게임 시작할 때 게임 화면의 범위를 가시적으로 나타냄
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
        buffer.DrawBox(0, 0, 60, 29, ConsoleColor.White);
        buffer.DrawVLine(40, 1, 27, '|', ConsoleColor.White);
        buffer.DrawVLine(10, 1, 20, '|', ConsoleColor.White);
        buffer.DrawVLine(20, 1, 20, '|', ConsoleColor.White);
        buffer.DrawVLine(30, 1, 20, '|', ConsoleColor.White);

    }
}