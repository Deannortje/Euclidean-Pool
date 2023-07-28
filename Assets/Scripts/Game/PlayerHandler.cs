using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using TMPro;

    

public class PlayerHandler : MonoBehaviour
{
    private Vector3 mousePos;
    private GameCreator gameCreator;
    private LazerClass newLazer;
    private GhostBallClass newGhostBall;
    private CircleLazerClass newCircleLazer;
    private CirclePointClass newCirclePoint;
    //private dragablesGeneralScript dragable;
    private Vector3 ghostBallPos;

    enum Stages {
        stage1,
        stage2,
        stage3,
        stage4,
        stage5
    }
    
    [SerializeField] Stages st;

    enum GameStates {
        none,
        defaultState,
        placingLazerState1,
        placingLazerState2,
        placingGhostBall,
        placingCircleLazer1,
        placingCircleLazer2,
        showingGiven
    }

    [SerializeField] GameStates gs;
    [SerializeField] GameObject lazerIcon;
    [SerializeField] GameObject circleLazerIcon;
    [SerializeField] float radiusChange;
  
    [SerializeField] GameObject ghostIcon;
    [SerializeField] float powwow;
    [SerializeField] Vector3 velocityOfWhite;

    [SerializeField] GameObject ghostButton;
    [SerializeField] GameObject lazerButton;
    [SerializeField] GameObject inventoryLazerSlot;
    [SerializeField] GameObject inventoryGhostBallSlot;
    [SerializeField] GameObject inventoryCircleLazerSlot;
    [SerializeField] GameObject circleLazerButton;
    [SerializeField] GameObject Lables;
    [SerializeField] GameObject answerDrop;
    [SerializeField] GameObject answerDropObj;
    [SerializeField] Slider slider;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject answerText;
    [SerializeField] GameObject lables;
    [SerializeField] GameObject dropDowns;
    [SerializeField] GameObject scoretext;
    [SerializeField] GameObject inventory;
    

    private float numberOflazers;
    private float numberOfGhostBalls;
    private float numberOfCircleLazers;
    
    public List<LazerClass> lazerObjects;
    public List<GhostBallClass> ghostObjects;
    public List<CircleLazerClass> circleLazerObjects;
    public List<CirclePointClass> circlePointObjects;

    public void Start()
    {
        gs = GameStates.defaultState;
        gameCreator = new GameCreator(1);
        st = Stages.stage1;
        numberOflazers = 2;
        numberOfGhostBalls = 1;
        numberOfCircleLazers = 0;
        Lables.SetActive(false);
        canvas.SetActive(false);
        inventory.SetActive(true);
        answerDrop.GetComponent<Renderer>().enabled = false;

        lazerObjects = new List<LazerClass>();
        ghostObjects = new List<GhostBallClass>();
        circleLazerObjects = new List<CircleLazerClass>();
        circlePointObjects = new List<CirclePointClass>();
    }

    public void resetState(int level)
    {
        gs = GameStates.defaultState;
        gameCreator.resetLevel(level);
        st = Stages.stage1;
        numberOflazers = 2;
        numberOfGhostBalls = 1;
        numberOfCircleLazers = 0;
        Lables.SetActive(false);
        canvas.SetActive(false);
        inventory.SetActive(true);
        answerDrop.GetComponent<Renderer>().enabled = false;
        foreach (LazerClass lazers in lazerObjects) {
            lazers.DeleteLazer();
        }
        foreach (GhostBallClass ghostObj in ghostObjects) {
            ghostObj.DeleteGhostBall();
        }
        foreach (CircleLazerClass circleObj in circleLazerObjects) {
            circleObj.DeleteCircleLazer();
        }
        foreach (CirclePointClass circlePointObj in circlePointObjects) {
            circlePointObj.DeleteCirclePoint();
        }
    }
    
