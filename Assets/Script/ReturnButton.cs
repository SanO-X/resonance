using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    public int sceneIndexToLoad = 3;

    public void ReturnToScene()
    {
        Debug.Log("RETURN BUTTON НАЖАТ. Загружаю сцену: " + sceneIndexToLoad);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndexToLoad);
    }

    public void RestartCurrentScene()
    {
        Debug.Log("RESTART BUTTON НАЖАТ. Перезапускаю текущую сцену");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}