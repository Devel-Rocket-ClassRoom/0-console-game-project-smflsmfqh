using Framework.Engine;
using System;
using System.Diagnostics;

class PlayScene : Scene
{
    private Stage stage;
    private Lane lane1;
    private Lane lane2;
    private Lane lane3;
    private Lane lane4;
    private MathcedNote matchedNote;

    private MusicNotes notes = new MusicNotes();

    private Stopwatch stopWatch = new Stopwatch();

    public event GameAction PlayAgainRequested;
    public override void Load()
    {
        stopWatch.Start();
        stage = new Stage(this);
        AddGameObject(stage);

        lane1 = new Lane(this, 0, notes);
        AddGameObject(lane1);
        lane2 = new Lane(this, 1, notes);
        AddGameObject(lane2);
        lane3 = new Lane(this, 2, notes);
        AddGameObject(lane3);
        lane4 = new Lane(this, 3, notes);
        AddGameObject(lane4);

        matchedNote = new MathcedNote(this);
        AddGameObject(matchedNote);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        Lane[] lanes = new Lane[]
        {
            lane1, lane2, lane3, lane4
        };

        int currentTime = (int)stopWatch.ElapsedMilliseconds;
        foreach (Lane lane in lanes)
        {
            lane.LookaheadNotes(currentTime);
        }
        
    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);    
    }
}