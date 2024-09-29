using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = 0.4f;
        _player.SetVelocity(5 * -_player.FacingDir, _player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_stateTimer < 0)
            _stateMachine.ChangeState(_player.AirState);

        if (_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.IdleState);
    }
}
