using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] private string enemyName;
    [SerializeField] private Transform spawnPoint;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "CharacterHitbox"){
            SpawnEnemy();
            Destroy(gameObject);
        }
    }

    private void SpawnEnemy(){
        Stage.instance.EnemySpawned();

        if(spawnPoint == null){
            spawnPoint = transform;
        }

        GameObject enemy = Instantiate(EnemyManager.instance.enemies[enemyName], spawnPoint.position, Quaternion.identity, gameObject.transform.parent);
        enemy.SetActive(true);
        SpriteRenderer[] bodypart = enemy.GetComponentsInChildren<SpriteRenderer>();
        float randomNumber = Random.Range(1f, 100f);
        foreach(SpriteRenderer part in bodypart){
            part.sortingOrder = Mathf.CeilToInt(randomNumber + part.sortingOrder);
        }
    }
}
