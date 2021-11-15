using System.Collections;
using UnityEngine;

public class Gun : Weapon{
    [SerializeField] private string m_name;
    [SerializeField] private WeaponType m_WeaponType;
    [SerializeField] private int m_Level;
    [SerializeField] private float m_Damage;
    [SerializeField] private float m_AttackSpeed;
    [SerializeField] private int m_MaxAmmo;
    [SerializeField] private int m_SupplyAmmo;
    private int currentAmmo;

    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private LineRenderer bullet;
    
    [SerializeField] private LayerMask hitTarget;

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
    public int MaxAmmo{
        get => m_MaxAmmo;
    }
    public int SupplyAmmo{
        get => m_SupplyAmmo;
    }

    public Transform FirePoint{
        get => m_FirePoint;
    }

    void Start(){
        currentAmmo = MaxAmmo;
        TriggerUpdateAmmoBar();
    }

    public void Shoot(){
        if(currentAmmo > 0){
            RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.right, 1000f, hitTarget);
            
            if(hitInfo){
                if(hitInfo.transform.parent.TryGetComponent<Enemy>(out Enemy enemy)){
                    StartCoroutine(DrawBulletLine(FirePoint.position, new Vector3(hitInfo.transform.position.x, FirePoint.position.y)));
                    enemy.GetDamage(Damage, 0.5f);
                }
            }else{
                StartCoroutine(DrawBulletLine(FirePoint.position, FirePoint.position + FirePoint.right * 100));
            }

            currentAmmo--;
            TriggerUpdateAmmoBar();
        }
    }

    private IEnumerator DrawBulletLine(Vector3 startLine, Vector3 endLine){
        LineRenderer tempBullet = Instantiate(bullet).GetComponent<LineRenderer>();
        
        tempBullet.enabled = true;

        tempBullet.SetPosition(0, startLine);
        tempBullet.SetPosition(1, endLine);

        yield return new WaitForSeconds(0.02f);

        tempBullet.enabled = false;
        yield return null;
    }

    public void FillAmmo(){
        currentAmmo += SupplyAmmo;
    }

    public void TriggerUpdateAmmoBar(){
        if(!StagePlayerInfo.instance.isAmmoPanelActive){
            StagePlayerInfo.instance.ShowAmmoBar(true, currentAmmo, MaxAmmo);
        }

        StagePlayerInfo.instance.UpdateAmmoBar(currentAmmo);
    }
}
