using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using ExampleAssets.Scripts;



public class LevelSelectionScript : MonoBehaviour
{
    public ScrollRect levelList;
    public GameObject simpleButton;
	private string selectedLevelPath; // Path of the selected level

    private EventSystem eventSystem;


    //private static string levelPath = "Assets\\StreamingAssets\\test"; //Application.persistentDataPath;
    // private static string levelPath = Application.persistentDataPath + "\\LevelFiles";
    // Start is called before the first frame update


    async void Start()
    {
        await downloadLevels();

        string[] levelNames = getLevelNames();
		printLevelNames(levelNames);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task downloadLevels()
    {
        ServerConnection serverConnection = new ServerConnection();
        await serverConnection.test();
    }

    private string[] getLevelNames()
    {
		
        string[] levelNames;

        levelNames = Directory.GetFiles(Application.persistentDataPath + "\\LevelFiles", "*.json");

        foreach(string name in levelNames)
        {
            Debug.Log("available Level: " + name);
        }

        return levelNames;
		

			
    }

    private void printLevelNames(string[] levelNames)
    {
		Vector3 buttonPosition = new Vector3(-3.20f, 0.7f, 0f); // Set the button position

        foreach(string filePath in levelNames)
        {
			
			string levelName = Path.GetFileNameWithoutExtension(filePath);
            GameObject levelButton = Instantiate(simpleButton, levelList.content);
            //Button levelButton = Instantiate(simpleButton, levelList.content);



            Text buttonTextComponent = levelButton.GetComponentInChildren<Text>();
            buttonTextComponent.text = levelName;

			Button buttonButtonComponent = levelButton.GetComponentInChildren<Button>();


            levelButton.transform.localScale = new Vector3(3, 3, 3);
			levelButton.transform.position = buttonPosition; // Set button position

			buttonPosition.y -= 0.18f; // Adjust the y position for the next button

            //store fiel path if button click

            //Button button = levelButton.GetComponent<Button>();
            buttonButtonComponent.onClick.AddListener(() => OnLevelButtonClicked());
        }        

    }

    private void OnLevelButtonClicked()
	{
        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        Text buttonText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();

        selectedLevelPath = Application.persistentDataPath + "\\LevelFiles\\" + buttonText.text + ".json";
		
		//copy selected level to "Assets/StreamingAssets" as TestLevel.json
		File.Copy(selectedLevelPath, Path.Combine(Application.persistentDataPath + "/LevelFiles", "TestLevel.json"), true);
		Debug.Log("Selected level copied to: " + Path.Combine(Application.persistentDataPath + "/LevelFiles", "TestLevel.json"));
		
		//go back to main menu
		SceneManager.LoadScene("MainMenu");
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
