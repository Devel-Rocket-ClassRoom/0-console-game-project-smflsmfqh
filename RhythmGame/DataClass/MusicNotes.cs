using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// 임의로 하드 코딩 -> 여유 있으면 음악별로 노트 데이터파일 만들기
class MusicNotes
{
    private Note[] notes = new[]
    {
        new Note { TargetTime = 500,  LaneId = 0 },
        new Note { TargetTime = 500,  LaneId = 2 },
        new Note { TargetTime = 1000, LaneId = 1 },
        new Note { TargetTime = 1000, LaneId = 3 },
        new Note { TargetTime = 1500, LaneId = 0 },
        new Note { TargetTime = 2000, LaneId = 2 },
        new Note { TargetTime = 2000, LaneId = 1 },
        new Note { TargetTime = 2500, LaneId = 3 },
        new Note { TargetTime = 3200, LaneId = 0 },
        new Note { TargetTime = 3000, LaneId = 2 },
        new Note { TargetTime = 3600, LaneId = 1 },
        new Note { TargetTime = 4200, LaneId = 0 },
        new Note { TargetTime = 4600, LaneId = 1 },
        new Note { TargetTime = 4800, LaneId = 2 },
        new Note { TargetTime = 4800, LaneId = 3 },
        new Note { TargetTime = 5000, LaneId = 2 },
        new Note { TargetTime = 5200, LaneId = 3 },
        new Note { TargetTime = 5400, LaneId = 0 },
        new Note { TargetTime = 5800, LaneId = 1 },
        new Note { TargetTime = 6000, LaneId = 2 },
        new Note { TargetTime = 6200, LaneId = 3 },
        new Note { TargetTime = 7000, LaneId = 0 },
        new Note { TargetTime = 7200, LaneId = 2 },
        new Note { TargetTime = 7400, LaneId = 1 },
        new Note { TargetTime = 8200, LaneId = 0 },
        new Note { TargetTime = 8600, LaneId = 1 },
        new Note { TargetTime = 9000, LaneId = 2 },
        new Note { TargetTime = 9800, LaneId = 3 },
    };
    private Queue<Note> _musicNotes;
    public Queue<Note> Notes { get { return _musicNotes; } }

    public MusicNotes()
    {
        _musicNotes = new Queue<Note>(notes.Length);
        foreach (Note note in notes)
        {
            _musicNotes.Enqueue(note);
        }
        
    }
    public Note Peek()
    {
        return _musicNotes.Peek(); 
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var note in  _musicNotes)
        {
            yield return note;  
        }
    }

}