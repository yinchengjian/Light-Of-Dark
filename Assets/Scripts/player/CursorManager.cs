using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

	public static CursorManager instance;
	public Texture2D cursor_normal;
	public Texture2D cursor_attack;
	public Texture2D cursor_lockTarget;
	public Texture2D cursor_talk;

	private Vector2 hotpot = Vector2.zero;
	private CursorMode mode = CursorMode.Auto;

	// Use this for initialization
	void Start () {
		instance = this;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetNormal(){
		Cursor.SetCursor (cursor_normal, hotpot, mode);
	}

	public void SetAttack(){
		Cursor.SetCursor (cursor_attack, hotpot, mode);
	}

	public void SetTalk(){
		Cursor.SetCursor (cursor_talk, hotpot, mode);
	}
	public void SetLockTarget() {
		Cursor.SetCursor(cursor_lockTarget, hotpot, mode);
	}
}
