using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/New Crit")]
public class CritStatusEffect : BaseStatusEffect
{
    public int MaxCritStacks;
    public float PercentPerStack;


    public override int OnHitEffect(int stacks)
    {
        int CS = Mathf.Min(stacks + 1, MaxCritStacks);
        DebuffIndicator.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (CS * PercentPerStack) + "%";
        return CS;

    }
    public override void OnRemoveEffect(GameObject debuffSpawn)
    {
        base.OnRemoveEffect(debuffSpawn);
        
    }

}
