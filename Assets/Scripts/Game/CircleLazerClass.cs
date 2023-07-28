using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLazerClass: MonoBehaviour // want to create game positioning etc via a text file 
{
    public GameObject circleLazer;
    
    public CircleLazerClass(Vector3 point, float radius)
    {
        Debug.Log("CircleLazerClass");
        circleLazer = Instantiate((Resources.Load("Prefabs/TorusPrefab") as GameObject));
        circleLazer.transform.position = new Vector3(point.x, 0, point.z);
        circleLazer.transform.localScale = new Vector3(radius, radius, radius);
        //circleLazer.gameObject.tag = "circleLazer";
        
    }

    public void DeleteCircleLazer(){
        Destroy(circleLazer);
    }
   
}