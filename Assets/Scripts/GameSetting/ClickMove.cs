using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnBeginDrag(PointerEventData eventData){
		
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData){
		
	}
}
