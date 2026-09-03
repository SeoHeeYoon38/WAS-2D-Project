using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameProgressManager.Instance.LoadProgress();
        SceneManager.LoadScene(sceneName);
    }

    public void InitScene(string sceneName)
    {
        GameProgressManager.Instance.InitializeProgress();
        SceneManager.LoadScene(sceneName);
    }
}
