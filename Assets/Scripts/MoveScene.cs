using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public string targetSceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}