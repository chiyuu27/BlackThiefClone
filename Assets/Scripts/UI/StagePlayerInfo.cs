using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StagePlayerInfo : MonoBehaviour {
    private static StagePlayerInfo m_instance;
    public static StagePlayerInfo instance{
        get => m_instance;
    }

    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject ammoPanel;
    [SerializeField] private TextMeshProUGUI ammoText;

    public bool isAmmoPanelActive;

    private float currentAmmo;
    private float maxAmmo;

    private CharacterCombat player;
    // [SerializeField] 

    private void Awake(){
        if(m_instance == null){
            m_instance = this;
        }
    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("CharacterOnPlay").GetComponent<CharacterCombat>();
        healthBar.maxValue = player.MaxHealthPoint;
        healthBar.value = player.HealthPoint;

        isAmmoPanelActive = false;
        currentAmmo = 0;
        maxAmmo = 0;
    }

    public void UpdateHealthBar(){
        healthBar.value = player.HealthPoint;
        healthText.text = player.HealthPoint + " / " + player.MaxHealthPoint;
    }

    public void ShowAmmoBar(bool show){
        ammoPanel.SetActive(show);
        ammoText.text = this.currentAmmo + " / " + this.maxAmmo;
        isAmmoPanelActive = show;
    }

    public void ShowAmmoBar(bool show, float currentAmmo, float maxAmmo){
        ammoPanel.SetActive(show);
        this.currentAmmo = currentAmmo;
        this.maxAmmo = maxAmmo;
        ammoText.text = this.currentAmmo + " / " + this.maxAmmo;
        isAmmoPanelActive = show;
    }

    public void UpdateAmmoBar(float currentAmmo){
        this.currentAmmo = currentAmmo;
        ammoText.text = this.currentAmmo + " / " + this.maxAmmo;
    }
}