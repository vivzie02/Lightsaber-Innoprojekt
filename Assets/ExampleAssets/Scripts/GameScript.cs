using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Data;

public class GameScript : MonoBehaviour
{
    public GameObject Textblock;

    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        List<string> blockText = new List<string>();
        DataSet currentLevel = new DataSet("level");
        currentLevel.ReadXml(Application.streamingAssetsPath + "/Texts/Testtext.xml");

        sentence = currentLevel.Tables[0].Rows[0][0].ToString();
        
        foreach (DataRow row in currentLevel.Tables[0].Rows)
        {
            foreach (DataColumn column in currentLevel.Tables[0].Columns)
            {
                blockText.Add(row[column].ToString());
            }
        }

        words = blockText;

        InvokeRepeating("createBlock", 10.0f, 4.0f);
    }

    public int noBlocks = 0;
    public List<string> words = new List<string>();
    public string sentence;

    private void createBlock()
    {
        if (this.noBlocks > words.Count)
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
        Instantiate(Textblock, new Vector3(0.6f, 3, 15), Quaternion.identity);
        GameObject block = GameObject.Find("Textblock(Clone)");
        block.name = "Block" + noBlocks.ToString();
        noBlocks++;
    }
}
