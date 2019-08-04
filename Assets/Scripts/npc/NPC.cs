using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    void OnMouseEnter() {
		CursorManager.instance.SetTalk();
    }
    void OnMouseExit() {
        CursorManager.instance.SetNormal();
    }

}
