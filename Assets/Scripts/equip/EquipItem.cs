using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipItem : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler {

	private Image img;
	public int id;
	private bool flag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(flag){
			if(Input.GetMouseButtonDown(1)){//装备卸下
				EquipAndBag.instance.getId(id);
				if (!EquipAndBag.instance.isfalse) {
					
				} else {
					Equip.instance.takeoff (this.gameObject);
				}

			}	
		}
	}

	public void setIconname(string icon_name){
		img = this.GetComponent<Image> ();
		img.overrideSprite = Resources.Load("Icon/"+icon_name, typeof(Sprite)) as Sprite;
	}

	public void SetId(int id){
		this.id = id;
		objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);

	}

	public void SetInfo(objectInfo info){
		this.id = info.id;
		img = this.GetComponent<Image> ();
		img.overrideSprite = Resources.Load("Icon/"+info.icon_name, typeof(Sprite)) as Sprite;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{	
		flag = true;
        item_dis.instance.showDis(id);
        //item_dis.instance.transform.position = Input.mousePosition;
	}

	public void OnPointerExit(PointerEventData eventData){
		flag = false;
        item_dis.instance.gameObject.SetActive(false);
    }


}
