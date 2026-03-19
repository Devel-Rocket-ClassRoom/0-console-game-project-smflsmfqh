using Framework.Engine;
using System;

class PlayScene : Scene
{
    private Stage stage;
    public event GameAction PlayAgainRequested;
    public override void Load()
    {
        stage = new Stage(this);
        AddGameObject(stage);   
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);
    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);    
    }
}