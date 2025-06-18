using UnityEditor.Presets;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject spawner1;
    string spawner1Name = "spawner1";
    public GameObject spawner2;
    string spawner2Name = "spawner2";
    public GameObject bossSpawner;
    string bossSpawnerName = "bossSpawner";

    public GameObject enemyPrefab;
    public GameObject enemyHealthbarPrefab;
    public Preset enemyHealthbarLocationPreset;

    int enemyCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var canvas = GameObject.Find("Canvas");

        Spawn(canvas, spawner1, spawner1Name);
        Spawn(canvas, spawner2, spawner2Name);
        Spawn(canvas, bossSpawner, bossSpawnerName);
    }

    void Spawn(GameObject canvas, GameObject spawner, string name)
    {
        var enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity);
        enemy.transform.Rotate(new Vector3(0, 180, 0));
        enemy.name = $"Enemy{enemyCount}";

        if (name == "bossSpawner") 
        {
            enemy.GetComponent<HealthSystem>().SetMaxHealth(200);
            enemy.GetComponent<EnemyCombat>().SetDamaage(20);
        } else
        {
            enemy.GetComponent<EnemyCombat>().SetDamaage(10);
        }


        var healthbar = Instantiate(enemyHealthbarPrefab);
        healthbar.name = $"Enemy_Healthbar{enemyCount++}";


        enemy.GetComponent<HealthSystem>().healthbarUI = healthbar.GetComponent<Slider>();
        healthbar.transform.SetParent(canvas.transform);
        healthbar.gameObject.SetActive(false);
        

        RectTransform rect = healthbar.GetComponent<RectTransform>();
        enemyHealthbarLocationPreset.ApplyTo(rect);    
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
