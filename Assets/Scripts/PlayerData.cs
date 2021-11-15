using UnityEngine;

public class PlayerData : MonoBehaviour{
    static PlayerData s_instace;
    public static PlayerData instance{
        get{
            return s_instace;
        }
    }

    public string playerName;
    public string storyProgress;
    public int loginDayCount;
    public int loginConsencutive;

    void Awake(){
        if(s_instace == null){
            s_instace = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

        // Initialize
        playerName = "Tester 01";
    }

    void ResetPlayerData(){
        storyProgress = "1-1";
        loginDayCount = 0;
        loginConsencutive = 0;
    }
}
