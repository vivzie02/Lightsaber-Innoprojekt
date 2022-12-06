using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameScript : MonoBehaviour
{
    public GameObject Textblock;

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        InvokeRepeating("createBlock", 2.0f, 4.0f);
    }

    private System.Random rnd;
    private string word;

    private void createBlock()
    {
        int blocks = rnd.Next(0, 2);
        if (blocks == 0)
        {
            Instantiate(Textblock, new Vector3(-1, 3, 15), Quaternion.identity);
        }
        else if (blocks == 1)
        {
            Instantiate(Textblock, new Vector3(3, 3, 15), Quaternion.identity);
        }
        else if (blocks == 2)
        {
            Instantiate(Textblock, new Vector3(-1, 3, 15), Quaternion.identity);
            Instantiate(Textblock, new Vector3(3, 3, 15), Quaternion.identity);
        }
    }
}
