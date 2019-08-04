using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopweapon : MonoBehaviour {

	public static shopweapon instance;

	public GameObject weaponitem_prefab;
	public GameObject content;
	public float content_height;

	public int[] weaponArray;

	private int count=0;
	public float itemHeight;
	// Use this for initialization
	void Awake () {
		instance = this;
		content_height= content.GetComponent<RectTransform>().rect.height; 
		//content = transform.Find ("Content").gameObject;
		foreach(int id in weaponArray){
			GameObject weaponitem = Instantiate (weaponitem_prefab);
			weaponitem.transform.SetParent (content.transform);
			weaponitem.GetComponent<WeaponItem> ().setId (id);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void AllBuyBtn(int id){
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);
		int price = info.price_buy;
		bool success = EquipAndBag.instance.getCoin (price);
		if (success) {
			EquipAndBag.instance.getId (id);
		}

	}
}
