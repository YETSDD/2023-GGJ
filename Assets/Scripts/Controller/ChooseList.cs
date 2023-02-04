using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left,
    Up,
    Down
}
public class ChooseList : MonoBehaviour, IController
{
    public PlayerController Controller => GetComponent<PlayerController>();

    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Down;
    private Dictionary<Direction, GameObject> ArrowObject;
    private GameObject selecting;

    public Dictionary<Direction, Action> InputMap;
    private Action actionToDo;
    void Awake()
    {
        ArrowObject = new Dictionary<Direction, GameObject>();
        ArrowObject.Add(Direction.Left, Left);
        ArrowObject.Add(Direction.Right, Right);
        ArrowObject.Add(Direction.Up, Up);
        ArrowObject.Add(Direction.Down, Down);

        InputMap = new Dictionary<Direction, Action>();
        InputMap.Add(Direction.Right, ToMove);
        InputMap.Add(Direction.Left, Look);
        InputMap.Add(Direction.Up, null);
        InputMap.Add(Direction.Down, null);
    }

    void Update()
    {

    }

    public void Handle(Direction direct)
    {
        OnRollOut();
        if (ArrowObject[direct] != null)
        {
            selecting = ArrowObject[direct];
            OnRollOver();
        }
        actionToDo = InputMap[direct];
    }

    private void OnRollOver()
    {
        var sprite = selecting.GetComponent<SpriteRenderer>();
        if (sprite != null) { sprite.ChangeV(50); }
    }

    private void OnRollOut()
    {
        if (selecting != null)
        {
            var sprite = selecting.GetComponent<SpriteRenderer>();
            if (sprite != null) { sprite.ChangeV(-50); }
        }
    }

    public void Confirm()
    {
        actionToDo?.Invoke();
        actionToDo = null;
    }

    public void OnEnter()
    {
        foreach (var obj in ArrowObject)
        {
            if (obj.Value != null)
                obj.Value.SetActive(true);
        }

        //Ä¬ÈÏÒÆ¶¯
        actionToDo = InputMap[Direction.Right];
        selecting = ArrowObject[Direction.Right];
        OnRollOver();
        ArrowObject[Direction.Right].SetActive(true);
        ArrowObject[Direction.Left].SetActive(true);
    }
    public void OnExit()
    {
        foreach (var obj in ArrowObject)
        {
            if (obj.Value != null)
                obj.Value.SetActive(false);
        }
        actionToDo = null;
        OnRollOut();
    }

    private void ToMove()
    {
        Controller.SwitchTo(ControllState.DirectArrow);
    }

    private bool lookLock = false;
    private void Look()
    {
        if (lookLock) { Debug.Log("Cannot Look"); return; }
        //TODO
        lookLock = true;
        GameManager.Instance.NextRound();
    }
}
