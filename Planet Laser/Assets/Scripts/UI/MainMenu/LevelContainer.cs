using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelContainer : MonoBehaviour
{
    public LevelsGrid levelsGridPrefab = null;

    void Awake()
    {
        SetupGrid();
    }

    private void SetupGrid()
    {
        RemoveGrids();
        int levelCount = SceneManager.sceneCountInBuildSettings - 1;
        int gridCount = Mathf.CeilToInt((float)levelCount / LevelsGrid.LEVELS_PER_GRID);

        for(int i = 0; i < gridCount; i++)
        {
            LevelsGrid grid = Instantiate<LevelsGrid>(levelsGridPrefab, transform);
            int start = i * LevelsGrid.LEVELS_PER_GRID;
            int count = (levelCount - start) % LevelsGrid.LEVELS_PER_GRID;
            grid.SetupButtons(start, count, count);
        }
    }

    private void RemoveGrids()
    {
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.name.Contains(levelsGridPrefab.name)) Destroy(child);
        }
    }
}
