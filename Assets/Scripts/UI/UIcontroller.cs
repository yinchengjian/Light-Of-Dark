using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIcontroller : MonoBehaviour {

	public static UIcontroller instance;
	public Text taskption;
	public int killcount = 0;

	public GameObject task;

	private playerstatus status;
	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void Start(){
		status = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void acceptBtn(){
		taskption.text = "任务:\n已猎杀"+killcount+"/10只小狼\n\n奖励:\n1000金币";
	}
	public void okBtn(){
		if (killcount >= 10) {
			task.SetActive (false);
			EquipAndBag.instance.GetMoney (1000);
		} else {
			task.SetActive (false);
		}
	}

}
