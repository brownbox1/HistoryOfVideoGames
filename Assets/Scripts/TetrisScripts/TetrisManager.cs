using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisManager : MonoBehaviour
{
    public static float dropTime = 0.9f;
    public static float quickDropTime = 0.05f;
    public static int width = 10, height = 20;
    public GameObject[] blocks;
    public Transform[,] grid = new Transform[width,height];
    public GameObject dotMatrix;


   void Start()
   {
        SpawnBlock();
        VisualGrid();
   }

   void Update()
   {

   }

   public void SpawnBlock()
   {
    int guess = Random.Range(0, blocks.Length);
    Instantiate(blocks[guess], new Vector3(4.5f, 20.5f, -1), Quaternion.identity);
   }

   void VisualGrid()
   {
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            GameObject dot = Instantiate(dotMatrix, new Vector3(x+0.5f, y + 0.5f, -0.5f), Quaternion.identity);
            dot.transform.parent = this.transform;
        }
    }
   }

   public void CheckForLines()
   {
    for (int row = 0; row < height; row++)
    {
        if (IsRowFilled(row))
        {
            deleteRow(row);
            MoveRowsDown(row+1);
            row--;
        }
    }
   }
   
   bool IsRowFilled(int row)
   {
    for (int col = 0; col < width; col++)
    {
        if (grid[col, row] == null)
        {
            return false;
        }
    }
        return true;
   }

   void deleteRow(int row)
   {
    for (int col = 0; col < width; col++)
    {
        Destroy(grid[col, row].gameObject);
        grid[col, row] = null;
    }
   }

   void MoveRowsDown(int startRow)
   {
    for (int row = startRow; row < height; row++)
    {
        for (int col = 0; col < width; col++)
        {
            if (grid[col, row] != null)
            {
                grid[col, row - 1] = grid[col, row];
                grid[col, row] = null;

                grid[col, row-1].position += new Vector3(0, -1, 0);
            }
        }
    }
   }
}
