using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    const int PRELOAD_SCENE_INDEX = 0;
    const int START_MENU_SCENE_INDEX = 1;
    const int HQ_SCENE_INDEX = 2;
    const int SMUGGLING_RUN_SCENE_INDEX = 3;
    
    public void GoToPreload() {
        SceneManager.LoadScene(PRELOAD_SCENE_INDEX);
    }

    public void GoToStartMenu() {
        SceneManager.LoadScene(START_MENU_SCENE_INDEX);
    }

    public void GoToHQScene() {
        SceneManager.LoadScene(HQ_SCENE_INDEX);
    }

    public void LoadSmugglingRunScene() {
        SceneManager.LoadScene(SMUGGLING_RUN_SCENE_INDEX);
    }

    public void WaitAndLoadRunResultsScene(float delay = 0) {
        StartCoroutine(LoadRunResultsScene(delay));
    }

    IEnumerator LoadRunResultsScene(float delay) {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SMUGGLING_RUN_SCENE_INDEX + 1);
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
