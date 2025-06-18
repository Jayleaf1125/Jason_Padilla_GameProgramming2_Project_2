using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform attackOnePos;
    public float attackOneRange;
    public LayerMask enemyLayer;

    public float damage;
    AudioManager _audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    public void HandleAttackOne()
    {
        Collider2D enemy = Physics2D.OverlapCircle(attackOnePos.position, attackOneRange, enemyLayer) ?? null;

        if (enemy == null)
        {
            Debug.Log("Enemy Not Found");
            return;
        }

        _audioManager.PlaySwordHitSound();
        enemy.GetComponent<HealthSystem>().DecereaseHealth(damage);
        StartCoroutine(Test(enemy));
    }

    IEnumerator Test(Collider2D obj)
    {
        obj.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOnePos.position, attackOneRange);
    }


}
