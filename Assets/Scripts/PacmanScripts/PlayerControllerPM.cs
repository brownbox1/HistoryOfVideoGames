using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerPM : MonoBehaviour
{

    public GameObject startNode;
    
    public Vector2 startPos;

    MovementControllerPM movementController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        movementController = GetComponent<MovementControllerPM>();

        startNode = movementController.currentNode;
    }

    public void Setup()
    {
        movementController.currentNode = startNode;
        movementController.lastMovingDirection = "left";
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;

        if (keyboard.leftArrowKey.isPressed)
        {
            movementController.SetDirection("left");
        }
        if (keyboard.rightArrowKey.isPressed)
        {
            movementController.SetDirection("right");
        }
        if (keyboard.upArrowKey.isPressed)
        {
            movementController.SetDirection("up");
        }
        if (keyboard.downArrowKey.isPressed)
        {
            movementController.SetDirection("down");
        }
    }
}