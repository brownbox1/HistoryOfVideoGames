using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerPM : MonoBehaviour
{

    public GameObject startNode;
    
    public GameManagerPM gameManager;
    public Vector2 startPos;

    public MovementControllerPM movementController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startPos = new Vector2(-0.05f, -0.065f);
        movementController = GetComponent<MovementControllerPM>();
        gameManager = GameObject.Find("GameManagerPM").GetComponent<GameManagerPM>();
        startNode = movementController.currentNode;
    }

    public void Setup()
    {
        movementController.currentNode = startNode;
        movementController.lastMovingDirection = "left";
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameIsRunning)
        {
            return;
        }

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