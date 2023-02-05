using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Menu,
    Gaming,
    Result
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController ControllerPrefab;
    public Transform EntityParent;
    public Cinemachine.CinemachineVirtualCamera VirtualCamera1;
    public Cinemachine.CinemachineVirtualCamera VirtualCamera2;
    public int sizeX = 100;
    public int sizeY = 100;

    public List<GenerateModel> GenerateModels;

    public int AgentCount;
    [HideInInspector]
    public int round;

    private PlayerController controller;
    public Player Player;

    public List<Player> AllPlayer;

    public State State = State.Menu;
    public int StartPosX;
    public int StartPosY;
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
        AllPlayer = new List<Player>();
    }

    void Start()
    {
        StartPosX = sizeX / 2;
        StartPosY = sizeY / 2;
        GridManager.Instance.Initialize(sizeX, sizeY);
        VisualizeManager.Instance.Initialize(StartPosX, StartPosY);
    }

    int waitFrame = 60;
    void Update()
    {
        if (State == State.Menu)
        {

            UIManager.Instance.Menu();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
        else if (State == State.Gaming)
        {
            UIManager.Instance.Gaming();
        }
        else if (State == State.Result)
        {
            UIManager.Instance.Result();
            if (waitFrame > 0) { waitFrame--; }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Clear();
                    State = State.Menu;
                    waitFrame = 60;
                }
            }
        }
    }


    public void StartGame()
    {
        AllPlayer.Clear();
        round = 0;
        State = State.Gaming;
        Player = new Player(GridManager.Instance.GetGrid(StartPosX, StartPosY));
        AllPlayer.Add(Player);
        AgentManager.Instance.Initialize();
        AllPlayer.AddRange(AgentManager.Instance.GetAllAgents());
        controller = GameObject.Instantiate(ControllerPrefab);
        //VirtualCamera1.m_Lens.OrthographicSize = 5;

        VirtualCamera1.gameObject.SetActive(true);
        VirtualCamera2.gameObject.SetActive(false);
        VirtualCamera1.Follow = controller.transform;
        VirtualCamera2.Follow = controller.transform;
        controller.Init(StartPosX, StartPosX);
        VisualizeManager.Instance.SetSize();
        VisualizeManager.Instance.UpdateEntity();
        VisualizeManager.Instance.UpdateGrid(StartPosX, StartPosY);
    }


    public void NextRound()
    {
        AgentManager.Instance.UpdateAgent();

        if (CheckDead()) { GameOver(); return; }
        VisualizeManager.Instance.UpdateEntity();
        VisualizeManager.Instance.UpdateGrid(Player.HeadGrid.PosX, Player.HeadGrid.PosY);
        round++;
        Debug.Log("Round:" + round);

        //应该做成listener 等待完成
        StartCoroutine(ControllerActivate());
    }

    IEnumerator ControllerActivate()
    {
        yield return new WaitForEndOfFrame();
        controller.SwitchTo(ControllState.ChooseList);

    }
    private bool CheckDead()
    {
        if (!Player.Alive || Player.Life <= 0) { return true; }
        int x = GameManager.Instance.Player.HeadGrid.PosX;
        int y = GameManager.Instance.Player.HeadGrid.PosY;
        int stuckDirectCount = 0;
        foreach (var pair in GridManager.Instance.DirectVector)
        {
            var grid = GridManager.Instance.GetGrid(x + pair.Value.x, y + pair.Value.y);
            if (grid == null)
            {
                stuckDirectCount++;
                continue;
            }
            else
            {
                //TODO 吃人
                if (!grid.CanTrigger() || grid.Owner == GameManager.Instance.Player)
                {
                    stuckDirectCount++;
                    continue;
                }
            }
        }
        if (stuckDirectCount == 4) { return true; }
        return false;
    }

    public void GameOver()
    {
        //TODO 处理
        VisualizeManager.Instance.SetSize(50, 50);
        VisualizeManager.Instance.UpdateEntity();
        VisualizeManager.Instance.UpdateGrid(Player.HeadGrid.PosX, Player.HeadGrid.PosY);
        VirtualCamera1.gameObject.SetActive(false);
        VirtualCamera2.gameObject.SetActive(true);
        //StartCoroutine(CameraUp(count))
        //VirtualCamera1.m_Lens.OrthographicSize = 20;
        State = State.Result;

        Debug.Log("GameOver");
    }

    IEnumerator CameraUp(int count)
    {
        yield return new WaitForEndOfFrame();

    }

    public bool ClearAll = false;
    private void Clear()
    {
        if (ClearAll)
        {
            GridManager.Instance.Initialize(sizeX, sizeY);
            VisualizeManager.Instance.Initialize(sizeX / 2, sizeY / 2);
        }
        Destroy(controller.gameObject);
        AgentManager.Instance.ClearAll();
        VisualizeManager.Instance.ClearAll();

        //TODO 处理
    }

    public void NewGeneration(GridBase grid)
    {
        //产生新的个体
        var gridPosX = grid.PosX;
        var gridPosY = grid.PosY;
        var canGenerateGrid = new List<GridBase>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                var temp = GridManager.Instance.GetGrid(gridPosX + i, gridPosY + j);
                if (temp != null && temp.Owner == null)
                {
                    canGenerateGrid.Add(temp);
                }
            }
        }

        if (canGenerateGrid.Count == 0)
        {
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    var temp = GridManager.Instance.GetGrid(gridPosX + i, gridPosY + j);
                    if (temp != null && temp.Owner == null)
                    {
                        canGenerateGrid.Add(temp);
                    }
                }
            }
        }
        if (canGenerateGrid.Count == 0)
        {
            Debug.Log($"----Generate Fail----");
            GameManager.Instance.GameOver();
            return;
        }

        var random = Random.Range(0, canGenerateGrid.Count);
        var choosenGrid = canGenerateGrid[random];

        var startPosX = choosenGrid.PosX;
        var startPosY = choosenGrid.PosY;
        Debug.Log($"----Generate New({startPosX},{startPosY})----");

        Player = new Player(GridManager.Instance.GetGrid(startPosX, startPosY));
        AllPlayer.Add(Player);

        VisualizeManager.Instance.Initialize(startPosX, startPosY);
        controller.MoveTo();
    }
}
