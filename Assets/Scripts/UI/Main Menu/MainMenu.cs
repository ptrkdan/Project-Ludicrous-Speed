using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button deleteSaveButton;

    private void Start()
    {
        if (SaveSystem.IsSaveFileExists())
        {
            continueButton.gameObject.SetActive(true);
            deleteSaveButton.gameObject.SetActive(true);
        }
    }

    public void DeleteSave()
    {
        SaveSystem.DeleteSavedGame();
        continueButton.gameObject.SetActive(false);
        deleteSaveButton.gameObject.SetActive(false);
    }
}
