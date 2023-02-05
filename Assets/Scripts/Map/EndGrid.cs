using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGrid : GridBase
{
    int count = 3;

    public EndGrid(int x, int y) : base(x, y)
    {
        BaseColor = Color.gray;
        Movable = false;
        AvailableTriggerTimes = 5;
    }

    public override void Trigger(EntityBase entity)
    {
        //ÓÐÖ÷ÁË
        if (this.Owner != null) { return; }
        base.Trigger(entity);

        count--;
        Color.RGBToHSV(BaseColor, out var h, out var s, out var v);
        v += 0.2f;
        v = Mathf.Clamp01(v);
        BaseColor = Color.HSVToRGB(h, s, v);

        if (count <= 0)
        {
            if (entity is Player player)
            {
                GridManager.Instance.ChangeGrid(PosX, PosY, GridType.Soil);
                player.Rooted = true;
                GridManager.Instance.EndGrids.Remove(this);
                if (player == GameManager.Instance.Player)
                {
                    GameManager.Instance.NewGeneration(this);
                }
                else
                {
                    AgentManager.Instance.AddAgent(this);
                }
            }
        }
    }
}
