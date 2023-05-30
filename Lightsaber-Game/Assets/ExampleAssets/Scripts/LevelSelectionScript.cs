using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelSelectionScript : MonoBehaviour
{
    public ScrollRect levelList;
    public GameObject simpleButton;

    //private static string levelPath = "Assets\\StreamingAssets\\test"; //Application.persistentDataPath;
	private static string levelPath = "Assets/StreamingAssets/test";
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

        levelNames = Directory.GetFiles(levelPath, "*.txt");

        foreach(string name in levelNames)
        {
            Debug.Log("available Level: " + name);
        }

        return levelNames;
		

			
    }

    private void printLevelNames(string[] levelNames)
    {
		Vector3 buttonPosition = new Vector3(-3.20f, -0.8f, 0f); // Set the button position

        foreach(string filePath in levelNames)
        {
			
			string levelName = Path.GetFileNameWithoutExtension(filePath);
            GameObject levelButton = Instantiate(simpleButton, levelList.content);


            Text buttonTextComponent = levelButton.GetComponentInChildren<Text>();
            buttonTextComponent.text = levelName;
            levelButton.transform.localScale = new Vector3(3, 3, 3);
			levelButton.transform.position = buttonPosition; // Set button position

			buttonPosition.y -= 0.18f; // Adjust the y position for the next button

        }

		
    }
	
/*
	private void printLevelNames(string[] levelNames)
	{
		float buttonHeight = 50f; // Set the button height
		float spacing = 10f; // Spacing between buttons

		Vector3 buttonPosition = levelList.content.position; // Initial position

		for (int i = 0; i < levelNames.Length; i++)
		{
			string levelName = Path.GetFileNameWithoutExtension(levelNames[i]);
			GameObject levelButton = Instantiate(simpleButton, levelList.content);

			Text buttonTextComponent = levelButton.GetComponentInChildren<Text>();
			buttonTextComponent.text = levelName;
			levelButton.transform.localScale = new Vector3(3, 3, 3);

			// Adjust the button position based on index
			float buttonY = buttonPosition.y - (i * (buttonHeight + spacing));
			levelButton.transform.position = new Vector3(buttonPosition.x, buttonY, buttonPosition.z);
		}
	}*/

}
