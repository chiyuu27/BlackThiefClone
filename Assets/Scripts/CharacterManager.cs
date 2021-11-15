using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour{
    private static CharacterManager m_Instance;
    public static CharacterManager instace{
        get{
            return m_Instance;
        }
    }

    public List<GameObject> characters = new List<GameObject>();
    public GameObject selectedCharacter;

    void Awake() {
        if(m_Instance == null){
            GameObject[] result = GameObject.FindGameObjectsWithTag("Character");
            selectedCharacter = result[0];
            selectedCharacter.tag = "CharacterSelected";

            foreach(GameObject obj in result){
                characters.Add(obj);
                obj.SetActive(false);
            }

            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

    }
}
