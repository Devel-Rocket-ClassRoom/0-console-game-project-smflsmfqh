using Framework.Engine;
using System;

public class RhythmGame : GameApp
{
    private readonly SceneManager<Scene> _scenes = new SceneManager<Scene>();
    public RhythmGame() : base(90, 120) { }
    public RhythmGame(int width, int height) : base(90, 120)
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

    }
    
    private void ChangeToPlay()
    {
        
    }
    
}