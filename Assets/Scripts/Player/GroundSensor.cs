using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{

    public PlayerBase m_root;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        m_root = this.transform.root.GetComponent<PlayerBase>();
        rb2d = m_root.GetComponent<Rigidbody2D>();
    }



    ContactPoint2D[] contacts = new ContactPoint2D[1];

    void OnTriggerStay2D(Collider2D other)
    {


        if (other.CompareTag("Ground")) // || other.CompareTag("Block")
        {

            if (other.CompareTag("Ground"))
            {
                m_root.Is_DownJump_GroundCheck = true;

            }
            else
            {
               m_root.Is_DownJump_GroundCheck = false;
            }

            if (rb2d.linearVelocity.y <= 0)
            {

                m_root.isGrounded = true;
                //m_root.currentJumpCount = 0;
            }


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

       m_root.isGrounded = false;

    }



}
