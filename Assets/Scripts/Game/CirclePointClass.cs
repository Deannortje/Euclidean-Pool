using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePointClass: MonoBehaviour // want to create game positioning etc via a text file 
{
    public GameObject circlePoint;
    
    public CirclePointClass(Vector3 ghostBallPos, Vector3 whiteBallPos, Vector3 targetPos, string type)
    {
        //Debug.Log("CirclePointClass");

        float distance = Vector3.Distance(ghostBallPos, whiteBallPos);
        //Debug.Log("Distance" + distance);
        distance = distance + ghostBallPos.z;
        //Debug.Log("Distance after offset" + distance);   
        circlePoint = Instantiate((Resources.Load("Prefabs/circlePoint") as GameObject));
        if(type == "top")
        {
            circlePoint.transform.position = new Vector3(ghostBallPos.x, 0, distance);
        }
        if(type == "targetBallLine")
        {   
            distance = Vector3.Distance(ghostBallPos, whiteBallPos);

            Vector3 newVector = targetPos - ghostBallPos;
            newVector = newVector.normalized * distance;
            // float temp = getAngleFromVectorFloat(targetPos);
            // Debug.Log("temp" + temp);
            // Vector3 newVector = CreateVectorFromAngle(temp);

            circlePoint.transform.position = new Vector3(newVector.x + ghostBallPos.x, 0, newVector.z + ghostBallPos.z);
        }

       
        // ghostBall.gameObject.tag = "circlepoint";
      
    }


    public Vector3 CreateVectorFromAngle(float angle)
    {
        Vector3 vector = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        return vector;
    }
   
   public float getAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void DeleteCirclePoint(){
        Destroy(circlePoint);
    }
}