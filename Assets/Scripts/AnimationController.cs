using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{

    /*
     * Private Variables
     */
    private PlayerMovement playerMovement;
    private PlayerController playerController;

    /*
     * Public Variables
     */

    public Animator animator;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
    }

    //Returns true if Animator is playing an animation
    private bool AnimatorIsPlaying() 
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    //Returns true if animation is playing a specifc animation
    public bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    //Update animator values for turning 
    public void UpdateAnimator(float forwardAmount, float turnAmount)
    {
       animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
       animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }


    /*
     * Animation Events
     */

    private void FootR() { }
    private void FootL() { }
    private void AnimationStart()
    {
    }
 
    private void AnimationEnd ()
    {

    }

}
