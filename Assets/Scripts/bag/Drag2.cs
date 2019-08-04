using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter,IPointerEnterHandler, IPointerExitHandler
{
	
	private static Transform canvasTra;
	private Transform nowParent;//物品是格子的子物体，nowParent记录的是当前物品属于哪个格子（格子1）
	private bool isRaycastLocationValid = true;//默认射线不能穿透物品
	private Image img;
	bool flag = false;
	private int id;
	public int count;
	private playerstatus ps;
	void Start()
	{
		nowParent = transform.parent;
		EquipAndBag_Grid now = nowParent.gameObject.GetComponent<EquipAndBag_Grid> ();
		this.id = now.id;
		this.count = now.num;
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
	}

	public void setIconname(string icon_name){
		img = this.GetComponent<Image> ();
		img.overrideSprite = Resources.Load("Icon/"+icon_name, typeof(Sprite)) as Sprite;
	}

	public void OnBeginDrag(PointerEventData eventData)//开始拖拽
	{
		if (canvasTra == null) canvasTra = GameObject.Find("Canvas").transform;
		//canvasCamera = canvasTra.GetComponentInChildren<Camera>();
		//Debug.Log(Input.mousePosition);
		nowParent = transform.parent;//nowparent为被拖拽物体的原始父物体
		nowParent.GetComponent<EquipAndBag_Grid> ().getItemCount ().SetActive (false);
        nowParent.GetComponent<EquipAndBag_Grid>().clearInfo();
		transform.SetParent(canvasTra);//将当前拖拽的物品放在canvas下
		isRaycastLocationValid = false;//ui穿透：置为可以穿透  【拖拽物体移动的时候鼠标下是有物体一直跟随遮挡的，如果不穿透就获取不到放置位置（OnEndDrag中判断是空格子，物体，还是无效位置）】

	}

	public void OnDrag(PointerEventData eventData)//拖拽过程中
	{
		//transform.position = canvasCamera.ScreenToWorldPoint(Input.mousePosition);//鼠标左键按住拖拽的时候，物体跟着鼠标移动
		transform.position = Input.mousePosition;//鼠标左键按住拖拽物体的时候不显示物体
	}

	public void OnEndDrag(PointerEventData eventData)//
	{
		GameObject go = eventData.pointerCurrentRaycast.gameObject;//获取到鼠标终点位置下 可能的物体


		if (go != null)
		{
			Debug.Log(go.name);
			if (go.tag.Equals("Grid"))//鼠标终点位置下是： 空格子（所以直接放进去）
			{
				
				SetParentAndPosition(transform, go.transform);
                go.GetComponent<EquipAndBag_Grid>().id = this.id;
                go.GetComponent<EquipAndBag_Grid>().num = this.count;
                //ChangeParentId (transform.parent,go.transform);
                ChangeCount (transform.parent,go.transform);
            }
			else if (go.tag.Equals("PlayerPetItem"))//鼠标终点位置下是： 也是一个物体（所以需要交换位置）
			{//交换位置要注意可能需要把物品下的子物体的Raycast Target关掉（不去掉可能无法交换）
				//ChangeParentId (transform.parent,go.transform.parent);
                go.transform.parent.GetComponent<EquipAndBag_Grid>().id = this.id;
                go.transform.parent.GetComponent<EquipAndBag_Grid>().num = this.count;
                transform.parent.GetComponent<EquipAndBag_Grid>().id = go.GetComponent<Drag2>().id;
                transform.parent.GetComponent<EquipAndBag_Grid>().num = go.GetComponent<Drag2>().count;
                //ChangeIdandCount(transform.parent,go.transform.parent);
                SetParentAndPosition(transform, go.transform.parent);//将被拖拽的物体1放到鼠标终点下的格子2里面
				SetParentAndPosition(go.transform, nowParent);//将鼠标终点格子2里面物体2 放到 原来物体1的格子1里面
				ChangeCount (transform.parent,go.transform.parent);


				if (transform.position == go.transform.position)
				{
					Debug.Log("error");
				}
			}else if(go.tag.Equals("shortcutdrug")){
				
				go.GetComponent<ShortCutItem> ().setDrag2 (id,count);
				Destroy (gameObject);
			}else if(go.tag.Equals("shopweapon")){

                objectInfo info = ObjectsInfo.instance.FindObjecInfoById (id);

				EquipAndBag.instance.GetMoney (info.price_sell*count);
                Destroy(gameObject);

            }
			else//鼠标终点是：无效位置（所以物体放回原来的位置）
			{
				SetParentAndPosition(transform, nowParent);
			}
		}
		else//
		{
			SetParentAndPosition(transform, nowParent);
		}
		isRaycastLocationValid = true;//ui事件穿透：置为不能穿透
	}

	private void SetParentAndPosition(Transform child, Transform parent)//将child放到parent下作为子物体
	{
		child.SetParent(parent);
		child.position = parent.position;
	}

    private void getidandcount() {

    }
	private void ChangeParentId(Transform nowparent, Transform nextparent){

		EquipAndBag_Grid now = nowParent.gameObject.GetComponent<EquipAndBag_Grid> ();
		EquipAndBag_Grid next = nextparent.gameObject.GetComponent<EquipAndBag_Grid> ();

		int temp = now.id;
		now.id = next.id;
		next.id = temp;

		int count1 = now.num;
		now.num = next.num;
		next.num = count1;

		
	}

	public void ChangeCount(Transform nowparent, Transform nextparent){

		EquipAndBag_Grid now = nowParent.gameObject.GetComponent<EquipAndBag_Grid> ();
		EquipAndBag_Grid next = nextparent.gameObject.GetComponent<EquipAndBag_Grid> ();
        now.setCount();
        next.setCount();
        now.getItemCount ().SetActive (false);
		next.getItemCount ().SetActive (true);
	}

	private void ChangeIdandCount(Transform nowparent, Transform nextparent){
		EquipAndBag_Grid now = nowParent.gameObject.GetComponent<EquipAndBag_Grid> ();
		EquipAndBag_Grid next = nextparent.gameObject.GetComponent<EquipAndBag_Grid> ();
		int id = now.id;
		int num = now.num;
		now.setId (next.id,next.num);
		next.setId (id,num);
	}


	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)//UI事件穿透：如置为false即可以穿透，被图片覆盖的按钮可以被点击到
	{
		return isRaycastLocationValid;
	}


	public void OnPointerEnter(PointerEventData eventData)
	{	


		item_dis.instance.showDis (id);

		flag = true;

		//item_dis.instance.transform.position = Input.mousePosition;
	}

	public void OnPointerExit(PointerEventData eventData){
		item_dis.instance.gameObject.SetActive (false);　
		flag = false;
	}

	void LateUpdate(){
		if (flag) {
			if (Input.GetMouseButtonDown (1)) {
				item_dis.instance.gameObject.SetActive (false);　
				EquipAndBag_Grid now = transform.parent.gameObject.GetComponent<EquipAndBag_Grid> ();
				if(now.id>=1001&&now.id<=1003){
					if(id==0){
						return;
					}
					objectInfo info = ObjectsInfo.instance.FindObjecInfoById (now.id);
					int hp = info.hp;
					int mp = info.mp;
					ps.GetDrug (hp,mp);
					transform.parent.GetComponent<EquipAndBag_Grid> ().MinsNumber ();
				}else if(now.id>=2001&&now.id<=2023){
					bool success = Equip.instance.Dress (now.id);
					if (success) {
						transform.parent.GetComponent<EquipAndBag_Grid> ().MinsNumber ();
               
                        
					}
				}

			}
		}
	}
		
}


