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
    private const float DUMMY_REST = 3f;
    public int MaxHealth;
    public int Hp;
    public BaseEnemy stats;

    public event Action<int, int, int, bool, bool, Color> OnHealthChanged = delegate { };

    public int dmg = 1;
    public int chance = 1;
    private WaveSpawner spawner;
    
    private void OnEnable()
    {
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveSpawner>();
         stats = spawner.GetRandomEnemyType();
         MaxHealth = spawner.CurrentWave.Number * stats.BaseHealthPerLevel;
        
        Hp = MaxHealth;
       
    }
    private void Start()
    {
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
           spawner.RemoveEnemy();
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
