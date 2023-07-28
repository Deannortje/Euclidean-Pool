using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueClass: MonoBehaviour
{

    //[SerializeField] Transform Ball;
    public GameObject Cue;
    //[SerializeField] float Offset = 0.55f;
    [SerializeField] MeshRenderer CueRenderer;
    float starttime;
    Transform BaseShotPosition;
    Transform BaseBallPosition;
    Transform BasePuck;
    Transform LastPos;
    //LineRenderer lr;

    private int State = -1;  
    /*
     1 - Following
     2 - Shooting
     3 - Moving
    */

    // Start is called before the first frame update
    void Start()
    {
        // AimLazer.positionCount = 2;
    }

    public CueClass(GameObject cue)
    {
        Cue = Instantiate(cue, new Vector3(0, 0, 0), Quaternion.identity);
        State = 1;
    }

    public Transform getTransform()
    {
        return Cue.GetComponent<Transform>();
    }

    public void setState(int state)
    {
        State = state;
    }

    public int getState()
    {
        return State;
    }

    public void nextState()
    {
        State++;
        if(State == 5)
        {
            State = 1;
        }
    }

  

    // Update is called once per frame
    public void UpdateCuePosition(Transform Ball, float Offset)
    {
        LineRenderer Lazer = Cue.GetComponent<LineRenderer>();
        Transform CueTransform = Cue.GetComponent<Transform>();
        Vector3 InitialBallPos = Vector3.zero;
        
        if(State == 1)
        {  
            Cue.GetComponent<Transform>().position = calculatePointOnRadius(getMouseWorldPos(),Ball.position,Offset);
            InitialBallPos= Ball.position;
            setYto0(Cue.GetComponent<Transform>());
            CueLookAt(Cue.GetComponent<Transform>(),Ball);
            setAimLazer(Lazer,Ball,CueTransform.position);
            starttime = Time.time;
        }
        if(State == 2)
        {  
            Cue.GetComponent<Transform>().position -= (InitialBallPos-Cue.GetComponent<Transform>().position).normalized*0.00035f;
        }
        if(State==3)
        {
            //does nothing because shot is held, maybe add shacking animation
        }
        if(State == 4)
        {
            Cue.GetComponent<Transform>().position += (InitialBallPos-Cue.GetComponent<Transform>().position)*0.0035f;
        }
    }

    Vector3 setHeightTo015(Vector3 v)
    {
        v.y = 0.015f;
        return v;
    }
    Vector3 HitscanInDirection(Vector3 origin, Vector3 direction)
    {
        origin = setHeightTo015(origin);
        direction = zeroy(direction);
        RaycastHit hit;
        if (Physics.Raycast(origin, -direction, out hit,20f))
        {
//            Debug.Log(hit.point);
            return hit.point;
        }
        return origin;
    }
    void setAimLazer(LineRenderer lr,Transform Start,Vector3 End)
    {
        lr.SetPosition(0,Start.position-new Vector3(0f,0.015f,0f));
        lr.SetPosition(1,HitscanInDirection(Start.position,End));
    }
    Transform CopyTransform(Transform source)
    {
        Transform copy = new GameObject().transform;
        copy.position = source.position;
        copy.rotation = source.rotation;
        copy.localScale = source.localScale;
        return copy;
    }
    
    bool isMoving(Transform A, Transform B)
    {
        return (A.position-B.position).magnitude != 0f;
    }

    float PullBack(float t)
    {
        float a = -0.00117370892019f;
        float b = 0.0068544600939f;
        return (a*t*t+b*t)*0.15f;
    }
    //maps the two functions to have a gradual pull back, then at 0.8 a sharp hit that then pulls back to 0 at 1.5, used to adjust distance offset on puck to ball
    float OffsetAtTime(float t)
    {
        
        if(t<0.8)
        {
            return (-t*t+2*t)*0.15f;
        }
        else
        {
            float a = 2.95373321742f;
            float b = -8.16501497151f;
            float c = 5.60162271805f;
            return (a*t*t+b*t+c)*0.15f;;
        }
         
    }



    //only need the y angle as other rotations are set
    void CueLookAt(Transform A, Transform B)
    {
        A.LookAt(B);
        A.rotation = Quaternion.Euler(90, A.rotation.eulerAngles.y, 0);
    }

    Transform setYto0(Transform t)
    {
        t.position = new Vector3(t.position.x, 0.03f, t.position.z);
        return t;
    }


    Vector3 getMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    Vector3 calculatePointOnRadius(Vector3 TargetDirection, Vector3 CircleCenter, float radius)
    {
        Vector3 puckPosition = CircleCenter + (zeroy(TargetDirection-CircleCenter).normalized * radius);
        return puckPosition;
    }
    Vector3 zeroy(Vector3 v)
    {
        v.y = 0;
        return v;
    }
}
