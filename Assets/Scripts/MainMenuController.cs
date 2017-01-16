using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu, optionsMenu;

    public void ShowMain()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("NetworkLobby");
    }

    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
