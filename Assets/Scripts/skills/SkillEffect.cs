using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour {

	public float attack = 0;
	private List<WolfBaby> wolflist = new List<WolfBaby> ();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col){
		if(col.tag.Equals("enemy")){
			WolfBaby baby = col.GetComponent<WolfBaby> ();
			int index = wolflist.IndexOf (baby);
			if(index==-1){
				baby.TakeDamage ((int)attack);
				wolflist.Add (baby);
			}
		}
	}
}
