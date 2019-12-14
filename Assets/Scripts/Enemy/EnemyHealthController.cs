using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private EnemyHealthBar healthBarPrefab;

    private Dictionary<EnemyHealth, EnemyHealthBar> healthBars = new Dictionary<EnemyHealth, EnemyHealthBar>();
    // Start is called before the first frame update
    void Awake()
    {
        EnemyHealth.OnHealthAdded += AddHealthBar;
        EnemyHealth.OnHealthRemoved += RemoveHealthBar;
    }

   private void AddHealthBar(EnemyHealth health)
    {
        if (healthBars.ContainsKey(health) == false)
        {
            var hb = Instantiate(healthBarPrefab, transform);
            health.debuffSpawn = hb.GetComponent<EnemyHealthBar>().DebuffSpawn;    
            healthBars.Add(health, hb);
            hb.SetHealth(health);
        }
    }
    private void RemoveHealthBar(EnemyHealth health)
    {
        if (healthBars.ContainsKey(health))
        {
           if (healthBars[health])
            {
                Destroy(healthBars[health].gameObject);
                healthBars.Remove(health);
            }
            
        }
    }
}
