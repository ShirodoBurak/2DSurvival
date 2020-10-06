using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeWorldGrow : MonoBehaviour
{
    GenerateChunks cnkContinue = new GenerateChunks();
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        cnkContinue.Start();
    }
}
