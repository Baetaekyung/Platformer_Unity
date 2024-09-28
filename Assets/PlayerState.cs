using UnityEngine;

public class PlayerState
{
    private readonly int _yVelocityHash = Animator.StringToHash("yVelocity");
    
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    protected Rigidbody2D _rb;

    protected float _xInput;

    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this._player = player;
        this._stateMachine = stateMachine;
        this._animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        _player.AnimCompo.SetBool(_animBoolName, true);
        _rb = _player.RbCompo;
    }

    public virtual void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _player.AnimCompo.SetFloat(_yVelocityHash, _rb.velocity.y);
    }

    public virtual void Exit()
    {
        _player.AnimCompo.SetBool(_animBoolName, false);
    }
}
