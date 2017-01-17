using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PauseMenu : NetworkBehaviour
{

    public bool paused;
    public GameObject pauseMenu;
    void Start()
    {
        paused = false;
    }

    public void LoadMenu()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        paused = true;
    }

    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        paused = false;
    }

    public void QuitGame()
    {
        if(isServer)
        {
            NetworkLobbyManager NM = GameObject.FindGameObjectWithTag("NM").GetComponent<NetworkLobbyManager>();
            NM.StopHost();
            NM.StopMatchMaker();
            print("Destroyed Server");
        }
        else
        {

            NetworkLobbyManager NM = GameObject.FindGameObjectWithTag("NM").GetComponent<NetworkLobbyManager>();
            NM.StopClient();
            NM.StopMatchMaker();
            print("Left Match");
        }
    }
}
