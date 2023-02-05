using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Transform HpBar;
    public GameObject Five;
    public GameObject One;

    public GameObject Hint;
    public Image Eye;
    public Image emoji;
    public Image mark;

    public Sprite EmptyX;
    public Sprite HalfX;
    public Sprite FullX;

    public Text Tree;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }
    }

    void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            var player = GameManager.Instance.Player;
            UpdateHp(player.Life);
            UpdateMark(2 - player.RemainChanceToDie);
            UpdateTree();
            Eye.gameObject.SetActive(player.LookCount > 0);
        }
    }
    public void Menu()
    {
        HpBar.gameObject.SetActive(false);
        Hint.SetActive(true);
        mark.gameObject.SetActive(false);
    }

    public void Gaming()
    {
        HpBar.gameObject.SetActive(true);
        Hint.SetActive(true);
        mark.gameObject.SetActive(true);
    }

    public void Result()
    {
        HpBar.gameObject.SetActive(false);
        Hint.SetActive(false);
        mark.gameObject.SetActive(false);
    }

    public void UpdateMark(int count)
    {
        if (count == 0)
        {
            mark.sprite = EmptyX;
        }
        else if (count == 1)
        {
            mark.sprite = HalfX;

        }
        else if (count == 2)
        {
            mark.sprite = FullX;
        }

    }

    public void UpdateHp(int value)
    {
        ClearHp();
        int fiveValue = value / 5;
        int oneValue = value % 5;

        for (int i = 0; i < fiveValue; i++)
        {
            Instantiate(Five, HpBar);
        }

        for (int i = 0; i < oneValue; i++)
        {
            Instantiate(One, HpBar);
        }
    }

    private void ClearHp()
    {
        for (int i = 0; i < HpBar.childCount; i++)
        {
            Destroy(HpBar.GetChild(i).gameObject);
        }
    }

    private void UpdateTree()
    {
        var player = GameManager.Instance.Player;
        Tree.text = string.Empty;
        //Queue<CustomNode<GridBase>> q = new Queue<CustomNode<GridBase>>();
        //q.Enqueue(player.Body.Head);
        //int depth = 0
        //while (q.Count > 0)
        //{
        //    var 

        //}
        DrawTree(player.Body.Head);
    }

    private void DrawTree(CustomNode<GridBase> root)
    {
        if (root.Pres.Count == 0) { Tree.text += "*"; return; }
        if (root.Pres.Count == 1) { DrawTree(root.Pres[0]); return; }

        //Tree.text += "||\r\n";
        Tree.text += "\r\n|\r\n";
        //Tree.text += "|\r\n";
        int index = 0;
        foreach (var pre in root.Pres)
        {
            //if (index == root.Pres.Count - 1)
            //{
            //    Tree.text += "*";
            //}
            //else
            //{
            //    Tree.text += "*-----";

            //}

            DrawTree(pre);
            index++;
        }
    }
}
