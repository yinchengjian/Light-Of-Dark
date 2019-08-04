using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipAndBag : MonoBehaviour {

	public static EquipAndBag instance;
	public List<EquipAndBag_Grid> gridList = new List<EquipAndBag_Grid>();
  
    public GameObject bag_item_temp;
	public bool isfalse = true;
	public int coincount = 300 ;
	public Text coin;
	// Use this for initialization

	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	public void getId(int id,int count=1){
		EquipAndBag_Grid grid = null;
		foreach (EquipAndBag_Grid temp in gridList) {
			if (temp.id == id) {
				grid = temp;
				break;
			}
		}
		if (grid != null) {
			grid.PlusNum (count);
		} else {
			foreach (EquipAndBag_Grid temp in gridList) {
				if (temp.id == 0) {
					grid = temp;
					break;
				}
			}
			if (grid != null) {
				GameObject bag_item = Instantiate (bag_item_temp);
				bag_item.transform.SetParent (grid.transform);
				bag_item.transform.position = grid.transform.position;
				grid.setId (id, count);

			} else {
				isfalse = false;
			}
		}
	}
    public void getId(Item it) {
        EquipAndBag_Grid grid = null;
        foreach (EquipAndBag_Grid temp in gridList)
        {
            if (temp.id == it.id)
            {
                grid = temp;
                break;
            }
        }
        if (grid != null)
        {
            grid.PlusNum(it.count);
        }
        else
        {
            foreach (EquipAndBag_Grid temp in gridList)
            {
                if (temp.id == 0)
                {
                    grid = temp;
                    break;
                }
            }
            if (grid != null)
            {
                GameObject bag_item = Instantiate(bag_item_temp);
                bag_item.transform.SetParent(grid.transform);
                bag_item.transform.position = grid.transform.position;
                grid.setId(it.id, it.count);

            }
            else
            {
                isfalse = false;
            }
        }
    }

	//取款方法
	public bool getCoin(int count){
		if (coincount >= count) {
			coincount -= count;
			coin.text = coincount.ToString ();
			return true;
		} else {
			return false;
		}
	}

	public void GetMoney(int money){
		this.coincount += money;
		coin.text= coincount.ToString ();
	}

    public void SetCoinShow(int coincount) {
        this.coincount = coincount;
        coin.text = coincount.ToString();
    }
}
