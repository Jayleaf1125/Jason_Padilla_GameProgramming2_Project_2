using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    float _currentHealth;
    [SerializeField] float _maxHealth;
    PlayerAnimatorManager _playerAnimatorManager;

    // public bool isPlayerHealth

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;    
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) IncreaseHealth(10);
        if (Input.GetKeyDown(KeyCode.K)) DecereaseHealth(10);
    }

    public float GetCurrentHealth() => _currentHealth;

    public void DecereaseHealth(float damage)
    {
        float remainingHealth = _currentHealth - damage;

        if (remainingHealth <= 0)
        {
            _playerAnimatorManager.PlayDeathAnimation();
            return;
        }

        Debug.Log($"Current Health is {remainingHealth}");

        _currentHealth = remainingHealth;
        StartCoroutine(TakingDamageAni());
    }

    IEnumerator TakingDamageAni()
    {
        PlayerMovement.canMove = false;
        _playerAnimatorManager.SetPlayerHurtingTrue();
        yield return new WaitForSeconds(0.25f);
        PlayerMovement.canMove = true;
        _playerAnimatorManager.SetPlayerHurtingFalse();
    }


    public void IncreaseHealth(float amount)
    {
        float healedHealth = _currentHealth + amount;

        if (healedHealth > _maxHealth) return;

        Debug.Log($"Current Health is {healedHealth}");

        _currentHealth = healedHealth;
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
