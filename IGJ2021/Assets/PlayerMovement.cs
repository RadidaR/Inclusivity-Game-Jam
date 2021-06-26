using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCam;

    PlayerInput playerInput;

    bool isMoving;


    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Game.MouseClick.performed += ctx => Clicked();
    }

    void Clicked()
    {
        if (!isMoving)
        {
            Vector2 clickedSpot = mainCam.ScreenToWorldPoint(playerInput.Game.MousePosition.ReadValue<Vector2>());
            StartCoroutine(Move(clickedSpot));
        }


        //if (Physics2D.OverlapCircle.)
    }

    IEnumerator Move(Vector2 location)
    {
        Vector2 initialPosition = transform.position;
        for (float i = 0; i <= 1; i += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            Vector2 newPosition = new Vector2(Mathf.Lerp(initialPosition.x, location.x, i), Mathf.Lerp(initialPosition.y, location.y, i));
            transform.position = newPosition;
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
}
