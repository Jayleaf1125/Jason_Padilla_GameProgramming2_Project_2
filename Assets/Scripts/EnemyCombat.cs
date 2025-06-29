using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackOnePos;
    public float attackOneRange;
    public LayerMask playerLayer;

    EnemyAnimatorManager _enemyAnimatorManager;
    public bool _isAttackOneTriggered = false;
    public float damage;

    AudioManager _audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    public void SetIsAttackOneTriggered(bool boolean) => _isAttackOneTriggered = boolean;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackOnePos.position, attackOneRange);
    }

    public void HandleEnemyAttackOne()
    {
        Collider2D player = Physics2D.OverlapCircle(attackOnePos.position, attackOneRange, playerLayer);

        if (player == null) { Debug.Log("Cannot Find Player"); return; }

        HandlePlayerFeedback(player);        
    }

    void HandlePlayerFeedback(Collider2D player)
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        PlayerState playerCurrentState = pm.GetPlayerState();

        if (playerCurrentState != PlayerState.Defend)
        {
            HealthSystem ph = player.GetComponent<HealthSystem>();
            _audioManager.PlayPlayerTakingDamage();
            ph.DecereaseHealth(damage);
        }
        else
        {
            PlayerAnimatorManager pam = player.GetComponent<PlayerAnimatorManager>();
            _audioManager.PlaySwordClash();
            pam.PlayDefendSuccessfulAnimation();
        }
    }

    public void SetDamaage(float newDamage) => damage = newDamage;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isAttackOneTriggered = true;
            this.gameObject.GetComponent<HealthSystem>().healthbarUI.gameObject.SetActive(true);
            StartCoroutine(AttackOne());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isAttackOneTriggered = false;
            this.gameObject.GetComponent<HealthSystem>().healthbarUI.gameObject.SetActive(false);
        }
    }

    IEnumerator AttackOne()
    {
        if(_isAttackOneTriggered)
        {
            _enemyAnimatorManager.SetEnemyAttackingTrue();
            yield return new WaitForSeconds(0.5f);
            _enemyAnimatorManager.SetEnemyAttackingFalse();
            StartCoroutine(ResetAttackOne());
        }
    }

    IEnumerator ResetAttackOne()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(AttackOne());
    }
}
