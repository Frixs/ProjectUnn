using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 0.5f;
    private Color color;
    private TextMeshProUGUI textMesh;
    private float disappearTimer;
    private float disappearSpeed = 4f;
    private Vector3 spawnPosition;
    [SerializeField] private float damageOffset = 0.45f;
    private Vector3 movementOffset;
    public static DamagePopup Create(Vector3 position, Transform parent, int damageAmount, bool isCrit, bool isHeal, Color PopupColor)
    { 
        Transform damagePopupTransform = Instantiate(GameAssets.I.damagePopupPrefab, parent);
        DamagePopup damagePopupScript = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopupScript.Setup(damageAmount, position, isCrit, isHeal, PopupColor);
        return damagePopupScript;
    }

    private static int sortingOrder;
    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        color = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;
    }
   public void Setup(int damageAmount, Vector3 spawn, bool isCrit, bool isHeal, Color PopupColor)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCrit)
        {
            //Normal
            textMesh.fontSize = 25; 
            color = (isHeal) ? Color.green :  PopupColor; 

        }
        else
        {
            //Critical Hit
            textMesh.fontSize = 30;
            color = (isHeal) ? Color.green : PopupColor;

        }
        textMesh.color = color;
        spawnPosition = spawn;
        movementOffset =  new Vector3 (1, 1) * 1f;
        sortingOrder++;
       
    }
    private void LateUpdate()
    {
        
        transform.position = Camera.main.WorldToScreenPoint(spawnPosition + Vector3.up * damageOffset);
        
    }
    private void Update()
    {
        
        spawnPosition += movementOffset * Time.deltaTime;
        movementOffset -= movementOffset * 3f * Time.deltaTime;
        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            color.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = color;
            if (color.a <= 0) Destroy(gameObject);
        }
    }
}
