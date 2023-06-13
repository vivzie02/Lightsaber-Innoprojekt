using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Xml;
using ExampleAssets.Scripts;

public class GameScript : MonoBehaviour
{
    private GameObject sentenceObject;
    public SentenceScript sentenceScript;

    public GameObject Textblock;
    public int correctBlock;
    public int noBlocks = 0;
    public List<string> words = new List<string>();
    public string sentence;

    public int levelCounter = 0;
    public List<LevelInfo> levelParts = new List<LevelInfo>();


    // Start is called before the first frame update
    void Start()
    {
        //Download level files
        ServerConnection serverConnection = new ServerConnection();
        serverConnection.test();

        sentenceObject = GameObject.Find("Text");
        sentenceScript = sentenceObject.GetComponent<SentenceScript>();

        string filePath = Path.Combine(Application.persistentDataPath + "/LevelFiles", "TestLevel.json");

        string jsonString = File.ReadAllText(filePath);

        List<string> jsonSingles = seperateJson(jsonString);

        foreach(string content in jsonSingles)
        {
            Debug.Log(content);
            LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(content);
            levelParts.Add(levelInfo);
        }
        sentence = levelParts[0].sentence;
        words = levelParts[0].blocks;
        correctBlock = levelParts[0].correctBlock + 1;

        sentenceScript.updateSentence();

        InvokeRepeating("createBlock", 0.5f, 4.0f);
    }

    private void createBlock()
    {
        noBlocks++;
        if (this.noBlocks > words.Count)
        {
            levelCounter++;
            noBlocks = 0;

            if (levelCounter >= levelParts.Count)
            {
                Debug.Log("Quit");
                //Quit level --> back to main menu/end screen 
            }

            Debug.Log("switch");

            sentence = levelParts[levelCounter].sentence;
            words = levelParts[levelCounter].blocks;
            correctBlock = levelParts[levelCounter].correctBlock + 1;

            sentenceScript.updateSentence();
            return;
        }
        Instantiate(Textblock, new Vector3(0.6f, 3, 15), Quaternion.identity);
        GameObject block = GameObject.Find("Textblock(Clone)");
        block.name = "Block" + noBlocks.ToString();
    }

    public List<string> seperateJson(string jsonString)
    {
        jsonString = jsonString.Trim('[');

        bool splitAtComma = false;
        string jsonObject = "";
        List<string> jsonList = new List<string>();

        foreach(char c in jsonString)
        {
            if(c == '{')
            {
                splitAtComma = false;
                jsonObject += c;
            }
            else if(c == '}')
            {
                splitAtComma = true;
                jsonObject += c;
            }
            else if((c == ',' || c == ']') && splitAtComma)
            {
                jsonList.Add(jsonObject);
                jsonObject = "";
            }
            else
            {
                jsonObject += c;
            }
        }

        return jsonList;
    }
}

public class LevelInfo
{
    public string sentence;
    public List<string> blocks;
    public int correctBlock;
}