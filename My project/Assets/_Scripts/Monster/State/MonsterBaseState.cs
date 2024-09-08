public interface MonsterBaseState
{
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
    public abstract void OnStateFixedUpdate();
}