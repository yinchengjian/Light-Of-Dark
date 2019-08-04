using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuntionBar : MonoBehaviour {

    public Text level;
    public Text name;
    public playerstatus ps;
	// Use this for initialization
	void Start () {
        ps = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<playerstatus>();
	}
	
	// Update is called once per frame
	void Update () {
        level.text = ps.player.level.ToString();
        name.text = ps.player.name;
	}




}
