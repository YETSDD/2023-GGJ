using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectArrow : MonoBehaviour, IController
{
    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Down;
    private Dictionary<Direction, GameObject> ArrowObject;

    readonly Dictionary<Direction, Vector2Int> directVector = new Dictionary<Direction, Vector2Int>() { { Direction.Left, new Vector2Int(-1, 0) }, { Direction.Right, new Vector2Int(1, 0) }, { Direction.Up, new Vector2Int(0, 1) }, { Direction.Down, new Vector2Int(0, -1) } };
    public PlayerController Controller => GetComponent<PlayerController>();

    private Direction currentDirection;
    private List<Direction> cancelDirection;

    private void Awake()
    {
        ArrowObject = new Dictionary<Direction, GameObject>();
        ArrowObject.Add(Direction.Left, Left);
        ArrowObject.Add(Direction.Right, Right);
        ArrowObject.Add(Direction.Up, Up);
        ArrowObject.Add(Direction.Down, Down);

        cancelDirection = new List<Direction>();

    }
    public void OnEnter()
    {
        cancelDirection.Clear();

        int x = GameManager.Instance.Player.HeadGrid.PosX;
        int y = GameManager.Instance.Player.HeadGrid.PosY;
        foreach (var pair in directVector)
        {
            var grid = GridManager.Instance.GetGrid(x + pair.Value.x, y + pair.Value.y);
            if (grid == null)
            {
                cancelDirection.Add(pair.Key);
                continue;
            }
            else
            {
                //TODO ≥‘»À
                if (grid.Owner != null)
                {
                    cancelDirection.Add(pair.Key);
                    continue;
                }
            }
        }

        foreach (var obj in ArrowObject)
        {
            obj.Value.SetActive(true);
        }

        foreach (var cancel in cancelDirection)
        {
            ArrowObject[cancel].SetActive(false);
        }
    }
    public void OnExit()
    {
        foreach (var obj in ArrowObject)
        {
            obj.Value.SetActive(false);
        }
    }
    public void Handle(Direction direct)
    {
        currentDirection = direct;
    }

    public void Confirm()
    {
        if (!cancelDirection.Contains(currentDirection))
        {
            TryMoveTo(directVector[currentDirection]);
        }

        Controller.SwitchTo(ControllState.ChooseList);

    }

    private void TryMoveTo(Vector2Int vec)
    {
        var player = GameManager.Instance.Player;
        var currentGrid = player.Body.Head.Data;
        var currentX = currentGrid.PosX;
        var currentY = currentGrid.PosY;
        var targetGrid = GridManager.Instance.GetGrid(currentX + vec.x, currentY + vec.y);
        if (targetGrid == null)
        {
            Debug.LogWarning($"Invalid Index {currentX + vec.x}, {currentY + vec.y}");
            return;
        }

        var canMove = player.TryMoveTo(targetGrid);

        if (canMove)
        {
            Controller.MoveTo(vec);
        }

        GameManager.Instance.NextRound();
    }
}
