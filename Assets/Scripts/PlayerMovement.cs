using UnityEngine;

public enum MovementState
{
    Idle,
    Walk,
    Run,
    Jump,
    Dash
}

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public float runSpeed;

    Rigidbody2D _rb;
    Vector2 _vectorMovement;
    PlayerAnimatorManager _playerAnimatorManager;

    bool _isFacingRight;

    bool _isRunning;
    bool _isRunningMultplierActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        _isFacingRight = true;
        _isRunning = false;
        _isRunningMultplierActive = false;
    }

    void Update()
    {
        CheckLeftShift();
    }
    void CheckLeftShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed /= runSpeed;
            _isRunning = false;
            _isRunningMultplierActive = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _vectorMovement.x = Input.GetAxisRaw("Horizontal");

        if (_vectorMovement.x == 0)
        {
            SetState(MovementState.Idle);
        } else 
        {
            SetState(MovementState.Walk);

            if(_isRunning) SetState(MovementState.Run);
        } 

    }

    void SetState(MovementState s)
    {
        switch (s)
        {
            case MovementState.Idle:
                Idling();
                break;
            case MovementState.Walk:
                Walking();
                break;
            case MovementState.Run:
                Running();
                break;
        }
    }


    void Idling()
    {
        Debug.Log("Is Now Idling");
        _playerAnimatorManager.PlayIdleAnimation();
    }

    void Walking()
    {
        Debug.Log("Is Now Walking");
        _rb.MovePosition(_rb.position + _vectorMovement * movementSpeed * Time.fixedDeltaTime);
        _playerAnimatorManager.PlayWalkAnimation();
        FlipCharacterSprite();
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
        Debug.Log("Is Now Running");

        if(!_isRunningMultplierActive)
        {
            movementSpeed *= runSpeed;
            _isRunningMultplierActive = true;
            _playerAnimatorManager.PlayRunAnimation();
        }
    }
}
