using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    PlayerController player;
    public int CritStacks = 0;
    public Dictionary<BaseStatusEffect, int> StatusEffects = new Dictionary<BaseStatusEffect, int>();
    public float[] StatusEffectDurations = new float[5]; 
    public Dictionary<BaseStatusEffect, int> RemoveList = new Dictionary<BaseStatusEffect, int>();

    private void Update()
    {
     
       foreach (var item in StatusEffects)
        {
            StatusEffectDurations[StatusEffects[item.Key]] -= Time.deltaTime;
            
            item.Key.UpdateEffect(StatusEffectDurations[StatusEffects[item.Key]]);
            if (StatusEffectDurations[StatusEffects[item.Key]] < 0)
            {   
                RemoveList.Add(item.Key, item.Value);
            }
        }

        foreach (var item in RemoveList)
        {
            StatusEffects.Remove(item.Key);
            StatusEffectDurations[item.Value] = 0;
            item.Key.DealDamage -= GetComponent<EnemyHealth>().TakeDamage;
            item.Key.OnRemoveEffect(GetComponent<EnemyHealth>().debuffSpawn);
            if (item.Key is CritStatusEffect) CritStacks = 0;
            Debug.Log(item.Key.Name + " ended");
        }
        RemoveList.Clear();


    }
    private void Start()
    {
        player = GameAssets.I.player;
         
       
    }
    public void OnCollisionEnter(Collision other)
    {
        GameObject arrow = other.transform.parent.gameObject;
        if (arrow == null) return;
        if (arrow.CompareTag("Arrow"))
        {
            ApplyStatusEffect ase = arrow.GetComponent<ApplyStatusEffect>();
            InitDamage(ase.Element);
            if (ase.Effect == null) return;
            AddStatusEffect(ase.Effect);
            CritStacks = ase.Effect.OnHitEffect(CritStacks);
            //Destroy(other.gameObject);
        }
    }
    private void AddStatusEffect(BaseStatusEffect effect)
    {
        if (StatusEffects.ContainsKey(effect))
        {
            StatusEffectDurations[StatusEffects[effect]] = effect.Duration;
        }
        else
        {
            int CurrentCount = StatusEffects.Count;
            Debug.Log(CurrentCount);
            StatusEffects.Add(effect, CurrentCount);
            effect.DealDamage += GetComponent<EnemyHealth>().TakeDamage;
            StatusEffectDurations[CurrentCount] = effect.Duration;
            effect.InitEffect(GetComponent<EnemyHealth>().debuffSpawn);
        }

    }
    private void InitDamage(ArrowTypes Element)
    {

        int damage = Mathf.RoundToInt(player.GetStat("PhysicalDamage"));
        int chance = UnityEngine.Random.Range(0, 100);
        bool isCrit = false;
        CritStatusEffect critStatusEffect = GetStatusEffectByElement(ArrowTypes.Lightning) as CritStatusEffect;
        float bonusCrit = 0;
        if (critStatusEffect != null)
        {
            bonusCrit =  CritStacks * critStatusEffect.PercentPerStack;   
        }
        if (chance <= player.GetStat("CritChance") + bonusCrit)
        {
            isCrit = true;
             
            damage += Mathf.RoundToInt((float)damage * ((float)player.GetStat("CritDamage") / 100));

        }
        float ls = player.GetStat("LifeSteal");
        
        /*if (ls > 0 && player.playerHealth.Health < player.playerHealth.MaxHealth)
        {
            int lsAmount = Mathf.RoundToInt((ls / 100) * damage);
            if (lsAmount > 0)
            {
                player.playerHealth.HealDamage(lsAmount, isCrit);
            }
           
        }*/ 
       
        GetComponent<EnemyHealth>().TakeDamage(damage, isCrit, GetElementColor(Element));
    }
    private BaseStatusEffect GetStatusEffectByElement(ArrowTypes type)
    {
         foreach (var item in StatusEffects)
        {
             if (item.Key.Element == type)
            {
                return item.Key;
            }
        }
        return null;
    }
   private Color GetElementColor(ArrowTypes element)
    {
        switch (element)
        {
            case ArrowTypes.Normal:
                return Color.yellow;

            case ArrowTypes.Fire:
                return Color.red;

            case ArrowTypes.Water:
                return Color.blue;

            case ArrowTypes.Lightning:
                return Color.magenta;

            case ArrowTypes.Ice:
                return Color.cyan;

        }
        return Color.yellow;
        
    }
}

 
