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
        int eff = Mathf.RoundToInt(GameAssets.I.player.GetStat("LightningEff"));
        float pps = PercentPerStack + (float)eff;
        int bonusStacks = (eff % 4 == 0) ? (eff / 4)  : 0;
        int CS = Mathf.Min(stacks + 1, MaxCritStacks + bonusStacks );
        DebuffIndicator.transform.GetChild(1).GetComponent<Text>().text = (CS * pps) + "%";
        return CS;

    }

}
