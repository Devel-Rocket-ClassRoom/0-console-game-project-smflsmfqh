using Framework.Engine;
using System;
using System.Collections.Generic;

//  --- 레인별 노트와 출력 대기 컬렉션을 다루는 오브젝트 ---
// _stageingNotes: 유저가 선택한 노래와 해당하는 레인에 따라 초기화된 노트 컬렉션
// _fallingNotes: 출력 범위에 있는 노트들 컬렉션 -  출력 범위 (TargetTime이 현재 시간 + 3000ms 이하인 노트들)
class Lane : GameObject
{
    private MusicNotes _notes;
    private Queue<Note> _stagingNotes = new Queue<Note>();
    private LinkedList<Note> _fallingNotes = new LinkedList<Note>();

    private int _laneId;
    public int Count { get { return _fallingNotes.Count + _stagingNotes.Count; } }

    private const int k_MatchedLineY = 20; // 판정선의 y 좌표
    private const float k_MoveInterval = 0.017f; // 노트 출력 속도

    // --- 생성자 ---
    // lane 번호와, 선택한 음악 인덱스를 받아 초기화
    public Lane(Scene scene, int lane, int musicIndex) : base(scene)
    {
        Name = "Lane";
        _laneId = lane;
        _notes = new MusicNotes(musicIndex);
        Initalize(_laneId, _notes);
    }

    // --- _stagingNotes 초기화 메서드 ---
    // 해당하는 레인의 Queue를 참조
    private void Initalize(int lane, MusicNotes notes)
    {
        _stagingNotes = notes.GetLaneQueue(lane);

    }

    // --- 출력 범위에 속한 노트 컬렉션 및 출력 좌표 갱신 메서드
    // 출력 범위에 따라 _fallingNote에 _stagintNotes의 요소 Dequeue 후 추가
    // _fallingNotes의 각 노트들의 coordinate 갱신
    public void LookaheadNotes(int currentTime)
    {
        int x = _laneId * 10 + 1; // 각 레인별 출력할 x 좌표 

        _fallingNotes.Clear();

        while (_stagingNotes.Count > 0 && _stagingNotes.Peek().TargetTime <= currentTime + 3000)
        {
            _fallingNotes.AddLast(_stagingNotes.Dequeue());
        }

        foreach (Note note in _fallingNotes)
        {
            int y = (int)CalculateY(currentTime, note);

            if (y <= k_MatchedLineY)
            {
                if (y == 0)
                {
                    y += 1;
                }
                note.coordinate = (x, y);
            }
        }
    }

    // --- 출력할 Y 좌표 계산 메서드 --- 
    // k_MatchedLineY(판정선): 노트가 도달해야할 목표 y 좌표
    // note.TargetTime - currentTime: 판정선까지 남은 시간(ms)
    // k_MoveInterval: 1ms당 이동하는 픽셀 수(행)
    private float CalculateY(int currentTime, Note note)
    {
        float y = k_MatchedLineY - (note.TargetTime - currentTime) * k_MoveInterval;

        return y;
    }

    // --- 노트 판정 메서드 ---
    // 유저가 레인에 맞는 키를 입력했을 때, 해당하는 레인의 가장 앞에서 출력되고 있는 노트와 유저가 키를 입력한 시간의 차이로 결과 판정하기 위함
    public ComboEnum CalculateMatched(int currentTime)
    {
        ComboEnum result = ComboEnum.None;

        if (_fallingNotes.Count == 0)
        {
            return result;
        }

        Note fallingNote = PeekAFallingNote();
        int scale = Math.Abs(currentTime - fallingNote.TargetTime);

        if (scale > 220) // 판정 범위에 도달하지 않았다고 간주 -> 아무런 판정도 하지 않음
        {
            result = ComboEnum.None;
            return result;
        }
        if (scale <= 90)
        {
            result = ComboEnum.Perfect;
        }
        else if (scale <= 140)
        {
            result = ComboEnum.Good;
        }
        else if (scale <= 190)
        {
            result = ComboEnum.Bad;
        }
        else
        {
            result = ComboEnum.Miss;

        }

        // 판정된 노트는 삭제
        _fallingNotes.RemoveFirst();

        return result;
    }

    // --- 놓친 노트 판정 메서드 ---
    // 노트를 맟혀야되는 타이밍을 놓쳤을 때 miss 처리하기 위함
    public ComboEnum MissingNote(int currentTime)
    {
        if (_fallingNotes.Count == 0) { return ComboEnum.None; }

        Note fallingNote = PeekAFallingNote();
        int scale = currentTime - fallingNote.TargetTime; // 키를 입력했을 때 판정 메서드와 달리 절댓값 처리 하지 않음

        ComboEnum comboEnum = ComboEnum.None;

        // scale이 양수라는 건, 노트를 쳐야할 타이밍이 이미 지났다는 것을 의미
        // 키 입력 받았을 때 판정 메서드에서의 bad 판정 범위보다 클 때 miss 처리
        if (scale > 190)
        {
            // 판정된 노트는 삭제
            _fallingNotes.RemoveFirst();
            comboEnum = ComboEnum.Miss;
        }

        return comboEnum;
    }

    // --- _fallingNotes의 첫번째 노트 값 반환 메서드 ---
    private Note PeekAFallingNote()
    {
        return _fallingNotes.First.Value;
    }

    public override void Update(float deltaTime)
    {

    }

    // --- _fallintNotes의 각 노트들의 좌표 출력 --- 
    public override void Draw(ScreenBuffer buffer)
    {
        var node = _fallingNotes.First;

        while (node != null)
        {
            var x_cor = node.Value.coordinate.X;
            var y_cor = node.Value.coordinate.Y;

            if (y_cor <= k_MatchedLineY)
            {
                buffer.FillRect(x_cor, y_cor, 8, 1, '☐', ConsoleColor.White);
            }
            node = node.Next;
        }

    }
}