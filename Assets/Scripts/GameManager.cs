using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject bossSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(spawner1.transform.position, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(spawner2.transform.position, 1);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(bossSpawner.transform.position, 1);
    }
}
