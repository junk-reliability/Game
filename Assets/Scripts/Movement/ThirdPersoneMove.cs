using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersoneMove : MonoBehaviour
{
    CharacterController control;
    Animation animator;
    public float Speed = 15f;
    public Vector3 VCam;// вектор движения в сторону камеры
    public Vector3 Vturn;// вектор поворота
    public GameObject Camera;
    public float Turn = 1f;// скорость поворота
    public float Slow = 0.2f;
    public int jumpforce;
    public bool IsGround, IsShift;
    // Use this for initialization
    void Start ()
    {
        control = GetComponent<CharacterController>();
        animator = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        Jump();
	}
    public void Jump()
    {
        float jm = Input.GetAxis("Jump");
        VCam = transform.up * jm;
        control.Move(VCam);
    }
    public void Movement()
    {
            VCam = Camera.transform.forward;// вектор движения в сторону камеры
            Vturn = Camera.transform.right;// вектор поворота 

            if (Input.GetKey(KeyCode.W) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                control.Move(VCam * Speed);// Задаем движение по вектору
            }
            if (Input.GetKey(KeyCode.A) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                control.Move(Vturn * -Speed * Turn);
            }
            if (Input.GetKey(KeyCode.D) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                control.Move(Vturn * Speed * Turn);
            }
            if (Input.GetKey(KeyCode.S) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                control.Move(VCam * -Speed);
            }
            if (Input.GetKey(KeyCode.LeftControl))// торможение
            {
                control.Move(VCam * Speed * Turn);
                control.Move(VCam * -Speed * Turn);
            }



       
    }
}
