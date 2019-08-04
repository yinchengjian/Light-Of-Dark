using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class Typer : MonoBehaviour
{

    public float charsPerSeconds = 0.2f;

    private string content;

    private Text textTest;

    private float timer;

    private int currentPos;

    private bool isActive;

    // Use this for initialization

    void Start()
    {

        textTest = GetComponent<Text>();

        content = textTest.text;

        textTest.text = "";

        charsPerSeconds = Mathf.Max(0.2f, charsPerSeconds);

        timer = charsPerSeconds;

        isActive = false;

        currentPos = 0;

    }

    // Update is called once per frame

    void Update()
    {
        if (currentPos < content.Length) {
            StartTyperEffect();
        }
       

    }

    public void TyperEffect()
    {

        isActive = true;

    }

    private void StartTyperEffect()
    {

        timer += Time.deltaTime;

        if (timer > charsPerSeconds)
        {

            timer -= charsPerSeconds;

            currentPos++;

            textTest.text = content.Substring(0, currentPos);

        }

    }

}