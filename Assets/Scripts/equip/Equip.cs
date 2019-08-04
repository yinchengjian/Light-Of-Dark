using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour {

	public static Equip instance;

	public playerstatus ps;

	public GameObject headgear;
	public GameObject armor;
	public GameObject lefthand;
	public GameObject righthand;
	public GameObject shoe;
	public GameObject accessory;

	public GameObject equip_item;

    public List<int> equip_id = new List<int>();

	public int attack = 0;
	public int def = 0;
	public int speed = 0;
    // Use this for initialization
    void Awake()
    {
        instance = this;
        
    }

    void Start(){
       
        ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onEquipButton(){
		
		this.transform.localPosition = new Vector3 (-19, 40, 0);			
		this.gameObject.SetActive (true);
	}

	public bool Dress(int id){
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);
		if (info.type != objectType.Equip) {
			return false;
		}
		if(ps.hero==herotype.magician){
			if (info.applicationType == ApplicationType.Swordman) {
				return false;
			}
		}
		if (ps.hero == herotype.swordman) {
			if (info.applicationType == ApplicationType.Magician) {
				return false;
			}
		}

		GameObject parent = null;
		switch(info.dressType){
		case DressType.Headgear:
			parent = headgear;
			break;
		case DressType.Armor:
			parent = armor;
			break;
		case DressType.LeftHand:
			parent = lefthand;
			break;
		case DressType.RightHand:
			parent = righthand;
			break;
		case DressType.Shoe:
			parent = shoe;
			break;
		case DressType.Accessory:
			parent = accessory;
			break;
		}
			
		EquipItem item = parent.GetComponentInChildren<EquipItem> ();
		if (item != null) {
            EquipAndBag.instance.getId(item.id);//装备卸下，放回
            MinProperty(item.id);
			item.SetInfo (info);
            plusProperty(info.id);

		} else {
			GameObject itemGo = Instantiate(equip_item);
			itemGo.transform.SetParent (parent.transform);
			itemGo.transform.position = parent.transform.position;
			itemGo.GetComponent<EquipItem> ().SetInfo (info);
            plusProperty(id);
        }
        
		//UpdateProperty ();
		return true;
	}


    public bool Dress_load(int id) {
        objectInfo info = ObjectsInfo.instance.FindObjecInfoById(id);
        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.LeftHand:
                parent = lefthand;
                break;
            case DressType.RightHand:
                parent = righthand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }

        EquipItem item = parent.GetComponentInChildren<EquipItem>();
        if (item != null)
        {
            EquipAndBag.instance.getId(item.id);//装备卸下，放回
            MinProperty(item.id);
            item.SetInfo(info);
            plusProperty(info.id);

        }
        else
        {
            GameObject itemGo = Instantiate(equip_item);
            itemGo.transform.SetParent(parent.transform);
            itemGo.transform.position = parent.transform.position;
            itemGo.GetComponent<EquipItem>().SetInfo(info);
            plusProperty(id);
        }

        //UpdateProperty ();
        return true;
    }
    


	public void takeoff(GameObject go){
        int id = go.GetComponent<EquipItem>().id;
        MinProperty(id);
        GameObject.Destroy (go);
	}

    void MinProperty(int id) {
        objectInfo info = ObjectsInfo.instance.FindObjecInfoById(id);
        this.attack -= info.attack;
        this.def -= info.def;
        this.speed -= info.speed;
    }

    void plusProperty(int id) {
        objectInfo info = ObjectsInfo.instance.FindObjecInfoById(id);
        this.attack += info.attack;
        this.def += info.def;
        this.speed += info.speed;
    }

	void UpdateProperty(){
		this.attack = 0;
		this.def = 0;
		this.speed = 0;
		EquipItem headgearItem = headgear.GetComponentInChildren<EquipItem>();
		PlusProperty(headgearItem);
		EquipItem armorItem = armor.GetComponentInChildren<EquipItem>();
		PlusProperty(armorItem);
		EquipItem leftHandItem = lefthand.GetComponentInChildren<EquipItem> ();
		PlusProperty(leftHandItem);
		EquipItem rightHandItem = righthand.GetComponentInChildren<EquipItem> ();
		PlusProperty(rightHandItem);
		EquipItem shoeItem = shoe.GetComponentInChildren<EquipItem>();
		PlusProperty(shoeItem);
		EquipItem accessoryItem = accessory.GetComponentInChildren<EquipItem>();
		PlusProperty(accessoryItem);

	}

	void PlusProperty(EquipItem item){
		if (item != null) {
           
			objectInfo info = ObjectsInfo.instance.FindObjecInfoById (item.id);
			this.attack += info.attack;
			this.def += info.def;
			this.speed += info.speed;
		}

	}
}
