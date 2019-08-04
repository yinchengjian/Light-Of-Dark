using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;




public class Game : MonoBehaviour { 

	public playerstatus ps;
	[SerializeField]
	private GameObject menu;
	[SerializeField]
	private GameObject targets;
    [SerializeField]
    private EquipAndBag_Grid[] grids;
    [SerializeField]
    private GameObject[] equipments;

    private bool isPaused = false;


    public List<Item> items = new List<Item>();

    public List<int> equip_id = new List<int>();

    public Dictionary<string, Task> dictionary = new Dictionary<string, Task>();


    public GameObject suceesstip;


    // Use this for initialization
    private void Awake()
	{
        
	}
	void Start () {
		targets = GameObject.FindGameObjectWithTag ("Player").gameObject;
		ps = targets.GetComponent<playerstatus> ();

	}
	public void Pause()
	{
		menu.SetActive(true);
		Cursor.visible = true;
		Time.timeScale = 0;
		isPaused = true;
	}

	public void Unpause()
	{
		menu.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1;
		isPaused = false;
	}

	public bool IsGamePaused()
	{
		return isPaused;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (isPaused)
			{
				Unpause();
			}
			else
			{
				Pause();
                
            }
		}
	}

	private Save CreateSaveGameObject()
	{   //为了防止每次保存都往里面写东西
        Save save = new Save();
        save.coincount = EquipAndBag.instance.coincount;
        save.i = Dialog.instance.i;
        save.j = Dialog.instance.j;
        save.isTask = BarNPC._instance.isInTask;
        save.dia = Dialog.instance.talk.text;
        items.Clear();
        foreach (EquipAndBag_Grid grid in grids) {
            if (grid.id != 0) {
                Item it = new Item(grid.id, grid.num);
                items.Add(it);
            }
        }
        foreach (GameObject go in equipments) {
            if (go.transform.childCount != 0) {
                int id = go.GetComponentInChildren<EquipItem>().id;
                if (!equip_id.Contains(id)) {
                    equip_id.Add(id);
                }             
            }
               
        }
        //保存任务
        dictionary = TaskManager.Instance.dictionary;




        return save;
	}

	public void SaveGame()
	{
		// 1
		Save save = CreateSaveGameObject();

		// 2
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();
	}

	public void LoadGame()
	{ 
		// 1
		if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
		{
			

			// 2
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize(file);
			file.Close();

			// 3

			//targets.GetComponent<playerstatus> ().def_plus = save.def_plus;

			// 4


			Debug.Log("Game Loaded");

		}
		else
		{
			
			Debug.Log("No game saved!");
		}
	}

	public void SaveJSON(){
        Save save = CreateSaveGameObject();
        string save_json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/save.json", save_json);

        string item_json = JsonUtility.ToJson(new Serialization<Item>(items));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/items.json", item_json);

        string player_json = JsonUtility.ToJson(ps.player);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/players.json", player_json);

        string equip_id_json = JsonUtility.ToJson(new Serialization<int>(equip_id));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/equip_id.json", equip_id_json);
       
        string tsak_json = JsonUtility.ToJson(new Serialization<string, Task>(dictionary));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/taskDic.json", tsak_json);

        StartCoroutine(ShowTip());


    }

    IEnumerator ShowTip() {
        suceesstip.SetActive(true);
        print("lalal");
        yield return new WaitForSeconds(0.5f);
        
        suceesstip.SetActive(false);
    }

    public void quit(){
    #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();  
    #endif

    }


    public void LoadJSON() {
        string json = File.ReadAllText(Application.dataPath + "/Json/items.json");
        List<Item> items = JsonUtility.FromJson<Serialization<Item>>(json).ToList();
        foreach (Item it in items)
        {
            EquipAndBag.instance.getId(it);
        }

    }
}
