using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform attackOnePos;
    public float attackOneRange;
    public LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void HandleAttackOne()
    {
        Collider2D enemy = Physics2D.OverlapCircle(attackOnePos.position, attackOneRange, enemyLayer);
        GameObject enemyObj = enemy.transform.parent.gameObject; ;

        if (enemyObj == null)
        {
            Debug.Log("Enemy Not Found");
            return;
        }

        enemyObj.GetComponent<HealthSystem>().DecereaseHealth(25);
        StartCoroutine(Test(enemyObj));
    }

    IEnumerator Test(GameObject obj)
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
