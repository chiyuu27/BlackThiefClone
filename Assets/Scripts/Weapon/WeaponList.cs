using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour{
    private static WeaponList s_Instance;
    public static WeaponList instance{
        get{
            return s_Instance;
        }
    }

    [SerializeField] public Dictionary<string, GameObject> weapons = new Dictionary<string, GameObject>();
    List<GameObject> fuckingWeapon;

    void Awake(){
        if(s_Instance == null){
            s_Instance = this;
            DontDestroyOnLoad(gameObject);

            GameObject[] weaponObjs = GameObject.FindGameObjectsWithTag("PlayerWeapon");

            foreach(GameObject obj in weaponObjs){
                weapons.Add(obj.name, obj);
                obj.SetActive(false);
            }
        }else{
            Destroy(gameObject);
        }
    }


}