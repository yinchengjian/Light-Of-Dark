using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

public class TaskManager : MonoSingletion<TaskManager> {

    public Dictionary<string, Task> dictionary = new Dictionary<string,Task>();//id,task
    public XElement rootElement;

    public event EventHandler<TaskEventArgs> getEvent;//接受任务时,更新任务到任务面板等操作
    public event EventHandler<TaskEventArgs> finishEvent;//完成任务时,提示完成任务等操作
    public event EventHandler<TaskEventArgs> rewardEvent;//得到奖励时,显示获取的物品等操作
    public event EventHandler<TaskEventArgs> cancelEvent;//取消任务时,显示提示信息等操作

    void Awake()
    {
        MesManager.Instance.checkEvent += CheckTask;
        rootElement = XElement.Load(Application.dataPath + "/StreamingAssets/Task.xml");//得到根元素
    }

    public void GetTask(string taskID)
    {
        if (!dictionary.ContainsKey(taskID))
        {
            Task t = new Task(taskID);
            dictionary.Add(taskID, t);

            TaskEventArgs e = new TaskEventArgs();
            e.taskID = taskID;
            getEvent(this, e);
        }
    }
    public void LoadTask(Dictionary<string, Task> dictionary)
    {
        this.dictionary = dictionary;

        foreach (KeyValuePair<string, Task> kv in dictionary) {
            Task t = kv.Value;

            TaskEventArgs e = new TaskEventArgs();
            e.taskID = t.taskID;
            TaskPanel.Instance.LoadItem(e);

            foreach (TaskCondition tc in t.taskConditions)
            {
                e.id = tc.id;

                t.taskItem.Modify(e.id, tc.nowAmount);
            }
            for (int i = 0; i < t.taskConditions.Count; i++)
            {
                if (!t.taskConditions[i].isFinish)
                {
                    t.taskItem.Finish(false);
                    return;
                }
            }
            t.taskItem.Finish(true);
            
        }

    }

    public void CheckTask(System.Object sender, TaskEventArgs e)
    {
        foreach (KeyValuePair<string, Task> kv in dictionary)
        {
            kv.Value.Check(e);
        }
    }

    public void FinishTask(TaskEventArgs e)
    {
        
        finishEvent(this, e);
    }

    public void GetReward(TaskEventArgs e)
    {
        if (dictionary.ContainsKey(e.taskID))
        {
            Task t = dictionary[e.taskID];
            for (int i = 0; i < t.taskRewards.Count; i++)
            {
                TaskEventArgs a = new TaskEventArgs();
                a.id = t.taskRewards[i].id;
                print(a.id);
                a.amount = t.taskRewards[i].amount;
                a.taskID = e.taskID;
                rewardEvent(this,a);
            }
        }
        dictionary.Remove(e.taskID);
    }

    public void CancelTask(TaskEventArgs e)
    {
        if (dictionary.ContainsKey(e.taskID))
        {
            cancelEvent(this,e);
            dictionary.Remove(e.taskID);
        }
    } 
}
