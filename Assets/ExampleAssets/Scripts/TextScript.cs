using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text text;

    private string word;

    // Start is called before the first frame update
    void Start()
    {
        // read word from some file
        // save it in word string
        word = "Hello World";
        text = GetComponent<Text>();
        text.text = word;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
