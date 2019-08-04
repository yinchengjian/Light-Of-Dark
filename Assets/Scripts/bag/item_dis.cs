using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_dis : MonoBehaviour {
	public static item_dis instance;

	// Use this for initialization
	void Awake () {
		instance = this;
		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showDis(int id){
		this.gameObject.SetActive (true);
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);
		string des = "";
		switch(info.type){
		case objectType.Drug:
			des = GetDrugDis (id);
			break;
		case objectType.Equip:
			des = GetEquipDis (id);
			break;
		}
		this.GetComponent<Text> ().text = des;
	}

	string GetDrugDis(int id){
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);
		string str = "";

		str += "name:"+info.name+"\n";
		str += "HP:"+info.hp+"\n";
		str += "MP:"+info.mp+"\n";
		str += "出售价:"+info.price_sell+"\n";
		str += "购买价:"+info.price_buy+"\n";

		return str;
	}

	string GetEquipDis(int id){
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);
		string str = "";
		str += "name:"+info.name+"\n";
		switch(info.dressType){
		case DressType.Headgear:
			str+="穿戴类型：头盔\n";
			break;
		case DressType.Armor:
			str += "穿戴类型：盔甲\n";
			break;
			break;
		case DressType.LeftHand:
			str += "穿戴类型：左手\n";
			break;
			break;
		case DressType.RightHand:
			str += "穿戴类型：右手\n";
			break;
			break;
		case DressType.Shoe:
			str += "穿戴类型：鞋\n";
			break;
			break;
		case DressType.Accessory:
			str += "穿戴类型：饰品\n";
			break;
		}

		switch(info.applicationType){
		case ApplicationType.Swordman:
			str += "适用类型：剑士\n";
			break;
		case ApplicationType.Magician:
			str += "适用类型：魔法师\n";
			break;
		case ApplicationType.Common:
			str += "适用类型：通用\n";
			break;

		}

		str+="伤害值："+info.attack+"\n";
		str+="防御值："+info.def+"\n";
		str+="速度值："+info.speed+"\n";


		str += "出售价:"+info.price_sell+"\n";
		str += "购买价:"+info.price_buy+"\n";

		return str;
	}
}
