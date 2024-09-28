public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _player.SetVelocity(_xInput * _player.moveSpeed, _rb.velocity.y);

        if(_xInput == 0)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
    }
}
