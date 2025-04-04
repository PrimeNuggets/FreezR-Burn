using System;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerHealth : PlayerBase
{
    [Header("Respawn")]
    private Vector3 respawnPoint;

    [Header("Defensive")]
    [SerializeField] private float playerHPMax = 100f;
    [NonSerialized] public float playerHP;
    [SerializeField] private int playerLivesMax = 3;
    [NonSerialized] public int playerLives;
    [SerializeField] private GameObject healthBar;

    [Header("Offensive")]
    [SerializeField] private float playerConstAttack = 100f;
    [NonSerialized] public float playerAttack;
    public InputActionReference attackBind; //Keybind for attack
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capsuleCollider2D = this.transform.GetComponent<CapsuleCollider2D>(); //References the CapsuleCollider2D component attatched to this game object
        anim = this.transform.Find("model").GetComponent<Animator>(); //References the Animator component attatched to this game object
        rb2D = this.transform.GetComponent<Rigidbody2D>(); //References the Rigidbody2D component attatched to this game object

        respawnPoint = this.transform.position;
        Debug.Log("Respawn Position set to: " + respawnPoint);

        SetHP(GetHP(true));
        SetLives(GetLives(true));
        SetAttackStat(GetAttackStat(true));
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBar();
        FlipBar(transform.localScale.x >= 0);
    }

    public void FlipBar(bool bLeft) {
        healthBar.transform.localScale = new Vector3(bLeft ? 0.02f : -0.02f, 0.02f, 0.02f); //Flips vertical axis depending on bLeft
    }

    private void FixedUpdate()
    {
        if (playerHP <= 0 && playerLives > 0)
        {
            SetLives(1, 2); //Subtracts 1 life
            SetHP(GetHP(true)); //Resets the player's hp to the max hp
            transform.transform.position = respawnPoint; //Teleports the player back to the last respawn point
        }
    }

    public float GetAttackStat(bool max = false)
    {
        return (max) ? playerConstAttack : playerAttack;
    }

    public void SetAttackStat(float value, int operation = 0)
    {
        SetValue(ref playerAttack, value, operation);
    }

    public float GetHP(bool max = false)
    {
        return (max) ? playerHPMax : playerHP;
    }

    public void SetHP(float value, int operation = 0)
    { 
        SetValue(ref playerHP, value, operation);
        ChangeHealthText();
        ChangeBar();
    }

    public int GetLives(bool max = false)
    {
        return max ? playerLivesMax : playerLives;
    }

    public void SetLives(int value, int operation = 0)
    {
        SetValue(ref playerLives, value, operation);
    }
    private void ChangeHealthText()
    {
        Text text = healthBar.GetComponentInChildren<Text>();
        if (text == null) return;
        text.text = $"{playerHP}/{playerHPMax}";
    }
    private void ChangeBar()
    {
        Image barFill = healthBar.GetComponentInChildren<Image>();
        float healthPercentage = (GetHP() / GetHP(true)) * 100;
        barFill.fillAmount = healthPercentage / 100;
        if (healthPercentage > 75)
        {
            barFill.color = Color.green;
        }
        else if (healthPercentage > 50)
        {
            barFill.color = Color.yellow;
        }
        else if (healthPercentage > 25)
        {
            barFill.color = new Color(1f, 0.5f, 0f);
        }
        else
        {
            barFill.color = Color.red;
        }
    }

    private float AttackDamage(float baseDMG)
    {
        float multiplier = playerAttack / 100;
        float dmg = multiplier * baseDMG;
        return dmg;
    }

    private void OnEnable()
    {
        attackBind.action.started += PlayAttack; //Add this method to a run list
    }

    private void OnDisable()
    {
        attackBind.action.started -= PlayAttack; //Remove this method from a run list
    }

    private void PlayAttack(InputAction.CallbackContext obj) {
        Debug.Log("Attacking");
        SetHP(AttackDamage(5), 2);
        anim.Play("Demo_Attack"); //WIP
    }
}
