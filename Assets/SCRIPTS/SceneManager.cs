using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{

    public void RestartLevel()
    {
        LoadScene(GetActiveScene().name);
    }
}
