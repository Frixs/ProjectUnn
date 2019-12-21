using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    /*
     * Public Variables
     */


    // How fast the player moves
    public float speed = 3; 


    /*
     * Private Variables
     */

    
    private Rigidbody rb;
    private PlayerInput playerInput;
    private AnimationController animationController;
    private PlayerController playerController;
    [SerializeField] private Transform PlayerCenter;
    // Calculates direction and move velocity
    public Vector3 lookPos;
    private Vector3 moveInput;
    public  Vector3 moveVelocity;

    //Handles animation smoothness and switching
    private float forwardAmount;
    private float turnAmount;

    //Camera Variables;
    private Transform camTransform;
    private Vector3 camForward;
    private Vector3 move;

    //Handle Roll
    private float rollSpeed;
    public bool isRolling = false;

    public bool canMove = true;

    public int MaxRollCharges;
    private int NumOfRolls;
    public float MaxRollRecharge;
    private float CurrentRollRecharge;
    public float RollRechargeRate;
    [SerializeField] private Image RollImage;
    [SerializeField] private Text RollTxt;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = PlayerInput.Instance;
        animationController = GetComponent<AnimationController>();
        camTransform = Camera.main.transform;
        playerController = GetComponent<PlayerController>();
        rollSpeed = speed * 2;
        NumOfRolls = MaxRollCharges;
        CurrentRollRecharge = 0;
        RollRechargeRate = 0;
        UpdateRollUI();
    }
    // Update is called once per frame
    void Update()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(transform.position, hit.point) > 1.5)
                {
                    lookPos = hit.point;
                }
            }
            Vector3 lookDir = lookPos - transform.position;
            lookDir.y = 0;
       
        transform.LookAt(transform.position + lookDir, Vector3.up);
        //transform.rotation *= Quaternion.Euler(0, 45, 0);
       
       
        if (canMove)
        {
            moveInput = new Vector3(playerInput.Horizontal, 0f, playerInput.Vertical);
            moveVelocity = moveInput.normalized * ((isRolling) ? rollSpeed : speed);
        }
        else
        {
            moveVelocity = Vector3.zero;
        }
        if (PlayerInput.Instance.Roll && CanRoll())
        {
            if (moveInput.magnitude.Equals(0)) return;
                isRolling = true;
            animationController.animator.SetTrigger("Roll");
            NumOfRolls--;
        }
         
        if (NumOfRolls < MaxRollCharges)
        {
            CurrentRollRecharge += Time.deltaTime + (Time.deltaTime * RollRechargeRate);
             if (CurrentRollRecharge > MaxRollRecharge)
            {
                NumOfRolls++;
                CurrentRollRecharge = 0;
            }
            UpdateRollUI();
        }


    }
    private void UpdateRollUI()
    {
        RollImage.fillAmount = CurrentRollRecharge / MaxRollRecharge;
        RollTxt.text = NumOfRolls.ToString();
        
    }
    private bool CanRoll()
    {
        return NumOfRolls > 0 && !isRolling;
    }
    private void RollEnd()
    {
        isRolling = false;
    }
    private void FixedUpdate()
    {
        if (camTransform != null)
        {
            camForward = Vector3.Scale(camTransform.up, new Vector3(1, 0, 1)).normalized;
            move = playerInput.Vertical * camForward + playerInput.Horizontal * camTransform.right;
        }
        else
        {
            move = playerInput.Vertical * Vector3.forward + playerInput.Horizontal * Vector3.right;
        }
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Move();
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
     
    }
    void Move()
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        moveInput = move;
        ConvertMoveInput();
        animationController.UpdateAnimator(forwardAmount, turnAmount);
    }
    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }
}
