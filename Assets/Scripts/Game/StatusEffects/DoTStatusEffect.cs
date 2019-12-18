using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [CreateAssetMenu(menuName = "StatusEffect/New DoT")]
public class DoTStatusEffect : BaseStatusEffect
{
    public override event Action<int, bool, Color> DealDamage = delegate { };
    public int TickDamageFlat;  //Scale  with Damage 
    public float TicksPerSecondFlat;  //Scales with attack speed 

    public float TickDamageScaling;
    public float TicksPerSecondScaling;

    private float tickCounter = 0;
    public override void UpdateEffect(float currentDuration, GameObject debuffSpawn)
    {
      
        base.UpdateEffect(currentDuration, debuffSpawn);
        if (currentDuration <= 0) return;
        tickCounter += Time.deltaTime;
        float ticksPerSecond = TicksPerSecondFlat;
        float timeBetweenTicks = (1 / ticksPerSecond); 
        
         if (tickCounter > timeBetweenTicks)
        {
            Debug.Log("Tick");
            DealDamage(TickDamageFlat + Mathf.RoundToInt(GameAssets.I.player.GetStat("FireEff")), false, Color.red); 
            tickCounter = 0;
        }
    }
}
