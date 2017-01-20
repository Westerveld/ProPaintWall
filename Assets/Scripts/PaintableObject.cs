using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class PaintableObject : NetworkBehaviour {
    public Material redTeam, blueTeam;

    [SyncVar]
    public Team teamInControl = Team.NoTeam; //The current team in control of this object. ie who has it painted. 
    [SyncVar]
    int currentPaintWeight = 0;

    public delegate void UpdatePaintObjectCount(int addToRed, int addToBlue);
   [SyncEvent]
    public static event UpdatePaintObjectCount EventUpdatePaintObjectCount;


    public int maxPaintWeight = 100;
    //public int targetPaintWeight = 50;
    public int scoreWeight = 1;
    PaintableObjectManager pom; // Instance of PaintableObjectManager.
  
    public Team testHitTeam = Team.NoTeam;
    // Use this for initialization
    void Start () {
        //Set PaintableObjectManager.
    
    }

    
    void CmdUpdateMaterial()
    {
       if(teamInControl == Team.RedTeam)
        {
            this.GetComponent<MeshRenderer>().material = redTeam;
        }
       else if (teamInControl == Team.BlueTeam)
        {
            this.GetComponent<MeshRenderer>().material = blueTeam;
        }
       
    }

    void Update()
    {
        CmdUpdateMaterial();
     
    }

    //Deals with this object being hit with paint. 
    [Command]
    public void CmdHitObject(Team teamThatHitThisObject, int hitPower)
    {
        Team currentTeam = teamInControl;
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













     if(teamInControl != currentTeam && currentTeam!=Team.NoTeam)
        {
            if(teamInControl == Team.RedTeam)
            {
                EventUpdatePaintObjectCount(1,-1);

            }

            if (teamInControl == Team.BlueTeam)
            {
                EventUpdatePaintObjectCount(-1,1);

            }
        }
        else if(currentTeam == Team.NoTeam)
        {
            if(teamInControl == Team.RedTeam)
            {
                EventUpdatePaintObjectCount(1, 0);
            }
            if (teamInControl == Team.BlueTeam)
            {
                EventUpdatePaintObjectCount(0, 1);
            }
        }
        
       
      
       
        
    }
   
}
