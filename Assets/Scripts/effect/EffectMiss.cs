using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMiss : MonoBehaviour {
	public float speed = 20;
	// Use this for initialization
	void Start () {
		//
		Destroy(gameObject, 0.5f);
	}

	// Update is called once per frame
	void Update () 
	{
		//血量移动的效果
		transform.Translate(Vector3.up * Time.deltaTime * speed);
	}

}
