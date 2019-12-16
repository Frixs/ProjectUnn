using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StatusEffect/New Heal")]
public class HealStatusEffect : BaseStatusEffect
{
    public float PercentHealing;

    public override void OnHitEffect_NormalArrow()
    {
        base.OnHitEffect_NormalArrow();
        PlayerController pc = GameAssets.I.player.GetComponent<PlayerController>();
        pc.playerHealth.HealDamage((int)Mathf.Round(pc.playerHealth.MaxHealth * (PercentHealing / 100)), false);
        //Heal Player
    }
}
