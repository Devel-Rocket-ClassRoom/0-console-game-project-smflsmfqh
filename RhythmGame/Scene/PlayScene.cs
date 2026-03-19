using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class PlayScene : Scene
{
    private Stage stage;
    private Lane lane1;
    private Lane lane2;
    private Lane lane3;
    private Lane lane4;
    private MatchedLine matchedNote;
    private Combo combo;

    private int? _judgeCombo = null;

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

        matchedNote = new MatchedLine(this);
        AddGameObject(matchedNote);

        combo = new Combo(this);
        AddGameObject(combo);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    private void HandlingInput()
    {
        int currentTime = (int)stopWatch.ElapsedMilliseconds;

        if (Input.IsKeyDown(ConsoleKey.D) )
        {
            _judgeCombo = lane1.CalculateMatched(currentTime);
            return;
        }
        if (Input.IsKeyDown(ConsoleKey.F))
        {
            _judgeCombo = lane2.CalculateMatched(currentTime);
            return;
        }
        if (Input.IsKeyDown(ConsoleKey.J))
        {
            _judgeCombo = lane3.CalculateMatched(currentTime);    
            return;
        }
        if (Input.IsKeyDown(ConsoleKey.K))
        {
            _judgeCombo = lane4.CalculateMatched(currentTime);
            return;
        }
    }
    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);

        Lane[] lanes = new Lane[]
        {
            lane1, lane2, lane3, lane4
        };
        int currentTime = (int)stopWatch.ElapsedMilliseconds;
        foreach (Lane lane in lanes)
        {
            lane.LookaheadNotes(currentTime);
        }

        _judgeCombo = null;
        HandlingInput();
        if (_judgeCombo.HasValue)
        {
            combo.CalculateCombo(_judgeCombo.Value);
        }
       
    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);    
    }
}