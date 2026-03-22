using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

// --- 게임 플레이 씬 ---
class PlayScene : Scene
{
    private Stage _stage;
    private Lane[] _lanes; // 레인 배열: _lanes[0] = Lane 0, _lanes[1] = Lane 1, _lanes[2] = Lane 2, _lanes[3] = Lane 3
    private ConsoleKey[] _laneKeys = { ConsoleKey.D, ConsoleKey.F, ConsoleKey.J, ConsoleKey.K };
    private MatchedLine _matchedNote;
    private Combo _combo;
    private HealthBar _healthBar;
    private WAVPlayer _player;
    private bool isGameOver;
    private bool isGameSuccess;
    private int _selectedMusic;
    private float _gameEndTimer;
    public event GameAction<int> GameOverRequested; // 게임 오버 씬 호출 이벤트
    public event GameAction<int, int, int, int, int, int> GameSuccessRequested; // 게임 성공 씬 호출 이벤트

    // --- 생성자 메서드 ---
    public PlayScene(int index)
    {
        _selectedMusic = index;
    }
    public override void Load()
    {
        _stage = new Stage(this);
        AddGameObject(_stage);

        _matchedNote = new MatchedLine(this);
        AddGameObject(_matchedNote);

        _combo = new Combo(this);
        AddGameObject(_combo);

        InitalizeLane(4);

        _healthBar = new HealthBar(this);
        AddGameObject(_healthBar);

        // 선택된 노래에 따라 음악 플레이
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

    // --- 레인 배열 초기화 메서드 ---
    private void InitalizeLane(int n)
    {
        _lanes = new Lane[n];
        for (int i = 0; i < n; i++)
        {
            _lanes[i] = new Lane(this, i, _selectedMusic);
            AddGameObject(_lanes[i]);
        }
    }

    // --- isGameOver 변수 검사 및 할당 메서드 ---
    // 게임 플레이 시간 5초 이상인데 health == 0 -> 게임 오버
    private void IsAlive(int currentTime)
    {
        if (currentTime >= 5000 && _healthBar.Health == 0)
        {
            isGameOver = true;
        }
    }


    // --- 레인에 남은 노트가 있는지 검사하는 메서드 ---
    // _stagingNotes.Count +  _fallintNotes.Count == 0이면 남아있는 노트가 없음 => 게임 종료(성공)
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
            isGameSuccess = true;
        }
        return isGameSuccess;
    }

    // --- 사용자 입력 다루는 메서드 ---
    // 현재 시간을 매개변수로 받아서 노트 판정 및 노트 판정 결과 출력 준비
    // 갱신되는 스코어에 따라 헬스 조절
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
        // 게임 오버되면 게임 오버 씬으로
        if (isGameOver)
        {
            _gameEndTimer += deltaTime;
            if (_gameEndTimer >= 2f)
            {
                GameOverRequested?.Invoke(_selectedMusic);
            }
            return;
        }

        // 노트 다 사용되면 게임 성공 씬으로
        if (IsStageEmpty())
        {
            _gameEndTimer += deltaTime;
            if (_gameEndTimer >= 2f)
            {
                GameSuccessRequested?.Invoke(_selectedMusic, _combo.Score, _combo.Perfect, _combo.Good, _combo.Bad, _combo.Miss);
            }
            return;
        }

        UpdateGameObjects(deltaTime);

        int currentTime = (int)_player.GetCurrentMs(); // 재생되는 노래를 기준으로 현재 시간 가져옴(= 현재 재생 시점의 시간)
        foreach (Lane lane in _lanes)
        {
            lane.LookaheadNotes(currentTime);
        }
        HandlingInput(currentTime);
        IsAlive(currentTime);

    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);

    }
}