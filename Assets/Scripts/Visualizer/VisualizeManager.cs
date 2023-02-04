using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可视化格子 滑动窗口
/// </summary>
public class VisualizeManager : MonoBehaviour
{
    public static VisualizeManager Instance;
    public GameObject GridObject;
    public Transform GeneratePoint;

    private int midX;
    private int midY;

    const int width = 11;
    const int height = 11;

    public void Initialize(int x, int y)
    {
        UpdateGrid(x, y);
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
    }

    public void UpdateGrid(int x, int y)
    {
        midX = x;
        midY = y;
        var allActive = gridPool.FindAll(item => item.activeInHierarchy);
        foreach (var obj in allActive)
        {
            if (!InScreen(obj))
            {
                RecycleObject(obj);
            }
        }

        for (int i = 0; i < width; i++)
        {
            var offsetX = i - width / 2;

            for (int j = 0; j < height; j++)
            {
                var offsetY = j - height / 2;
                var grid = GridManager.Instance.GetGrid(midX + offsetX, midY + offsetY);
                if (grid != null)
                {
                    var gameObj = GetGridObject();
                    var worldPos = GridManager.Instance.GetPos(grid);
                    gameObj.transform.localPosition = worldPos;
                    var visualizer = gameObj.GetComponent<GridVisualizer>();
                    visualizer.Refresh();
                    visualizer.Show(grid);
                }
            }
        }
    }

    private bool InScreen(GameObject obj)
    {
        var midGrid = GridManager.Instance.GetGrid(midX, midY);
        if (midGrid == null) return false;

        var midPos = GridManager.Instance.GetPos(midGrid);
        var objPos = obj.transform.position;

        //以步数算距离
        if (Mathf.Abs(midPos.x - objPos.x) + Mathf.Abs(midPos.y - objPos.y) > width / 2 + height / 2)
        {
            return false;
        }
        return false;
    }
    #region Pool
    private List<GameObject> gridPool = new List<GameObject>();
    private GameObject GetGridObject()
    {
        var result = gridPool.Find(item => !item.activeInHierarchy);
        if (result == null)
        {
            result = CreateObject();
        }
        result.SetActive(true);
        return result;
    }
    private GameObject CreateObject()
    {
        var obj = GameObject.Instantiate(GridObject, GeneratePoint);
        gridPool.Add(obj);
        return obj;
    }

    private void RecycleObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion
}
