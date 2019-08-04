using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipAndBag_Grid : MonoBehaviour
{

	public int id = 0;
	public int num = 0;
	public objectInfo info = null;
	public GameObject itemcount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void setId(int id ,int num =1){
		this.id = id;
		info = ObjectsInfo.instance.FindObjecInfoById (id);
		Drag2 item = this.GetComponentInChildren<Drag2> ();
		item.setIconname (info.icon_name);
		itemcount.SetActive (true);
		this.num = num;
		itemcount.GetComponent<Text> ().text = num.ToString ();

	}

   

	public void PlusNum(int num=1){
		this.num+=num;
		itemcount.GetComponent<Text> ().text = this.num.ToString();
		Drag2 item = this.GetComponentInChildren<Drag2> ();
		item.count = this.num;
	}

	public bool MinsNumber(int num =1){
		if(this.num>=num){
			this.num -= num;
			itemcount.GetComponent<Text> ().text = this.num.ToString();
			if(this.num==0){
				clearInfo ();
				GameObject.Destroy (this.GetComponentInChildren<Drag2>().gameObject);
			}
			return true;
		}
		return false;
	}

	public void setCount(){
		itemcount.GetComponent<Text> ().text = this.num.ToString();
	}

	public GameObject getItemCount(){
		return itemcount;
	}

	public void clearInfo(){
		this.id = 0;
		this.num = 0;
		itemcount.GetComponent<Text> ().text = this.num.ToString();
		itemcount.SetActive (false);
	}
}
[System.Serializable]
public class Item{
    
    public int id;
    public int count;
	public Item(int id ,int count){
		this.id = id;
		this.count = count;
	}
}