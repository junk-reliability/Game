using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour {

    private Light Pulse;
    public float pulse = 0.001f;
    private float min = 0.7f;
    private float max = 1.8f;
    public float difrange= 0.0001f;
    private bool turn = false;
    // Use this for initialization
    void Start ()
    {
        Pulse = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Intens();
       
        
	}
    void Intens()
    {
        if (Pulse.intensity <= max && turn == false)
        {
            Pulse.intensity += pulse;
            Pulse.range += difrange;
        }
        else
        {
            turn = true;
        }
        if (Pulse.intensity > min && turn == true)
        {
            Pulse.intensity -= pulse;
            Pulse.range -= difrange;
        } 
        else
        {
            turn = false;
                
        }
    }
}
