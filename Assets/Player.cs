using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

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
    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState  = new PlayerAirState(this, StateMachine, "Jump");
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
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        RbCompo.velocity = new Vector2(xVelocity, yVelocity);
    }
}
