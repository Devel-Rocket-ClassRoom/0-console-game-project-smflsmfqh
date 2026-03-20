using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class PlayScene : Scene
{
    private Stage _stage;
    private Lane[] _lanes;
    private ConsoleKey[] _laneKeys = { ConsoleKey.D, ConsoleKey.F, ConsoleKey.J, ConsoleKey.K };
    private MatchedLine _matchedNote;
    private Combo _combo;
    private MusicNotes _notes = new MusicNotes();
    private Stopwatch _stopWatch = new Stopwatch();
    public event GameAction PlayAgainRequested;

    public override void Load()
    {
        _stopWatch.Start();
        _stage = new Stage(this);
        AddGameObject(_stage);

        _matchedNote = new MatchedLine(this);
        AddGameObject(_matchedNote);

        _combo = new Combo(this);
        AddGameObject(_combo);

        InitalizeLane(4);    
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    private void InitalizeLane(int n)
    {
        _lanes = new Lane[n];
        for (int i = 0; i < n; i++)
        {
            _lanes[i] = new Lane(this, i, _notes);
            AddGameObject(_lanes[i]);
        }
    }
    private void HandlingInput(int currentTime)
    {
        ComboEnum combo;
        for (int i = 0; i < _lanes.Length; i++)
        {
            if (Input.IsKeyDown(_laneKeys[i]))
            {
                combo = _lanes[i].CalculateMatched(currentTime);
                if (combo != ComboEnum.None)
                {
                    _combo.ReadyPritingCombo(combo);
                }
            }
            else
            {
                combo = _lanes[i].MissingNote(currentTime);
                if (combo == ComboEnum.Miss)
                {
                    _combo.ReadyPritingCombo(combo);
                }
            }
           
        }
    }

    public override void Update(float deltaTime)
    {
        int currentTime = (int)_stopWatch.ElapsedMilliseconds;

        UpdateGameObjects(deltaTime);

        foreach (Lane lane in _lanes)
        {
            lane.LookaheadNotes(currentTime);
        }
        HandlingInput(currentTime);

    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);    
    }
}