using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [NonSerialized] public int health;
    [SerializeField] private GameObject healthBar;

    [Header("Functionality")]
    [NonSerialized] public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetHealth(maxHealth);
        //player = 
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBar();
    }

    protected void Flip(bool bLeft)
    {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1); //Flips vertical axis depending on bLeft
        healthBar.transform.localScale = new Vector3(bLeft ? 0.02f : -0.02f, 0.02f, 0.02f); //Flips vertical axis depending on bLeft
    }

    protected float Facing(GameObject obj = null)
    {
        if (obj == null) obj = gameObject;
        return obj.transform.localScale.x; 
    }

    public void DestroySelf() {
        Destroy(gameObject);
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
    public int getHealth(bool max = false) {
        return max ? maxHealth : health;
    }
    public void SetHealth(int hp, int operation = 0) //Sets the player's current health to a desired value
    {
        SetValue(ref health, hp, operation);
        ChangeHealthText();
        ChangeBar();
    }
    private void ChangeHealthText()
    {
        Text text = healthBar.GetComponentInChildren<Text>();
        if (text == null) return;
        text.text = $"{health}/{maxHealth}";
    }
    private void ChangeBar() { 
        Image barFill = healthBar.GetComponentInChildren<Image>();
        float healthPercentage = (getHealth() / getHealth(true)) * 100;
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
}
