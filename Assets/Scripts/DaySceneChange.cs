using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySceneChange : MonoBehaviour
{
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
