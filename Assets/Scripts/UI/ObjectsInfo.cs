using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum objectType{
	Drug,
	Equip,
	Mat
}

public enum DressType {
	Headgear,
	Armor,
	RightHand,
	LeftHand,
	Shoe,
	Accessory
}

public enum ApplicationType{
	Swordman,//剑士
	Magician,//魔法师
	Common//通用
} 



public class ObjectsInfo : MonoBehaviour {
	public static ObjectsInfo instance;
	public TextAsset objectInfoList;

	private  Dictionary<int ,objectInfo> objectInfoDic = new Dictionary<int, objectInfo> ();
	// Use this for initialization
	void Awake () {
		instance = this;
		read ();

	}


	public objectInfo FindObjecInfoById(int id){
		objectInfo info = null;
		objectInfoDic.TryGetValue (id,out info);
		return info;
	}



	public void read(){
		
		string text = objectInfoList.text;
		string[] str = text.Split ('\n');
		foreach (string array in str) {
			objectInfo info = new objectInfo ();
			string[] s = array.Split (',');
			int id = int.Parse (s[0]);
			string name = s [1];
			string icon_name = s [2];
			string str_type = s [3];
			objectType type = objectType.Drug;
			switch (str_type) {
			case "Drug":
				type = objectType.Drug;
				break;
			case "Equip":
				type = objectType.Equip;
				break;
			case "Mat":
				type = objectType.Mat;
				break;
			}

			info.id = id;
			info.name = name;
			info.icon_name = icon_name;
			info.type = type;

			if(type == objectType.Drug){
				int hp = int.Parse(s [4]);
				int mp = int.Parse(s [5]);
				int price_sell = int.Parse(s [6]);
				int price_buy = int.Parse(s [7]);

				info.hp = hp;
				info.mp = mp;
				info.price_sell = price_sell;
				info.price_buy = price_buy;


			}else if (type == objectType.Equip) {
				info.attack = int.Parse(s[4]);
				info.def = int.Parse(s[5]);
				info.speed = int.Parse(s[6]);
				info.price_sell = int.Parse(s[9]);
				info.price_buy = int.Parse(s[10]);
				string str_dresstype = s[7];
				switch (str_dresstype) {
				case "Headgear":
					info.dressType = DressType.Headgear;
					break;
				case "Armor":
					info.dressType = DressType.Armor;
					break;
				case "LeftHand":
					info.dressType = DressType.LeftHand;
					break;
				case "RightHand":
					info.dressType = DressType.RightHand;
					break;
				case "Shoe":
					info.dressType = DressType.Shoe;
					break;
				case "Accessory":
					info.dressType = DressType.Accessory;
					break;
				}
				string str_apptype = s[8];
				switch (str_apptype) {
				case "Swordman":
					info.applicationType = ApplicationType.Swordman;
					break;
				case "Magician":
					info.applicationType = ApplicationType.Magician;
					break;
				case "Common":
					info.applicationType = ApplicationType.Common;
					break;
				}

			}
			objectInfoDic.Add (id,info);

		}

	}

}

public class objectInfo{
	public int id;
	public string name;
	public string icon_name;
	public objectType type;
	public int hp;
	public int mp;
	public int price_sell;
	public int price_buy;

	public int attack;
	public int def;
	public int speed;
	public DressType dressType;
	public ApplicationType applicationType;
}
