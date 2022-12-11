using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class GameScript : MonoBehaviour
{
    public GameObject Textblock;

    // Start is called before the first frame update
    void Start()
    {
        FileInfo theSourceFile = new FileInfo("./Assets/ExampleAssets/Texts/test.txt");
        StreamReader reader = theSourceFile.OpenText();
        string text;
        do
        {
            text = reader.ReadLine();
            words.Add(text);
        } while (text != null);

        InvokeRepeating("createBlock", 2.0f, 4.0f);
    }

    public int noBlocks = 0;
    public List<string> words = new List<string>();

    private void createBlock()
    {
        if (noBlocks > words.Count)
        {
            Application.Quit();
        }
        Instantiate(Textblock, new Vector3(0.6f, 3, 15), Quaternion.identity);
        GameObject block = GameObject.Find("Textblock(Clone)");
        block.name = "Block" + noBlocks.ToString();
        noBlocks++;
    }
}
