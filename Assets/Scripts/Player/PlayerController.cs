using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

    public float Accuracy;
    public int ArrowsShot;
    public int ArrowsHit;
    public int SkillPoints;
    public int Exp;
    public int MaxExp;
    public int Level;
    public List<ArrowCount> ArrowCounts;
    [SerializeField] private Text AccuracyText;
    [SerializeField] private GameObject ExpBar;
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
        playerHealth = GetComponent<PlayerHealth>();
        Level = 1;
        MaxExp = 10;
        Exp = 0;
        SkillPoints = 0;
        
    }

    private void Start()
    {
        stats = new List<Stat>()
        {
            new Stat("Damage", 5, 2),
            new Stat("FireEff", 1),
            new Stat("LightningEff", 1),
            new Stat("IceEff", 1),
            new Stat("WaterEff", 1),
            new Stat("CritChance", 0, 100),
            new Stat("CritDamage", 100)

        };
        ResetQuiver();
        /* ArrowCounts = new List<ArrowCount>()
         {
             new ArrowCount(ArrowTypes.Fire, 0),
             new ArrowCount(ArrowTypes.Ice, 0),
             new ArrowCount(ArrowTypes.Lightning, 0),
             new ArrowCount(ArrowTypes.Water, 0)

         };*/


    }
    public void Upgrade(string name)
    {
        if (SkillPoints > 0)
        {
            switch (name)
            {
                case "Normal":
                    Stat normal;
                     normal = stats.Find(s => s.Name == "Damage");
                     normal.Value += 2;
                    break;
                case "Fire":
                    Stat fire;
                    fire = stats.Find(s => s.Name == "FireEff");
                    fire.Value += 1;
                    break;
                case "Ice":
                    Stat ice = stats.Find(s => s.Name == "IceEff");
                    ice.Value += 1;
                    break;
                case "Lightning":
                    Stat lightning = stats.Find(s => s.Name == "LightningEff");
                    lightning.Value += 1;
                    break;
                case "Water":
                    Stat water = stats.Find(s => s.Name == "WaterEff");
                    water.Value += 1;
                    break;
                case "Dodge":
                    playerMovement.RollRechargeRate += 0.1f;
                    break;
                case "Health":
                    playerHealth.MaxHealth += 10;
                    playerHealth.Hp += 10;
                    playerHealth.HealDamage(10, false);
                    break;
            }
            
            SkillPoints--;
            GameObject.Find("LevelText").GetComponent<Text>().text = "Level " + Level + " (SP: " + SkillPoints + ")";
            if (SkillPoints <= 0)
            {
                UpgradeController.Instance.HideButtons();
            }

        }

    }
    public void AddExp(int exp)
    {
        bool levelUp = false;
        if (Exp + exp >= MaxExp)
        {
            levelUp = true;
            
        }

        ExpBar.GetComponent<PlayerExpBar>().HandleExpChanged(Exp, MaxExp, exp, levelUp);
        Exp += exp;
        if (Exp >= MaxExp)
        {
            Level++;
            MaxExp += 5;
            Exp = 0;
           // stats[0].Value += 2;
           //playerHealth.MaxHealth += (Level % 2 == 0) ? 10 : 0;
            SkillPoints += 2;
            GameObject.Find("LevelUpText").GetComponent<FadeText>().Show();
            GameObject.Find("LevelText").GetComponent<Text>().text = "Level " + Level + " (SP: " + SkillPoints + ")";
            UpgradeController.Instance.ShowButtons();
        }
        
    }
    public void CalculateAccuracy()
    {
        Accuracy = ((float)ArrowsHit / (float)ArrowsShot) * 100;
        AccuracyText.text = "Accuracy: " + Accuracy.ToString("0.0") + "%";
    }
    public void ResetQuiver()
    {
        foreach(ArrowCount count in ArrowCounts)
        {
            count.Reset();
        }
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
                ArrowTypes currentType = GetComponent<AE_AnimatorEvents>().CurrentArrowType;
                ArrowCount count = ArrowCounts.Find(ac => ac.Arrow == currentType);
                if (count.Unlimited || count.Current > 0)
                {
                    animationController.animator.SetBool("Shooting", true);
                    
                }
                else
                {
                    animationController.animator.SetBool("Shooting", false);
                }
                
                attackSpeedTimer = 0;
                
            } 
 
        }  
        else
        {
            animationController.animator.SetBool("Shooting", false);
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            AddExp(MaxExp - Exp);
        }
       
        attackSpeedTimer += Time.deltaTime;

    }
    public void DecreaseArrow()
    {
        
        ArrowTypes currentType = GetComponent<AE_AnimatorEvents>().CurrentArrowType;
        ArrowCount count = ArrowCounts.Find(ac => ac.Arrow == currentType);
        if (count.Unlimited) return;
        count.Current--;
        count.UpdateArrowCountText();
    }
   
    [System.Serializable]
    public class Stat
    {
        public string Name;
        public float Value;
        public float Max;
        public int Level;
        public Stat(string name, float value, float _max = float.MaxValue )
        {
            Name = name;
            Value = value;
            Max = _max;
            Level = 1;
        } 
    }
     
    [System.Serializable]
    public class ArrowCount
    {
        public ArrowTypes Arrow;
        public int Max;
        public int Current;
        public Text CountText;
        public bool Unlimited;

        public void Reset()
        {
            Current = Max;
            if (Unlimited) return;
            UpdateArrowCountText();
        }
        public void UpdateArrowCountText()
        {
            CountText.text = Current.ToString() ; 
        }
    }



}
