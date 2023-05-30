using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelSelectionScript : MonoBehaviour
{
    public ScrollRect levelList;
    public GameObject simpleButton;

    private static string levelPath = "Assets\\StreamingAssets\\test"; //Application.persistentDataPath;

    // Start is called before the first frame update
    void Start()
    {
        string[] levelNames = getLevelNames();
        printLevelNames(levelNames);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string[] getLevelNames()
    {
        string[] levelNames;

        levelNames = Directory.GetFiles(levelPath);

        foreach(string name in levelNames)
        {
            Debug.Log("available Level: " + name);
        }

        return levelNames;
    }

    private void printLevelNames(string[] levelNames)
    {
        foreach(string levelName in levelNames)
        {
            GameObject levelButton = Instantiate(simpleButton, levelList.content);

            Text buttonTextComponent = levelButton.GetComponentInChildren<Text>();
            buttonTextComponent.text = levelName;
            levelButton.transform.localScale = new Vector3(3, 3, 3);
        }
    }
}
