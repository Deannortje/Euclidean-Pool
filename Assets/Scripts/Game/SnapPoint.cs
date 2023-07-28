using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint: MonoBehaviour
{

    public GameObject SnapPointObj;
    public Transform point;
    public SphereCollider collider;
    public Rigidbody rigid;

    public SnapPoint(float radius, string tag)
    {
        this.SnapPointObj = new GameObject();
        this.SnapPointObj.name = "SnapPoint";
        this.collider = this.SnapPointObj.AddComponent<SphereCollider>();
        this.rigid = this.SnapPointObj.AddComponent<Rigidbody>();
        this.rigid.useGravity = false;
        this.collider.radius = radius;
        this.SnapPointObj.transform.position = new Vector3(0, 0.2f, 0);
        this.collider.isTrigger = true;
        this.SnapPointObj.tag = tag;
        this.SnapPointObj.AddComponent<collidedWithScript>();
    }
    public SnapPoint(GameObject point)
    {
        this.point = point.GetComponent<Transform>();
        this.collider = point.GetComponent<SphereCollider>();
    }

    public void SetParent(Transform parent)
    {
        SnapPointObj.GetComponent<Transform>().SetParent(parent);
    }

}