using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StatusEffect/New Heal")]
public class HealStatusEffect : BaseStatusEffect
{
    public float PercentHealing;

    public override void OnHitEffect_NormalArrow()
    {
       
        base.OnHitEffect_NormalArrow();
        PlayerController pc = GameAssets.I.player.GetComponent<PlayerController>();
        float percentHeal = PercentHealing + GameAssets.I.player.GetStat("WaterEff");
        pc.playerHealth.HealDamage((int)Mathf.Round(pc.playerHealth.MaxHealth * (percentHeal / 100)), false);
        //Heal Player
    }
    public override void OnHitEffect()
    {
        float percentHeal = PercentHealing + GameAssets.I.player.GetStat("WaterEff");
        DebuffIndicator.transform.GetChild(1).GetComponent<Text>().text = percentHeal + "%";

    }
}
