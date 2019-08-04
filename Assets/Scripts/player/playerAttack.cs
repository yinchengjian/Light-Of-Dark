using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum AttackState {//攻击时候的状态
	Moving,
	Idle,
	Attack
}

public enum ControlWalkState{
	Moving,
	Idle,
	Run,
}

public class playerAttack : MonoBehaviour {

	public static playerAttack _instance;
	public Animation animation;

	public ControlWalkState state = ControlWalkState.Idle;
	public AttackState attack_state = AttackState.Idle;

	public float time_normalattack;//普通攻击的时间
	public float rate_normalattack = 1;
	private float timer = 0;
	public float min_distance = 5;//默认攻击的最小距离
	private Transform target_normalattack;

	public GameObject effect;
	private bool showEffect = false;
	private playerstatus ps;
	public float miss_rate = 0.25f;

	public AudioClip miss_sound;
	public GameObject body;
	private Color normal;
	public string aniname_death;

	public GameObject[] efxArray;
	private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();

	public bool isLockingTarget = false;//是否正在选择目标
	private SkillInfo info = null;

	public playerAnimation pa;
	public GameObject prefab;
	public Vector3 offset = new Vector3(0,50,0);

	public Transform target;

	float speed = 5.0f;   //移动速度
	float rotationSpeed = 70.0f;  //旋转速度

    public GameObject setting;
    public bool dic = false;


    void Awake() {
		_instance = this;
        
        ps = this.GetComponent<playerstatus>();
		animation = GetComponent<Animation> ();
		normal = body.GetComponent<Renderer> ().material.color;
		pa = this.GetComponent<playerAnimation> ();
		foreach(GameObject go in efxArray){
			efxDict.Add (go.name , go);
		}

	}

	void Start() {
        setting = GameObject.Find("Canvas/over").gameObject;
    }

	void Update() {
        moveMouse ();
        //upmove();
        attack ();
	
	}

	public void attack(){
		if(Input.GetKey(KeyCode.X)){
			if (attack_state != AttackState.Attack) {
				StartCoroutine (normal_attack ());
			}
		}
	}
	IEnumerator normal_attack(){
		showEffect = false;
		pa.bigstate = PlayerState.NormalAttack;
		attack_state = AttackState.Attack;
		yield return new WaitForSeconds (0.5f);
		if(showEffect==false){
			showEffect = true;
			//播放特效
			GameObject go = null;
			if(ps.hero == herotype.swordman){
				go = GameObject.Instantiate(effect, transform.position + transform.forward * 1f, Quaternion.identity) as GameObject;
			}else if(ps.hero == herotype.magician){
				go = GameObject.Instantiate(effect, transform.position + transform.forward * 3f, Quaternion.identity) as GameObject;
			}

			go.GetComponent<SkillEffect> ().attack = GetAttack();
		}
		yield return new WaitForSeconds (0.5f);
		attack_state = AttackState.Idle;
	}

    public void upmove() {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (dic == true)
            {
                transform.Rotate(Vector3.up * 70 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up * -70 * Time.deltaTime);
            }

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (dic == true)
            {
                transform.Rotate(Vector3.up * -70 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up * 70 * Time.deltaTime);
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && dic == true)
        {
            transform.Rotate(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
            dic = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)&& dic == false)
        {
            transform.Rotate(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);//小车控制时，前进后退movespeed前都有负号
            dic = true;
        }
    }

	public void moveMouse(){
		// 使用上下方向键或者W、S键来控制前进后退
		if (Input.GetAxisRaw ("Vertical") != 0|| Input.GetAxisRaw("Horizontal") != 0) {
			pa.bigstate = PlayerState.ControlWalkState;
			state = ControlWalkState.Moving;
			speed = 5.0f;
			if(Input.GetKey(KeyCode.LeftShift)){
				state = ControlWalkState.Run;
				speed = 8.0f;

			}
		} else {
			state = ControlWalkState.Idle;
		}
        
		float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		//使用左右方向键或者A、D键来控制左右旋转
		float rotation = Input.GetAxis("Horizontal") * 3 * Time.deltaTime;
        if (Cursor.visible == false) {
            transform.Rotate(0, Input.GetAxis("Mouse X") * 50 * Time.deltaTime, 0);
        }
        transform.Translate(0, 0, translation);
        transform.Translate(rotation, 0, 0); //绕Y轴旋转  
        
    }


	public int GetAttack() {
		return (int)( Equip.instance.attack + ps.player.attack + ps.player.attack_plus);
	}



