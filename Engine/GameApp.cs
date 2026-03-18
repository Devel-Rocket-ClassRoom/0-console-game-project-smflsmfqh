using System;
using System.Threading;

namespace Framework.Engine
{
    public abstract class GameApp
    {
        private const int k_TargetFrameTime = 33; // 33 밀리세컨즈마다 갱신
        private bool _isRunning;

        protected ScreenBuffer Buffer { get; private set; } 

        public event GameAction GameStarted;
        public event GameAction GameStopped;

        protected GameApp(int width, int height) // console 사이즈 매개변수
        {
            Buffer = new ScreenBuffer(width, height);
        }

        
        public void Run() // Main에서 Run() 호출해서 게임 실행
        {
            // --- 게임 시작 초기화 ---
            Console.CursorVisible = false;
            Console.Clear();

            _isRunning = true;
            Initialize();
            GameStarted?.Invoke();

            int previousTime = Environment.TickCount; // 정보: 시스템(컴퓨터 자체) 시작 이후 경과 시간(밀리초)을 가져옴 

            // --- 게임 루프 --- 
            while (_isRunning)
            {
                int currentTime = Environment.TickCount;
                float deltaTime = (currentTime - previousTime) / 1000f;
                previousTime = currentTime;

                Input.Poll();
                Update(deltaTime);
                Buffer.Clear();
                Draw();
                Buffer.Present();

                int elapsed = Environment.TickCount - currentTime;
                int sleepTime = k_TargetFrameTime - elapsed;

                // 프레임 대기, 한 프레임에서의 작업이 빨리 끝나버림 -> 다음 프레임 시간까지 대기
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            }

            // --- 게임 종료 ---
            GameStopped?.Invoke();
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.Clear();
        }

        protected void Quit()
        {
            _isRunning = false;
        }

        protected abstract void Initialize();
        protected abstract void Update(float deltaTime);
        protected abstract void Draw();
    }
}
