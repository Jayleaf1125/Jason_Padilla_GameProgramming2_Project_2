using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    float _currentHealth;
    [SerializeField] float _maxHealth;
    PlayerAnimatorManager _playerAnimatorManager;
    EnemyAnimatorManager _enemyAnimatorManager;

    public Slider healthbarUI;

    public bool isPlayerHealth = true;

    AudioManager _audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        _currentHealth = _maxHealth;
        healthbarUI.maxValue = _maxHealth;
        healthbarUI.value = _currentHealth;

        if (isPlayerHealth)
        {
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        } else
        {
            _enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }

    }

    public void SetMaxHealth(float newMaxhealth) => _maxHealth = newMaxhealth;


    public float GetCurrentHealth() => _currentHealth;

    public void DecereaseHealth(float damage)
    {
        float remainingHealth = _currentHealth - damage;

        if (remainingHealth <= 0)
        {
            healthbarUI.value = 0;
            healthbarUI.gameObject.SetActive(false);

            if (isPlayerHealth)
            {
                _playerAnimatorManager.PlayDeathAnimation();
                _audioManager.PlayPlayerDeath();
                StartCoroutine(LoseScreenTransition());
                return;
            } else
            {
                GetComponent<EnemyCombat>().SetIsAttackOneTriggered(false);
                StartCoroutine(PlayEnemyDeath());
                return;
            }
        }

        _currentHealth = remainingHealth;
        healthbarUI.value = _currentHealth;
        StartCoroutine(TakingDamageAni());
    }

    IEnumerator LoseScreenTransition()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(2);
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
        healthbarUI.value = _currentHealth;
        StartCoroutine(HealingAni());   
    }

    IEnumerator HealingAni()
    {
        PlayerMovement.canMove = false;
        _playerAnimatorManager.SetPlayerHealingTrue();
        yield return new WaitForSeconds(0.75f);
        PlayerMovement.canMove = true;
        _playerAnimatorManager.SetPlayerHealingFalse();
    }
}
