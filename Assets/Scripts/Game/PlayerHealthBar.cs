using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private const float DAMAGED_FADE_TIMER_MAX = 1f;
    [SerializeField] private Image foreground;
    [SerializeField] private Image DamagedBar;
    [SerializeField] private Text healthText;
    private float damageFadeTimer;
    private Color damagedColor;
    [SerializeField] private float positionOffset;
    [SerializeField] private float updateSpeedSeconds = 0.2f;
    [SerializeField] private PlayerHealth health;
    public GameObject DebuffSpawn;

    private void Awake()
    {
        damagedColor = DamagedBar.color;
        damagedColor.a = 0f;
        DamagedBar.color = damagedColor;
      
    }
    public void SetHealth(PlayerHealth health)
    {
        this.health = health;
        healthText.text = "" + health.Hp;
        this.health.OnHealthChanged += HandleHealthChanged;
    }
    private void HandleHealthChanged(int Hp, int MaxHealth, int amount, bool isCrit, bool isHeal, Color PopupColor)
    {

        float hpPercent = (float)Hp / (float)MaxHealth;
        Vector3 randomPos = new Vector3(Random.Range(-1.5f, 1.5f), 2.25f, Random.Range(0f, 0.25f));
        DamagePopup.Create(health.transform.position + randomPos, transform.parent, amount, isCrit, isHeal, PopupColor);  
        if ( damagedColor.a <= 0)
        {
            DamagedBar.fillAmount = foreground.fillAmount;
        }

        damagedColor.a = 0.7f;
        DamagedBar.color = damagedColor;
        damageFadeTimer = DAMAGED_FADE_TIMER_MAX;
        healthText.text = "" + Hp;
        StartCoroutine(ChangeToPct(hpPercent));
    }

    private IEnumerator ChangeToPct(float pc)
    {
       
        float preChangePct = foreground.fillAmount;
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foreground.fillAmount = Mathf.Lerp(preChangePct, pc, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foreground.fillAmount = pc;
    }
    private void Update()
    {
      if ( damagedColor.a > 0)
        {
            damageFadeTimer -= Time.deltaTime; 
            if (damageFadeTimer < 0)
            {   
                float fadeAmount = 5f;
                damagedColor.a -= fadeAmount * Time.deltaTime;
                DamagedBar.color = damagedColor;
            }
        }
    }

}
