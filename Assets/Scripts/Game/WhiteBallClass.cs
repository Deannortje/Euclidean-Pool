using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBallClass : MonoBehaviour
{
    public GameObject Ball;
    public float Bloom = 0;
    int State = 0;
    float starttime;

    public void setName(string n)
    {
        Ball.name = n;
    }
    public WhiteBallClass(GameObject ball)
    {
        Ball = Instantiate(ball);
    }
   
   public GameObject getGameObject()
    {
        return Ball;
    }
    
    public Transform getTransform()
    {
        return Ball.GetComponent<Transform>();
    }

    public Rigidbody getRigidbody()
    {
        return Ball.GetComponent<Rigidbody>();
    }

    public void setVelocity(Vector3 vel)
    {
        Ball.GetComponent<Rigidbody>().velocity = vel;
    }

    public void resetState()
    {
        State = 0;
    }

    public void resetState(Vector3 pos)
    {
        State = 0;
        Ball.GetComponent<Transform>().position = pos;
        Ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
    }

    public void shoot(float shootStrength, Transform Cue)
    {

        if(State == 0)
        {
            Ball.GetComponent<Rigidbody>().AddForce(directionFromTarget(Ball.transform,Cue)*shootStrength+randomPerpendicularVec3(directionFromTarget(Ball.transform,Cue))*Bloom*shootStrength);
            State = 1;
        }
    }

    Vector3 directionFromTarget(Transform Origin, Transform cue){
        Vector3 direction = cue.position - Origin.position;
        direction *= -1;
        return direction;
    } 

    Vector3 randomPerpendicularVec3(Vector3 vec){
        Vector3 randomVec = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0);
        Vector3 perpendicularVec = Vector3.Cross(vec,randomVec);
        return perpendicularVec;
    }

}
