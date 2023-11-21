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

[Serializable]
public class RootObject
{
    public List<LevelInfo> levelParts;
}

[Serializable]
public class LevelInfo
{
    public string sentence;
    public List<Block> blocks;
    public int correctBlock;
}

[Serializable]
public class Block
{
    public string text;
    public string position;
}

public class GameScript : MonoBehaviour
{
    public GameObject sentenceObject;
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
        ///Download level files
        //ServerConnection serverConnection = new ServerConnection();
        //serverConnection.test();

        //sentenceObject = GameObject.Find("Text");
        sentenceScript = sentenceObject.GetComponent<SentenceScript>();

        string filePath = "";
        try
        {
            //filePath = Path.Combine(Application.persistentDataPath + "/LevelFiles", "TestLevel.json");
            filePath = ".\\Assets\\StreamingAssets\\test\\test.json";
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
            Application.Quit();
        }
        

        string jsonString = File.ReadAllText(filePath);

        List<string> jsonSingles = seperateJson(jsonString);

        foreach(string content in jsonSingles)
        {
            Debug.Log(content);
            LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(content);
            levelParts.Add(levelInfo);
        }
        sentence = levelParts[0].sentence;
        Debug.Log(levelParts[0].blocks[1].position);
        foreach(Block block in levelParts[0].blocks)
        {
            words.Add(block.text);
        }
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
            words.Clear();
            foreach (Block block2 in levelParts[levelCounter].blocks)
            {
                words.Add(block2.text);
            }
            correctBlock = levelParts[levelCounter].correctBlock + 1;

            sentenceScript.updateSentence();
            return;
        }
        if (levelParts[levelCounter].blocks[noBlocks - 1].position == "right")
        {
            Instantiate(Textblock, new Vector3(2f, 3, 15), Quaternion.identity);
        }
        else
        {
            Instantiate(Textblock, new Vector3(-1f, 3, 15), Quaternion.identity);
        }
        GameObject block = GameObject.Find("Textblock(Clone)");
        block.name = "Block" + noBlocks.ToString();
    }

    public List<string> seperateJson(string jsonString)
    {
        jsonString = jsonString.Trim('[');

        bool splitAtComma = false;
        string jsonObject = "";
        List<string> jsonList = new List<string>();
        bool inBlockList = false;

        foreach(char c in jsonString)
        {
            if(c == '[')
            {
                inBlockList = true;
                jsonObject += c;
            }
            else if(c == ']' && inBlockList)
            {
                inBlockList = false; 
                jsonObject += c;
            }
            else if(c == '{' && !inBlockList)
            {
                splitAtComma = false;
                jsonObject += c;
            }
            else if(c == '}' && !inBlockList)
            {
                splitAtComma = true;
                jsonObject += c;
            }
            else if((c == ',' || c == ']') && splitAtComma && !inBlockList)
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