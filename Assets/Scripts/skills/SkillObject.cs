using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillObject : MonoBehaviour {

	private playerstatus ps;
	public static SkillObject instance;
	public SkillUI[] skilluiArray; 
	public int level = 1;
	public herotype hero;
	// Use this for initialization
	void Start () {
		instance = this;
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
		hero = ps.hero;
		skillShow ();
		/*
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
		skillui = this.GetComponentsInChildren<SkillUI> ();
		if (ps.hero == herotype.magician) {
			
		}else if(ps.hero == herotype.swordman){
			int temp = 5001;
			foreach (SkillUI i in skillui) {
				i.id = temp++;
			}
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void skillShow(){
		if (hero == herotype.swordman) {
			int id = 4001;
			foreach (SkillUI skill in skilluiArray) {
				skill.id = id++;
				skill.setInfo ();
			}
		} else {
			int id = 5001;
			foreach (SkillUI skill in skilluiArray) {
				skill.id = id++;
				skill.setInfo ();
			}
		}
	}

	public void updateShow(int level){
		this.level = level;
		foreach(SkillUI skill in skilluiArray){
			skill.updateShow (level);
		}

	}

	public void ShowDis(int id){
		SkillInfo info = SkillsInfo._instance.GetSkillInfoById (id);
		string des = "";
		des = info.des;
		transform.Find ("des").GetComponent<Text> ().text = des;
	}
}
