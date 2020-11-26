using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct GridCell
{
    public Button button;
    public TMP_Text text;
}

public class LevelsGrid : MonoBehaviour
{
    public const int LEVELS_PER_GRID = 4 * 3;
    private GridCell[] buttons = new GridCell[LEVELS_PER_GRID];

    void Awake()
    {
        FindButtons();
    }

    private void FindButtons()
    {
        int rowCount = transform.childCount;
        for(int i = 0; i < rowCount; i++)
        {
            Transform row = transform.GetChild(i);
            int columnCount = row.childCount;
            for(int j = 0; j < columnCount; j++)
            {
                Transform cell = row.GetChild(j);
                
                GridCell btn;
                btn.button = cell.GetComponentInChildren<Button>();
                btn.text = cell.GetComponentInChildren<TMP_Text>();

                int index = i * columnCount + j;
                buttons[ index ] = btn;
            }
        }
    }

    public void SetupButtons(int start, int playable)
    {
        for(int i = 0; i < LEVELS_PER_GRID; i++)
        {
            GridCell cell = buttons[i];
            Debug.Assert(cell.button, "button is null");
            Debug.Assert(cell.text, "text is null");
            
            int level = start + i + 1;
            cell.button.gameObject.SetActive(i < playable);
            cell.button.onClick.RemoveAllListeners();
            cell.button.onClick.AddListener( delegate() { LoadLevel(level); } );

            cell.text.text = level.ToString();
        }
    }

    private void LoadLevel(int level)
    {
        Debug.Log(level);
    }
}