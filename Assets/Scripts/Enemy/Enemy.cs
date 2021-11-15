using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float healthPoint;
    [SerializeField] private float speed;
    [SerializeField] public float damage;
    
    public Animator animator;
    public Transform damageTextSpawnPoint;
    public GameObject damageText;
    [HideInInspector] public GameObject target;
    private CharacterCombat targetData;
    
    private SpriteRenderer[] spriteParts;
    private EnemyAttack attackTrigger;

    private bool lookDirection;
    private string currentLayer;
    private bool playerInAttackRange;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("CharacterOnPlay");
        targetData = target.GetComponent<CharacterCombat>();
        spriteParts = GetComponentsInChildren<SpriteRenderer>();
        attackTrigger = GetComponentInChildren<EnemyAttack>();

        lookDirection = false;

        if(transform.position.y > target.transform.position.y){
            currentLayer = "BehindPlayer";
            SpriteChangeLayer(false);
        }else{
            currentLayer = "FrontOfPlayer";
            SpriteChangeLayer(true);
        }
        playerInAttackRange = false;
    }

    void Update(){
        if(targetData.isAlive){
            if(!attackTrigger.playerInRange){
                ChasePlayer();
            }
            CheckLayerPosition();
        }
    }

    public void GetDamage(float damage, float knockback=0){
        healthPoint -= damage;
        GameObject damageText = Instantiate(this.damageText, damageTextSpawnPoint.position, Quaternion.identity);
        damageText.GetComponent<EnemyDamageText>().TriggerText(damage);

        if(knockback > 0){        
            Vector3 knock = new Vector3(knockback, 0f);

            if(lookDirection){
                transform.position -= knock;
            }else{
                transform.position += knock;
            }
        }

        if(healthPoint <= 0){
            Die();
        }
    }

    private void ChasePlayer(){
        bool targetOnRight = true;

        // Set look direction
        if(transform.position.x > target.transform.position.x){
            targetOnRight = false;
        }
        if((lookDirection && !targetOnRight) || (!lookDirection && targetOnRight)){
            SpriteFlip();
        }

        // Move Character
        Vector3 velocity = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y).normalized;
        transform.position += velocity * speed;
    }

    private void CheckLayerPosition(){
        bool targetOnFront = true;

        // Set layer
        if(transform.position.y > target.transform.position.y){
            targetOnFront = false;
        }
        if((currentLayer == "BehindPlayer" && targetOnFront) || (currentLayer == "FrontOfPlayer" && !targetOnFront)){
            SpriteChangeLayer(targetOnFront);
        }
    }


    private void Die(){
        Stage.instance.EnemyDied();
        Destroy(gameObject);
    }

    private void SpriteFlip(){
        lookDirection = !lookDirection;
        transform.Rotate(0f, 180f, 0f);
    }

    private void SpriteChangeLayer(bool isTargetFront){
        if(isTargetFront){
            currentLayer = "FrontOfPlayer";
        }else{
            currentLayer = "BehindPlayer";
        }

        foreach(SpriteRenderer obj in spriteParts){
            obj.sortingLayerName = currentLayer;
        }
    }
}