using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceScript : MonoBehaviour
{
    public Text text;

    GameScript TheScript;
    private GameObject TheGameController;

    private string word;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoSomething", 0.2f);//this will happen after 2 seconds
    }

    private void DoSomething()
    {
        TheGameController = GameObject.Find("Hallway_new");
        TheScript = TheGameController.GetComponent<GameScript>();

  

        // read word from some file
        // save it in word string
        word = TheScript.sentence;
        text = GetComponent<Text>();
        text.text = word;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
