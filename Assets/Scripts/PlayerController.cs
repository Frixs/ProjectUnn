using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    /*
     * Private Variables
     */

    
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    //Ray Cast hit setup
    private RaycastHit hit;
    //Attack Delay (Attack Speed)
    [SerializeField] private float DebugAttackSpeed;
    private float attackSpeedTimer = float.MaxValue;
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


    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.Action) //On Shoot
        {
             if (attackSpeedTimer > DebugAttackSpeed) //Is the attack speed timer reset (can  we attack)
            {
                animationController.animator.Play("Shooting-Fire-Rifle1");
                attackSpeedTimer = 0; //Reset attack speed timer
            }
           
 
        }


       
        attackSpeedTimer += Time.deltaTime;

    }



    

}
