using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public enum Team
{
    RedTeam = 0, GreenTeam = 1, BlueTeam = 2, YellowTeam = 3, NoTeam = 4
}
public class PaintableObjectManager : MonoBehaviour {
    public Material noTeamMat, redTeamMat, greenTeamMat, blueTeamMat, yellowTeamMat;
    public Image uiRed, uiGreen, uiBlue, uiYellow;
    public Transform pannel;
	int totalPaintRedTeam = 0, totalPaintGreenTeam = 0, totalPaintBlueTeam = 0 , totalPaintYellowTeam = 0, totalPaintedObjects = 0;
    Team[] teamsInGame;
    //<TESTING>
    public bool test = false;
    // Use this for initialization
	void Start () {
        //<TESTING>
        Team[] teams = new Team[4] { Team.YellowTeam, Team.RedTeam, Team.GreenTeam, Team.BlueTeam };//,  Team.BlueTeam, Team.RedTeam};
        SetTeamsInGame(teams);
        UpdatePaintBar();
    }

    void Update()
    {
        if (test)
        { 
        UpdateTeamPaintCount();
            test = false;
        }
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
                case Team.GreenTeam:
                    uiGreen.gameObject.SetActive(true);
                    uiGreen.transform.SetParent(pannel);
                    uiGreen.transform.SetSiblingIndex(layerCount);
                    break;
                case Team.BlueTeam:
                    uiBlue.gameObject.SetActive(true);
                    uiBlue.transform.SetParent(pannel);
                    uiBlue.transform.SetSiblingIndex(layerCount);
                    break;
                case Team.YellowTeam:
                    uiYellow.gameObject.SetActive(true);
                    uiYellow.transform.SetParent(pannel);
                    uiYellow.transform.SetSiblingIndex(layerCount);
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
        totalPaintRedTeam = totalPaintGreenTeam = totalPaintBlueTeam = totalPaintYellowTeam = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Paintable"))
            {
            switch (go.GetComponent<PaintableObject>().teamInControl)
            {
                case Team.RedTeam:
                    totalPaintRedTeam++;
                    break;
                case Team.GreenTeam:
                    totalPaintGreenTeam++;
                    break;
                case Team.BlueTeam:
                    totalPaintBlueTeam++;
                    break;
                case Team.YellowTeam:
                    totalPaintYellowTeam++;
                    break;
                case Team.NoTeam:
                    break;
                default:
                    break;
            }

            }
        //Update Total Score.
        totalPaintedObjects = totalPaintRedTeam + totalPaintGreenTeam + totalPaintBlueTeam + totalPaintYellowTeam;
        //Update the ui. 
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
                    case Team.GreenTeam:
                        uiGreen.rectTransform.offsetMin = new Vector2(average * count, uiGreen.rectTransform.offsetMin.y);
                        break;
                    case Team.BlueTeam:
                        uiBlue.rectTransform.offsetMin = new Vector2(average * count, uiBlue.rectTransform.offsetMin.y);
                        break;
                    case Team.YellowTeam:
                        uiYellow.rectTransform.offsetMin = new Vector2(average * count, uiYellow.rectTransform.offsetMin.y);
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
	    	int count = 0;
            foreach (Team team in teamsInGame)
            {          
                    switch (team)
                    {
                        case Team.RedTeam:
                        uiRed.rectTransform.offsetMin = new Vector2(average * count, uiRed.rectTransform.offsetMin.y);
                        count += totalPaintRedTeam;
                            break;
                        case Team.GreenTeam:
                        uiGreen.rectTransform.offsetMin = new Vector2(average * count, uiGreen.rectTransform.offsetMin.y);
                        count += totalPaintGreenTeam;
                            break;
                        case Team.BlueTeam:
                       uiBlue.rectTransform.offsetMin = new Vector2(average * count, uiBlue.rectTransform.offsetMin.y);
                       count += totalPaintBlueTeam;
                            break;
                        case Team.YellowTeam:
                       uiYellow.rectTransform.offsetMin = new Vector2(average * count, uiYellow.rectTransform.offsetMin.y);
                       count += totalPaintYellowTeam;
                            break;
                        case Team.NoTeam:
                            break;
                        default:
                            break;
                    }
            }
      
        }
    }

    //Gets the current score of the game RGBY Total.
    int[] GetResults()
    {
        int[] results = new int[5] { totalPaintRedTeam, totalPaintGreenTeam, totalPaintBlueTeam, totalPaintYellowTeam, totalPaintedObjects };
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
