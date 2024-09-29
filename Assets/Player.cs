using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    public bool IsBusy { get; private set; }

    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float _dashCooldown;
    private float _dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float DashDir { get; private set; } = 1;

    [Header("Collsion info")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;

    public int FacingDir { get; private set; } = 1;
    private bool _facingRight = true;

    #region Components

    public Animator AnimCompo { get; private set; }
    public Rigidbody2D RbCompo { get; private set; }

    #endregion
    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
    }

    private void Start()
    {
        AnimCompo = GetComponentInChildren<Animator>();
        RbCompo = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
        CheckForDashInput();
    }

    public IEnumerator BusyFor(float seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(seconds);
        IsBusy = false;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        _dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashUsageTimer < 0)
        {
            _dashUsageTimer = _dashCooldown;
            DashDir = Input.GetAxisRaw("Horizontal");

            if (DashDir == 0)
                DashDir = FacingDir;

            StateMachine.ChangeState(DashState);
        }
    }

    #region Velocity
    public void ZeroVelocity() => RbCompo.velocity = new Vector2(0, 0);

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        RbCompo.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    } 
    #endregion

    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(_groundCheck.position, Vector2.down,
    _groundCheckDistance, _whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDir,
        _wallCheckDistance, _whatIsGround);
    #endregion

    #region Flip
    public void Flip()
    {
        FacingDir = FacingDir * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (x < 0 && _facingRight)
        {
            Flip();
        }
    } 
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x,
            _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance,
            _wallCheck.position.y));
    }
}
