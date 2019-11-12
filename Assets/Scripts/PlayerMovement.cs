using UnityEngine;

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
    private bool isRolling = false;

    public bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = PlayerInput.Instance;
        animationController = GetComponent<AnimationController>();
        camTransform = Camera.main.transform;
        playerController = GetComponent<PlayerController>();
        rollSpeed = speed * 2;
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
       
       
        if (canMove)
        {
            moveInput = new Vector3(playerInput.Horizontal, 0f, playerInput.Vertical);
            moveVelocity = moveInput.normalized * ((isRolling) ? rollSpeed : speed);
        }
        else
        {
            moveVelocity = Vector3.zero;
        }
        if (PlayerInput.Instance.Roll && !isRolling)
        {
            if (moveInput.magnitude.Equals(0)) return;
                isRolling = true;
            animationController.animator.SetTrigger("Roll");
        }


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
