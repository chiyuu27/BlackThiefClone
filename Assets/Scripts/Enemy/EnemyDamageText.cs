using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyDamageText : MonoBehaviour{
    private float fadeTime = 1f;
    [SerializeField] private Vector3 floatSpeed = new Vector3(0, 0.005f);
    private TextMeshPro text;

    private void Awake() {
        text = GetComponent<TextMeshPro>();
    }

    private void Start(){
        gameObject.SetActive(true);
    }

    public void TriggerText(float damage){
        StartCoroutine(FadeText(damage.ToString()));
    }

    private IEnumerator FadeText(string text){
        this.text.text = text;

        // Set fade speed
        Color textColor = this.text.color;
        float fadeSpeed = (textColor.a - 0) / fadeTime;

        while(textColor.a > 0){
            // Color
            textColor.a -= fadeSpeed * Time.deltaTime;
            if(textColor.a < 0){
                textColor.a = 0;
            }

            // Position
            this.text.transform.position += floatSpeed;

            this.text.color = textColor;

            yield return null;
        }

        Die();
        yield return null;
    }

    private void Die(){
        Destroy(gameObject);
    }
}