using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public ButtonScale scaleScript;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Starting test procedure");
            scaleScript.ButtonSize = new Vector3(1, 1, 0.007f);
        }
    }
}
