using UnityEngine;

public class CharacterSpawner : MonoBehaviour{
    void Awake(){
        GameObject obj = Instantiate(CharacterManager.instace.selectedCharacter, transform.position, transform.rotation);
        obj.SetActive(true);
        obj.tag = "CharacterOnPlay";

        Destroy(gameObject);
    }
}
