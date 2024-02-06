public abstract class BaseState
{
    // IM2073 Project
    public Enemy enemy;
    public StateMachine stateMachine;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
// End Code