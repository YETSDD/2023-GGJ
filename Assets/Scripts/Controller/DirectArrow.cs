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
        foreach (var pair in GridManager.Instance.DirectVector)
        {
            var grid = GridManager.Instance.GetGrid(x + pair.Value.x, y + pair.Value.y);
            if (grid == null)
            {
                cancelDirection.Add(pair.Key);
                continue;
            }
            else
            {
                if (!grid.CanTrigger() || grid.Owner == GameManager.Instance.Player)
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
        if (cancelDirection.Contains(currentDirection))
        {
            currentDirection = Direction.None;
        }
        OnRollOver();
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
        OnRollOut();
        currentDirection = direct;
        OnRollOver();
    }

    public void Confirm()
    {
        OnRollOut();
        if (currentDirection != Direction.None && !cancelDirection.Contains(currentDirection))
        {
            TryMoveTo(currentDirection);
            Controller.SwitchTo(ControllState.None);
        }
        else
        {
            Controller.SwitchTo(ControllState.ChooseList);
        }
    }

    private void OnRollOver()
    {
        if (currentDirection != Direction.None)
        {
            ArrowObject[currentDirection].GetComponent<SpriteRenderer>().ChangeV(1);
        }
    }

    private void OnRollOut()
    {
        if (currentDirection != Direction.None)
        {
            ArrowObject[currentDirection].GetComponent<SpriteRenderer>().ChangeV(0.5f);
        }
    }
    private void TryMoveTo(Direction direct)
    {
        var player = GameManager.Instance.Player;
        //var currentGrid = player.Body.Head.Data;
        //var currentX = currentGrid.PosX;
        //var currentY = currentGrid.PosY;
        //var targetGrid = GridManager.Instance.GetGrid(currentX + vec.x, currentY + vec.y);
        //if (targetGrid == null)
        //{
        //    Debug.LogWarning($"Invalid Index {currentX + vec.x}, {currentY + vec.y}");
        //    return;
        //}

        var moveSucess = player.TryMoveTo(direct);

        if (moveSucess)
        {
            Controller.MoveTo();
        }

        GameManager.Instance.NextRound();
    }
}
