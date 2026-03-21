using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

class GameSuccessScene : Scene
{
    private int _selectedMusic;
    private int _totalScore;    
    private int _perfectCount;
    private int _goodCount;
    private int _badCount;  
    private int _missCount;

    private string[] _successArt =
    {
         "███████╗██╗   ██╗ ██████╗ ██████╗███████╗███████╗███████╗",
        "██╔════╝██║   ██║██╔════╝██╔════╝██╔════╝██╔════╝██╔════╝",
        "███████╗██║   ██║██║     ██║     █████╗  ███████╗███████╗",
        "╚════██║██║   ██║██║     ██║     ██╔══╝  ╚════██║╚════██║",
        "███████║╚██████╔╝╚██████╗╚██████╗███████╗███████║███████║",
        "╚══════╝ ╚═════╝  ╚═════╝ ╚═════╝╚══════╝╚══════╝╚══════╝",
    "                                                         "
    };

    private ConsoleColor[] _colors =
    {
        ConsoleColor.Green,
        ConsoleColor.DarkGreen,
        ConsoleColor.Cyan,
        ConsoleColor.DarkCyan,
        ConsoleColor.Blue,
        ConsoleColor.DarkBlue,
   
    };

    private float _colorTimer;
    private float _colorSpeed = 0.1f;
    private int _colorOffset;

    private WAVPlayer _player;

    public event GameAction BackToMenuRequested;
    public event GameAction<int> PlayAgainRequested;

    public GameSuccessScene(int index, int totalScore, int perfectCount, int goodCount, int badCount, int missCount)
    {
        _selectedMusic = index;
        _totalScore = totalScore;
        _perfectCount = perfectCount;   
        _goodCount = goodCount;
        _badCount = badCount;
        _missCount = missCount;
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
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            PlayAgainRequested?.Invoke(_selectedMusic);
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // 블럭 인코딩 용
        for (int i = 0; i < _successArt.Length; i++)
        {
            buffer.WriteText(1, 5 + i, _successArt[i], _colors[(i + _colorOffset) % _colors.Length]);
        }
        buffer.WriteLines(19, 13, new string[]
        {
            $"Total Score: {_totalScore}",
            $"Perfect: {_perfectCount}",
            $"Good: {_goodCount}",
            $"Bad: {_badCount}",
            $"Miss: {_missCount}"
        }, ConsoleColor.Black, ConsoleColor.White);

        buffer.WriteTextCentered(20, "Press ENTER to Retry!", ConsoleColor.Black, ConsoleColor.White);
        buffer.WriteTextCentered(22, "← to select the music", ConsoleColor.Red);
    }
}