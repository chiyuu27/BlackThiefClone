using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour{
    private class WeaponSpecify{
        public GameObject obj;
        public string type;

        public WeaponSpecify(GameObject obj, GameObject contactPoint){
            this.obj = obj;

            if(obj.TryGetComponent<Gun>(out Gun gunComponent)){
                type = gunComponent.Type;
            }else if(obj.TryGetComponent<Melee>(out Melee meleeComponent)){
                type = meleeComponent.Type;
                meleeComponent.contactPoint = contactPoint;
            }else{
                type = "Unknown";
            }
        }
    }

    private Character character;
    private Animator animator;
    [SerializeField] private Collider2D hitbox;
    private List<WeaponSpecify> weapons = new List<WeaponSpecify>();
    private int currentWeaponIndex;
    private string currentWeaponType;

    private float maxHealthPoint;
    private float healthPoint;
    private float skillPoint;

    [HideInInspector] public bool isAlive;
    private bool isSwitchWeapon;
    private bool isAttack;
    private bool isDodge;

    public float HealthPoint{
        get => healthPoint;
    }

    public float MaxHealthPoint{
        get => maxHealthPoint;
    }

    void Awake() {
        character = GetComponent<Character>();
        animator = character.animator;
        maxHealthPoint = character.maxHealthPoint;
        healthPoint = character.maxHealthPoint;
        skillPoint = character.maxSkillPoint;

        isAlive = true;
        isSwitchWeapon = false;
        isAttack = false;
        isDodge = false;

        // Add initial weapon
        List<GameObject> weaponLoad = new List<GameObject>();

        weaponLoad.Add(WeaponList.instance.weapons["Pan"]);
        weaponLoad.Add(WeaponList.instance.weapons["Glock"]);
        currentWeaponIndex = 0;

        // Call weapon to holdings
        for(int i=0; i<weaponLoad.Count; i++){
            weapons.Add(new WeaponSpecify(Instantiate(weaponLoad[i], character.weaponHold), character.contactPoint));

        }

        // Call weapon
        ChangeWeapon(0);
    }

    void Start(){
        // Info
        animator.SetTrigger("weaponChange");
        animator.SetBool("weaponMelee", true);
    }

    void Update(){
        isSwitchWeapon = false;
        isAttack = false;
        
        if(Input.GetKeyDown(KeyCode.F)){
            ChangeWeapon(++currentWeaponIndex);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            Dodge();
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            GetHurt(5);
        }

    }

    private void ChangeWeapon(int weaponIndex=0){
        if(weaponIndex >= weapons.Count){
            weaponIndex = 0;
        }
        currentWeaponIndex = weaponIndex;

        isSwitchWeapon = true;
        animator.SetTrigger("weaponChange");
        // Testing below, please find another elegant way
        animator.SetBool("weaponMelee", false);
        animator.SetBool("weaponGun", false);

        // Hide non-selected weapon
        // Show selected weapon and play it's draw animation
        for(int i=0; i<weapons.Count; i++){
            if(i == currentWeaponIndex){
                // Check weapon type and play it's animation
                switch(weapons[i].type){
                    case "Gun":
                        if(weapons[i].obj.TryGetComponent<Gun>(out Gun gComponent)){
                            gComponent.TriggerUpdateAmmoBar();
                        }

                        character.contactPoint.SetActive(false);
                        break;
                    case "Melee":
                        if(weapons[i].obj.TryGetComponent<Melee>(out Melee mComponent)){
                            mComponent.TriggerHideAmmoBar();
                        }

                        character.contactPoint.SetActive(true);
                        break;
                }
                currentWeaponType = weapons[i].type;

                weapons[i].obj.SetActive(true);
                animator.SetBool("weapon"+currentWeaponType, true);
            }else{
                weapons[i].obj.SetActive(false);
            }
        }
    }

    private void Attack(){
        isAttack = true;

        switch(currentWeaponType){
            case "Melee":
                weapons[currentWeaponIndex].obj.GetComponent<Melee>().Hit();
                animator.SetTrigger("weaponMeleeAttack");
                break;
            case "Gun":
                weapons[currentWeaponIndex].obj.GetComponent<Gun>().Shoot();
                // No attack animation so not going to do anything with animator
                break;
        }
    }

    private void Dodge(){
        isDodge = true;
    }

    public void GetHurt(float damage){
        healthPoint -= damage;
        
        // Update healthbar
        StagePlayerInfo.instance.UpdateHealthBar();

        if(healthPoint <= 0){
            Defeated();
        }
        animator.SetTrigger("receiveDamage");
    }

    private void Defeated(){
        isAlive = false;
        gameObject.SetActive(false);
    }
}
