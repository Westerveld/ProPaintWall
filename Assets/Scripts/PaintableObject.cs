using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
class TeamScores
{
    [SyncVar]
    private int _m_score;

    public int m_score
    {
        get
        {
            return _m_score;
        }
        set
        {
            if (value > 0)
            {
                _m_score = value;
            }
            else
            {
                _m_score = 0;
            }
        }
    }
    
    public Team m_team;

    public TeamScores(Team team)
    {
        this.m_team = team;
        this._m_score = 0;
    }

    public void AddToScore(int score)
    {
        m_score = m_score + score;
        
    }

    [Command]
    public void CmdSetScore(int score)
    {
        m_score = score;
    }
}

public class PaintableObject : NetworkBehaviour {

    [SyncVar]
    public Team teamInControl = Team.NoTeam; //The current team in control of this object. ie who has it painted. 

    public int maxPaintWeight = 100;
    public int targetPaintWeight = 50;
    public int scoreWeight = 1;
    TeamScores[] teamScores = new TeamScores[4] { new TeamScores(Team.RedTeam), new TeamScores(Team.GreenTeam), new TeamScores(Team.BlueTeam), new TeamScores(Team.YellowTeam) } ;
    PaintableObjectManager pom; // Instance of PaintableObjectManager.
    //<Testing>
    public bool test;
    public Team testHitTeam = Team.NoTeam;
    // Use this for initialization
    void Start () {
        //Set PaintableObjectManager.
        pom = GameObject.FindGameObjectWithTag("POM").GetComponent<PaintableObjectManager>();
    }

    void Update()
    {
        if(test)
        {
            HitObject(testHitTeam, 10);
           test = false;
        }
    }

    void UpdateMaterial()
    {
        this.GetComponent<MeshRenderer>().material = pom.GetMateralForTeam(teamInControl);
    }

    //Deals with this object being hit with paint. 
    public void HitObject(Team teamThatHitThisObject, int hitPower)
    {
        //Ensure there is a team.
        if (teamThatHitThisObject != Team.NoTeam)
        {
            
            foreach (TeamScores team in teamScores)
            {   if (team.m_team == teamThatHitThisObject)
                {
                   
                    // team.AddToScore(hitPower);
                    team.CmdSetScore(hitPower + team.m_score);
                    if (team.m_score > maxPaintWeight)
                    {
                        team.CmdSetScore(maxPaintWeight);
                    }
                }
                else
                {
                 
                    team.AddToScore(-hitPower);
                    if (team.m_score < 0)
                    {
                        team.CmdSetScore(0);
                    }
                }
         }
            CmdCalculateControlOfThisObject();

            pom.UpdateTeamPaintCount();
        }
    }
    [Command]
    void CmdCalculateControlOfThisObject()
    {
        Team teamOnTop = Team.NoTeam;
        int topScore = 0;
        for (int i = 0; i < teamScores.Length; i++)
        {
            if (teamScores[i].m_score > topScore)
            {
                teamOnTop = teamScores[i].m_team;
                topScore = teamScores[i].m_score;
            }
        }

        if(topScore >= targetPaintWeight)
        {
            teamInControl = teamOnTop;
            UpdateMaterial();
        }
        else
        {
            teamInControl = Team.NoTeam;
        }
    }
}
