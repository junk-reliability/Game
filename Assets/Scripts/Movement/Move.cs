using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private const float V = 15f;//скорость
    private Rigidbody rb;// получаем тело 
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();// инициализируем тело
	}
	
	// Update is called once per frame
	void Update ()
    {
        float V_Move = Input.GetAxis("Vertical");//вертикальное движение
        float H_Move = Input.GetAxis("Horizontal");// горизонтальное движение
        Vector3 V3Move = new Vector3(H_Move,0,V_Move);// создаем вектор 
        rb.AddForce(V3Move * V);// Задаем движение по вектору
    }
}
