using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Base Enemy")]
public class BaseEnemy : ScriptableObject 
{
    public int BaseHealth;
    public int BaseDamage;
    public int HealthPerLevel;
    public int DamagePerLevel;


}
