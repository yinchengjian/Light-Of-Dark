using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minmap : MonoBehaviour {

	private Camera minmapCam;
	// Use this for initialization
	void Start () {
		minmapCam = GameObject.FindGameObjectWithTag ("MinMap").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnZoomInClick(){
		minmapCam.orthographicSize--;
	}
	public void OnZoomOutClick(){
		minmapCam.orthographicSize++;
	}
}
