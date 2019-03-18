﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    const int START_MENU_SCENE_INDEX = 0;
    const int SMUGGLING_RUN_SCENE_INDEX = 2;

    public void GoToStartMenu() {
        SceneManager.LoadScene(START_MENU_SCENE_INDEX);
    }

    public void StartGame() {
        SceneManager.LoadScene(START_MENU_SCENE_INDEX + 1);
    }

    public void LoadSmugglingRunScene() {
        SceneManager.LoadScene(SMUGGLING_RUN_SCENE_INDEX);
    }

    public void WaitAndLoadGameOverScene(float delay = 0) {
        StartCoroutine(LoadGameOverScene(delay));
    }

    IEnumerator LoadGameOverScene(float delay) {
        yield return new WaitForSeconds(delay);
            
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}