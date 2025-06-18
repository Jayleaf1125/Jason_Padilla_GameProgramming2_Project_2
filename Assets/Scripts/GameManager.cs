using UnityEditor.Presets;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject bossSpawner;

    public GameObject enemyPrefab;
    public GameObject enemyHealthbarPrefab;
    public Preset enemyHealthbarLocationPreset;

    int enemyCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var canvas = GameObject.Find("Canvas");

        Spawn(canvas, spawner1);
        Spawn(canvas, spawner2);
        Spawn(canvas, bossSpawner);
    }

    void Spawn(GameObject canvas, GameObject spawner)
    {
        var enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity);
        enemy.transform.Rotate(new Vector3(0, 180, 0));
        enemy.name = $"Enemy{enemyCount}";

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
