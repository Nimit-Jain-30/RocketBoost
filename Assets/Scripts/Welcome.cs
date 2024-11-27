using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    float displayDuration = 3f;
    void Start()
    {
        Invoke("StartGame", displayDuration);
    }

    void StartGame(){
        SceneManager.LoadScene(1);
    }
}
