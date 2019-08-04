using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarNPC : NPC {

    public static BarNPC _instance;
   
    public GameObject acceptBtnGo;
    public GameObject okBtnGo;
    public GameObject cancelBtnGo;

    public GameObject dialog;

	public GameObject task;

	public Text task_text;

    public bool isInTask = false;//表示是否在任务中
    public int killCount = 0;//表示任务进度，已经杀死了几只小野狼

	private playerstatus status;
    public Dialog d;

    void Awake() {
        _instance = this;
    }
    void Start() {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstatus>();
        d = dialog.GetComponent<Dialog>();
    }

    void OnMouseOver() {//当鼠标位于这个collider之上的时候，会在每一帧调用这个方法
        if (Input.GetMouseButtonDown(0)) {//当点击了老爷爷

            if (isInTask) {

            }
            else {
                dialog.SetActive(true);
                
            }
            
            /*
            if (isInTask) {
                TaskEventArgs e = new TaskEventArgs();
                e.id = "NPC";
                e.amount = 1;
                MesManager.Instance.Check(e);
                //ShowTaskProgress();
            } else {
                //  ShowTaskDes();
                dialog.SetActive(true);

                //TaskManager.Instance.GetTask("Task4");
               // isInTask = true;
            }
           */ 
        }
    }

    
    public void OnKillWolf() {
        if (isInTask) {
            killCount++;
        }
    }

    void ShowTaskDes(){
		task_text.text = "任务：\n杀死了10只狼\n\n奖励：\n1000金币";
		task.SetActive (true);
       /*
        okBtnGo.SetActive(false);
        acceptBtnGo.SetActive(true);
        cancelBtnGo.SetActive(true);
        */

	}
    void ShowTaskProgress(){
		task_text.text = "任务：\n你已经杀死了" + killCount + "\\10只狼\n\n奖励：\n1000金币";
		task.SetActive (true);
		/*
		okBtnGo.SetActive(true);
        acceptBtnGo.SetActive(false);
        cancelBtnGo.SetActive(false);
        */
    }

    //任务系统 任务对话框上的按钮点击时间的处理
    public void OnCloseButtonClick() {
        //HideQuest();
    }

    public void OnAcceptButtonClick() {
        ShowTaskProgress();
        isInTask = true;//表示在任务中
    }
    public void OnOkButtonClick() {
        if(killCount>=10){//完成任务
			EquipAndBag.instance.GetMoney (1000);
            killCount = 0;
            ShowTaskDes();
        }else{
            //没有完成任务
           // HideQuest();
        }
    }
    public void OnCancelButtonClick() {
        //HideQuest();
    }
}
