using Framework.Engine;
using System;

class MenuSelectScene : Scene
{
    private Menu menu;
    public event GameAction PlayRequested;
    public event GameAction BackToTitleRequested;
    
    public override void Load()
    {
        menu = new Menu(this);
        AddGameObject(menu);    
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);

       if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            if (menu.SelectedIndex == 0 )
            {
                PlayRequested?.Invoke();
                return;
            }
            if (menu.SelectedIndex == 1 )
            {
                PlayRequested?.Invoke();
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
        DrawGameObjects(buffer);
        buffer.WriteTextCentered(8, "Select the Music!", ConsoleColor.White);
    }

}