    public void Update()
    {
        // Cue.UpdateCuePosition(Ball.getTransform(), 0.55f);
        //click specific button in UI 
        if(numberOflazers < 1)
        {
            lazerButton.GetComponent<Button>().interactable = false;
            inventoryLazerSlot.SetActive(false);
        }
        else
        {
            lazerButton.GetComponent<Button>().interactable = true;
            inventoryLazerSlot.SetActive(true);
        }

        if(numberOfGhostBalls < 1)
        {
            ghostButton.GetComponent<Button>().interactable = false;
            inventoryGhostBallSlot.SetActive(false);
        }
        else
        {
            ghostButton.GetComponent<Button>().interactable = true;
            inventoryGhostBallSlot.SetActive(true);
        }
        if(numberOfCircleLazers < 1)
        {
            circleLazerButton.GetComponent<Button>().interactable = false;
            inventoryCircleLazerSlot.SetActive(false);
        }
        else
        {
            circleLazerButton.GetComponent<Button>().interactable = true;
            inventoryCircleLazerSlot.SetActive(true);
        }


        if(numberOflazers < 1 && numberOfGhostBalls < 1 && st == Stages.stage1)
        {
            
            st = Stages.stage2; // move to stage 2
            numberOfCircleLazers = 1;
        }
        if(numberOfCircleLazers < 1 && st == Stages.stage2)
        {
            st = Stages.stage3; // move to stage 3
            numberOflazers = 3;
        }
        if(numberOflazers < 1 && st == Stages.stage3)
        {
            st = Stages.stage4; // move to stage 4
            
        }
        
        if(numberOflazers < 1 && numberOfGhostBalls < 1 && numberOfCircleLazers < 1)
        {
            gs = GameStates.showingGiven;
        }

        switch(gs)
        {
            case GameStates.none:
                break;
            case GameStates.defaultState:
                //TEMPORARY FOR SHOOTING
                // if(Input.GetKeyDown(KeyCode.C))
                // {
                //     gameCreator.getWhiteBall().setVelocity(velocityOfWhite);
                // }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    //Debug.Log(gameCreator.getTarget().transform.position);
                    //Debug.Log(gameCreator.getTargetBall().transform.position);
                    //Debug.Log(gameCreator.getWhiteBall().getTransform().position);
                    shootBallAt(calculateFinalAngle(gameCreator.getTarget().transform.position,gameCreator.getTargetBall().transform.position,gameCreator.getWhiteBall().getTransform().position));
                    //shootBallAt(315);
                    Debug.Log(-1*(-270 + calculateFinalAngle(gameCreator.getTarget().transform.position,gameCreator.getTargetBall().transform.position,gameCreator.getWhiteBall().getTransform().position)));
                }
                break;
            case GameStates.placingLazerState1:
                lazerState1();
                break;
            case GameStates.placingLazerState2:
                lazerState2();
                break;
            case GameStates.placingGhostBall:
                GhostBallState();
                break;
            case GameStates.placingCircleLazer1:
                CircleLazerState1();
                break;
            case GameStates.showingGiven:
                showGiven();
                setPlaceAnswer();
                break;
            
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            resetState(1);
        }
        
    }

    public Vector3 invertVector(Vector3 vec1)
    {
        return new Vector3(vec1.x * -1, vec1.y * -1, vec1.z * -1);
    }

    public float calculateFinalAngle(Vector3 vec1, Vector3 vec2, Vector3 vec3)
    {
        Vector3 ghostball = vec1-vec2;
        //rb = GetComponent<Rigidbody>();
        ghostball = invertVector(ghostball);
        ghostball = ghostball.normalized;
        ghostball = ghostball*radiusChange;
        //Debug.Log(Vector3.Angle(ghostball, Vector3.forward));
        ghostball = vec2+ghostball;
        //Debug.Log(ghostball);
        //Debug.Log(ghostball.x +" AND "+ ghostball.z);
        //fist we want tyo get the ghost ball position we do this by - targetball to target vector add double, invert then normalise *0.06 
        // ghostball = targetball + ans 
        //
        ghostBallPos = ghostball;
        Vector3 shotVector = vec3 - ghostball;//this is white - ghost 

        //x and z
        // if x 
        //Debug.Log(shotVector);
        //Debug.Log(90 - Vector3.Angle(shotVector, Vector3.forward));
        return Vector3.Angle(shotVector, Vector3.forward) + 180;
        // we want to find angle that white ball must shoot at
        //vector from whiteball to ghostball
        //get aangle from thaat vector 
    }

    public void shootBallAt(float degree)
    {
        //fist we want tyo get the ghost ball position we do this by - targetball to target vector add double, invert then normalise *0.06 
        // ghostball = targetball + ans 
        // we want to find angle that white ball must shoot at
        //vector from whiteball to ghostball
        //get aangle from thaat vector 

        Vector3 vectorfromAngle = new Vector3(Mathf.Sin(degree * Mathf.Deg2Rad), 0, Mathf.Cos(degree * Mathf.Deg2Rad));
        gameCreator.getWhiteBall().getRigidbody().AddForce(vectorfromAngle * powwow);
    }

    public void lazerState1()
    {
        
        
        lazerIcon.transform.position = new Vector3(lazerIcon.transform.position.x, 0, lazerIcon.transform.position.z);

        lazerIcon.GetComponent<Renderer>().enabled = true;

       
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            lazerIcon.GetComponent<Renderer>().enabled = false;
            gs = GameStates.defaultState;
        }

        if(Input.GetMouseButtonDown(0))// if click and on snap point 
        {

            mousePos = mousetoWorldPos();
            
            int layerMask = 1 << 8;

            layerMask = ~layerMask;

            RaycastHit hit;
            Ray ray = cameraToObject(lazerIcon.transform.position + lazerIcon.GetComponent<SphereCollider>().center*0.25f);
          
     
            if (Physics.Raycast(ray, out hit,100,layerMask, QueryTriggerInteraction.Collide))
            {
            
                if(lazerIcon.GetComponent<dragablesGeneralScript>().dragCollided)
                {
                  
                    newLazer = new LazerClass(lazerIcon.transform.position,hit.transform.position,Color.blue); // cant be mousePos, use mousepos to get the snappoint and its center.
                    lazerObjects.Add(newLazer);
                    //Debug.Log("hit snap point");
                    gs = GameStates.placingLazerState2;
                    
                }
            }
        }
    }

    public void lazerState2()
    {

        newLazer.AlterLazer(lazerIcon.transform.position);

        if(Input.GetMouseButtonDown(0)) 
        {
            if(lazerIcon.GetComponent<dragablesGeneralScript>().dragCollided)
                {
                gs = GameStates.defaultState;
                lazerIcon.GetComponent<Renderer>().enabled = false;
                numberOflazers--;
                }
        }

         if(Input.GetKeyDown(KeyCode.Escape))
        {
            gs = GameStates.placingLazerState1;
        }
    }

    public void GhostBallState()
    {
        float temp = calculateFinalAngle(gameCreator.getTarget().transform.position,gameCreator.getTargetBall().transform.position,gameCreator.getWhiteBall().getTransform().position);
            
        ghostIcon.transform.position = new Vector3(ghostIcon.transform.position.x, 0, ghostIcon.transform.position.z);

        ghostIcon.GetComponent<Renderer>().enabled = true;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gs = GameStates.defaultState;
        }

        if(Input.GetMouseButtonDown(0))
        {
            
            mousePos = mousetoWorldPos();
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;
            Ray ray = cameraToObject(ghostIcon.transform.position + ghostIcon.GetComponent<SphereCollider>().center*0.25f);
            if (Physics.Raycast(ray, out hit,100,layerMask, QueryTriggerInteraction.Collide))
            {
                if(ghostIcon.GetComponent<dragablesGeneralScript>().dragCollided && ghostIcon.GetComponent<dragablesGeneralScript>().tag == "targetBall" )
                { 

                    newGhostBall = new GhostBallClass(ghostBallPos);//set this to the correct position of the ghost ball
                    ghostObjects.Add(newGhostBall);
                    gs = GameStates.defaultState;
                    ghostIcon.GetComponent<Renderer>().enabled = false;
                    numberOfGhostBalls--;
                }
            }
        }
    }

    public void CircleLazerState1()
    {
        float temp = calculateFinalAngle(gameCreator.getTarget().transform.position,gameCreator.getTargetBall().transform.position,gameCreator.getWhiteBall().getTransform().position);
            
        circleLazerIcon.transform.position = new Vector3(circleLazerIcon.transform.position.x, 0, circleLazerIcon.transform.position.z);

        circleLazerIcon.GetComponent<Renderer>().enabled = true;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gs = GameStates.defaultState;
        }
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = mousetoWorldPos();
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;  
            Ray ray = cameraToObject(circleLazerIcon.transform.position + circleLazerIcon.GetComponent<SphereCollider>().center*0.25f);

            if (Physics.Raycast(ray, out hit,100,layerMask, QueryTriggerInteraction.Collide))
            {

                if(circleLazerIcon.GetComponent<dragablesGeneralScript>().dragCollided && circleLazerIcon.GetComponent<dragablesGeneralScript>().tag == "targetBall")
                {
                    float distance = Vector3.Distance(ghostBallPos,gameCreator.getWhiteBall().getTransform().position);
                    newCircleLazer = new CircleLazerClass(ghostBallPos, distance);
                    circleLazerIcon.GetComponent<Renderer>().enabled = false;
                    gs = GameStates.defaultState;
                    newCirclePoint = new CirclePointClass(ghostBallPos,gameCreator.getWhiteBall().getTransform().position,gameCreator.getTarget().transform.position,"top");
                    circlePointObjects.Add(newCirclePoint);
                    newCirclePoint = new CirclePointClass(ghostBallPos,gameCreator.getWhiteBall().getTransform().position,gameCreator.getTarget().transform.position, "targetBallLine");
                    circlePointObjects.Add(newCirclePoint);
                    Vector3 vec = new Vector3(ghostBallPos.x + distance,0,ghostBallPos.z);
                    Vector3 vec2 = new Vector3(ghostBallPos.x,0,ghostBallPos.z);
                    newLazer = new LazerClass(vec2,vec,Color.green); // cant be mousePos, use mousepos to get the snappoint and its center.
                    lazerObjects.Add(newLazer);
                    circleLazerObjects.Add(newCircleLazer);
                    
                    
                    numberOfCircleLazers--;
                    Lables.SetActive(true);
                    
                }
            }
        }   
    }

    public void LazerButtonHandler()
    {
        if(gs == GameStates.defaultState)
        {
            gs = GameStates.placingLazerState1;
        }
    }

    public void GhostBallButtonHandler()
    {
        if(gs == GameStates.defaultState)
        {
            gs = GameStates.placingGhostBall;
        }
    }

    public void CircleLazerButtonHandler()
    {
        if(gs == GameStates.defaultState)
        {
            gs = GameStates.placingCircleLazer1;
        }
    }

    public void AnswerHandler()
    {
        //Debug.Log("Entering answer handler");
        //GameObject answer = (Resources.Load("Prefabs/AnswerDrop") as GameObject);
        answerDrop.GetComponent<TMP_Text>().text = (slider.value).ToString();
        //inputField.text = (slider.value).ToString();
        //var text = inputField.GetComponent<TMP_InputField>().text;

        answerDrop.GetComponent<Renderer>().enabled = true;
    }

    public void setPlaceAnswer(){
        //WHEN COLLIDED WITH COLLIDER THEN CHANGE VALUE
        if(Input.GetMouseButtonDown(0))
        {
            //HERE
            
            mousePos = mousetoWorldPos();
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;
             
            Ray ray = cameraToObject(answerDropObj.transform.position + answerDropObj.GetComponent<SphereCollider>().center*0.5f);
            
            if (Physics.Raycast(ray, out hit,100,layerMask, QueryTriggerInteraction.Collide))
            {
                //Debug.Log(answerDropObj.GetComponent<dragablesGeneralScript>().dragCollided);
                //Debug.Log(answerDropObj.GetComponent<dragablesGeneralScript>().tag);
                if(answerDropObj.GetComponent<dragablesGeneralScript>().dragCollided && (
                answerDropObj.GetComponent<dragablesGeneralScript>().tag == "LabelA" ||
                answerDropObj.GetComponent<dragablesGeneralScript>().tag == "LabelO1" ||
                answerDropObj.GetComponent<dragablesGeneralScript>().tag == "LabelO2" ||
                answerDropObj.GetComponent<dragablesGeneralScript>().tag == "LabelO3" ||
                answerDropObj.GetComponent<dragablesGeneralScript>().tag == "LabelX" ||
                answerDropObj.GetComponent<dragablesGeneralScript>().tag == "LabelB (1)")) 
                { 
                    //Debug.Log("THERE");
                    //if collided change value
                    GameObject newLabel = lables.transform.Find(answerDropObj.GetComponent<dragablesGeneralScript>().tag).gameObject;
                    newLabel.GetComponent<TMP_Text>().text = answerDrop.GetComponent<TMP_Text>().text;
                }
            }
        }
    }

    private void showGiven()
    {
        inventory.SetActive(false);
       canvas.SetActive(true);
        // B1 = 54°  
        // (<s of isos Triangle)

        // O3 = 72°
        // (<s of triangle)

        // O2 = 72 + 23 = 95°

        // O1 = 360 - (95 + 72 + x) = 184°
        // X = 360 - (184° + 95 + 72) = 9°
        // (<s around a point)

        // X = 9°
        try{

        
        answerText.transform.Find("B1").gameObject.GetComponent<TMP_Text>().text = lables.transform.Find("LabelB (1)").gameObject.GetComponent<TMP_Text>().text ;
        answerText.transform.Find("O3").gameObject.GetComponent<TMP_Text>().text = lables.transform.Find("LabelO3").gameObject.GetComponent<TMP_Text>().text ;

        var O2ans = lables.transform.Find("LabelO2").gameObject.GetComponent<TMP_Text>().text;
        var O3ans = answerText.transform.Find("O3").gameObject.GetComponent<TMP_Text>().text;
        var O1ans = lables.transform.Find("LabelO1").gameObject.GetComponent<TMP_Text>().text;

        if(O1ans != "O1")
        {
            answerText.transform.Find("O1").gameObject.GetComponent<TMP_Text>().text = lables.transform.Find("LabelO1").gameObject.GetComponent<TMP_Text>().text;
        }
        else
        {
            O1ans = "184";
            answerText.transform.Find("O1").gameObject.GetComponent<TMP_Text>().text = O1ans +"°";
        }

        var Xans = lables.transform.Find("LabelX").gameObject.GetComponent<TMP_Text>().text;

        answerText.transform.Find("O2 (1)").gameObject.GetComponent<TMP_Text>().text = O2ans;
        answerText.transform.Find("X (1)").gameObject.GetComponent<TMP_Text>().text = Xans;
        answerText.transform.Find("O2").gameObject.GetComponent<TMP_Text>().text = O3ans + "° + 23° = ";

        if(O3ans != "O3")
        {
            var O2Calc = float.Parse(O3ans) + 23;
            var O2 = float.Parse(O2ans);
            answerText.transform.Find("O2").gameObject.GetComponent<TMP_Text>().text = O3ans + "° + 23° = " + O2Calc.ToString();

            
            if(O2ans != "O2")
            {
                if(O1ans != "O1" && lables.transform.Find("LabelO1").gameObject.GetComponent<TMP_Text>().text != "O1")
                {   
                    answerText.transform.Find("O1").gameObject.GetComponent<TMP_Text>().text = "360° - (" + O2ans + "° + " + O3ans + "° + x°) \n = 184°";
                    var X = 360 - (184 + O2 + float.Parse(O3ans));
                    answerText.transform.Find("X").gameObject.GetComponent<TMP_Text>().text = "360° - (184° + " + O2ans + "° + " + O3ans + "°) \n = " + X.ToString() + "°";
                }
                // X = 360 - (184° + 95 + 72) = 9°
            }
        }
        }
        catch (System.Exception e)
        {
           
        }
        // O1 = 360 - (95 + 72 + x) = 184°
    }

    public void submitButton()
    {
        try{
        var angle = float.Parse(answerText.transform.Find("X (1)").gameObject.GetComponent<TMP_Text>().text); 
          
        shootBallAt(270 - angle);

        var score = 0;
        var scoreReasoning = 0;
        //dropDowns
        if(answerText.transform.Find("B1").gameObject.GetComponent<TMP_Text>().text == "54")
        {
            score++;
        }
        if(answerText.transform.Find("O3").gameObject.GetComponent<TMP_Text>().text == "72")
        {
            score++;
        }
        if(answerText.transform.Find("O2 (1)").gameObject.GetComponent<TMP_Text>().text == "95")
        {
            score++;
        }
        if(angle == 9)
        {
            score++;
            Debug.Log("Correct");
        }

        if(dropDowns.transform.Find("Dropdown (1)").gameObject.GetComponent<TMP_Dropdown>().value == 5)
        {
            scoreReasoning++;
        }
        if(dropDowns.transform.Find("Dropdown (2)").gameObject.GetComponent<TMP_Dropdown>().value == 7)
        {
            scoreReasoning++;
        }
        if(dropDowns.transform.Find("Dropdown (3)").gameObject.GetComponent<TMP_Dropdown>().value == 1)
        {
            scoreReasoning++;
        }
        if(dropDowns.transform.Find("Dropdown (4)").gameObject.GetComponent<TMP_Dropdown>().value == 2)
        {
            scoreReasoning++;
        }
        if(dropDowns.transform.Find("Dropdown (5)").gameObject.GetComponent<TMP_Dropdown>().value == 2)
        {
            scoreReasoning++;
        }
        int final = score + scoreReasoning;
        Debug.Log(score);
        Debug.Log(scoreReasoning);
        Debug.Log("Score: " + final);
        scoretext.GetComponent<TMP_Text>().text =  final.ToString() + "/9";
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }








       
    }

     public void Restart()
    {
   
        resetState(1);
                    
    }

    private Vector3 mousetoWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
   
    private Ray cameraToObject(Vector3 objectPos){
        Vector3 camPos = new Vector3(0,2,0);
        Ray ray = new Ray(camPos, ((objectPos-camPos)));

        return ray;
    }

  /// Place all values in correct position
  /// Make drop downs in correct position for the answer
  /// Add positions of angle values
  /// Make angle values change on drag
  /// Make O1 and X do math 
  /// Clean up after
  /// Try impliment shooting for placing values

// Given

// A1 = 55°
// O1 = 184° + x
// O2 = O3 + 25°

// Correct Answer

// B2 = 55°  
// (<s of isos Triangle)

// O3 = 70°
// (<s of triangle)

// O2 = (95°)

// O1 = 184°-(70°+95°)+x=360° 
// (<s around a point)

// X = 360° - 184° 
// (<s around a point)

// X = 11°

// Given

// A1 = 54°
// O1 = 184° + x
// O2 = O3 + 23°

// Correct Answer

// B1 = 54°  
// (<s of isos Triangle)

// O3 = 72°
// (<s of triangle)

// O2 = 72 + 23 = 95°

// O1 = 360 - (95 + 72 + x) = 184°
// X = 360 - (184° + 95 + 72) = 9°
// (<s around a point)

// X = 9°

}