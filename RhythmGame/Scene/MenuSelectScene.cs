using Framework.Engine;
using NAudio.MediaFoundation;
using System;

class MenuSelectScene : Scene
{
    private Menu menu;
    private WAVPlayer _player;
    private string[] _menuArt =
    {
        "███████╗███████╗██╗     ███████╗ ██████╗████████╗",
        "██╔════╝██╔════╝██║     ██╔════╝██╔════╝╚══██╔══╝",
        "███████╗█████╗  ██║     █████╗  ██║        ██║   ",
        "╚════██║██╔══╝  ██║     ██╔══╝  ██║        ██║   ",
        "███████║███████╗███████╗███████╗╚██████╗   ██║   ",
        "╚══════╝╚══════╝╚══════╝╚══════╝ ╚═════╝   ╚═╝   ",

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
        ConsoleColor.Gray,
    };
    private float _colorTimer;
    private float _colorSpeed = 0.1f;
    private int _colorOffset;

    public event GameAction<int> PlayRequested;
    public event GameAction BackToTitleRequested;
    
    public override void Load()
    {
        menu = new Menu(this);
        AddGameObject(menu);

        _player = new WAVPlayer(sounds.Title);
        _player.PlayLooping();
    }

    public override void Unload()
    {
        _player.Stop();
        _player.Dispose();
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);
        _colorTimer += deltaTime;
        if (_colorTimer > _colorSpeed)
        {
            _colorOffset++;
            _colorTimer = 0;
        }

        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            if (menu.SelectedIndex == 0 )
            {
                PlayRequested?.Invoke(menu.SelectedIndex);
                return;
            }
            if (menu.SelectedIndex == 1 )
            {

                PlayRequested?.Invoke(menu.SelectedIndex);
                return;
            }
            if (menu.SelectedIndex == 2)
            {
                BackToTitleRequested?.Invoke(); 
            }
        }
       
    }
    
    public override void Draw(ScreenBuffer buffer)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // 블럭 인코딩 용

        DrawGameObjects(buffer);
        
        for (int i = 0; i < _menuArt.Length; i++)
        {
            buffer.WriteText(4, 5 + i, _menuArt[i], _colors[(i + _colorOffset) % _colors.Length]);
        }
        buffer.WriteText(5, 22, "[Key Info]", ConsoleColor.Black, ConsoleColor.Yellow);
        buffer.WriteText(5, 23, "Controls", ConsoleColor.Yellow);
        buffer.WriteText(5, 24, "\t  D\tF\tJ\tK",  ConsoleColor.Magenta);
        buffer.WriteText(5, 25, "\t  ↓\t↓\t↓\t↓",  ConsoleColor.Gray);
        buffer.WriteText(5, 26, "Lane: 0\t 1\t 2\t 3",  ConsoleColor.White);
    }

}