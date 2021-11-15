using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{
    private static EnemyManager m_Instance;
    public static EnemyManager instance{
        get{
            return m_Instance;
        }
    }

    public Dictionary<string, GameObject> enemies = new Dictionary<string, GameObject>();

    void Awake(){
        if(m_Instance == null){
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject enemy in enemyList){
                enemies.Add(enemy.name, enemy);
                enemy.SetActive(false);
            }

            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
}