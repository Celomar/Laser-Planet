using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] menus = null;

    void Awake()
    {
        if(menus == null || menus.Length < 1) PopulateMenusListFromChildren();
        Debug.Assert(menus.Length > 0, "There are no menus for main menu");
        TurnAllMenusOffExcept(menus[0]);
    }

    private void PopulateMenusListFromChildren()
    {
        int childCount = transform.childCount;
        menus = new GameObject[childCount];
        for(int i = 0; i < childCount; i++)
        {
            menus[i] = transform.GetChild(i).gameObject;
        }
    }

    public void TurnAllMenusOffExcept(GameObject targetMenu)
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(menu == targetMenu);
        }
    }
}
