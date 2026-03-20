using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

class HealthBar : GameObject
{
    private int _displayTime = 0;
    private int _health = 0;
    private int _yCoordinate = 19;
    public HealthBar(Scene scene) : base(scene)
    {

    }

    public int ScaleHealth(int score)
    {
        _displayTime = 300;
        _health = (score * _yCoordinate) / 100;
        return _health;
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
        for (int i = 0; i < _health; i++ )
        {
            buffer.SetCell(41, _yCoordinate - i, '■', ConsoleColor.Magenta);
        }
    }
}