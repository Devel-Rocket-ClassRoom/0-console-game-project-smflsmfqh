using Framework.Engine;
using NAudio.MediaFoundation;
using System;

class MenuSelectScene : Scene
{
    private Menu menu;
    private WAVPlayer _player;
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
        DrawGameObjects(buffer);
        buffer.WriteTextCentered(8, "Select the Music!", ConsoleColor.White);
    }

}