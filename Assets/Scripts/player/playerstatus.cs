using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public enum herotype{
	swordman,
	magician,
	none,

}

public class playerstatus : MonoBehaviour {
    /*
	public string name = "moren";

	public int level =1;
	public int hp =100;
	public int mp =100;
	public int hp_remain = 100;
	public int mp_remain = 100;

	public int coin = 200;

	public int attack = 20;
	public int attack_plus = 0;

	public int def = 20;
	public int def_plus = 0;

	public int speed = 20;
	public int speed_plus = 0;

	public int point_remain = 0;

	public float exp = 0;
*/
    public GameObject levelup;

    private string jsondata;
    public herotype hero = herotype.none;
    
	public Player player;

    private float time = 3f;
    private float timer = 0;

	void Start(){
		
		GetExp (0);
	}

	public bool GetPoint(int point=1){
		if (player.point_remain >= point) {
			player.point_remain -= point;
			return true;
		} else {
			return false;
		}
	}


    void AutoAdd() {     
        if (this.player.hp_remain<this.player.hp) {
            this.player.hp_remain += this.player.hp_plus;
            if (this.player.hp_remain > this.player.hp) {
                this.player.hp_remain = this.player.hp;
            }
        }
        if (this.player.mp_remain < this.player.mp) {
            this.player.mp_remain += this.player.mp_plus;
            if (this.player.mp_remain > this.player.mp)
            {
                this.player.mp_remain = this.player.mp;
            }
        }
    }

    void Update(){
        timer += Time.deltaTime;
        if (timer>time) {
            AutoAdd();
            timer = 0;
        }
    }

    public void GetDrug(int hp,int mp){
		player.hp_remain += hp;
		player.mp_remain += mp;
		if(player.hp_remain>this.player.hp){
			player.hp_remain = this.player.hp;
		}
		if(player.mp_remain>this.player.mp){
			player.mp_remain = this.player.mp;
		}
		Hp.instance.updateShow ();
	}

	public void GetExp(int exp){
		this.player.exp += exp;
		int total_exp = 100 + player.level * 30;
		while (this.player.exp >= total_exp) {
			this.player.level++;
			GameObject go = Instantiate (levelup, transform.position, Quaternion.identity)as GameObject;
			level_up ();
			this.player.exp -= total_exp;
			total_exp = 100 + player.level * 30;
		}

		ExpBar.instance.setValue (this.player.exp/total_exp);
	}
		

	public bool TakeMP(int count) {
		if (player.mp_remain >= count) {
			player.mp_remain -= count;
			Hp.instance.updateShow();
			return true;
		} else {
			return false;
		}
	}

	public void level_up(){
		this.player.hp = 100 + player.level * 60;
		this.player.mp = 100 + player.level * 60;
        this.player.hp_plus = player.level * 2;
        this.player.mp_plus = player.level * 2;
        this.player.hp_remain = this.player.hp;
		this.player.mp_remain = this.player.mp;
		this.player.attack += 10;
		this.player.def += 10;
		Hp.instance.updateShow ();
		status.instance.UpdateShow ();
		SkillObject.instance.updateShow (this.player.level);
	}
		
}
[System.Serializable]
public class Player{
	public int id;
	public string name;
	public herotype hero;
	public int level;
	public int hp;
	public int mp;

    public int hp_plus = 1;
    public int mp_plus = 1;

    public int hp_remain;
	public int mp_remain;

	public int attack;
	public int attack_plus;

	public int def;
	public int def_plus;

	public int speed;
	public int speed_plus;

	public int point_remain;

	public float exp;

}
