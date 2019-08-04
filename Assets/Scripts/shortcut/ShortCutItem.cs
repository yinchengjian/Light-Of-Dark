using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShortCutType{
	Skill,
	Drug,
	None
}

public class ShortCutItem : MonoBehaviour {

	public KeyCode ky;
	public Text num_text;
	public ShortCutType type = ShortCutType.None;
	public int id;
	public int count;
	private SkillInfo skinfo;
	private objectInfo obinfo;
	private Image img;

	public playerstatus ps;
	int num = 0;

	public Text count_text;

	// Use this for initialization
	void Start () {
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
		num_text = transform.parent.Find ("shortcutkey").GetComponent<Text> ();
		num = int.Parse (num_text.text);
		switch(num){
		case 1:
			ky = KeyCode.Alpha1;
			break;
		case 2:
			ky = KeyCode.Alpha2;
			break;
		case 3:
			ky = KeyCode.Alpha3;
			break;
		case 4:
			ky = KeyCode.Alpha4;
			break;
		case 5:
			ky = KeyCode.Alpha5;
			break;
		case 6:
			ky = KeyCode.Alpha6;
			break;
		case 7:
			ky = KeyCode.Alpha7;
			break;
		case 8:
			ky = KeyCode.Alpha8;
			break;
		case 9:
			ky = KeyCode.Alpha9;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(ky)){
			if (type == ShortCutType.Drug) {
				UseDrug ();
			}else if(type == ShortCutType.Skill){
				
			}
		}
	}

	public void setId(int id){
		
		this.id = id;
		this.skinfo = SkillsInfo._instance.GetSkillInfoById (id);
		img = this.GetComponent<Image> ();
		img.overrideSprite = Resources.Load("Icon/"+skinfo.icon_name, typeof(Sprite)) as Sprite;
		type = ShortCutType.Skill;
	}

	public void setDrag2(int id,int count){
		count_text = transform.parent.Find ("shortcutcount").GetComponent<Text> ();
		this.id = id;
		this.count = count;
		count_text.text = count.ToString ();
		count_text.gameObject.SetActive (true);
		this.obinfo = ObjectsInfo.instance.FindObjecInfoById (id);
		img = this.GetComponent<Image> ();
		img.overrideSprite = Resources.Load("Icon/"+obinfo.icon_name, typeof(Sprite)) as Sprite;
		type = ShortCutType.Drug;

	}

	public void UseDrug(){
		if(id==0){
			return;
		}
		int hp = obinfo.hp;
		int mp = obinfo.mp;
		ps.GetDrug (hp,mp);
		count--;
		if (count <= 0) {
			count_text.gameObject.SetActive (false);
			this.id = 0;
			this.obinfo = null;
			img.overrideSprite = null;
			type = ShortCutType.None;
		} else {
			count_text.text = count.ToString ();
		}

	}
}
