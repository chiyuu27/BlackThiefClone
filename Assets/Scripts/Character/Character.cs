using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour{
    [SerializeField] protected int m_Level;
    [SerializeField] protected float m_MaxHealthPoint;
    [SerializeField] protected float m_MaxSkillPoint;
    [SerializeField] protected float m_Speed;
    [SerializeField] protected LayerMask m_AttackTarget;
    public Transform weaponHold;
    public GameObject contactPoint;

    [SerializeField] protected Animator m_Animator;

    public float speed{
        get{
            return m_Speed;
        }
    }

    public float maxHealthPoint{
        get{
            return m_MaxHealthPoint;
        }
    }

    public float maxSkillPoint{
        get{
            return m_MaxSkillPoint;
        }
    }

    public LayerMask AttackTarget{
        get => m_AttackTarget;
    }

    public Animator animator{
        get{
            return m_Animator;
        }
    }

    private void Awake() {
        
    }
}