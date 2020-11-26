using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public MainMenu mainMenu = null;
    public GameObject levelSelectMenu = null;

    void Awake()
    {
        if(!mainMenu)
        {
            mainMenu = transform.GetComponentInParent<MainMenu>();
        }
    }

    public void Play()
    {
        mainMenu.TurnAllMenusOffExcept(levelSelectMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
