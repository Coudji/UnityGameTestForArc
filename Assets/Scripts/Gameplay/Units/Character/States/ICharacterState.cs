public interface ICharacterState
{
    void Enter();
    void Update();
    void Exit();
    bool CanAttack { get; }
    bool CanMove { get; }
}
