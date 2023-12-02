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
    public float delayInSeconds = 5f;

    async void Start()
    {
        Debug.Log("Start");
        await downloadLevels();

        string[] levelNames = getLevelNames();
		printLevelNames(levelNames);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Test method to check if button is pressed correctly in the unity editor
    //IEnumerator ClickLastButtonAfterDelay()
    //{
    //    yield return new WaitForSeconds(delayInSeconds);

    //    OnLevelButtonClicked("test123");
    //}

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
        Vector3 buttonPosition = new Vector3(-3.20f, 0.7f, 0f);

        foreach (string filePath in levelNames)
        {
            string levelName = Path.GetFileNameWithoutExtension(filePath);
            GameObject levelButton = Instantiate(simpleButton, levelList.content);

            Text buttonTextComponent = levelButton.GetComponentInChildren<Text>();
            buttonTextComponent.text = levelName;

            Button buttonButtonComponent = levelButton.GetComponentInChildren<Button>();

            levelButton.transform.localScale = new Vector3(3, 3, 3);
            levelButton.transform.position = buttonPosition;

            buttonPosition.y -= 0.18f;

            // Pass the button text as a parameter to the listener
            buttonButtonComponent.onClick.AddListener(() => OnLevelButtonClicked(buttonTextComponent.text));
        }
    }


    private async void OnLevelButtonClicked(string buttonText)
    {
        string sourceFilePath = Application.persistentDataPath + "/LevelFiles/" + buttonText + ".json";
        string destinationFilePath = Path.Combine(Application.persistentDataPath, "LevelFiles", "TestLevel.json");

        if (!File.Exists(destinationFilePath))
        {
            // Create the file
            File.Create(destinationFilePath).Dispose();

        }

        try
        {
            // If the file is not in use, proceed with copying
            Debug.Log("Attempting to copy file, OraOra");
            File.Copy(sourceFilePath, destinationFilePath, true);
            Debug.Log("Selected level copied to: " + destinationFilePath);
        }
        catch (IOException ex)
        {
            // Handle the exception (e.g., log an error or notify the user)
            Debug.LogError("Error copying the file: " + ex.Message);
        }

        // go back to the main menu
        SceneManager.LoadScene("SampleScene");
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
