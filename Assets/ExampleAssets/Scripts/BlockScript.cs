using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
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

    void OnCollisionEnter(Collision collision)
    {
        GameObject block;
        for(int i = 0; i < 20; i++)
        {
            block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.position = transform.localPosition;
            if(i % 2 == 0)
            {
                block.transform.Rotate(0, 90, 0);
            }
            block.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
            Rigidbody rb = block.AddComponent<Rigidbody>();
            rb.mass = 5;
        }
        Destroy(this.gameObject);
    }
}