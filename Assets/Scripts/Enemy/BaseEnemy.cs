using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Base Enemy")]
public class BaseEnemy : ScriptableObject 
{
    public int BaseHealthPerLevel;
    public int BasePhysicalDamagePerLevel;
    public float BaseArmorChancePerLevel;
    public float BaseAttackSpeed;

}
