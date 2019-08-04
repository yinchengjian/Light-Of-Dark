using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SkillUI : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler {
	public int id;
	private Image skillmask_icon;
	public SkillInfo skillinfo;
	private playerstatus ps;
	public GameObject plus;
	public Image skill_icon;
	public GameObject mask;

	public int level;

	public bool isok = true;

	public float coldtime = 0;
	private float timer =0;

	public playerAttack pa;

	public Text key_text;
	public KeyCode key;
	// Use this for initialization
	void Start () {
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
		pa = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerAttack> ();

		//setInfo ();


    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (key) && isok&& !mask.activeSelf) {
			bool success = ps.TakeMP (skillinfo.mp);
            switch (key) {
                case KeyCode.Q:
                  //  this.GetComponent<AudioSource>().Play();
                    break;
            }
			if (success) {
				pa.UseSkill (skillinfo);
				isok = false;
				timer = 0;
			} else {
			
			}
		}
		if(skillinfo!=null){
			if(timer<skillinfo.coldTime){
				timer += Time.deltaTime;
				skill_icon.fillAmount = timer / skillinfo.coldTime;
				if(timer>=skillinfo.coldTime){
					isok = true;
				}
			}
		}

	}

	public void setInfo(){
		key_text = transform.Find ("shortcutkey").GetComponent<Text> ();
		mask = transform.Find ("mask").gameObject;
		skillmask_icon = transform.Find ("skillmask").GetComponent<Image> ();
		skill_icon = transform.Find ("skillmask/skill").GetComponent<Image> ();
		switch(key_text.text){
		case "1":
			key = KeyCode.Alpha1;
			break;
		case "2":
			key = KeyCode.Alpha2;
			break;
		case "3":
			key = KeyCode.Alpha3;
			break;
		case "4":
			key = KeyCode.Alpha4;
			break;
		}
		skillinfo = SkillsInfo._instance.GetSkillInfoById (id);
		skillmask_icon.overrideSprite = Resources.Load("Icon/"+skillinfo.icon_name, typeof(Sprite)) as Sprite;
		skill_icon.overrideSprite = Resources.Load("Icon/"+skillinfo.icon_name, typeof(Sprite)) as Sprite;
		level = skillinfo.level;
		timer = skillinfo.coldTime;
		updateShow (1);
	}
		
	public void OnPointerEnter(PointerEventData eventData)
	{	

		SkillObject.instance.ShowDis (id);
		SkillObject.instance.transform.Find ("des").gameObject.SetActive (true);　

		//item_dis.instance.transform.position = Input.mousePosition;
	}

	public void OnPointerExit(PointerEventData eventData){
		SkillObject.instance.transform.Find ("des").gameObject.SetActive (false);　
	}


	public void updateShow(int level){
		this.level = level;
		if (skillinfo.level <= level) {//技能可用
			mask.SetActive(false);
		} else {
			mask.SetActive (true);
		}

	}

}
