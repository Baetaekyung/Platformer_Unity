using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.ChangeState(_player.WallJumpState);
            return;
        }

        if (_xInput != 0 && _player.FacingDir != _xInput)
                _stateMachine.ChangeState(_player.IdleState);

        if (_yInput < 0)
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y * 0.7f);

        if (_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.IdleState);
    }
}
