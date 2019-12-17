using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour
{
    public GameObject debuffSpawn;
    public int MaxHealth;
    public int Hp;
    public PlayerHealthBar healthBar;

    public event Action<int, int, int, bool, bool, Color> OnHealthChanged = delegate { };

    public int dmg = 1;
    public int chance = 1;
    private WaveSpawner spawner;
    
    private void OnEnable()
    {
        MaxHealth = 50;
        Hp = MaxHealth;
        healthBar.SetHealth(this);
       
    }
    public void HealDamage(int amount, bool isCrit)
    {
        Debug.Log("HealDamage");
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
         
           Destroy(this.gameObject); 
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            TakeDamage(25, false, Color.yellow);
        }  
    }
}
