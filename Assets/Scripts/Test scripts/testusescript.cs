using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testusescript : MonoBehaviour {

    // Use this for initialization
    public int score = 0;
    public Text Score;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Score.text = "Score " + score;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Use"))
        {
            Destroy(other.gameObject);
            score++;
        }
    }
}
