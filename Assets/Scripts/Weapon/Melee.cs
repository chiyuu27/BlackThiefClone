using UnityEngine;

public class Melee : Weapon{
    [SerializeField] private string m_name;
    [SerializeField] private WeaponType m_WeaponType;
    [SerializeField] private int m_Level;
    [SerializeField] private float m_Damage;
    [SerializeField] private float m_AttackSpeed;
    [SerializeField] private LayerMask hitTarget;

    public GameObject contactPoint;

    public string Name{
        get => m_name;
    }
    public string Type{
        get{
            return m_WeaponType.ToString();
        }
    }
    public int Level{
        get => m_Level;
        set => m_Level = value;
    }
    public float Damage{
        get => m_Damage;
    }
    public float AttackSpeed{
        get => m_AttackSpeed;
    }

    public void Hit(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(contactPoint.transform.position, 0.8f, hitTarget);

        foreach(Collider2D obj in hitEnemies){
            if(obj.transform.parent.TryGetComponent<Enemy>(out Enemy enemy)){
                enemy.GetDamage(Damage, 0.5f);
            }
        }
    }

    public void TriggerHideAmmoBar(){
        StagePlayerInfo.instance.ShowAmmoBar(false);
    }
}
