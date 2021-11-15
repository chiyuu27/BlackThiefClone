using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Stage : MonoBehaviour{
    private static Stage m_instance;
    public static Stage instance{
        get => m_instance;
    }
    
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public int enemySpawnerLeft;
    public int enemyLeft;

    private CharacterCombat player;
    private bool isStageClear;

    private void Awake(){
        if(m_instance == null){
            m_instance = this;
        }
    }

    private void Start(){
        enemyLeft = 0;
        isStageClear = false;
        enemySpawnerLeft = GameObject.FindGameObjectsWithTag("EnemySpawner").Length;
        player = GameObject.FindGameObjectWithTag("CharacterOnPlay").GetComponent<CharacterCombat>();
    }

    private void Update(){
        if(((enemySpawnerLeft <= 0 && enemyLeft <= 0) || player.HealthPoint <= 0) && !isStageClear){
            isStageClear = true;

            if((enemySpawnerLeft <= 0 && enemyLeft <= 0)){
                StageClear();
            }

            if(player.HealthPoint <= 0){
                StageFailed();
            }

            resultPanel.SetActive(true);
        }
    }

    private void StageClear(){
        resultText.text = "STAGE CLEAR";
    }

    private void StageFailed(){
        resultText.text = "STAGE FAILED";
    }

    public void RestartStage(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToLevelSelection(){
        SceneManager.LoadScene("LevelSelection");
    }

    public void EnemySpawned(){
        enemyLeft++;
        enemySpawnerLeft--;
    }

    public void EnemyDied(){
        enemyLeft--;
    }

}