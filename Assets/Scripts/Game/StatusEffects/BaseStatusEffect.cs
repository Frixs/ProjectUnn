using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseStatusEffect : ScriptableObject
{
    public virtual event Action<int, bool, Color> DealDamage = delegate { };
    public GameObject DebuffIndicatorPrefab;
    public string Name;
   
    public float Duration;
    public ArrowTypes Element;
    protected GameObject DebuffIndicator;
    public virtual void InitEffect(GameObject debuffSpawn)
    {
        DebuffIndicator = Instantiate(DebuffIndicatorPrefab, debuffSpawn.transform);
    }
    public virtual int OnHitEffect(int Stacks)
    {
        return 0;
    }
    public virtual void OnHitEffect()
    {

    }
    public virtual void OnHitEffect_NormalArrow()
    {

    }
    public virtual void UpdateEffect(float currentDuration, GameObject debuffSpawn)
    {
        
        if (currentDuration <= 0 || DebuffIndicator == null)
        {
            OnRemoveEffect(debuffSpawn);
        }
        else
        {
            DebuffIndicator.transform.GetChild(0).GetComponent<Image>().fillAmount = (currentDuration / Duration);
        }
       
      
    }
    public virtual void OnRemoveEffect(GameObject debuffSpawn)
    {
       
        GameObject g =GameObject.FindGameObjectWithTag("Debuff_" + Element.ToString());
        Debug.Log(g);
        Destroy(g);
    }
}
