using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersoneMove : MonoBehaviour
{
    CharacterController control;
    Animator animator;
    public float Speed = 15f;
    float tmp;
    float Slow;
    bool fl;
    public Vector3 VCam;// вектор движения в сторону камеры
    public Vector3 Vturn;// вектор поворота
    public GameObject Camera;
    public GameObject FlashLight;
    public int jumpforce;
    public bool IsGround, IsShift, IsCtrl;
    // Use this for initialization
    void Start ()
    {
        Slow = Speed / 2;//хранение значения скорости crawl
        tmp = Speed; // хранение значения скорости
        control = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Chek();
        Shift();
        L_Ctrl();
        Movement();
        Jump();
        FL();
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
    public void L_Ctrl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))// Красться
        {
            if (IsCtrl == true)
            {
                IsCtrl = false;
            }
            else
            { 
                IsCtrl = true;
            }

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
    public void FL ()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (fl)
            {
                FlashLight.SetActive(false);
                fl = false;
            }
            else
            {
                FlashLight.SetActive(true);
                fl = true;
            }
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            float jm = Input.GetAxis("Jump");
            Vector3 dir = transform.TransformDirection(new Vector3(0f, jm * jumpforce, 0f));
            control.Move(dir * Time.smoothDeltaTime);
        }
       
    }
    public void Movement()
    {
        VCam = Camera.transform.forward;// вектор движения в сторону камеры
        Vturn = Camera.transform.right;// вектор поворота
        transform.LookAt(new Vector3(VCam.x + transform.position.x , transform.position.y, VCam.z + transform.position.z ));//поворот персонажа в сторону камеры
            if (Input.GetKey(KeyCode.W) && IsShift)
        {
            animator.SetBool("Run", true);
            control.Move(VCam * Speed * 2 * Time.deltaTime);//ускорение
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("walk", true);// воспроизводим анимацию через аниматор
            control.Move(VCam * Speed * Time.deltaTime);// Задаем движение по вектору
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            control.Move(Vturn * -Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            control.Move(Vturn * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            VCam = new Vector3(Camera.transform.forward.x, 0f, Camera.transform.forward.z);
            control.Move(VCam * -Speed * Time.deltaTime);
        }

        if (IsCtrl)
        {
            Speed = Slow;
        }
        else
        {
            Speed = tmp;
        }

    }
}
