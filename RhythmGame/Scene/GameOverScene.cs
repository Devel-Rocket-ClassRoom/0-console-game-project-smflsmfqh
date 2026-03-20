using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

class GameOverScene : Scene
{
    public event GameAction BackToMenuRequested;
    public event GameAction<int> PlayAgainRequested;

    private int _selectedMusic;
    private WAVPlayer _player;

    private string[] _gameOver = {
    "██████╗  █████╗ ███╗   ███╗███████╗",
    "██╔════╝ ██╔══██╗████╗ ████║██╔════╝",
    "██║  ███╗███████║██╔████╔██║█████╗  ",
    "██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ",
    "╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗",
    " ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝",
    "                                    ",
    " ██████╗ ██╗   ██╗███████╗██████╗   ",
    "██╔═══██╗██║   ██║██╔════╝██╔══██╗  ",
    "██║   ██║██║   ██║█████╗  ██████╔╝  ",
    "██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗  ",
    "╚██████╔╝ ╚████╔╝ ███████╗██║  ██║  ",
    " ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝  ",
    "                                    ",
    };

    private ConsoleColor[] _colors =
    {
        ConsoleColor.Yellow,
        ConsoleColor.DarkYellow,
        ConsoleColor.Green,
        ConsoleColor.DarkGreen,
        ConsoleColor.Cyan,
        ConsoleColor.DarkCyan,
        ConsoleColor.Blue,
        ConsoleColor.DarkBlue,
        ConsoleColor.Magenta,
        ConsoleColor.DarkMagenta,
        ConsoleColor.Red,
        ConsoleColor.DarkRed,
        ConsoleColor.Gray,
    };
    private float _colorTimer;
    private float _colorSpeed = 0.1f;
    private int _colorOffset;

    public GameOverScene(int index)
    {
        _selectedMusic = index;
    }
    public override void Load()
    {
        _player = new WAVPlayer(sounds.Title);
        _player.PlayLooping();
    }

    public override void Unload()
    {
        _player.Stop();
        _player.Dispose();
    }

    public override void Update(float deltaTime)
    {
        _colorTimer += deltaTime;
        if (_colorTimer > _colorSpeed)
        {
            _colorOffset++;
            _colorTimer = 0;
        }
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            BackToMenuRequested?.Invoke();
        }
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            PlayAgainRequested?.Invoke(_selectedMusic);
        }
    }
    public override void Draw(ScreenBuffer buffer)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // 블럭 인코딩 용
        for (int i = 0; i < _gameOver.Length; i++)
        {
            buffer.WriteText(13, 5 + i, _gameOver[i], _colors[(i + _colorOffset) % _colors.Length]);
        }
        buffer.WriteTextCentered(20, "Press ENTER to Retry!", ConsoleColor.Black, ConsoleColor.White);
        buffer.WriteTextCentered(22, "ESC: Quit", ConsoleColor.Red);

    }
}
