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
    public virtual void UpdateEffect(float currentDuration)
    {
       
        DebuffIndicator.transform.GetChild(0).GetComponent<Image>().fillAmount = (currentDuration / Duration);
    }
    public virtual void OnRemoveEffect(GameObject debuffSpawn)
    {
        Destroy(debuffSpawn.transform.GetChild(0).gameObject);
    }
}
