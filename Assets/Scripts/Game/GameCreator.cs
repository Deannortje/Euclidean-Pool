using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCreator: MonoBehaviour // want to create game positioning etc via a text file 
{
    public LevelClass level;

    public GameCreator(int levelNumber)
    {   
        //this.level = new LevelClass(levelNumber);
        this.level = new LevelClass();
    }

    public void resetLevel(int levelNumber)
    {
        this.level.resetLevel(levelNumber);
    }

    public WhiteBallClass getWhiteBall()
    {
        return this.level.getWhiteBall();
    }

    public GameObject getTargetBall()
    {
        return this.level.getTargetBall();
    }

    public GameObject getTarget()
    {
        return this.level.getTarget();
    }

    

}