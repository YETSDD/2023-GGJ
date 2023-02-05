using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager
{
    private static AgentManager instance;
    public static AgentManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AgentManager();
            }
            return instance;
        }
    }

    public List<AgentController> Controllers;

    public List<AgentController> AgentToAdd;
    public AgentManager()
    {
        Controllers = new List<AgentController>();
        AgentToAdd = new List<AgentController>();
    }

    public void Initialize()
    {
        Controllers.Clear();
        for (int i = 0; i < GameManager.Instance.AgentCount; i++)
        {
            var chooseGrid = GridManager.Instance.GetRandomGeneratePoint();
            Controllers.Add(new AgentController(chooseGrid));
        }
    }

    public List<Player> GetAllAgents()
    {
        var result = new List<Player>();
        foreach (var controller in Controllers)
        {
            result.Add(controller.Player);
        }
        return result;
    }
    public void AddAgent(GridBase grid)
    {
        //var canGenerateGrid = GameManager.GetGenerateGrids(grid);
        //if (canGenerateGrid.Count == 0)
        //{
        //    Debug.Log($"----Generate Fail----");
        //    return;
        //}

        //var random = Random.Range(0, canGenerateGrid.Count);
        //var choosenGrid = canGenerateGrid[random];
        var choosenGrid = grid;

        var startPosX = choosenGrid.PosX;
        var startPosY = choosenGrid.PosY;
        Debug.Log($"----Generate New Agent At({startPosX},{startPosY})----");

        var newController = new AgentController(choosenGrid);
        AgentToAdd.Add(newController);
        GameManager.Instance.AllPlayer.Add(newController.Player);
    }

    public void UpdateAgent()
    {
        //TODO ¾ö¶¨Ë³Ðò

        foreach (var agent in AgentToAdd)
        {
            Controllers.Add(agent);
        }
        AgentToAdd.Clear();

        foreach (var controller in Controllers)
        {
            var agent = controller.Player;
            if (agent != null && agent.Alive)
            {
                controller.Act();
            }
        }
    }

    public void ClearAll()
    {
        Controllers.Clear();
    }
}
