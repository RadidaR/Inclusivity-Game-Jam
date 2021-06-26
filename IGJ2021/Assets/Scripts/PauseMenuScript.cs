using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    PlayerInput playerInput;

    bool gamePaused = false;
    public GameObject pauseMenu;


    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Game.Escape.performed += ctx => PauseUnpause();
    }

    public void PauseUnpause()
    {
        if (gamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            gamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            gamePaused = true;
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
