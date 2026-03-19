using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// xÁÂÇ¥ ¹üÀ§ Lane 0: 1~9, Lane1: 11 ~ 19, Lane2: 20 ~ 29, Lane3: 30 ~ 39
class Lane : GameObject
{
    private LinkedList<Note> _stagingNotes = new LinkedList<Note>();
    private LinkedList<Note> _printingNotes = new LinkedList<Note>();
    private LinkedList<(int X, int Y)> _printingNoteXY = new LinkedList<(int X, int Y)>();

    private int _laneId;
    public int LaneId { get { return _laneId; } }
    public LinkedList<Note> FallingNotes { get { return _printingNotes; } }
    
    private const int k_MatchedLineY = 21;
    private const float k_MoveInterval = 0.008f;

    public Lane(Scene scene, int lane, MusicNotes notes) : base(scene)
    {
        Name = "Lane";
        _laneId = lane; 
        Initalize(_laneId, notes);
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
        _printingNotes.Clear(); 
        _printingNoteXY.Clear();    

        foreach(Note note in _stagingNotes)
        {
            if (note.TargetTime <= currentTime + 3000 )
            {
                _printingNotes.AddLast(note);
            }
        }

        foreach (Note note in _printingNotes)
        {
            int y = CalculateY(currentTime, note);
            if (y <= k_MatchedLineY)
            {
                _printingNoteXY.AddLast((x, y));
            }
        }
        return _printingNotes;
    }

    private int CalculateY(int currentTime, Note note)
    {
        int y = (int)(k_MatchedLineY - (note.TargetTime - currentTime) * k_MoveInterval);

        return y;
    }

    public int CalculateMatched(int currentTime)
    {
        if (_printingNotes.Count != 0 && _printingNoteXY.Count != 0) // && _printingNoteXY.First.Value.Y <= k_MatchedLineY
        {
            int targetTime = _printingNotes.First.Value.TargetTime;
            int scale = targetTime - currentTime;
            return Math.Abs(scale);
        }
        return -1;
    }

    public override void Update(float deltaTime)
    {
       
    }

    public override void Draw(ScreenBuffer buffer)
    {
        var node = _printingNoteXY.First;
        while (node != null)
        {
            if (node.Value.Y <= k_MatchedLineY)
            {
                buffer.FillRect(node.Value.X, node.Value.Y, 8, 1, '¡á', ConsoleColor.White);
                node = node.Next;
            }
        }
    }
}