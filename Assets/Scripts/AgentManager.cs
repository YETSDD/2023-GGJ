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
    public AgentManager()
    {
        Controllers = new List<AgentController>();
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

    public void UpdateAgent()
    {
        //TODO ¾ö¶¨Ë³Ðò
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
