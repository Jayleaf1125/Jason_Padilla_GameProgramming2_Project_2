using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    float _currentHealth;
    [SerializeField] float _maxHealth;
    PlayerAnimatorManager _playerAnimatorManager;
    EnemyAnimatorManager _enemyAnimatorManager;

    public bool isPlayerHealth = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;    

        if (isPlayerHealth)
        {
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        } else
        {
            _enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.J)) IncreaseHealth(10);
        //if (Input.GetKeyDown(KeyCode.K)) DecereaseHealth(10);
    }

    public float GetCurrentHealth() => _currentHealth;

    public void DecereaseHealth(float damage)
    {
        float remainingHealth = _currentHealth - damage;

        if (remainingHealth <= 0)
        {
            if(isPlayerHealth)
            {
                _playerAnimatorManager.PlayDeathAnimation();
                return;
            } else
            {
                GetComponent<EnemyCombat>().SetIsAttackOneTriggered(false);
                StartCoroutine(PlayEnemyDeath());
                return;
            }
        }

        _currentHealth = remainingHealth;
        StartCoroutine(TakingDamageAni());
    }

    IEnumerator PlayEnemyDeath()
    {
        _enemyAnimatorManager.PlayDeathAnimation();
        yield return new WaitForSeconds(.75f);
        Destroy(this.gameObject);
    }


    IEnumerator TakingDamageAni()
    {
        if (isPlayerHealth)
        {
            //PlayerMovement.canMove = false;
            _playerAnimatorManager.SetPlayerHurtingTrue();
            yield return new WaitForSeconds(0.25f);
            //PlayerMovement.canMove = true;
            _playerAnimatorManager.SetPlayerHurtingFalse();
        } else
        {
            //EnemyMovement.canMove = false;
            _enemyAnimatorManager.SetEnemyHurtingTrue();
            yield return new WaitForSeconds(0.25f);
            //EnemyMovement.canMove = true;
            _enemyAnimatorManager.SetEnemyHurtingFalse();
        }
    }


    public void IncreaseHealth(float amount)
    {
        float healedHealth = _currentHealth + amount;

        if (healedHealth > _maxHealth) return;

        _currentHealth = healedHealth;
        StartCoroutine(HealingAni());   
    }

    IEnumerator HealingAni()
    {
        if (isPlayerHealth)
        {
            PlayerMovement.canMove = false;
            _playerAnimatorManager.SetPlayerHealingTrue();
            yield return new WaitForSeconds(0.75f);
            PlayerMovement.canMove = true;
            _playerAnimatorManager.SetPlayerHealingFalse();
        } else
        {
            _enemyAnimatorManager.SetEnemyHurtingTrue();
            yield return new WaitForSeconds(0.75f);
            _enemyAnimatorManager.SetEnemyHurtingFalse();
        }
    }
}
