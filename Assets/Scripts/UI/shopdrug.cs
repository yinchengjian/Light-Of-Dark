using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class shopdrug : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static shopdrug instance;
	private int buy_id = 0;
	public InputField buy_num;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void buy1001btn(){
		Buy (1001);
	}
	public void buy1002btn(){
		Buy (1002);
	}
	public void buy1003btn(){
		Buy (1003);
	}
	void Buy(int id){
		buy_num.text = "0";
		buy_id = id;
	}
	public void okbtn(){
		int count = int.Parse (buy_num.text);
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (buy_id);
		int price = info.price_buy;
		int price_total = price * count;
		bool success = EquipAndBag.instance.getCoin (price_total);
		if (success) {
			if (count > 0) {
				EquipAndBag.instance.getId (buy_id,count);
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData){

	}

	public void OnDrag(PointerEventData eventData){
		
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData){

	}

}
