using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="StatusEffect/New Slow")]
public class SlowStatusEffect : BaseStatusEffect
{
    public int MaxSlowStacks;
    public float PercentPerStack;


    public override int OnHitEffect(int stacks)
    {
        int eff = Mathf.RoundToInt(GameAssets.I.player.GetStat("IceEff"));
        float pps = PercentPerStack + (float)eff;
        int bonusStacks = (eff % 4 == 0) ? (eff / 4) : 0;

        int SS = Mathf.Min(stacks + 1, MaxSlowStacks + bonusStacks);
        DebuffIndicator.transform.GetChild(1).GetComponent<Text>().text = (SS * pps) + "%";
        return SS;

    }
}
