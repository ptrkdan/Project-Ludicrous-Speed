using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    const int START_MENU_SCENE_INDEX = 0;
    const int HQ_SCENE_INDEX = 1;
    const int SMUGGLING_RUN_SCENE_INDEX = 2;

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
            
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
