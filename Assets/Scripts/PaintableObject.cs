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
    [SyncVar]
    int currentPaintWeight = 0;


    public int maxPaintWeight = 100;
    //public int targetPaintWeight = 50;
    public int scoreWeight = 1;
    PaintableObjectManager pom; // Instance of PaintableObjectManager.
  
    public Team testHitTeam = Team.NoTeam;
    // Use this for initialization
    void Start () {
        //Set PaintableObjectManager.
        pom = GameObject.FindGameObjectWithTag("POM").GetComponent<PaintableObjectManager>();
    }

    void UpdateMaterial()
    {
        this.GetComponent<MeshRenderer>().material = pom.GetMateralForTeam(teamInControl);
    }

    //Deals with this object being hit with paint. 
    [Command]
    public void CmdHitObject(Team teamThatHitThisObject, int hitPower)
    {
        //Ensure there is a team.
        if (teamThatHitThisObject != Team.RedTeam || teamThatHitThisObject != Team.BlueTeam)
        {
        if(teamThatHitThisObject == Team.RedTeam)
            {
                this.currentPaintWeight += hitPower;
                if(currentPaintWeight > maxPaintWeight)
                {
                    this.currentPaintWeight = maxPaintWeight;
                }
            }   
        else if(teamThatHitThisObject == Team.BlueTeam)
            {
                this.currentPaintWeight -= hitPower;
                if(currentPaintWeight < maxPaintWeight*-1)
                {
                  this.currentPaintWeight = maxPaintWeight*-1;
                }
            } 
          
        }

        if(currentPaintWeight > 0)
        {
            this.teamInControl = Team.RedTeam;
           
        }
        else
        {
            this.teamInControl = Team.BlueTeam;
        }
        UpdateMaterial();
        pom.UpdateTeamPaintCount();
        
    }
   
}
