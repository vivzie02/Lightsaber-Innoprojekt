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
        var _path = Application.streamingAssetsPath + "/Texts/test.txt";
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(_path);
        www.SendWebRequest();
        while (!www.isDone)
        {
        }
        String allWordsString = www.downloadHandler.text;

        words = allWordsString.Split('\n').ToList();

        InvokeRepeating("createBlock", 10.0f, 4.0f);
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
