using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class PlayScene : Scene
{
    private Stage _stage;

    private Lane[] _lanes;
    private ConsoleKey[] _laneKeys = { ConsoleKey.D, ConsoleKey.F, ConsoleKey.J, ConsoleKey.K };

    private MatchedLine _matchedNote;
    private Combo _combo;
    private Stopwatch _stopWatch = new Stopwatch();
    private HealthBar _healthBar;
    private WAVPlayer _player;
    private bool isGameOver;

    private int _selectedMusic;

    public event GameAction<int> PlayAgainRequested;
    public event GameAction<int> GameOverRequested;

    public PlayScene(int index)
    {
        _selectedMusic = index; 
    }
    public override void Load()
    {
        _stopWatch.Start();
        _stage = new Stage(this);
        AddGameObject(_stage);

        _matchedNote = new MatchedLine(this);
        AddGameObject(_matchedNote);

        _combo = new Combo(this);
        AddGameObject(_combo);

        InitalizeLane(4);    // Lane Create

        _healthBar = new HealthBar(this);   
        AddGameObject(_healthBar);

        
        if (_selectedMusic == 0)
        {
            _player = new WAVPlayer(sounds.Chopstick);
        }
        else if (_selectedMusic == 1)
        {
            _player = new WAVPlayer(sounds.Moonlight);
        }
        _player.Play();

    }

    public override void Unload()
    {
        _player.Stop();
        _player.Dispose();
        ClearGameObjects();
    }

    private void InitalizeLane(int n)
    {
        _lanes = new Lane[n];
        for (int i = 0; i < n; i++)
        {
            _lanes[i] = new Lane(this, i, _selectedMusic);
            AddGameObject(_lanes[i]);
        }
    }

    private bool IsStageEmpty()
    {
        int empty = 0;
        foreach (Lane lane in _lanes)
        {
            if (lane.Count == 0)
            {
                empty++;
            }
        }
        if (empty == 4)
        {
            isGameOver = true;
        }
        return isGameOver;
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
              combo = _lanes[i].MissingNote(currentTime);
              if (combo == ComboEnum.Miss)
              {
                  _combo.ReadyPritingCombo(combo);
              }
            
            _healthBar.ScaleHealth(_combo.Score);
        }
    }

    public override void Update(float deltaTime)
    {
        if (isGameOver)
        {
            Thread.Sleep(2000);
            GameOverRequested?.Invoke(_selectedMusic);
            return;
        }

        //int currentTime = (int)_stopWatch.ElapsedMilliseconds;

         
        UpdateGameObjects(deltaTime);
        int currentTime = (int)_player.GetCurrentMs();
        foreach (Lane lane in _lanes)
        {
            lane.LookaheadNotes(currentTime);
        }
        HandlingInput(currentTime);
        IsStageEmpty();

    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
        //buffer.WriteTextCentered(10, ((int)_player.GetTotalMs()).ToString(), ConsoleColor.White);
       // buffer.WriteTextCentered(11, ((int)_player.GetCurrentMs()).ToString(), ConsoleColor.White);


    }
}