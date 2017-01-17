using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;



public class SelectionManager : MonoBehaviour {
    
    public GameObject[] buttons;

	public void JoinPurple()
    {
        foreach(GameObject go in buttons)
        {
            go.SetActive(!go.activeSelf);
        }
        
    }

    public void JoinOrange()
    {
        foreach(GameObject go in buttons)
        {
            go.SetActive(!go.activeSelf);
        }
    }
}
