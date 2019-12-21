using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    PlayerController player;
    public int CritStacks = 0;
    public int SlowStacks = 0;
    public Dictionary<ArrowTypes, BaseStatusEffect> StatusEffects = new Dictionary<ArrowTypes, BaseStatusEffect>();
    public Dictionary<ArrowTypes, float> StatusEffectDurations = new Dictionary<ArrowTypes, float>();
    public List<ArrowTypes> RemoveList = new List<ArrowTypes>();
    public float Speed;
    private NavMeshAgent navmesh;
    private Animator anim;


    private float attackSpeedTimer = float.MaxValue;
    public float attackSpeed;
    private void Update()
    {
     
       foreach (var key in StatusEffects.Keys)
        {
            
            if (StatusEffectDurations[key] <= 0)
            {
                StatusEffects[key].OnRemoveEffect(GetComponent<EnemyHealth>().debuffSpawn);
                RemoveList.Add(key); 
            } 
            else
            {
                StatusEffectDurations[key] -= Time.deltaTime;
                StatusEffects[key].UpdateEffect(StatusEffectDurations[key], GetComponent<EnemyHealth>().debuffSpawn);
            }
        }

        foreach (var item in RemoveList)
        {
            StatusEffectDurations.Remove(item);
            StatusEffects[item].DealDamage -= GetComponent<EnemyHealth>().TakeDamage;

            //StatusEffects[item].OnRemoveEffect(GetComponent<EnemyHealth>().debuffSpawn);
            if (StatusEffects[item] is CritStatusEffect) CritStacks = 0;
            if (StatusEffects[item] is SlowStatusEffect)
            {
                SlowStacks = 0;
                UpdateSpeed(1);
            }
            Debug.Log(StatusEffects[item].Name + " ended");
            StatusEffects.Remove(item);


        }
        RemoveList.Clear();
        if (Vector3.Distance(transform.position, GameAssets.I.player.transform.position) > 3)
        {
            navmesh.isStopped = false;
            navmesh.SetDestination(GameAssets.I.player.transform.position);
            
        }
        else
        {
            navmesh.isStopped = true;
            if (attackSpeedTimer > attackSpeed)
            {
                anim.Play("Unarmed-Attack-L1");
                attackSpeedTimer = 0;
              }
            attackSpeedTimer += Time.deltaTime;
        }
        if (navmesh.velocity.magnitude > 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }
    private void EnemyDamage()
    {
        if (GameAssets.I.player.GetComponent<PlayerMovement>().isRolling) return;
        EnemyHealth e = GetComponent<EnemyHealth>();
        GameAssets.I.player.GetComponent<PlayerHealth>().TakeDamage(e.stats.BaseDamage + (e.stats.DamagePerLevel * e.spawner.CurrentWave), false, Color.red);

    }
    private void Start()
    {
        player = GameAssets.I.player;
        navmesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        UpdateSpeed(1);
    }
    public void UpdateSpeed(float percent)
    {
  
        navmesh.speed = Speed * percent;
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


            if (ase.Effect is CritStatusEffect)
            {
                CritStacks = ase.Effect.OnHitEffect(CritStacks);
            }
            else if (ase.Effect is SlowStatusEffect)
            {
                SlowStacks = ase.Effect.OnHitEffect(SlowStacks);
                UpdateSpeed((SlowStacks * (ase.Effect as SlowStatusEffect).PercentPerStack) / 100);
            }
            else
            {
                ase.Effect.OnHitEffect();
            }
            //Destroy(other.gameObject);
        }
    }
    private void AddStatusEffect(BaseStatusEffect effect)
    {
        if (StatusEffects.ContainsKey(effect.Element))
        {
            StatusEffectDurations[effect.Element] = effect.Duration;
             
           // StatusEffectDurations[StatusEffects[effect]] = effect.Duration;
        }
        else
        {

            effect.DealDamage += GetComponent<EnemyHealth>().TakeDamage;
            StatusEffects.Add(effect.Element, effect);
            StatusEffectDurations.Add(effect.Element, effect.Duration);
            effect.InitEffect(GetComponent<EnemyHealth>().debuffSpawn);
           
        }

    }

    private void InitDamage(ArrowTypes Element)
    {

        int damage = Mathf.RoundToInt(player.GetStat("Damage"));
        int chance = UnityEngine.Random.Range(0, 100);
        bool isCrit = false;
        CritStatusEffect critStatusEffect = GetStatusEffectByElement(ArrowTypes.Lightning) as CritStatusEffect;
        float bonusCrit = 0;
  
        if (critStatusEffect != null)
        {
            bonusCrit = CritStacks * critStatusEffect.PercentPerStack;   
            Debug.Log(bonusCrit);
        }
        if (chance <= bonusCrit)
        {
            isCrit = true;
             
            damage += Mathf.RoundToInt((float)damage * ((float)player.GetStat("CritDamage") / 100));

        }
        float ls = player.GetStat("LifeSteal");
        HealStatusEffect healStatusEffect = GetStatusEffectByElement(ArrowTypes.Water) as HealStatusEffect;
        if (healStatusEffect != null)
        {
            healStatusEffect.OnHitEffect_NormalArrow();
        }
       

        GetComponent<EnemyHealth>().TakeDamage(damage, isCrit, GetElementColor(Element));
    }
    private BaseStatusEffect GetStatusEffectByElement(ArrowTypes type)
    {
       if (StatusEffects.ContainsKey(type))
        {
            return StatusEffects[type];
        }
       else
        {
            return null;
        }
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

 
