using Framework.Engine;
using System;

public class RhythmGame : GameApp
{
    private readonly SceneManager<Scene> _scenes = new SceneManager<Scene>();
    public RhythmGame() : base(61, 30) { }
    public RhythmGame(int width, int height) : base(61, 30)
    {
        
    }

    protected override void Initialize()
    {
        ChangeToTitle();
    }
 
    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }
        _scenes.CurrentScene?.Update(deltaTime);
    }

    protected override void Draw()
    {
        _scenes.CurrentScene?.Draw(Buffer);
    }

    private void ChangeToTitle()
    {
        var title = new TitleScene();
        title.StartRequested += ChangeToMenu;
        _scenes.ChangeScene(title);
    }

    private void ChangeToMenu()
    {
        var menu = new MenuSelectScene();   
        menu.BackToTitleRequested += ChangeToTitle;
        menu.PlayRequested += ChangeToPlay;
        _scenes.ChangeScene(menu);
    }
    
    private void ChangeToPlay(int index)
    {
        var play = new PlayScene(index);
        //play.PlayAgainRequested += ChangeToPlay;
        play.GameOverRequested += ChangeToGameOver; 
        _scenes.ChangeScene(play);
    }

    private void ChangeToGameOver(int index)
    {
        var gameOver = new GameOverScene(index);
        gameOver.BackToMenuRequested += ChangeToMenu;
        gameOver.PlayAgainRequested += ChangeToPlay;
        _scenes.ChangeScene(gameOver);
    }
    
}