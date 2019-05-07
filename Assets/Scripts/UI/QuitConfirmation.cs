using UnityEngine;

public class QuitConfirmation : MonoBehaviour
{
    public void CancelQuit()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void ConfirmQuit()
    {
        Time.timeScale = 1;
        Quit();
    }

    private static void Quit()
    {
        FindObjectOfType<SceneLoader>().GoToStartMenu();
    }
}
