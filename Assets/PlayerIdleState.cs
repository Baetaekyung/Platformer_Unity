using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(_xInput == _player.FacingDir && _player.IsWallDetected())
            return;

        if(_xInput != 0 && !_player.IsBusy)
        {
            _stateMachine.ChangeState(_player.MoveState);
        }
    }
}
