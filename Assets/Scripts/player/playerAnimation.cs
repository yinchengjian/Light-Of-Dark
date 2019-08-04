using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState{
	ControlWalkState,
	NormalAttack,
	SkillAttack,
	Death
}




public class playerAnimation : MonoBehaviour{


	public PlayerState bigstate = PlayerState.ControlWalkState;
	public Animation animation;
	public playerstatus ps;
	public playerAttack attack;

	// Use this for initialization
	void Start()
	{
		ps = GetComponent<playerstatus> ();
		animation = GetComponent<Animation>();
		attack = GetComponent<playerAttack> ();
	}

	// Update is called once per frame
	void LateUpdate(){	
		if (bigstate == PlayerState.NormalAttack) {
			if (attack.attack_state == AttackState.Moving) {
				if (ps.hero == herotype.swordman) {
					animation.CrossFade ("Sword-Walk");
				} else {
					animation.CrossFade ("Walk");
				}

			} else if (attack.attack_state == AttackState.Attack) {
				
				if (ps.hero == herotype.swordman) {
					
					animation.CrossFade ("Sword-Attack2");
				} else {
					animation.CrossFade ("Attack1");
				}
			} else if (attack.attack_state == AttackState.Idle) {
				if (ps.hero == herotype.swordman) {
					animation.CrossFade ("Sword-Idle");
				} else {
					animation.CrossFade ("Idle");
				}
			}
		} else if (bigstate == PlayerState.Death) {
			if (ps.hero == herotype.swordman) {
				animation.CrossFade ("Sword-Death");
			} else {
				animation.CrossFade ("Death");
			}
		} else if (bigstate == PlayerState.ControlWalkState) {
			if(attack.state == ControlWalkState.Moving){

				if (ps.hero == herotype.swordman) {
					animation.CrossFade ("Sword-Walk");
				} else {
					animation.CrossFade ("Walk");
				}


			}else if(attack.state == ControlWalkState.Idle){
				
				if (ps.hero == herotype.swordman) {
					animation.CrossFade ("Sword-Idle");
				} else {
					animation.CrossFade ("Idle");
				}
			}else if(attack.state == ControlWalkState.Run){
				if (ps.hero == herotype.swordman) {
					animation.CrossFade ("Sword-Run");
				} else {
					animation.CrossFade ("Run");
				}
			}
		}
	}
}

