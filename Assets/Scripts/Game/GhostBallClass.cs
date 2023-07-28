using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBallClass: MonoBehaviour // want to create game positioning etc via a text file 
{
    public GameObject ghostBall;
    
    public GhostBallClass(Vector3 point)
    {
        Debug.Log("GhostBallClass");
        ghostBall = Instantiate((Resources.Load("Prefabs/ghostBallPrefab") as GameObject));
        ghostBall.transform.position = point;
        ghostBall.gameObject.tag = "ghostBall";
      
    }

    public void DeleteGhostBall(){
        Destroy(ghostBall);
    }
   
}