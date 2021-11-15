using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour{
    private Enemy enemy;

    public bool playerInRange;
    public bool isAttacking;
    [SerializeField] LayerMask attackTarget;

    private void Awake() {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void Start() {
        isAttacking = false;
        playerInRange = false;
    }

    private void Update() {
        if(playerInRange && !isAttacking){
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "CharacterHitbox"){
            playerInRange = true;
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.tag == "CharacterHitbox"){
            playerInRange = false;
        }
    }

    private IEnumerator Attack(){
        isAttacking = true;
        enemy.animator.SetTrigger("Attacking");

        // Wait for animation for a moment
        yield return new WaitForSeconds(0.2f);

        // Checking collider
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, attackTarget);
        foreach(Collider2D obj in hitEnemies){
            if(obj.transform.parent.TryGetComponent<CharacterCombat>(out CharacterCombat component)){
                component.GetHurt(enemy.damage);
            }
        }

        isAttacking = false;

        yield return null;
    }
}