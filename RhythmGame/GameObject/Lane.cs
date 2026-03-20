using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

//  Lane 0: 1~9, Lane1: 11 ~ 19, Lane2: 20 ~ 29, Lane3: 30 ~ 39
class Lane : GameObject
{
    private MusicNotes _notes;
    private LinkedList<Note> _stagingNotes = new LinkedList<Note>();
    private LinkedList<Note> _fallingNotes = new LinkedList<Note>();

    private int _laneId;
    public int Count { get { return _stagingNotes.Count; } }

    private const int k_MatchedLineY = 20;
    private const float k_MoveInterval = 0.017f;

    //private float keyOmissscale;
    //private float keyXmissscale;

    public Lane(Scene scene, int lane, int musicIndex) : base(scene)
    {
        Name = "Lane";
        _laneId = lane;
        _notes = new MusicNotes(musicIndex);
        Initalize(_laneId, _notes);
    }

    private LinkedList<Note> Initalize(int lane, MusicNotes notes)
    {
        foreach (Note note in notes)
        {
            if (note.LaneId == lane)
            {
                _stagingNotes.AddLast(note);
            }
        }
        return _stagingNotes;
    }

    public LinkedList<Note> LookaheadNotes(int currentTime)
    {
        int x = _laneId * 10 + 1;

        _fallingNotes.Clear();

        foreach (Note note in _stagingNotes)
        {
            if (note.TargetTime <= currentTime + 3000)
            {
                _fallingNotes.AddLast(note);
            }
        }

        foreach (Note note in _fallingNotes)
        {
            int y = (int)CalculateY(currentTime, note);
            if (y <= k_MatchedLineY)
            {
                note.coordinate = (x, y);
            }
        }
        return _fallingNotes;
    }

    private float CalculateY(int currentTime, Note note)
    {
        float y = k_MatchedLineY - (note.TargetTime - currentTime) * k_MoveInterval;

        return y;
    }

    public ComboEnum CalculateMatched(int currentTime)
    {
        ComboEnum result = ComboEnum.None;

        if (_fallingNotes.Count == 0)
        {
            return result;
        }

        Note fallingNote = PeekAFallingNote();
        float y = CalculateY(currentTime, fallingNote);
        int scale = Math.Abs(currentTime - fallingNote.TargetTime);
        float yScale = Math.Abs(k_MatchedLineY - y);

        if (scale > 220) 
        {
            result = ComboEnum.None;
            return result;
        }
        if (scale <= 90 ) 
        {
            result = ComboEnum.Perfect;
        }
        else if (scale <= 140 )
        {
            result = ComboEnum.Good;
        }
        else if (scale <= 190 )
        {
            result = ComboEnum.Bad;
        }
        else
        {
            result = ComboEnum.Miss;
            //keyOmissscale = scale;
            
        }

        _stagingNotes.Remove(fallingNote);
        _fallingNotes.RemoveFirst();

        return result;
    }

    public ComboEnum MissingNote(int currentTime)
    {
        if (_fallingNotes.Count == 0) { return ComboEnum.None; }
        Note fallingNote = PeekAFallingNote();
        int scale = currentTime - fallingNote.TargetTime;

        ComboEnum comboEnum = ComboEnum.None;
       
        if (scale > 190)
        { 
            _stagingNotes.Remove(fallingNote);
            _fallingNotes.RemoveFirst();
            comboEnum = ComboEnum.Miss;
           // keyXmissscale = scale;
            
        }
        
        return comboEnum;
    }

    private Note PeekAFallingNote()
    {
        return _fallingNotes.First.Value;
    }

    public override void Update(float deltaTime)
    {

    }

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
        //buffer.WriteTextCentered(10, keyOmissscale.ToString(), ConsoleColor.White);
       // buffer.WriteTextCentered(11, keyXmissscale.ToString(), ConsoleColor.White);


    }
}