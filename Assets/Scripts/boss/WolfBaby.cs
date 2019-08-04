using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum bossState {
	idle,
	walk,
	attack,
	death
}

public class WolfBaby : MonoBehaviour {

	public bossState state = bossState.idle;

	public int hp = 100;
	public int exp = 20;
	public int money;
	public int attack = 10;
	public float miss_rate;

	public Animation animation_wolf;
	public string animation_now;
	public string animation_walk;
	public string animation_idle;
	public string animation_death;
	public GameObject body;

	public float time = 1;
	public float timer = 0;

	public float speed;

	private bool is_attacked = false;
	public Color normal;
	public AudioClip miss_sound;

	public Transform target;

	public GameObject prefab;
	public Vector3 offset = new Vector3(0,20,0);

	public string aniname_normalattack;
	public float time_normalattack;

	public string aniname_crazyattack;
	public float time_crazyattack;
	public float crazyattack_rate;

	public string aniname_attack_now;
	public float attack_rate =1;//攻击速率 每秒
	private float attack_timer = 0;

	public float minDistance=2;
	public float maxDistance=5;

	private playerstatus ps;

    public string name;

	// Use this for initialization
	void Start () {
		animation_wolf = GetComponent<Animation> (); 
		normal = body.GetComponent<Renderer> ().material.color;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerstatus> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (target!=null) {
            if (Vector3.Distance(transform.position, target.position) < maxDistance)
            {
                state = bossState.attack;
            }
        }
		if (state == bossState.death){
			animation_wolf.CrossFade(animation_death);
		}
		else if (state == bossState.attack){
			//StartCoroutine (turn_idle ());
			AutoAttack ();
		}
		else {
			animation_wolf.CrossFade(animation_now);
			if (animation_now == animation_walk) {
				
				transform.Translate (0,0,1*speed*Time.deltaTime);

			}
			timer += Time.deltaTime;
			if (timer >= time) {
				timer = 0;
                RandomState();	
			}

		}
	}

	void RandomState() {
		int i = Random.Range(0,2);
		if (i == 0)
		{
			animation_now = animation_idle;
		}
		else {
			if (animation_now == animation_idle) {
                transform.Rotate(transform.up * Random.Range(0, 360));
			}
			animation_now = animation_walk;
		}
	}

	public void TakeDamage(int attack) {//受到伤害
 		if (state == bossState.death) return;
		target = GameObject.FindGameObjectWithTag("Player").transform;
		state = bossState.attack;
		float value = 1f;
		if (value < miss_rate) {// Miss效果
			AudioSource.PlayClipAtPoint(miss_sound, transform.position);
			showhurt("Miss");
			//hudtext.Add("Miss", Color.gray, 1);
		} else {//打中的效果
			//hudtext.Add("-"+attack, Color.red, 1);
			showhurt((0-attack).ToString());
			this.hp -= attack;
			StartCoroutine( ShowBodyRed() );
			if (hp <= 0) {
                target = null;
                state = bossState.death;
				DestroySelf ();
			}
		}
	}
	IEnumerator ShowBodyRed() {
		body.GetComponent<Renderer> ().material.color = Color.red;
		yield return new WaitForSeconds(1f);
		body.GetComponent<Renderer> ().material.color = normal;
	}

	void showhurt(string attack){
		
		GameObject temp = GameObject.Instantiate(prefab);
		if (attack.Equals ("Miss")) {
			temp.GetComponent<Text> ().color = Color.yellow;
		} else {
			temp.GetComponent<Text> ().color = Color.red;
		}

		temp.transform.parent = GameObject.Find("Canvas").transform;

		temp.transform.position = Camera.main.WorldToScreenPoint(transform.position) + offset;

		temp.GetComponent<Text>().text = attack;
	}

	void AutoAttack() {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null) {
			PlayerState playerState = target.GetComponent<playerAnimation>().bigstate;
			if (playerState == PlayerState.Death) {
				target = null;
				state = bossState.idle;
				return;
			}
			float distance = Vector3.Distance(target.position, transform.position);
			if (distance > maxDistance) {//停止自动攻击
				//target = null;
				state = bossState.idle;
			} else if (distance <= minDistance) {//自动攻击
				transform.LookAt (target);
				attack_timer+=Time.deltaTime;
				animation_wolf.CrossFade(aniname_attack_now);
				if (aniname_attack_now == aniname_normalattack) {
					if (attack_timer > time_normalattack) {
						//产生伤害 
						target.GetComponent<playerAttack>().TakeDamage(attack);
						aniname_attack_now = animation_idle;
					}
				} else if (aniname_attack_now == aniname_crazyattack) {
					if (attack_timer > time_crazyattack) {
                        //产生伤害 
						target.GetComponent<playerAttack>().TakeDamage(attack+10);
						aniname_attack_now = animation_idle;
					}
				}
				if (attack_timer > (1f / attack_rate)) {
					RandomAttack();
					attack_timer = 0;
				}
			} else {//朝着角色移动
				transform.LookAt(target);
				//cc.SimpleMove(transform.forward * speed);
				transform.Translate (0,0,1*speed*Time.deltaTime);
				animation_wolf.CrossFade(animation_walk);
			}
		} else {
			state = bossState.idle;
		}
	}

	void RandomAttack() {
		float value = Random.Range(0f, 1f);
		if (value < crazyattack_rate) {//进行疯狂攻击
			aniname_attack_now = aniname_crazyattack;
		} else {//进行普通攻击
			aniname_attack_now = aniname_normalattack;
		}
	}


	void DestroySelf(){
        transform.parent.GetComponent<WolfSpawn>().MinusNumber();
		Destroy(this.gameObject, 1);
		ps.GetExp (exp);
		EquipAndBag.instance.GetMoney (money);
        TaskEventArgs e = new TaskEventArgs();
        e.id = name;
        e.amount = 1;
        MesManager.Instance.Check(e);
        //BarNPC._instance.OnKillWolf ();
    }

	void OnMouseEnter() {
		if(playerAttack._instance.isLockingTarget==false)
			CursorManager.instance.SetAttack();
	}
	void OnMouseExit() {
		if (playerAttack._instance.isLockingTarget == false)
			CursorManager.instance.SetNormal();
	}
}
