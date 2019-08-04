using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class camera_run : MonoBehaviour {

	public Transform player;


	private bool Isrotate = false;
	public float ratio = 5;

	public Vector3 offset;
	public float distance;
	public float speed;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
		offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		first_person ();
		//ScaleView ();
		RotateView ();
	}
	void first_person(){
		transform.position = player.position +offset;
		transform.LookAt (player);
	}
		
	void ScaleView(){
		distance = offset.magnitude;
		if(Input.GetAxis ("Mouse ScrollWheel")!=0&&!EventSystem.current.IsPointerOverGameObject())
		distance -= Input.GetAxis ("Mouse ScrollWheel") * speed;
		distance = Mathf.Clamp (distance,2,18);
		offset = offset.normalized * distance;
	}

	void RotateView(){
		
		float rotationSpeed = 50.0f;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
		//transform.Rotate(0, rotation, 0);

		transform.RotateAround(player.transform.position,player.up,rotation);
		offset = transform.position - player.position;

		/*

		if (Input.GetMouseButtonDown (0)&&!EventSystem.current.IsPointerOverGameObject()) {
			Isrotate = true;
		} 
		if(Input.GetMouseButtonUp (0)){
			Isrotate = false;
		}
		if (Isrotate) {
			transform.RotateAround(player.transform.position,player.up,ratio*Input.GetAxis ("Mouse X"));
			offset = transform.position - player.position;
		}
      */
	}

}
