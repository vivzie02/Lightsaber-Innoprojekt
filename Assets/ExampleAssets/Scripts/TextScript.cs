using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text text;

    GameScript TheScript;
    private GameObject TheGameController;

    private string word;

    // Start is called before the first frame update
    void Start()
    {
        TheGameController = GameObject.Find("Hallway_new");
        TheScript = TheGameController.GetComponent<GameScript>();

        // read word from some file
        // save it in word string
        word = TheScript.words[TheScript.noBlocks-1];

        text = GetComponent<Text>();
        text.text = word;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
