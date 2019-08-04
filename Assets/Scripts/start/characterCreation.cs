using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class characterCreation : MonoBehaviour {

	public static characterCreation instance;


	public GameObject[] prefabs; 
	private GameObject[] selected;
	public  int selectedindex = 0;
	private int length;
	public GameObject player;

	public InputField username_input;
	public string username;


	// Use this for initialization
	void Start () {
		instance = this;
		player = GameObject.Find ("player");
		length = prefabs.Length;
		selected = new GameObject[length];
		for (int i=0; i < length; i++) {
			selected[i] = GameObject.Instantiate (prefabs[i],transform.position,transform.rotation);
			selected [i].transform.parent = player.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		showSomeone ();
	}

	public void forwordsetIdex(){
		selectedindex++;
		selectedindex %= length;
    }
	public void backsetIdex(){
		selectedindex--;
		if (selectedindex < 0) {
			selectedindex = length - 1;
		}
		selectedindex %= length;

	}
	
	void showSomeone(){
		selected [selectedindex].SetActive (true);
		for (int i = 0; i < length; i++) {
			if (i != selectedindex) {
				selected [i].SetActive (false);
			}
		}
	}

	public void OnOKbutton(){
		PlayerPrefs.SetString ("name",username_input.text);
		PlayerPrefs.SetInt ("SelectedCharacterIndex",selectedindex);
		username = username_input.text;
		SceneManager.LoadScene (2);
	}
    public void LoadScene() {
        SceneManager.LoadScene(0);
    }

}
