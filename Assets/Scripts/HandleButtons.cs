using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleButtons : MonoBehaviour
{
    public void LoadScene(int sceneNumber) => SceneManager.LoadSceneAsync(sceneNumber);
}
