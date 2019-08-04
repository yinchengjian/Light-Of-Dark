using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {

	public static ExpBar instance;
	public Slider exp_slider;
	public Text exp_text;
	// Use this for initialization
	void Awake(){
		instance = this;
		exp_slider = this.GetComponent<Slider> ();
		exp_text = transform.Find ("Fill Area/exp_text").GetComponent<Text> ();

	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setValue(float value){
		exp_slider.value = value;
		float x = value * 100;
		exp_text.text = x+"%";
	}	
}
