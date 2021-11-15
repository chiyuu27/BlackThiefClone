using System;
using UnityEngine;


public class Weapon : MonoBehaviour {
    [Serializable]
    public enum WeaponType{
        Melee,
        Gun,
    }
}