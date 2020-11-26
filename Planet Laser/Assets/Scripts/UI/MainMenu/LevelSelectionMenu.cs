using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    public MainMenu mainMenu = null;
    public GameObject startMenu = null;

    void Awake()
    {
        if(!mainMenu)
        {
            mainMenu = transform.GetComponentInParent<MainMenu>();
        }
    }

    public void CloseLevelSelection()
    {
        mainMenu.TurnAllMenusOffExcept(startMenu);
    }
}
