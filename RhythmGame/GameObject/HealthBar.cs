using Framework.Engine;
using System;

// --- 헬스바 오브젝트 ---
// 점수에 따라 헬스바 출력 
class HealthBar : GameObject
{
    private int _displayTime = 0;
    private int _health = 0;
    private int _yCoordinate = 19;
    public int Health { get { return _health; } }

    // --- 생성자 메서드 ---
    public HealthBar(Scene scene) : base(scene)
    {

    }

    // --- 헬스바 범위 스케일 메서드 ---
    // 출력할 y 좌표에 따라 범위 조정
    // 출력 지속 시간 셋팅
    public int ScaleHealth(int score)
    {
        _displayTime = 300;
        _health = score * (_yCoordinate - 3) / 100;
        return _health;
    }

    public override void Update(float deltaTime)
    {
        if (_displayTime > 0)
        {
            _displayTime -= (int)(deltaTime * 1000);
        }
    }
    public override void Draw(ScreenBuffer buffer)
    {
        for (int i = 0; i < _health; i++)
        {
            buffer.SetCell(41, _yCoordinate - i, '■', ConsoleColor.Magenta);
        }
    }
}