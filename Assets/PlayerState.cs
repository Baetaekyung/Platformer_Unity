using UnityEngine;

public class PlayerState
{
    private readonly int _yVelocityHash = Animator.StringToHash("yVelocity");
    
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    protected Rigidbody2D _rb;

    protected float _xInput;
    protected float _yInput;
    private string _animBoolName;

    protected float _stateTimer;
    protected bool _triggerCalled = false;

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
        _triggerCalled = false;
    }

    public virtual void Update()
    {
        _stateTimer -= Time.deltaTime;

        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");
        _player.AnimCompo.SetFloat(_yVelocityHash, _rb.velocity.y);
    }

    public virtual void Exit()
    {
        _player.AnimCompo.SetBool(_animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
