using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StatusEffect/New Crit")]
public class CritStatusEffect : BaseStatusEffect
{
    public int MaxCritStacks;
    public float PercentPerStack;


    public override int OnHitEffect(int stacks)
    {
        int CS = Mathf.Min(stacks + 1, MaxCritStacks);
        DebuffIndicator.transform.GetChild(1).GetComponent<Text>().text = (CS * PercentPerStack) + "%";
        return CS;

    }

}
