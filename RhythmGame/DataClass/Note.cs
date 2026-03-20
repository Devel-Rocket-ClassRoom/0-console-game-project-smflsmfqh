using Framework.Engine;
using System;

class Note
{
    public int TargetTime { get; set; } 
    public int LaneId { get; set; }
    public (int X, int Y) coordinate { get; set; } = (-1, -1);
}
