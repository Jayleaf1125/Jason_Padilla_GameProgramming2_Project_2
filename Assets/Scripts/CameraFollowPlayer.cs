using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    [Header("Camera Follow Properties")]
    public float x = 6;
    public float y = 0;
    public float z = -10;

    // Update is called once per frame
    void Update()
    {
        // Script: https://discussions.unity.com/t/how-to-get-camera-to-follow-player-2d/128194
        //transform.position = new Vector3(player.position.x + 6, 0, -10); // Camera follows the player but 6 to the right
        transform.position = new Vector3(player.position.x + x, y, z); // Camera follows the player but 6 to the right
    }
}
