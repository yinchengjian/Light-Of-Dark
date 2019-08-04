using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status : MonoBehaviour {
	public static status instance;

	public Text attack;
	public Text def;
	public Text speed;
	public Text pointremain;
	public Text summary;

	public GameObject attackbutton;
	public GameObject defbutton;
	public GameObject speedbutton;

	private playerstatus ps;


	// Use this for initialization
	void Awake () {
		
		instance = this;

	}

	void Start(){
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void onstatusButton(){
		UpdateShow ();
		this.transform.localPosition = new Vector3 (-19, 40, 0);			
		this.gameObject.SetActive (true);
	}


	public void UpdateShow(){//更新示
		attack.text =  ps.player.attack+"+"+ps.player.attack_plus+"+"+Equip.instance.attack;
		def.text =  ps.player.def+"+"+ps.player.def_plus+"+"+Equip.instance.def;
		speed.text = ps.player.speed + "+" + ps.player.speed_plus+"+"+Equip.instance.speed;

		pointremain.text = ps.player.point_remain.ToString ();
		summary.text = "伤害：" + (ps.player.attack + ps.player.attack_plus+Equip.instance.attack)
			+ " " + "防御：" + (ps.player.def + ps.player.def_plus+Equip.instance.def)
			+ " " + "速度：" + (ps.player.speed + ps.player.speed_plus+Equip.instance.speed);
        
        if (ps.player.point_remain > 0) {
			attackbutton.SetActive (true);
			defbutton.SetActive (true);
			speedbutton.SetActive (true);
		} else {
			attackbutton.SetActive (false);
			defbutton.SetActive (false);
			speedbutton.SetActive (false);
		}
	}

	public void onAttackBtn(){
		bool success = ps.GetPoint ();
		if (success) {
			ps.player.attack_plus++;
			UpdateShow ();
		}
	}
	public void onDefBtn(){
		bool success = ps.GetPoint ();
		if (success) {
			ps.player.def_plus++;
			UpdateShow ();
		}
	}
	public void onSpeedBtn(){
		bool success = ps.GetPoint ();
		if (success) {
			ps.player.speed_plus++;
			UpdateShow ();
		}
	}

}
