using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMove : MonoBehaviour {

	public float speed = 5.0f;
	public float rotationSpeed = 50;
	public Vector3  tagetposition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		moveMouse ();
	}

	public void moveMouse(){
		// 使用上下方向键或者W、S键来控制前进后退

		if (Input.GetAxisRaw ("Vertical") != 0) {
			

			speed = 5.0f;
			if(Input.GetKey(KeyCode.LeftShift)){
				
				speed = 8.0f;

			}
		} else {
			
		}
		float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		//使用左右方向键或者A、D键来控制左右旋转
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
		transform.Translate(0, 0, translation); //沿着Z轴移动
		transform.Rotate(0, rotation, 0); //绕Y轴旋转
	}


	void move(){
		if (Input.GetMouseButtonDown (1)) {
			Ray _ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitinfo;
			if (Physics.Raycast (_ray, out hitinfo)&&hitinfo.collider.tag=="Ground") {
				tagetposition = new Vector3(transform.position.x, transform.position.y, hitinfo.point.z);
			}
		}
		transform.LookAt (tagetposition);
		transform.Translate (tagetposition);
	}
}
