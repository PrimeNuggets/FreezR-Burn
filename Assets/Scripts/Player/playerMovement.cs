using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : PlayerBase
{
    //-------------------------------------------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------------------------------------------
    [Header("Functionality")]
    public InputActionReference pause; //Keybind for pausing the game
    private bool isCrouching = false;
    private bool OnceJumpRayCheck = false;
    Vector2 RayDir = Vector2.down;
    float PretmpY;
    float GroundCheckUpdateTic = 0;
    float GroundCheckUpdateTime = 0.01f;

    [Header("Movement")]
    public InputActionReference move; //Keybinds for movement
    public InputActionReference jump; //Keybinds for jumping
    [SerializeField] private float constPlayerSpeed; //Doesn't change after initialization
    private float playerSpeed;
    [SerializeField] private float constPlayerJumpSpeed; //Doesn't change after initialization
    private float playerJumpSpeed;
    private Vector2 _moveDirection; //Variable to store the direction that the player is moving

    //-------------------------------------------------------------------------------------------------------
    // Methods
    //-------------------------------------------------------------------------------------------------------
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capsuleCollider2D = this.transform.GetComponent<CapsuleCollider2D>(); //References the CapsuleCollider2D component attatched to this game object
        anim = this.transform.Find("model").GetComponent<Animator>(); //References the Animator component attatched to this game object
        rb2D = this.transform.GetComponent<Rigidbody2D>(); //References the Rigidbody2D component attatched to this game object

        playerSpeed = constPlayerSpeed; //Sets the changeable speed to the constant speed
        playerJumpSpeed = constPlayerJumpSpeed; //Sets the changeable jump speed to the constant jump speed
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>(); //Constantly read the player's directional movement
        if (rb2D.linearVelocity.magnitude > 30)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x - 0.1f, rb2D.linearVelocity.y - 0.1f);

        }
    }

    // Fixed Update constrains to Time.timescale
    private void FixedUpdate()
    {
        if (_moveDirection.y < 0f && isGrounded)
        {
            anim.Play("Demo_Sit");
            isCrouching = true;
        } else
        {
            isCrouching = false;
        }
        if (!isCrouching)
        {
            if (_moveDirection == Vector2.zero) //If the player isn't moving
            {
                anim.Play("Demo_Idle"); //Play the idle animation
            }
            else if (_moveDirection.x != 0) //If the player is moving horizontally
            {
                anim.Play("Demo_Run"); //Play the run animation
                transform.transform.Translate(Vector2.right * _moveDirection.x * GetSpeed() * Time.deltaTime); //Move the player
            }
        }
        if (_moveDirection != Vector2.zero)
        {
            Flip(_moveDirection.x < 0f); //Flip the player's vertical axis if moving backwards
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!isGrounded) return;
        if (isCrouching) return;
        anim.Play("Demo_Jump");

        rb2D.linearVelocity = new Vector2(0, 0);

        rb2D.AddForce(Vector2.up * GetJumpSpeed(), ForceMode2D.Impulse);

        OnceJumpRayCheck = true;
        isGrounded = false;
    }

    private void DownJump(InputAction.CallbackContext obj)
    {
        if (!isGrounded)
            return;

        if (!Is_DownJump_GroundCheck)
        {
            anim.Play("Demo_Jump");

            rb2D.AddForce(-Vector2.up * 10);
            isGrounded = false;

            capsuleCollider2D.enabled = false;

            StartCoroutine(GroundCapsulleColliderTimmerFuc());

        }
    }

    IEnumerator GroundCapsulleColliderTimmerFuc()
    {
        yield return new WaitForSeconds(0.3f);
        capsuleCollider2D.enabled = true;
    }

    protected void GroundCheckUpdate()
    {
        if (!OnceJumpRayCheck)
            return;

        GroundCheckUpdateTic += Time.deltaTime;
        if (GroundCheckUpdateTic > GroundCheckUpdateTime)
        {
            GroundCheckUpdateTic = 0;
            if (PretmpY == 0)
            {
                PretmpY = transform.position.y;
                return;
            }
            float reY = transform.position.y - PretmpY;  //    -1  - 0 = -1 ,  -2 -   -1 = -3
            if (reY <= 0)
            {
                if (isGrounded)
                {
                    LandingEvent();
                    OnceJumpRayCheck = false;
                }
                else
                {
                    Debug.Log("Not Grounded");
                }
            }
            PretmpY = transform.position.y;
        }
    }

    protected void LandingEvent() {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Run") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
            anim.Play("Demo_Idle");
    }

    private void OnEnable()
    {
            pause.action.started += PauseToggle; //Add this method to a run list
            jump.action.started += Jump; //Add this method to a run list
    }

    private void OnDisable()
    {
            pause.action.started -= PauseToggle; //Remove this method from a run list
            jump.action.started -= Jump; //Add this method to a run list
    }

    private void PauseToggle(InputAction.CallbackContext obj)
    {
        UI.TogglePause();
    }

    public float GetSpeed(bool initialSpeed = false) //Returns the player's current/initial speed
    {
        return (!initialSpeed) ? playerSpeed : constPlayerSpeed;
    }

    public void SetSpeed(float speed, int operation = 0) //Sets the player's current speed to a desired value
    {
        SetValue(ref playerSpeed, speed, operation);
    }

    public float GetJumpSpeed(bool initialSpeed = false) //Returns the player's current/initial jump speed
    {
        return (!initialSpeed) ? playerJumpSpeed : constPlayerJumpSpeed;
    }

    public void SetJumpSpeed(float speed, int operation = 0) //Sets the player's current jump speed to a desired value
    {
        SetValue(ref playerJumpSpeed, speed, operation);
    }
}