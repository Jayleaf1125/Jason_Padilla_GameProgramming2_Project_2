using UnityEngine;
using System.Collections;

public enum MovementState
{
    Idle,
    Walk,
    Run,
    Jump,
    Dash,
    Attack1
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Movement Properties")]
    public float movementSpeed;
    public float runSpeed;

    [Header("Dash Properties")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    [Header("Current State")]
    public MovementState currentState;

    // dash timer cooldown
    bool _isDashAvailable;
    float _dashTimer;

    Rigidbody2D _rb;
    Vector2 _vectorMovement;
    PlayerAnimatorManager _playerAnimatorManager;

    bool _isFacingRight;

    bool _isRunning;
    bool _isRunningMultplierActive;

    bool _isAttackTriggered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        _isFacingRight = true;
        _isRunning = false;
        _isRunningMultplierActive = false;
        _isDashAvailable = true;
        _dashTimer = dashCooldown;
        _isAttackTriggered = false;
    }

    void Update()
    {
        IsRunning();
        if(Input.GetKeyDown(KeyCode.Space) && _isDashAvailable)
        {
            SetState(MovementState.Dash);
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            SetState(MovementState.Attack1);
        }
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
            movementSpeed /= runSpeed;
            _isRunningMultplierActive = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MovementSetUp();

        if (_vectorMovement.x == 0 && !_isAttackTriggered)
        {
            SetState(MovementState.Idle);
        } else if (_vectorMovement.x == 1 || _vectorMovement.x == -1) {
            if(_isRunning)
            {
                SetState(MovementState.Run);
                return;
            }
            SetState(MovementState.Walk);
        } 
        
        SetState(currentState);
    }

    void MovementSetUp()
    {
        _vectorMovement.x = Input.GetAxisRaw("Horizontal");
        _rb.MovePosition(_rb.position + _vectorMovement * movementSpeed * Time.fixedDeltaTime);
        FlipCharacterSprite();
    }

    void SetState(MovementState s)
    {
        if (s == currentState) return;
        currentState = s;

        switch (s)
        {
            case MovementState.Idle:
                Debug.Log("Is Idling");
                Idling();
                break;
            case MovementState.Walk:
                Debug.Log("Is Walking");
                Walking();
                break;
            case MovementState.Run:
                Debug.Log("Is Running");
                Running();
                break;
            case MovementState.Dash:
                Debug.Log("Is Dashing");
                StartCoroutine(Dashing());
                break;
            case MovementState.Attack1:
                Debug.Log("Is Commencing Attack One");
                StartCoroutine(AttackOne());
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
     
        Debug.Log("Is Dashing");
        //movementSpeed *= dashSpeed;
        //_isDashAvailable = false;
        _playerAnimatorManager.PlayDashAnimation();
        _isDashAvailable = false;
        yield return new WaitForSeconds(dashCooldown);
        _isDashAvailable = true;
        SetState(MovementState.Idle);


        //movementSpeed /= dashSpeed;
        //RunDashCooldown();
    }

    void RunDashCooldown()
    {
        _isDashAvailable = false;

        while(_dashTimer > 0f)
        {
            _dashTimer -= Time.fixedDeltaTime;
            //Debug.Log(_dashTimer);
        }

        _dashTimer = dashCooldown;
        _isDashAvailable = true;
        SetState(MovementState.Idle);
    }

    IEnumerator AttackOne()
    {
        if(!_isAttackTriggered)
        {
            _playerAnimatorManager.PlayAttackOneAnimation();
            _isAttackTriggered = true;
        }

        yield return new WaitForSeconds(0.5f);
        _isAttackTriggered = false;
    }

}
