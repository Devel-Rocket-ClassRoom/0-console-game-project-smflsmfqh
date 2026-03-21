namespace Framework.Engine
{
    public delegate void GameAction();
    public delegate void GameAction<T>(T value);
    public delegate void GameAction<T1, T2, T3, T4, T5, Y6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, Y6 value6);
}
