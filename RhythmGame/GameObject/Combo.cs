using Framework.Engine;
using System;

// --- 판정 결과 출력과 점수 관리 오브젝트 ---
// _lastJudge로 매 프레임 판정 결과를 저장 및 출력
// _displayTime으로 판정 결과 출력 지속 시간 조절
class Combo : GameObject
{
    private int _score;

    private int _perfect;
    private int _good;
    private int _bad;
    private int _miss;

    private ComboEnum _lastJudge = ComboEnum.None;
    private int _displayTime = 0;
    private string[] perfect = {
    @"  ___          __        _   _ ",
    @" | _ \___ _ _ / _|___ __| |_| |",
    @" |  _/ -_) '_|  _/ -_) _|  _|_|",
    @" |_| \___|_| |_| \___\__|\__(_) ",
   };
    private string[] good = {
    @"   ___              _ _ ",
    @"  / __|___  ___  __| | |",
    @" | (_ / _ \/ _ \/ _` |_|",
    @"  \___\___/\___/\__,_(_)",
   };

    private string[] bad = {
    @" ___          _ _ ",
    @" | _ ) __ _ __| | |",
    @" | _ \/ _` / _` |_|",
    @" |___/\__,_\__,_(_)",
   };

    private string[] miss = {
    @"  __  __ _       _ ",
    @" |  \/  (_)_____| |",
    @" | |\/| | (_-<_-<_|",
    @" |_|  |_|_/__/__(_)",
    };

    public int Score { get { return _score; }  }
    public int Perfect { get { return _perfect; }  }
    public int Good { get { return _good; } }
    public int Bad { get { return _bad; } }
    public int Miss { get { return _miss; }  }

    // --- 생성자 ---
    public Combo(Scene scene) : base(scene)
    {
        Name = "Combo";
    }

    // --- 점수가 음수일 때 범위 조정 메서드 --- 
    private void ScaleScore()
    {
        if (_score < 0)
        {
            _score = 0;
        }
    }

    // --- _lastJudge 갱신 메서드 ---
    public void ReadyPritingCombo(ComboEnum combo)
    {
        _lastJudge = combo;
        _displayTime = 500; // _lastJudge 출력 지속 시간 타이머(ms)

        if (combo == ComboEnum.Miss)
        {
            _miss++;
            _score -= 7;
            ScaleScore();
            return;
        }
        if (combo == ComboEnum.Perfect)
        {
            _perfect++;
            _score += 7;

            return;
        }
        if (combo == ComboEnum.Good)
        {
            _good++;
            _score += 4;

            return;
        }
        if (combo == ComboEnum.Bad)
        {
            _bad++;
            _score += 2;

            return;
        }
    }

    public override void Update(float deltaTime)
    {
        if (_displayTime > 0)
        {
            _displayTime -= (int)(deltaTime * 1000);
        }
    }

    // --- _displayTime 동안 콤보 판정 결과 출력 ---
    public override void Draw(ScreenBuffer buffer)
    {
        string[][] comboStrings = { perfect, good, bad, miss }; // 각 콤보에 해당하는 아스키 아트를 담은 문자열 배열의 배열

        if (_displayTime > 0)
        {
            for (int i = 0; i < comboStrings.Length; i++)
            {
                if ((int)_lastJudge == i)
                {
                    if (i == 3) { buffer.WriteLines(10, 23, comboStrings[i], ConsoleColor.Red); }
                    else if (i == 1) { buffer.WriteLines(8, 23, comboStrings[i], ConsoleColor.Cyan); }
                    else if (i == 0) { buffer.WriteLines(5, 23, comboStrings[i], ConsoleColor.Green); }
                    else { buffer.WriteLines(8, 23, comboStrings[i], ConsoleColor.Magenta); }
                }
            }
        }
        
        // 점수 출력
        buffer.WriteText(42, 21, $"Score: {_score}", ConsoleColor.White);
        buffer.WriteText(42, 23, $"Perfect Combo: {_perfect}", ConsoleColor.White);
        buffer.WriteText(42, 24, $"Good Combo: {_good}", ConsoleColor.White);
        buffer.WriteText(42, 25, $"Bad Combo: {_bad}", ConsoleColor.White);
        buffer.WriteText(42, 26, $"Miss Combo: {_miss}", ConsoleColor.White);

    }
}
