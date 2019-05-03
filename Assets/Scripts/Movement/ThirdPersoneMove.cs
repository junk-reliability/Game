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
    private float Slow;
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
        Chek();
        Shift();
        Movement();
        Jump();
    }
    public void Chek()// Проверка касается ли объект поверхности
    {
        RaycastHit rh;
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 0.1f));
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out rh, 0.5f))// проверяем касается ли луч поверхности
        {
            IsGround = true;
            
        }
        else
        {
            IsGround = false;
        }
    }
    public void Shift()// Проверка на нажатие шифта
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsShift = true;
        }
        else
        {
            IsShift = false;
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
        {
           
        }
    }
    public void Movement()
    {
            VCam = Camera.transform.forward;// вектор движения в сторону камеры
            Vturn = Camera.transform.right;// вектор поворота 
        if (IsGround)
        {
            if (Input.GetKey(KeyCode.W) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                control.Move(VCam * Speed);// Задаем движение по вектору
                if (IsShift)
                {
                    control.Move(VCam * Speed*2);//ускорение
                }
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
}
