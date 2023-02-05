using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ControllState
{
    None,
    ChooseList,
    DirectArrow,
}

[RequireComponent(typeof(ChooseList), typeof(DirectArrow), typeof(InputHandler))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private ControllState state = ControllState.None;

    private ChooseList chooseList;
    private DirectArrow directArrow;
    public IController currentController;

    public void Init(int x, int y)
    {
        SwitchTo(ControllState.ChooseList);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        chooseList = GetComponent<ChooseList>();
        directArrow = GetComponent<DirectArrow>();
        SwitchTo(ControllState.ChooseList);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Handle(Direction direct)
    {
        currentController?.Handle(direct);
    }

    public void Confirm()
    {
        currentController?.Confirm();
    }

    public void SwitchTo(ControllState state)
    {
        //Debug.Log($"[{state.ToString()}]");
        this.state = state;
        if (currentController != null)
        {
            currentController.OnExit();
        }

        switch (state)
        {
            case ControllState.ChooseList:
                currentController = chooseList;
                break;
            case ControllState.DirectArrow:
                currentController = directArrow;
                break;
            default:
                currentController = null;
                break;
        }
        if (currentController != null)
        {
            currentController.OnEnter();
        }
    }

    public void MoveTo()
    {
        Vector3 headPos = GridManager.Instance.GetPos(GameManager.Instance.Player.HeadGrid);
        Vector3 directToMove = headPos - gameObject.transform.position;
        gameObject.transform.position += directToMove;
    }

}