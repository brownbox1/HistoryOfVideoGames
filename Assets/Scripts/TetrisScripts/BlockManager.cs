using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BlockManager : MonoBehaviour
{
    float timer = 0f;
    bool movable = true;
    public TetrisManager tetrisManager;
    
    void Start()
    {
        tetrisManager = Object.FindFirstObjectByType<TetrisManager>();
    }
    void RegisterBlock()
    {
        foreach (Transform subBlock in transform)
        {
            tetrisManager.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] = subBlock;
        }
    }

    bool CheckValid()
    {
        foreach (Transform subBlock in transform)
        {
            int x = Mathf.FloorToInt(subBlock.position.x);
            int y = Mathf.FloorToInt(subBlock.position.y);
            
            if(x >= TetrisManager.width || x < 0 || y < 0)
            {
                return false;
            }
            if (y < TetrisManager.height && tetrisManager.grid[x,y] != null)
            {
                return false;
            }
        }
        return true;
    }

    void LockPieceInGrid()
    {
        foreach (Transform subBlock in transform)
        {
            int x = Mathf.FloorToInt(subBlock.position.x);
            int y = Mathf.FloorToInt(subBlock.position.y);
            tetrisManager.grid[x, y] = subBlock;
        }
        tetrisManager.CheckForLines();
        tetrisManager.SpawnBlock();
        this.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (movable)
        {
            var keyboard = Keyboard.current;
        if (keyboard == null) return;
        timer += 1 * Time.deltaTime;

        // natural drop
        if (keyboard.downArrowKey.isPressed && timer > TetrisManager.quickDropTime)
        {
            gameObject.transform.position -= new Vector3(0, 1, 0);
            timer = 0;
            if (!CheckValid())
            {
               movable = false;
               gameObject.transform.position += new Vector3(0, 1, 0); 
               LockPieceInGrid();
            }
        }
        else if (timer > TetrisManager.dropTime)
        {
            gameObject.transform.position -= new Vector3(0, 1, 0);
            timer = 0;
            if (!CheckValid())
            {
               movable = false;
               gameObject.transform.position += new Vector3(0, 1, 0); 
               LockPieceInGrid();
            }
        }

        else if (keyboard.leftArrowKey.wasPressedThisFrame)
        {
            gameObject.transform.position -= new Vector3(1, 0, 0);
            if (!CheckValid())
            {
               gameObject.transform.position += new Vector3(1, 0, 0); 
            }
        }
        else if (keyboard.rightArrowKey.wasPressedThisFrame)
        {
            gameObject.transform.position += new Vector3(1, 0, 0);
            if (!CheckValid())
            {
               gameObject.transform.position -= new Vector3(1, 0, 0); 
            }
        }
        else if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            gameObject.transform.eulerAngles -= new Vector3(0, 0, 90);
            if (!CheckValid())
            {
               gameObject.transform.eulerAngles += new Vector3(0, 0, 90); 
            }
        }
        if (keyboard.escapeKey.isPressed)
        {
            SceneManager.LoadScene("MainMenu");
        }
        }
        
    }

}
