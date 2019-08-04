using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hp : MonoBehaviour {
	public static Hp instance;
	public Text hp_text;
	public Text mp_text;
	public Slider hp_slider;
	public Slider mp_slider;
	private playerstatus ps;
	// Use this for initialization
	void Awake(){
		instance = this;
	}
	void Start () {

		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();

	}
	
	// Update is called once per frame
	void Update () {
        updateShow();
	}



	public void updateShow(){
		hp_text.text = ps.player.hp_remain + "/" + ps.player.hp;
		mp_text.text = ps.player.mp_remain + "/" + ps.player.mp;
		hp_slider.value = (float)ps.player.hp_remain / ps.player.hp;
		mp_slider.value = (float)ps.player.mp_remain / ps.player.mp;

	}
}
