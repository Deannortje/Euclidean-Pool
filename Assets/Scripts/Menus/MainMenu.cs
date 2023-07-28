using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] Object gameScene;
   public void PlayGame()
   {
       //SceneManager.LoadScene(gameScene.name);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void QuitGame()
   {
       Debug.Log("Quit");
       Application.Quit();
   }
}
