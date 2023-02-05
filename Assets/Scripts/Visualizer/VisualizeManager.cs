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
    public GameObject BodyObject;
    public GameObject LineObject;
    public GameObject HeadObject;

    public Transform GridGeneratePoint;
    public Transform BodyGeneratePoint;

    private int midX;
    private int midY;

    const int baseSizeX = 11;
    const int baseSizeY = 11;

    public void SetSize(int x = baseSizeX, int y = baseSizeY)
    {
        width = x;
        height = y;
    }
    private int width = 11;
    private int height = 11;

    public void Initialize(int x, int y)
    {
        UpdateEntity();
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

    public void UpdateEntity()
    {
        bodyPool.ForEach(item => item.SetActive(false));
        linePool.ForEach(item => item.SetActive(false));
        headPool.ForEach(item => item.SetActive(false));
        foreach (var entity in GameManager.Instance.AllPlayer)
        {
            var head = entity.Body.Head;
            //判断成环 TODO
            Queue<CustomNode<GridBase>> q = new Queue<CustomNode<GridBase>>();
            q.Enqueue(head);
            //GameObject preBody;
            while (q.Count > 0)
            {
                var node = q.Dequeue();
                foreach (var pre in node.Pres)
                {
                    q.Enqueue(pre);
                }
                if (node.Data != null)
                {
                    if (node.Data.Owner != null)
                    {
                        if (InScreen(node.Data))
                        {
                            var worldPos = GridManager.Instance.GetPos(node.Data);

                            //转向 
                            var angle = node.GetDirection();
                            if (angle == -1)
                            {
                                //特殊处理 用圆形
                                var headObj = GetHeadObject();
                                headObj.transform.position = worldPos;
                            }
                            else
                            {
                                var body = GetBodyObject();
                                body.transform.position = worldPos;
                                body.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                                if (!entity.Alive)
                                {
                                    body.GetComponent<SpriteRenderer>().color = Color.black;
                                }
                                else
                                {
                                    body.GetComponent<SpriteRenderer>().color = Color.green;
                                }
                            }

                            foreach (var pre in node.Pres)
                            {
                                if (pre.Data != null && InScreen(pre.Data))
                                {
                                    var line = GetLineObject().GetComponent<LineRenderer>();
                                    line.positionCount = 2;
                                    var start = GridManager.Instance.GetPos(pre.Data);
                                    line.SetPositions(new Vector3[2] { start, worldPos });
                                    if (!entity.Alive)
                                    {
                                        line.startColor = Color.black;
                                    }
                                    else
                                    {
                                        line.startColor = Color.green;

                                    }
                                }
                            }

                        }

                    }
                }
            }
        }
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

    public void ClearAll()
    {
        bodyPool.ForEach(item => item.SetActive(false));
        linePool.ForEach(item => item.SetActive(false));
        headPool.ForEach(item => item.SetActive(false));
    }

    private bool InScreen(GridBase grid)
    {
        var worldPos = GridManager.Instance.GetPos(grid);
        var midGrid = GridManager.Instance.GetGrid(midX, midY);
        if (midGrid == null) return false;

        var midPos = GridManager.Instance.GetPos(midGrid);

        //以步数算距离
        if (Mathf.Abs(midPos.x - worldPos.x) > width / 2 || Mathf.Abs(midPos.y - worldPos.y) > height / 2)
        {
            return false;
        }
        return true;
    }

    private bool InScreen(GameObject obj)
    {
        var midGrid = GridManager.Instance.GetGrid(midX, midY);
        if (midGrid == null) return false;

        var midPos = GridManager.Instance.GetPos(midGrid);
        var objPos = obj.transform.position;

        //以步数算距离
        if (Mathf.Abs(midPos.x - objPos.x) > width / 2 || Mathf.Abs(midPos.y - objPos.y) > +height / 2)
        {
            return false;
        }
        return true;
    }
    #region GridPool
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
        var obj = GameObject.Instantiate(GridObject, GridGeneratePoint);
        gridPool.Add(obj);
        return obj;
    }

    private void RecycleObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion

    #region bodyPool
    private List<GameObject> bodyPool = new List<GameObject>();
    private GameObject GetBodyObject()
    {
        var result = bodyPool.Find(item => !item.activeInHierarchy);
        if (result == null)
        {
            result = CreateBodyObject();
        }
        result.SetActive(true);
        return result;
    }
    private GameObject CreateBodyObject()
    {
        var obj = GameObject.Instantiate(BodyObject, BodyGeneratePoint);
        bodyPool.Add(obj);
        return obj;
    }
    #endregion
    #region headPool
    private List<GameObject> headPool = new List<GameObject>();
    private GameObject GetHeadObject()
    {
        var result = headPool.Find(item => !item.activeInHierarchy);
        if (result == null)
        {
            result = CreateHeadObject();
        }
        result.SetActive(true);
        return result;
    }
    private GameObject CreateHeadObject()
    {
        var obj = GameObject.Instantiate(HeadObject, BodyGeneratePoint);
        headPool.Add(obj);
        return obj;
    }
    #endregion
    #region linePool
    private List<GameObject> linePool = new List<GameObject>();
    private GameObject GetLineObject()
    {
        var result = linePool.Find(item => !item.activeInHierarchy);
        if (result == null)
        {
            result = CreateLineObject();
        }
        result.SetActive(true);
        return result;
    }
    private GameObject CreateLineObject()
    {
        var obj = GameObject.Instantiate(LineObject, BodyGeneratePoint);
        linePool.Add(obj);
        return obj;
    }
    #endregion
}
