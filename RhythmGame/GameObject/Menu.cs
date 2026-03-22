using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// --- 음악 선택 씬에서 사용될 Menu 오브젝트 ---
class Menu : GameObject
{
    private string[] _menu = { "젓가락 행진곡", "월광", "돌아가기" };

    private int _selectedIndex = 0;
    public int SelectedIndex { get { return _selectedIndex; } }

    // --- 생성자 메서드 ---
    public Menu(Scene scene) : base(scene) { Name = "Menu"; }
    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.DownArrow))
        {
            _selectedIndex++;

            if (_selectedIndex > 2)
            {
                _selectedIndex = 0;
            }

        }
        else if (Input.IsKeyDown(ConsoleKey.UpArrow))
        {
            _selectedIndex--;
            if (_selectedIndex < 0)
            {

                _selectedIndex = _menu.Length - 1;

            }
        }
    }
    public override void Draw(ScreenBuffer buffer)
    {
        string prefix;
        ConsoleColor bg;
        ConsoleColor fg;
        for (int i = 0; i < _menu.Length; i++)
        {
            if (_selectedIndex == i)
            {
                bg = ConsoleColor.White;
                fg = ConsoleColor.Black;
                prefix = "> ";
            }
            else
            {
                bg = ConsoleColor.Black;
                fg = ConsoleColor.White;
                prefix = "  ";
            }

            buffer.WriteTextCentered(16 + i, prefix + _menu[i], fg, bg);
        }

    }
}
