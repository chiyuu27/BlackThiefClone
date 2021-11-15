using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour{
    public void OpenMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void PlayStage(string stageName){
        SceneManager.LoadScene(stageName);
    }
}
