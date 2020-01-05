using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button deleteSaveButton;

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        if (SaveSystem.IsSaveFileExists())
        {
            continueButton.gameObject.SetActive(true);
            deleteSaveButton.gameObject.SetActive(true);
        }
    }

    public void NewGame()
    {
        sceneLoader.StartNewGame();
    }

    public void Continue()
    {
        sceneLoader.LoadSavedGame();
    }

    public void Options()
    {

    }

    public void Quit()
    {
        sceneLoader.QuitGame();
    }

    public void DeleteSave()
    {
        SaveSystem.DeleteSavedGame();
        continueButton.gameObject.SetActive(false);
        deleteSaveButton.gameObject.SetActive(false);
    }
}
