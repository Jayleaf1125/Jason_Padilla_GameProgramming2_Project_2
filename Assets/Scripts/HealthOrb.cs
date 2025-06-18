using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    public float healthAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.GetComponent<HealthSystem>().IncreaseHealth(healthAmount);
            GameObject.Find("Audio Manager").gameObject.GetComponent<AudioManager>().PlayPickUpHealthSound();
            Destroy(this.gameObject);
        }
    }
}
