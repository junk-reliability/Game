using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testusescript : MonoBehaviour {

    // Use this for initialization
    public int score = 0;
    public Text Score;
    public Text Interactives;
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        Score.text = "Score " + score;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.gameObject.GetComponent<Rigidbody>();
        if(body != null)
        body.AddForce(hit.moveDirection * 10f);
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Door"))
        {
            Interactives.text = "Press E";
        }
        if(Input.GetKey(KeyCode.E))
         {
            collider.transform.rotation *= Quaternion.Euler(0f, 1f, 0f);
        }
 
    }
    private void OnTriggerExit(Collider other)
    {
        Interactives.text = null;
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
