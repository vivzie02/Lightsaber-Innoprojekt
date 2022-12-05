using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    Material material;

    private static string displayedText;
    private static bool isCorrect;
    public float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        isCorrect = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * this.speed * Time.deltaTime);
    }
}
