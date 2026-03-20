using Framework.Engine;
using System;
using System.Collections.Generic;

class TitleScene : Scene
{
    public event GameAction StartRequested;
    private WAVPlayer player;

    private string[] _mainTitle =
    {
    @"██████╗ ██╗  ██╗██╗   ██╗████████╗██╗  ██╗███╗   ███╗",
    @"██╔══██╗██║  ██║╚██╗ ██╔╝╚══██╔══╝██║  ██║████╗ ████║",
    @"██████╔╝███████║ ╚████╔╝    ██║   ███████║██╔████╔██║",
    @"██╔══██╗██╔══██║  ╚██╔╝     ██║   ██╔══██║██║╚██╔╝██║",
    @"██║  ██║██║  ██║   ██║      ██║   ██║  ██║██║ ╚═╝ ██║",
    @"╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚═╝  ╚═╝╚═╝     ╚═╝",
    @"                                                     ",
    @"        ███████╗████████╗ █████╗ ██████╗            ",
    @"        ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗           ",
    @"        ███████╗   ██║   ███████║██████╔╝           ",
    @"        ╚════██║   ██║   ██╔══██║██╔══██╗           ",
    @"        ███████║   ██║   ██║  ██║██║  ██║           ",
    @"        ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝          ",
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

    public override void Load()
    {
        player = new WAVPlayer(sounds.Title);
        player.PlayLooping();
    }

    public override void Unload()
    {
        player.Stop();
        player.Dispose();
        ClearGameObjects();

    }

    public override void Update(float deltaTime)
    {
        _colorTimer += deltaTime;

        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            StartRequested?.Invoke();
        }
        if (_colorTimer > _colorSpeed )
        {
            _colorOffset++;
            _colorTimer = 0;
        }

    }
    public override void Draw(ScreenBuffer buffer)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // 블럭 인코딩 용
        for (int i = 0; i < _mainTitle.Length; i++)
        {
            buffer.WriteText(4, 5 + i, _mainTitle[i], _colors[(i + _colorOffset) % _colors.Length]);
        }
        buffer.WriteTextCentered(20, "Press ENTER to start!", ConsoleColor.Black, ConsoleColor.White);
        buffer.WriteTextCentered(22, "ESC: Quit", ConsoleColor.Red);
    }

}