	public void TakeDamage(int attack) {
		if (pa.bigstate == PlayerState.Death) return;
		target = GameObject.FindGameObjectWithTag("enemy").transform;
        print(ps.player.def);
		float def = Equip.instance.def + ps.player.def + ps.player.def_plus;
		float temp = attack * ((200 - def) / 200);
		if (temp < 1) temp = 1;

		float value = Random.Range(0f, 1f);
		if (value < miss_rate) {//MISS
			AudioSource.PlayClipAtPoint(miss_sound, transform.position);
			showhurt ("Miss");
			//hudtext.Add("MISS", Color.gray, 1);
		} else {
			//hudtext.Add("-" + temp, Color.red, 1);
			showhurt("-"+temp);
			ps.player.hp_remain -= (int)temp;
			StartCoroutine(ShowBodyRed());
			if (ps.player.hp_remain <= 0) {
				pa.bigstate = PlayerState.Death;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameObject gameover = setting.transform.Find("GameOver").gameObject;
                Text dis = gameover.transform.Find("dis").GetComponent<Text>();
                dis.text = "游戏失败";
                gameover.SetActive(true);
            }
		}
		Hp.instance.updateShow();
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


	IEnumerator OnPassiveSkillUse(SkillInfo info ) {
		pa.bigstate = PlayerState.SkillAttack;
		GetComponent<Animation>().CrossFade(info.aniname);
		yield return new WaitForSeconds(info.anitime);
		pa.bigstate = PlayerState.ControlWalkState;
		int hp = 0, mp = 0;
		if (info.applyProperty == ApplyProperty.HP) {
			hp = info.applyValue;
		} else if (info.applyProperty == ApplyProperty.MP) {
			mp = info.applyValue;
		}

		ps.GetDrug(hp,mp);
		//实例化特效
		GameObject prefab = null;
		efxDict.TryGetValue(info.efx_name, out prefab);
		GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
	}

	IEnumerator OnBuffSkillUse(SkillInfo info) {
		pa.bigstate = PlayerState.SkillAttack;
		print (info.aniname);
		GetComponent<Animation>().CrossFade(info.aniname);

		yield return new WaitForSeconds(info.anitime/2);

		//实例化特效
		GameObject prefab = null;
		efxDict.TryGetValue(info.efx_name, out prefab);
		GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(info.anitime/2);
		pa.bigstate = PlayerState.ControlWalkState;
		switch (info.applyProperty) {
		case ApplyProperty.Attack:
			ps.player.attack *= (int)(info.applyValue / 100f);
			break;
		case ApplyProperty.AttackSpeed:
			rate_normalattack *= (int)(info.applyValue / 100f);
			break;
		case ApplyProperty.Def:
			ps.player.def *= (int)(info.applyValue / 100f);
			break;
		case ApplyProperty.Speed:
			speed *= (int)(info.applyValue / 100f);
			break;
		}
		yield return new WaitForSeconds(info.applyTime);
		switch (info.applyProperty) {
		case ApplyProperty.Attack:
			ps.player.attack /= (int)(info.applyValue / 100f);
			break;
		case ApplyProperty.AttackSpeed:
			rate_normalattack /= (int)(info.applyValue / 100f);
			break;
		case ApplyProperty.Def:
			ps.player.def /= (int)(info.applyValue / 100f);
			break;
		case ApplyProperty.Speed:
			speed /= (int)(info.applyValue / 100f);
			break;
		}
	}
		
	IEnumerator OnLockSingleTargetAndMultiTarget(SkillInfo info) {
		pa.bigstate = PlayerState.SkillAttack;
		print (info.aniname);
		print (info.anitime);
		GetComponent<Animation>().CrossFade(info.aniname);
		yield return new WaitForSeconds(info.anitime/2F);
		//实例化特效
		GameObject prefab = null;
		efxDict.TryGetValue(info.efx_name, out prefab);
		GameObject go = null;
		if(ps.hero == herotype.swordman){
			if (info.releaseType == ReleaseType.Self) {
				go = GameObject.Instantiate (prefab, transform.position+ transform.forward * 0.3f, Quaternion.identity) as GameObject;
			} else {
				go = GameObject.Instantiate(prefab, transform.position + transform.forward * 1f + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
			}
		}else if(ps.hero == herotype.magician){
			if(info.releaseType == ReleaseType.Enemy){
				go = GameObject.Instantiate(prefab, transform.position + transform.forward * 3f, Quaternion.identity) as GameObject;
			}else if(info.releaseType == ReleaseType.Position){
				go = GameObject.Instantiate(prefab, transform.position + transform.forward * 3f, Quaternion.identity) as GameObject;
			}
		}

		go.GetComponent<SkillEffect>().attack = GetAttack() * (info.applyValue / 100f);
		yield return new WaitForSeconds(info.anitime);
		pa.bigstate = PlayerState.ControlWalkState;

	}



	public void UseSkill(SkillInfo info) {
		if (ps.hero == herotype.magician) {
			if (info.applicableRole == ApplicableRole.Swordman) {
				return;
			}
		}
		if (ps.hero == herotype.swordman) {
			if (info.applicableRole == ApplicableRole.Magician) {
				return;
			}
		}
		switch (info.applyType) {
		case ApplyType.Passive:
			StartCoroutine( OnPassiveSkillUse(info));
			break;
		case ApplyType.Buff:
			StartCoroutine(OnBuffSkillUse(info));
			break;
		case ApplyType.SingleTarget:
			print ("xixi");
			StartCoroutine(OnLockSingleTargetAndMultiTarget(info));
			break;
		case ApplyType.MultiTarget:
			print ("xixi");
			StartCoroutine(OnLockSingleTargetAndMultiTarget(info));
			//OnLockSingleTargetAndMultiTarget(info);
			break;
		}

	}

}
