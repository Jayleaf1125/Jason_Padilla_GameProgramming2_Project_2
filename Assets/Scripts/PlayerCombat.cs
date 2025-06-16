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

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleAttackOne()
    {
        Debug.Log("Hello, I'm Attack One");
        Collider2D enemy = Physics2D.OverlapCircle(attackOnePos.position, attackOneRange, enemyLayer);

        if (enemy == null)
        {
            Debug.Log("Enemy Not Found");
            return;
        }

        StartCoroutine(Test(enemy));
    }

    IEnumerator Test(Collider2D obj)
    {
        obj.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        obj.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOnePos.position, attackOneRange);
    }


}
