using System;
using System.Collections;
using System.Collections.Generic;

class MusicNotes
{
    private Note[] notes =
    {
        new Note { TargetTime = 500,  lane = 0 },
        new Note { TargetTime = 500,  lane = 2 },
        new Note { time = 1000, lane = 1 },
        new Note { time = 1000, lane = 3 },
        new Note { time = 1500, lane = 0 },
        new Note { time = 2000, lane = 2 },
        new Note { time = 2000, lane = 1 },
        new Note { time = 2500, lane = 3 },
        new Note { time = 3000, lane = 0 },
        new Note { time = 3000, lane = 2 },
        new Note { time = 3500, lane = 1 },
        new Note { time = 4000, lane = 0 },
        new Note { time = 4000, lane = 1 },
        new Note { time = 4000, lane = 2 },
        new Note { time = 4000, lane = 3 },
        new Note { time = 4500, lane = 2 },
        new Note { time = 5000, lane = 3 },
        new Note { time = 5500, lane = 0 },
        new Note { time = 5500, lane = 1 },
        new Note { time = 6000, lane = 2 },
        new Note { time = 6500, lane = 3 },
        new Note { time = 7000, lane = 0 },
        new Note { time = 7000, lane = 2 },
        new Note { time = 7500, lane = 1 },
        new Note { time = 8000, lane = 0 },
        new Note { time = 8000, lane = 1 },
        new Note { time = 8000, lane = 2 },
        new Note { time = 8000, lane = 3 },
    }
    private Queue<Note> _musicNotes;



}