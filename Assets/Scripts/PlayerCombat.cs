using UnityEngine;
using System.Collections;

public enum CombatState
{
    Idle,
    Attack1,
    Attack2, 
    Attack3, 
    Defend
}


public class PlayerCombat : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _vectorMovement;
    PlayerAnimatorManager _playerAnimatorManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        SetState(CombatState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            SetState(CombatState.Attack1);
        }

    }

    void SetState(CombatState s)
    {
        switch (s)
        {
            case CombatState.Idle:
                Idling();
                break;
            case CombatState.Attack1:
                //StartCoroutine(AttackOne());
                AttackOne();
                break;
            case CombatState.Attack2:
                StartCoroutine(AttackTwo());
                break;
            case CombatState.Attack3:
                StartCoroutine(AttackThree());
                break;
            case CombatState.Defend:
                //StartCoroutine(Dashing());
                break;
        }
    }

    void Idling()
    {
         Debug.Log("Combat Idle");
        _playerAnimatorManager.PlayIdleAnimation();
    }

    //IEnumerator AttackOne()
    //{
    //    Debug.Log("Attak One Intiated");
    //    _playerAnimatorManager.PlayAttackOneAnimation();

    //    //if(Input.GetMouseButtonDown(0))
    //    //{
    //    //    SetState(CombatState.Attack2);
    //    //}

    //    yield return new WaitForSeconds(.1f);
    //    SetState(CombatState.Idle);
    //}

    void AttackOne()
    {
        Debug.Log("Attak One Intiated");
        _playerAnimatorManager.PlayAttackOneAnimation();
    }

    IEnumerator AttackTwo()
    {
        Debug.Log("Attak Two Intiated");
        _playerAnimatorManager.PlayAttackTwoAnimation();

        if (Input.GetMouseButtonDown(0)) { 
            SetState(CombatState.Attack3); 
        }

        yield return new WaitForSeconds(3f);
        SetState(CombatState.Idle);
    }

    IEnumerator AttackThree()
    {
        Debug.Log("Attak Three Intiated");
        _playerAnimatorManager.PlayAttackThreeAnimation();
        yield return new WaitForSeconds(0.5f);
            SetState(CombatState.Idle); 
    }
}
