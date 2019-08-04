using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public static Dialog instance;
    public Text talk;

    public GameObject accept;
    public GameObject cancel;
    public GameObject next;
    public TextAsset DialogText;
    public GameObject dia;


    public string[] dialoginfoArray;
    public int i = 1;
    public int j = 1;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        string text = DialogText.text;
        dialoginfoArray = text.Split('\n');
    }
	
	// Update is called once per frame
	void Update () {
        if (i % 2 == 1&&i!=1)
        {
            accept.SetActive(true);
            cancel.SetActive(true);
            next.SetActive(false);
        }
    }

    public void OnNextButton()
    {
        if (i==dialoginfoArray.Length) {
            dia.SetActive(false);
            return;
        }
        talk.text = dialoginfoArray[i];
        i++;
       

    }

    public void setTask() {

        string task = "Task" + j;
        TaskManager.Instance.GetTask(task);
        BarNPC._instance.isInTask = true;
        accept.SetActive(false);
        cancel.SetActive(false);
        next.SetActive(true);
        talk.text = dialoginfoArray[j*2+1];
        j++;
    }
    public void RefuseTak() {
        
        BarNPC._instance.isInTask = false;
        accept.SetActive(true);
        cancel.SetActive(true);
        next.SetActive(false);
    }
}
