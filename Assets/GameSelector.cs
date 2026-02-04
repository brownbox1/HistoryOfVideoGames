using UnityEngine;

public class GameSelector : MonoBehaviour
{
    public GameObject selectionMenu;
    public GameObject tennisForTwoObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        selectionMenu.SetActive(false);
        tennisForTwoObjects.SetActive(true);
    }

    public void ShowMenu()
    {
        selectionMenu.SetActive(true);
        tennisForTwoObjects.SetActive(false);
    }
}
