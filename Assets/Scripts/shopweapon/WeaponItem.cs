using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour {

	private int id;
	private objectInfo info = null;

	public Image iconname_sprite;
	public Text name_text;
	public Text effect_text;
	public Text price_text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setProperty(){
		iconname_sprite = transform.Find ("icon_name").GetComponent<Image> ();
		name_text = transform.Find ("name").GetComponent<Text> ();
		effect_text = transform.Find ("effect").GetComponent<Text> ();
		price_text = transform.Find ("price").GetComponent<Text> ();
	}

	public void setId(int id){
		
		setProperty ();
		this.id = id;
		info = ObjectsInfo.instance.FindObjecInfoById (id);
		iconname_sprite.sprite = Resources.Load("Icon/"+info.icon_name, typeof(Sprite)) as Sprite;
		name_text.text = info.name;
		if(info.attack>0){
			effect_text.text = "+伤害" + info.attack;
		}else if(info.def>0){
			effect_text.text = "+防御" + info.def;
		}else if(info.speed>0){
			effect_text.text = "+速度" + info.speed;
		}
		price_text.text = info.price_buy.ToString ();
	
	}

	public void OnBuyBtn(){
		shopweapon.instance.AllBuyBtn (id);
	}

}
