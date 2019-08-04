using UnityEngine;
using System.Collections;
using System;

public class Notification : MonoSingletion<Notification> {

    public GameObject tip;

    void Awake()
    {
        TaskManager.Instance.getEvent += getPrintInfo;
        TaskManager.Instance.finishEvent += finishPrintInfo;
        TaskManager.Instance.rewardEvent += rewardPrintInfo;
        TaskManager.Instance.cancelEvent += cancelPrintInfo;
    }

    public void getPrintInfo(System.Object sender, TaskEventArgs e)
    {
        print("接受任务" + e.taskID);
    }

    public void finishPrintInfo(System.Object sender, TaskEventArgs e)
    {
        BarNPC._instance.isInTask = false;
       
        tip.SetActive(true);
        print("完成任务" + e.taskID);
        if (e.taskID.Equals("Task1")) {
            Dialog.instance.i = 4;
        } else if (e.taskID.Equals("Task2")) {
            Dialog.instance.i = 6;
        }
        if (e.taskID.Equals("Task3")) {
            LastTalk.instance.isFinish = true;
        }
    }

    public void rewardPrintInfo(System.Object sender, TaskEventArgs e)
    {
        EquipAndBag.instance.getId(int.Parse(e.id), e.amount);
        print("奖励物品" + e.id + "数量" + e.amount);
    }

    public void cancelPrintInfo(System.Object sender, TaskEventArgs e)
    {
        print("取消任务" + e.taskID);
    }
}
