using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour {
	public GameObject player;
	public GameObject[] playerPrefab;
    public GameObject equip;
    public GameObject dialog;
    public string name;
    GameObject go = null;
    void Awake() {
		int selectdIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        //selectdIndex = -1;
        name = PlayerPrefs.GetString("name");
        if (selectdIndex == -1)
        {
            
            LoadGame();
        }
        else {
            NewGame(selectdIndex);
        }
		go.transform.SetParent (player.transform);

	}

    public void NewGame(int selectdIndex) {
        go = GameObject.Instantiate(playerPrefab[selectdIndex]) as GameObject;
        string json = File.ReadAllText(Application.dataPath + "/StreamingAssets/default_player.json");
        go.GetComponent<playerstatus>().player = JsonUtility.FromJson<Player>(json);
        go.GetComponent<playerstatus>().player.id = selectdIndex;
        go.GetComponent<playerstatus>().player.name = name;
    }

    public void JumpScence() {
        SceneManager.LoadScene(0);
    }



    public void LoadGame() {

        //人物信息
        string json = File.ReadAllText(Application.dataPath + "/StreamingAssets/players.json");
        Player p = JsonUtility.FromJson<Player>(json);
         if (p.id == 0)
        {
            go = GameObject.Instantiate(playerPrefab[0]) as GameObject;
            go.GetComponent<playerstatus>().player = p;
         }
        else
        {
            go = GameObject.Instantiate(playerPrefab[1]) as GameObject;
            go.GetComponent<playerstatus>().player = p;
        }
        //物品信息
        string item_json = File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json");
        List<Item> items = JsonUtility.FromJson<Serialization<Item>>(item_json).ToList();
        foreach (Item it in items)
        {
            EquipAndBag.instance.getId(it);
        }
        //装备信息
        string equip_id_json = File.ReadAllText(Application.dataPath + "/StreamingAssets/equip_id.json");
        List<int> equip_id = JsonUtility.FromJson<Serialization<int>>(equip_id_json).ToList();
        foreach (int id in equip_id)
        {

            equip.GetComponent<Equip>().Dress_load(id);
        }

        //金币信息
        string save_json = File.ReadAllText(Application.dataPath + "/StreamingAssets/save.json");
        Save save = JsonUtility.FromJson<Save>(save_json);
        EquipAndBag.instance.SetCoinShow(save.coincount);
        //对话信息
        BarNPC._instance.isInTask = save.isTask;
        dialog.GetComponent<Dialog>().i = save.i;
        dialog.GetComponent<Dialog>().j = save.j;
        dialog.GetComponent<Dialog>().talk.text = save.dia;
       

        //任务信息
        string tsak_json = File.ReadAllText(Application.dataPath + "/StreamingAssets/taskDic.json");
        Dictionary<string, Task> dictionary = JsonUtility.FromJson<Serialization<string, Task>>(tsak_json).ToDictionary();
  
            TaskManager.Instance.LoadTask(dictionary);
    
        


    }

}
