using Framework.Engine;
using System;

// --- Note 개별 데이터 ---
// 노트가 유저로부터 맞혀져야되는 시간: TargetTime
// 노트가 떨어질 레인의 위치: LaneId
// 노트가 그려질 좌표: coordinate

class Note
{
    public int TargetTime { get; set; }
    public int LaneId { get; set; }
    public (int X, int Y) coordinate { get; set; } = (-1, -1);
}
