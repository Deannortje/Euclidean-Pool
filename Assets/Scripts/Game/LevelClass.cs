using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class LevelClass : MonoBehaviour
{
    private WhiteBallClass whiteBall;
    private GameObject targetBall;
    private GameObject target;
    private GameObject cue;
    private LazerClass lazer;



    public LevelClass() // Constructor
    {
        GameObject baseBall = (Resources.Load("Prefabs/balls") as GameObject);
        
        this.whiteBall = new WhiteBallClass(baseBall.transform.GetChild(0).gameObject);
        this.whiteBall.setName("whiteBall");
        this.whiteBall.getGameObject().tag = "whiteBall";
        
        createChildSnapPoint(whiteBall.getGameObject(), "whiteBall");

        this.targetBall = Instantiate(baseBall.transform.GetChild(9).gameObject);
        this.targetBall.name = "targetBall";
        this.targetBall.GetComponent<MeshFilter>().mesh.name = "ball01";
        this.targetBall.tag = "targetBall";
        createChildSnapPoint(targetBall, "targetBall");

        this.target = Instantiate(Resources.Load("Prefabs/target") as GameObject);
        this.target.name = "target";
        this.target.tag = "target";
        createChildSnapPoint(target, "target");

        this.cue = Instantiate(Resources.Load("Prefabs/cue") as GameObject);
        this.cue.name = "cue";

        setUpLevel();

    }

    private void createChildSnapPoint(GameObject parent, string tag)
    {
        SnapPoint sp = new SnapPoint(0.05f, tag);
        sp.SetParent(parent.GetComponent<Transform>());
    }

    public void setUpLevel()
    { 
        whiteBall.resetState(new Vector3(0,0.03f,0.016f));//-0.155736f,0.03f,0 {2022/08/09}//-0,4155736 AND -0,04242641
        targetBall.GetComponent<Transform>().position = new Vector3 (-0.458f,0.03f ,0);//(-0.75f, 0, 0.2f);// (-1.142f,-0.02f,0.684f)
        targetBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        targetBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        targetBall.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,0);
        //whiteBall.resetState(new Vector3(-0.0f,0.03f,-0.5f ));
        //target.GetComponent<Transform>().position = new Vector3(-1f,0.03f,0f);
        target.GetComponent<Transform>().position = new Vector3(-1.142f,0.03f,0.684f); //lets incrment by 0.684 //30.9194671 angle
        //Debug.Log(Vector3.Angle(targetBall.GetComponent<Transform>().position, target.GetComponent<Transform>().position));
        killmomentum();

        whiteBall.getGameObject().transform.GetChild(0).GetComponent<Transform>().position = new Vector3(0,0.2f,0.016f);// (-0.155736f,0.2f,0);
        targetBall.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position = new Vector3 (-0.408f,0.299f ,-0.050f);
        target.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position = new Vector3 (-1.142f,0.2f,0.684f);
       

    }

    public void setUpLevel(Vector3 whiteBallPosition, Vector3 targetBallPosition, Vector3 targetPosition)
    {
        whiteBall.resetState(whiteBallPosition);
        targetBall.GetComponent<Transform>().position = targetBallPosition;
        targetBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        targetBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        target.GetComponent<Transform>().position = targetPosition;
        killmomentum();
    }

    public void getLevelData(string fileName)
    {
        string[] text = System.IO.File.ReadAllLines(fileName);
        setUpLevel();
    }

    public void killmomentum()
    {
        this.whiteBall.getRigidbody().velocity = Vector3.zero;
        this.targetBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void resetLevel(int levelNumber)
    {
        setUpLevel();
    }



 
    public WhiteBallClass getWhiteBall() {
		return this.whiteBall;
	}

	public void setWhiteBall(WhiteBallClass whiteBall) {
		this.whiteBall = whiteBall;
	}

	public GameObject getTargetBall() {
		return this.targetBall;
	}

	public void setTargetBall(GameObject targetBall) {
		this.targetBall = targetBall;
	}

	public GameObject getTarget() {
		return this.target;
	}

	public void setTarget(GameObject target) {
		this.target = target;
	}

	public GameObject getCue() {
		return this.cue;
	}

	public void setCue(GameObject cue) {
		this.cue = cue;
	}

	public LazerClass getLazer() {
		return this.lazer;
	}

	public void setLazer(LazerClass lazer) {
		this.lazer = lazer;
	}
}