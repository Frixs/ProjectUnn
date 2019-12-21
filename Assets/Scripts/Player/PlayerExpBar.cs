using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : MonoBehaviour
{
    private const float DAMAGED_FADE_TIMER_MAX = 1f;
    [SerializeField] private Image foreground;
    [SerializeField] private Image DamagedBar;
    [SerializeField] private Text healthText;
    private float damageFadeTimer;
    private Color damagedColor;
    [SerializeField] private float positionOffset;
    [SerializeField] private float updateSpeedSeconds = 0.2f;
    public GameObject DebuffSpawn;

    private void Awake()
    {
        damagedColor = DamagedBar.color;
        damagedColor.a = 0f;
        DamagedBar.color = damagedColor;
        
    }
 
    public void HandleExpChanged(int Exp, int MaxExp, int amount, bool levelUp)
    {
        
      
        healthText.text = "" + (Exp + amount) + "/" + MaxExp;
        float expPercent = (float)Exp / (float)MaxExp;
        foreground.fillAmount = expPercent;
       
            
        damagedColor.a = 0.7f;
        DamagedBar.color = damagedColor;
        damageFadeTimer = DAMAGED_FADE_TIMER_MAX;
        float expPercentNew = (float)(Exp + amount) / (float)MaxExp;
        DamagedBar.fillAmount = expPercentNew;
        StartCoroutine(ChangeToPct(expPercentNew));
        if (levelUp)
        {
            int diff = (Exp + amount) - MaxExp;
            HandleExpChanged(0, MaxExp + 5, diff, false);
        }
    }

    private IEnumerator ChangeToPct(float pc)
    {
        yield return new WaitForSeconds(1);
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
