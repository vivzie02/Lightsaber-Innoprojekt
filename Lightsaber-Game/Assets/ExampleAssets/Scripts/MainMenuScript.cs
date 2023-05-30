using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    private Button startGameButton;
    private Button levelSelectorButton;

    // Start is called before the first frame update
    void Start()
    {

        FindChildObjects();


        startGameButton.onClick.AddListener(StartGame);
        levelSelectorButton.onClick.AddListener(LevelSelector);

        levelSelectorButton.onClick.Invoke();
    }

    private void FindChildObjects()
    {
        //sucht StartGameBtn
        startGameButton = GameObject.Find("StartGameBtn").GetComponent<Button>();
        if (startGameButton != null)
        {
            startGameButton.name = "StartGameBtn";
        }
        else
        {
            Debug.LogError("Child object 'StartGameBtn' not found.");
        }

        //sucht LevelSelectorBtn 
        levelSelectorButton = GameObject.Find("LevelSelectorBtn").GetComponent<Button>();
        if (levelSelectorButton != null)
        {
            levelSelectorButton.name = "LevelSelectorBtn";
        }
        else
        {
            Debug.LogError("Child object 'LevelSelectorBtn' not found.");
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("GameScene");

    }


    private void LevelSelector()
    {
        SceneManager.LoadScene("LevelSelectorScene");

    }

}
