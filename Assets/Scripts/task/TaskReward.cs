using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class TaskReward
{
    public string id;//奖励id
    public string name;
    public int amount = 0;//奖励数量

    public TaskReward(string id, string name, int amount)
    {
        this.id = id;
        this.name = name;
        this.amount = amount;
    }
}
