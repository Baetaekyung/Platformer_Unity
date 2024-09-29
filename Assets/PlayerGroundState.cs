using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
            _stateMachine.ChangeState(_player.PrimaryAttackState);

        if (!_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.AirState);

        if (Input.GetKeyDown(KeyCode.Space) && _player.IsGroundDetected())
            _stateMachine.ChangeState(_player.JumpState);
    }
}
