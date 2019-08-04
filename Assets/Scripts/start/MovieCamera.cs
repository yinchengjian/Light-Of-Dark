using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class MovieCamera : MonoBehaviour {

    public float speed = 10;

    private float endZ = -20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z < endZ) {//还没有达到目标位置，需要移动
            transform.Translate( Vector3.forward*speed*Time.deltaTime);
        }
	}
	

	public void ScenceLoad(){
		SceneManager.LoadScene (1);
	}

	public void LoadGame(){
		PlayerPrefs.SetInt ("SelectedCharacterIndex",-1);
		SceneManager.LoadScene (2);
	}

    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();  
#endif

    }
}
