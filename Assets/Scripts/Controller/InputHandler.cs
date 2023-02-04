using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerController Controller => GetComponent<PlayerController>();
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Handle(Direction.Up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Handle(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Handle(Direction.Down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Handle(Direction.Right);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Confirm();
        }
    }

    public void Handle(Direction direct)
    {
        Controller.Handle(direct);
    }

    public void Confirm()
    {
        Controller.Confirm();
    }
}
