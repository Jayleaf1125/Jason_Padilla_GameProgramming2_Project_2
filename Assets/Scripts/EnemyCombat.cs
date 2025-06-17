using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackOnePos;
    public float attackOneRange;
    public LayerMask playerLayer;

    EnemyAnimatorManager _enemyAnimatorManager;
    public bool _isAttackOneTriggered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
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
            ph.DecereaseHealth(10f);
        }
        else
        {
            PlayerAnimatorManager pam = player.GetComponent<PlayerAnimatorManager>();
            pam.PlayDefendSuccessfulAnimation();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isAttackOneTriggered = true;
            StartCoroutine(AttackOne());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isAttackOneTriggered = false;
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
