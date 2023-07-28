using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcursor : MonoBehaviour
{

    [SerializeField] Transform box;
    // Start is called before the first frame update
    private bool moveState;

	public bool isMoveState() {
		return this.moveState;
	}

	public void setMoveState(bool moveState) {
		this.moveState = moveState;
	}

    void Start()
    {
        moveState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveState){
        box.position = mousetoWorldPos();
        }
        
        box.position = new Vector3(box.position.x, 0, box.position.z);
    }

    Vector3 mousetoWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnTriggerEnter(Collider other) {
        moveState = false;
        //Debug.Log("OnCollisionEnter");
        box.position = other.transform.position;
        
    }

    void OnTriggerStay(Collider other) {

        if(!moveState){
            Vector3 mousePos = mousetoWorldPos();
            mousePos = new Vector3(mousePos.x, 0.2f, mousePos.z);
            //check if mouse x y or z is greater than the radius.
            float distance = distanceBetweenPoints(mousePos, other.transform.position);
            //Debug.Log(distance);
            if (distance > 0.25f)
            {
                moveState = true;
            }
        }
    }
    
    void OnTriggerExit(Collider other) {
        moveState = true;
        //Debug.Log("OnCollisionExit");
    }   

    float distanceBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2) + Mathf.Pow(a.z - b.z, 2));
    }
}
