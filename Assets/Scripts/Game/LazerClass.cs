using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerClass: MonoBehaviour // want to create game positioning etc via a text file 
{
    public GameObject lazer;
    public Transform A;
    public Transform B; 
    
    // Start is called before the first frame update
    //CreateLazer(A,B,Color.blue);
    public LazerClass()
    {
        lazer = Instantiate((Resources.Load("Prefabs/LaserBeamEmitter") as GameObject));
    }
    
    public LazerClass(Vector3 pointA, Vector3 pointB, Color color)
    {
        lazer = Instantiate((Resources.Load("Prefabs/LaserBeamEmitter") as GameObject));
        lazer.transform.position = new Vector3(0f,0f,0f);
        LineRenderer lr = lazer.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        if(color == Color.green)
        {
            lr.SetPosition(0,pointA);

            
            lr.SetPosition(1,pointB);
        }
        else
        {
            lr.SetPosition(0,pointA);
            lr.SetPosition(1,pointB);
        }
         
        Material LazerMaterial = lazer.GetComponent<Renderer>().material;
        LazerMaterial.color = color;
    }

    public void AlterLazer(Vector3 pointB){
        
        
        LineRenderer lr = lazer.GetComponent<LineRenderer>();
        Vector3 end = pointB;
        lr.SetPosition(1,end); 
        Material LazerMaterial = lazer.GetComponent<Renderer>().material;
        
    }

    public void DeleteLazer(){
        Destroy(lazer);
    }
}