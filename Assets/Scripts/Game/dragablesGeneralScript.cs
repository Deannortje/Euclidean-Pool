using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragablesGeneralScript : MonoBehaviour
{
    public bool dragCollided = false;
    public string tag;

	public bool isDragCollided() {
		return this.dragCollided;
	}

	public void setDragCollided(bool dragCollided) {
		this.dragCollided = dragCollided;
	}


    void OnTriggerEnter(Collider other) {
        
        //Debug.Log("OnTriggerEnter");
        dragCollided = true;
        tag = other.gameObject.tag;
        
    }
    void OnTriggerExit(Collider other) {
        
        //Debug.Log("OnTriggerEnter");
        dragCollided = false;
        tag = "Untagged";
    }
}
