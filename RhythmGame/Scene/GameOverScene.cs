using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

// --- 게임 오버 씬 ---
class GameOverScene : Scene
{
    public event GameAction BackToMenuRequested;
    public event GameAction<int> PlayAgainRequested;
    private int _selectedMusic; // 전에 플레이 했던 음악의 인덱스를 저장하기 위한 변수
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
        ConsoleColor.Magenta,
        ConsoleColor.DarkMagenta,
        ConsoleColor.Red,
        ConsoleColor.DarkRed,

    };
    private float _colorTimer;
    private float _colorSpeed = 0.1f;
    private int _colorOffset;

    // --- 생성자 메서드 ---
    // 생성자로 음악 인덱스를 받아서 저장
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
        if (Input.IsKeyDown(ConsoleKey.LeftArrow))
        {
            BackToMenuRequested?.Invoke();
        }

        // 게임 재시작 시, 이전 플레이했던 노래 노트 재생성 위함
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
        buffer.WriteTextCentered(22, "← to select the music", ConsoleColor.Red);

    }
}
