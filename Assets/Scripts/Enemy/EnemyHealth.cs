using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public static event Action<EnemyHealth> OnHealthAdded = delegate { };
    public static event Action<EnemyHealth> OnHealthRemoved = delegate { };
    public GameObject debuffSpawn;
    public int MaxHealth;
    public int Hp;
    public int Damage;
    public BaseEnemy stats;

    public event Action<int, int, int, bool, bool, Color> OnHealthChanged = delegate { };

    public WaveSpawner spawner;
    

    private void Start()
    {
        MaxHealth = stats.BaseHealth + (spawner.CurrentWave * stats.HealthPerLevel);
        Hp = MaxHealth;
        Damage = stats.BaseDamage + (spawner.CurrentWave * stats.DamagePerLevel);
        OnHealthAdded(this);
    }
    public void HealDamage(int amount, bool isCrit)
    {
        Hp += amount;
        if (Hp > MaxHealth)
        {
            Hp = MaxHealth;
        }
        OnHealthChanged(Hp, MaxHealth, amount, isCrit, true, Color.green);
        
    }
    public void TakeDamage(int amount, bool isCrit, Color PopupColor)
    {
        Hp -= amount;
        OnHealthChanged(Hp, MaxHealth, amount, isCrit, false, PopupColor); 
        if (Hp <= 0)
        {
            spawner.EndRound();
            foreach (var s in GetComponent<Enemy>().StatusEffects)
            {
                s.Value.DealDamage -= GetComponent<EnemyHealth>().TakeDamage;
            }

            Destroy(this.gameObject); 
        }
    }
    private void Update()
    {
       
    }
    private void OnDisable()
    {
        OnHealthRemoved(this);
    }
}
