using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SunRotate : MonoBehaviour
{
    public float rotate = 0.5f;
    private Transform trans;
    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Time.timeScale = 1.0f;
        trans.transform.Rotate(rotate * Time.fixedDeltaTime , 0, 0,Space.Self);
    }
}
