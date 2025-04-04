using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    //[Header("General")]

    [Header("Game")]
    public GameObject pauseMenu;
    public GameObject mainMenuWarning;
    static public bool inputDisabled = false; //Determines whether the player can interact using their keybinds

    [Header("Player")]
    public playerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pauseMenu != null) //If pauseMenu exists
            UnpauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu != null) //If pauseMenu exists
        {
            if (IsPaused())
            {
                pauseMenu.SetActive(true); //Toggle the visibility of the pause menu on
            }
            else
            {
                pauseMenu.SetActive(false); //Toggle the visibility of the pause menu off
            }
        }
    }

    static public void TogglePause()
    {
        if (!inputDisabled) //If the player can use their keybinds
        {
            if (IsPaused())
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void MainMenuWarning(bool active = true)
    {
        ToggleInput(active); //Disable the use of the player's keybinds
        if (mainMenuWarning != null)
        {
            mainMenuWarning.SetActive(active); //Toggle the visibility of the warning to return to the title on
        }
    }

    public void DisableMainMenuWarning(string scene) //Optimized Method capable of use in buttons
    {
        MainMenuWarning(false);
        LoadScene(scene);
    }
    static public void EnableMenu(GameObject obj) //Optimized Method capable of use in buttons
    {
        if (obj != null)
        {
            ToggleMenu(obj, true);
        }
    }

    static public void DisableMenu(GameObject obj) //Optimized Method capable of use in buttons
    {
        if (obj != null)
        {
            ToggleMenu(obj, false);
        }
    }

    static public void ToggleMenu(GameObject obj, bool active = true) //Method to toggle the visibility of menus
    {
        ToggleInput(active);
        if (obj != null)
        {
            obj.SetActive(active);
        }
    }

    static public bool IsPaused()
    {
        return (Time.timeScale == 0);
    }

    static public void PauseGame()
    {
        Time.timeScale = 0f; //Everything that's affected is essentially stopped
    }

    static public void UnpauseGame()
    {
        Time.timeScale = 1f; //Everything that's affected runs at normal speed
    }

    static public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    static public void QuitGame()
    {
        Application.Quit();
    }

    static public void ToggleInput(bool active) //Method to toggle the ability to use keybinds
    {
        inputDisabled = active;
    }
}
