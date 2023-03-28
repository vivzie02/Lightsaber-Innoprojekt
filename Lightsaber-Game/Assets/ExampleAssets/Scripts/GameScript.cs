using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Xml;

public class GameScript : MonoBehaviour
{
    public GameObject Textblock;
    public int correctBlock;

    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        List<string> blockText = new List<string>();
        DataSet currentLevel = new DataSet("level");

        /*string filePath = Path.Combine(Application.streamingAssetsPath, "Testtext.xml");

        
        currentLevel.ReadXml(filePath);
        

        sentence = currentLevel.Tables[0].Rows[0][0].ToString();

        int i = 0;
        
        foreach (DataRow row in currentLevel.Tables[0].Rows)
        {
            foreach (DataColumn column in currentLevel.Tables[0].Columns)
            {
                if(i != 0)
                {
                    blockText.Add(row[column].ToString());
                }
                i++;
            }
        }*/

        string filePath = Path.Combine(Application.streamingAssetsPath, "Testtext.xml");
        string content;

        
        WWW reader = new WWW(filePath);
        while (!reader.isDone) { }

        content = reader.text;
        // Do something with the content

        XmlReader xmlReader = XmlReader.Create(new StringReader(content));
        currentLevel.ReadXml(xmlReader);    

        sentence = currentLevel.Tables[0].Rows[0][0].ToString();

        int i = 0;

        foreach (DataRow row in currentLevel.Tables[0].Rows)
        {
            foreach (DataColumn column in currentLevel.Tables[0].Columns)
            {
                if (i != 0)
                {
                    blockText.Add(row[column].ToString());
                }
                i++;
            }
        }


        words = blockText;

        this.correctBlock = Int32.Parse(currentLevel.Tables[0].Rows[0][5].ToString());

        InvokeRepeating("createBlock", 0.5f, 4.0f);
    }

    public int noBlocks = 1;
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
