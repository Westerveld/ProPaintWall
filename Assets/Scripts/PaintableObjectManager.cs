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
    public Material noTeamMat, redTeamMat, greenTeamMat, blueTeamMat, yellowTeamMat;
    public Image uiRed, uiGreen, uiBlue, uiYellow;
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
        SetTeamsInGame(teams);
        UpdatePaintBar();

      PaintableObject.EventUpdatePaintObjectCount += UpdateTeamCount;
        
    }
    void Destoy()
    {
        PaintableObject.EventUpdatePaintObjectCount -= UpdateTeamCount;
    }

    void Update()
    {
        if (test)
        { 
        UpdateTeamPaintCount();
            test = false;
        }
    }



    void UpdateTeamCount(int addToRed, int addToBlue)
    {
        redTeamCount += addToRed;
        blueTeamCount += addToBlue;
    }


    //Set the teams in play, this data is used to determain what teams to display on the ui. 
    //Order team ui elements correctly in the hierarchy.
    public void SetTeamsInGame(Team[] teams)
    {
        teamsInGame = teams;

        foreach (Team team in teams)
        {
            int layerCount = teams.Length;
            switch (team)
            {
                case Team.RedTeam:
                    uiRed.gameObject.SetActive(true);
                    uiRed.transform.SetParent(pannel);
                    uiRed.transform.SetSiblingIndex(layerCount);
                    break;
              
                case Team.BlueTeam:
                    uiBlue.gameObject.SetActive(true);
                    uiBlue.transform.SetParent(pannel);
                    uiBlue.transform.SetSiblingIndex(layerCount);
                    break;
                case Team.NoTeam:
                    break;
                default:
                    break;
            }
            layerCount--;
        }
    }


    //Check each "Paintable" object and update the score acodingly. 
    //<IMPROVE THIS> this is inefficient and could be improved.
    public void UpdateTeamPaintCount()
    {
        //Reset all scores to 0;
        totalPaintedObjects = redTeamCount + blueTeamCount;
      
        UpdatePaintBar ();
    }

    //Update the ui acording the the current scores. 
    void UpdatePaintBar()
    {
        //Display the score as even when every team has a score of 0. 
        // <FIX THIS> This currently doesnt take into acount the number of teams and will display all 4 teams even when there are no players on a team. 
        if(totalPaintedObjects <= 0)
        {
            
            int average = Screen.width / teamsInGame.Length;
            int count = 0;
            foreach (Team team in teamsInGame)
            {
                
                switch (team)
                {
                    case Team.RedTeam:
                        uiRed.rectTransform.offsetMin = new Vector2(average * count, uiRed.rectTransform.offsetMin.y);
                        break;
                    case Team.BlueTeam:
                        uiBlue.rectTransform.offsetMin = new Vector2(average * count, uiBlue.rectTransform.offsetMin.y);
                        break;
                   case Team.NoTeam:
                        break;
                    default:
                        break;
                }
                count++;
            }
                         
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
    public Material GetMateralForTeam(Team team)
    {
        switch (team)
        {
            case Team.RedTeam:
                return redTeamMat;
            case Team.GreenTeam:
                return greenTeamMat;
            case Team.BlueTeam:
                return blueTeamMat;
            case Team.YellowTeam:
                return yellowTeamMat;
            case Team.NoTeam:
                return noTeamMat;
            default:
                return noTeamMat;
        }
    }

}
