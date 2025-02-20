using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    void OnStart()
    {
        SceneManager.LoadScene("Title");
    }
}
