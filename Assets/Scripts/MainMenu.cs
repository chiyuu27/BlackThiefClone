using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu:MonoBehaviour{
    [SerializeField] TextMeshProUGUI m_playerName;

    void Start(){
        m_playerName.text = PlayerData.instance.playerName;
    }

    public void OpenLevelSelection(){
        SceneManager.LoadScene("LevelSelection");
    }
}
