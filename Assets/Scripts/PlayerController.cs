using System.Collections.Generic;
using UnityEngine;
using System;



[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private const string SHOOT_ANIM = "ShootArrow";
    /*
     * Private Variables
     */

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    //Ray Cast hit setup
    private RaycastHit hit;
    //Attack Delay (Attack Speed)
    [SerializeField] private float DebugAttackSpeed;
    [SerializeField] public List<Stat> stats;
    private float attackSpeedTimer = float.MaxValue;
    public PlayerHealth playerHealth;
    /*
     * Public Variables
     */
    
    [HideInInspector] public AnimationController animationController;

    
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = PlayerInput.Instance;
        playerMovement = GetComponent<PlayerMovement>();
        animationController = GetComponent<AnimationController>();
       
    }

    private void Start()
    {
        stats = new List<Stat>()
        {
            new Stat("PhysicalDamage", 5),
            new Stat("CritChance", 0, 100),
            new Stat("CritDamage", 100),
            new Stat("AttackSpeed", 1f),
            new Stat("LifeSteal", 1),
            new Stat("Armor", 0, 50) //Coming Soon
        };
    }
    public float GetStat(string Name)
    {
        Stat stat = stats.Find(s => s.Name == Name);
        if (stat == null)
        {
            return 0;
        }
        
        return stat.Value;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.Shoot) //On Shoot
        {
             if (attackSpeedTimer > DebugAttackSpeed) //Is the attack speed timer reset (can  we attack)
            {

                animationController.animator.SetBool("Shooting", true);
                attackSpeedTimer = 0;
                
            } 
 
        }  
        else
        {
            animationController.animator.SetBool("Shooting", false);
        }


       
        attackSpeedTimer += Time.deltaTime;

    }
 

    [System.Serializable]
    public class Stat
    {
        public string Name;
        public float Value;
        public float Max;
        public Stat(string name, float value, float _max = float.MaxValue)
        {
            Name = name;
            Value = value;
            Max = _max;
        }
        public Stat(string name) : this(name, 0)
        {
            Name = name;
        }
    }



}
