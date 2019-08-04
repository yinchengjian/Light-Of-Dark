using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTalk : MonoBehaviour {

    public static LastTalk instance;

    public bool isFinish = false;
    public GameObject player;
    public GameObject dia;
    public GameObject setting;

    public bool anxia = false;

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

   
    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && isFinish == true) {
            
            Text dis = setting.transform.Find("dis").GetComponent<Text>();
            dis.text = "恭喜你，游戏通关！！！";
            setting.SetActive(true);
            Time.timeScale = 0;
            isFinish = false;
        }
    }



}
