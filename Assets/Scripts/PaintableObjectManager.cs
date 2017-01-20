using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
public enum Team
{
    RedTeam = 0, GreenTeam = 1, BlueTeam = 2, YellowTeam = 3, NoTeam = 4
}
public class PaintableObjectManager : NetworkBehaviour {
 
    public Image uiRed, uiBlue;
    public Transform pannel;

   



    [SyncVar]
    int redTeamCount;
    [SyncVar]
    int blueTeamCount;

    int totalPaintedObjects;

    //int totalPaintRedTeam = 0, totalPaintGreenTeam = 0, totalPaintBlueTeam = 0 , totalPaintYellowTeam = 0, totalPaintedObjects = 0;
    Team[] teamsInGame;
    //<TESTING>
    public bool test = false;
    // Use this for initialization
	void Start () {
        //<TESTING>
        Team[] teams = new Team[2] {Team.RedTeam, Team.BlueTeam };//,  Team.BlueTeam, Team.RedTeam};
        UpdatePaintBar();

     
        PaintableObject.EventUpdatePaintObjectCount += UpdateTeamCount;
        
        
    }
    void Destoy()
    {
        PaintableObject.EventUpdatePaintObjectCount -= UpdateTeamCount;
    }

    void OnGUI()
    {
        UpdatePaintBar();
    }


    void UpdateTeamCount(int addToRed, int addToBlue)
    {
        redTeamCount += addToRed;
        blueTeamCount += addToBlue;
       
    }


   
    //Update the ui acording the the current scores. 
    void UpdatePaintBar()
    {
        totalPaintedObjects = redTeamCount + blueTeamCount;

       
        if (totalPaintedObjects <= 0)
        {
            
            int average = Screen.width / 2;
            uiBlue.rectTransform.offsetMin = new Vector2(average , uiBlue.rectTransform.offsetMin.y);
        }
        
        else
        { 
            //Calculate the size of each segment. 
		    int average = Screen.width / totalPaintedObjects;
	    	  uiBlue.rectTransform.offsetMin = new Vector2(average * redTeamCount, uiBlue.rectTransform.offsetMin.y);
        }
    }

    //Gets the current score of the game RGBY Total.
    int[] GetResults()
    {
        int[] results = new int[2] { redTeamCount, blueTeamCount };
        return results;
    }

    //Get the corisponding material for the team. Used by Paintable objects to change there material when hit.
  

}
