using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour{
    void Update(){
        if(Input.anyKeyDown){
            // Proper Function
            SceneManager.LoadScene("MainMenu");
            
            // EXPERIMENTAL, delete later
            // SceneManager.LoadScene("Main1-1");
        }
    }
}
