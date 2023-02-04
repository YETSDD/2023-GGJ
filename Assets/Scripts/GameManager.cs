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
    public Cinemachine.CinemachineVirtualCamera Camera;
    int sizeX = 100;
    int sizeY = 100;
    [HideInInspector]
    public int round;

    public Player Player;

    public List<EntityBase> AllEntities;

    State state = State.Menu;
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
        AllEntities = new List<EntityBase>();
    }

    void Start()
    {
        StartPosX = sizeX / 2;
        StartPosY = sizeY / 2;
        GridManager.Instance.Initialize(sizeX, sizeY);
        VisualizeManager.Instance.Initialize(sizeX / 2, sizeY / 2);
    }

    void Update()
    {

        if (state == State.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        AllEntities.Clear();
        round = 0;
        state = State.Gaming;
        Player = new Player(GridManager.Instance.GetGrid(StartPosX, StartPosY));
        AllEntities.Add(Player);
        AgentManager.Instance.Initialize();
        AllEntities.AddRange(AgentManager.Instance.GetAllAgents());
        PlayerController controller = GameObject.Instantiate(ControllerPrefab);
        Camera.Follow = controller.transform;
        controller.Init(sizeX / 2, sizeY / 2);
    }


    public void NextRound()
    {
        AgentManager.Instance.UpdateAgent();
        VisualizeManager.Instance.UpdateEntity();
        VisualizeManager.Instance.UpdateGrid(Player.HeadGrid.PosX, Player.HeadGrid.PosY);
        round++;
        Debug.Log("Round:" + round);

    }
}
