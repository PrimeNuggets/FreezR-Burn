using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    [Header("Components")]
    [NonSerialized] public CapsuleCollider2D capsuleCollider2D;
    [NonSerialized] public Animator anim;
    [NonSerialized] public Rigidbody2D rb2D;

    [Header("Gravity")]
    [NonSerialized] public bool isGrounded = false;
    [NonSerialized] public bool Is_DownJump_GroundCheck = false;

    [NonSerialized] public bool flipped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Flip(bool bLeft)
    {
        onFlip(true);
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1); //Flips vertical axis depending on bLeft
        onFlip(false);
    }
    public bool onFlip(bool ret)
    {
        flipped = ret;
        return flipped;
    }
    //Float variant
    static public void SetValue(ref float value1, float value2, int operation = 0)
    { //Method to easily set the value of a variable
        switch (operation)
        {
            case 0: //Set to Value
                value1 = value2;
                break;
            case 1: //Add Value
                value1 += value2;
                break;
            case 2: //Subtract Value
                value1 -= value2;
                break;
            case 3: //Multiply by Value
                value1 *= value2;
                break;
            case 4: //Divide by Value
                value1 /= value2;
                break;
            default:
                Debug.LogError("Invalid operation.\nOperation value out of bounds [0,-4]");
                break;
        }
    }
    //Int Variant
    static public void SetValue(ref int value1, int value2, int operation = 0)
    { //Method to easily set the value of a variable
        switch (operation)
        {
            case 0: //Set to Value
                value1 = value2;
                break;
            case 1: //Add Value
                value1 += value2;
                break;
            case 2: //Subtract Value
                value1 -= value2;
                break;
            case 3: //Multiply by Value
                value1 *= value2;
                break;
            case 4: //Divide by Value
                value1 /= value2;
                break;
            default:
                Debug.LogError("Invalid operation.\nOperation value out of bounds [0,-4]");
                break;
        }
    }
}
