using UnityEngine;
using UnityEngine.UI;

public class HideCursor : MonoBehaviour 
{
    int i = 0; 
	// Use this for initialization
	void Start () 
	{
		hide (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (i == 0)
            {
                hide(false);
                i = 1;
            }
            else {
                hide(true);
                i = 0;
            }
        }//隐藏鼠标
			

		
	}

	public void hide (bool hide)
	{
		if (hide) 
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} 
		else 
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
        
	}
}
