using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Dash,
    Attack1,
    Attack2,
    Attack3,
    Defend,
    DefendSuccessful
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Movement Properties")]
    public float movementSpeed;
    float _originalMovementSpeed;
    public float runSpeed;

    [Header("Dash Properties")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    [Header("Current State")]
    public PlayerState currentState;

    // dash timer cooldown
    bool _isDashing;
    bool _isDashingActive;

    Rigidbody2D _rb;
    Vector2 _vectorMovement;
    PlayerAnimatorManager _playerAnimatorManager;

    bool _isFacingRight;

    bool _isRunning;
    bool _isRunningMultplierActive;

    bool _isAttackOneTriggered;
    // Combat Variables
    bool _isAttackTwoReady = false;

    public float _attackTwoInterval;

    bool _isDefending = false;

    public List<string> AttackLog;
    public static bool  canMove = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        _isFacingRight = true;
        _isRunning = false;
        _isRunningMultplierActive = false;
        _isDashing = false;

        _isDashingActive = false;

        _isAttackOneTriggered = false;

        _originalMovementSpeed = movementSpeed;

    }

    void Update()
    {
        IsRunning();
        IsDashing();
        IsAttackOne();
        IsDefending();
    }
    void IsRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           _isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isRunning = false;
            movementSpeed = _originalMovementSpeed;
            _isRunningMultplierActive = false;
        }
    }

    void IsAttackOne()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetState(PlayerState.Attack1);
        }
    }

    void IsDefending()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetState(PlayerState.Defend);
        }

        if(Input.GetMouseButtonUp(1))
        {
            _isDefending = false;
        }
    }

    void IsDashing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isDashing = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        { 
            movementSpeed = _originalMovementSpeed;
            _isDashingActive = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove) MovementSetUp();

        if (_vectorMovement.x == 0 && !_isAttackOneTriggered && !_isAttackTwoReady && !_isDefending)
        {
            SetState(PlayerState.Idle);
        } else if (_vectorMovement.x == 1 || _vectorMovement.x == -1) {

            if(_isRunning)
            {
                SetState(PlayerState.Run);
                return;
            }

            if(_isDashing)
            {
                SetState(PlayerState.Dash);
                return;
            }

            SetState(PlayerState.Walk);
        } 
        
        SetState(currentState);
    }

    void MovementSetUp()
    {
        _vectorMovement.x = Input.GetAxisRaw("Horizontal");
        _rb.MovePosition(_rb.position + _vectorMovement * movementSpeed * Time.fixedDeltaTime);
        FlipCharacterSprite();
    }

    void SetState(PlayerState s)
    {
        if (s == currentState) return;
        currentState = s;

        switch (s)
        {
            case PlayerState.Idle:
                canMove = true;
                Idling();
                break;
            case PlayerState.Walk:
                canMove = true;
                Walking();
                break;
            case PlayerState.Run:
                canMove = true;
                Running();
                break;
            case PlayerState.Dash:
                canMove = true;
                StartCoroutine(Dashing());
                break;
            case PlayerState.Attack1:
                canMove = false;
                StartCoroutine(AttackOne());
                break;
            case PlayerState.Attack2:
                canMove = false;
                AttackTwo();
                break;
            case PlayerState.Attack3:
                canMove = false;
                break;
            case PlayerState.Defend:
                canMove = false;
                _isDefending = true;
                Defend();
                break;
        }
    }


    void Idling() => _playerAnimatorManager.PlayIdleAnimation();

    void Walking()
    {
        _playerAnimatorManager.PlayWalkAnimation();
    }

    private void FlipCharacterSprite()
    {
        if (_vectorMovement.x == -1 && _isFacingRight)
        {
            _isFacingRight = false;
            transform.Rotate(0, 180, 0);
            return;
        }

        if (_vectorMovement.x == 1 && !_isFacingRight)
        {
            _isFacingRight = true;
            transform.Rotate(0, 180, 0);
            return;
        }

    }

    void Running()
    {
        if (!_isRunningMultplierActive)
        {
            movementSpeed *= runSpeed;
            _isRunningMultplierActive = true;
        }

        _playerAnimatorManager.PlayRunAnimation();
    }

    IEnumerator Dashing()
    {
        if(!_isDashingActive)
        {
            movementSpeed *= dashSpeed;
            _isDashingActive = true;
        }

        _playerAnimatorManager.PlayDashAnimation();
        yield return new WaitForSeconds(0.5f);
        _isDashing = false;
    }

    IEnumerator AttackOne()
    {
        if(!_isAttackOneTriggered)
        {
            _playerAnimatorManager.PlayAttackOneAnimation();
            _isAttackOneTriggered = true;
        }

        yield return new WaitForSeconds(0.5f);
        _isAttackOneTriggered = false;
    }

    void AttackTwo()
    {
        _isAttackTwoReady = false;
        SetState(PlayerState.Idle);
    }

    void Defend()
    {
        _playerAnimatorManager.PlayDefendAnimation();
    }



    public IEnumerator LogAttack(PlayerState attack, float time)
    {
        AttackLog.Add(currentState.ToString());
        yield return new WaitForSeconds(time);
        //search the list for the oldest occurent of attackState and remove it

    }

    public IEnumerator ClearAttackLog(float time)
    {
        yield return new WaitForSeconds(time);
        AttackLog.Clear();
    }

    public PlayerState GetPlayerState() => currentState;

}
