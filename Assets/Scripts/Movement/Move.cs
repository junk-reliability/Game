using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static float Speed = 15f;//скорость
    public float Turn = 1f;// скорость поворота
    public float Slow = 1.0f;
    public float Accelerate = 2.5f;
    public float Accelerate_lim = Speed *2;// предел ускорения
    private Rigidbody rb;// получаем тело 
    private SphereCollider sc;
    Vector3 start;// координаты стартовой позиции
    Vector3 VCam;// вектор движения в сторону камеры
    Vector3 Vturn;// вектор поворота
    public GameObject Camera;
    public int jumpforce;
    public bool IsGround,IsShift;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();// инициализируем тело
        sc = GetComponent<SphereCollider>();
        GameObject go = GameObject.Find("Sphere");// ищем позицию сферы
        start = go.transform.position;// записываем координаты
    }
    // Update is called once per frame
    void Update()
    {
        Shift();
        Chek();
        Movement();
        Jump();
        Stop();
    }
    public void Stop()
    {
        if (Input.GetKey(KeyCode.LeftControl) && IsGround)// замедление движения
        {
            rb.AddForce(VCam* -Speed);
        }
        if (Input.GetKeyDown(KeyCode.P))// возвращает в изначальную позицию
        {
            rb.transform.position = start;
            Vector3 V3Stop = new Vector3(0, 0, 0);
            rb.velocity = V3Stop;// останавливаем движение
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
    public void Chek()// Проверка касается ли объект поверхности
    {
        Ray ray = new Ray(gameObject.transform.position, Vector3.down);// луч от персонажа вниз
        RaycastHit rh;
        if (Physics.Raycast(ray, out rh, sc.radius + 0.5f))// проверяем касается ли луч поверхности
        {
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            rb.AddForce(Vector3.up * jumpforce);
        }
    }
    public void Movement()
    {
        VCam = Camera.transform.forward;// вектор движения в сторону камеры
        Vturn = Camera.transform.right;// вектор поворота 
        if (IsGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(VCam * Speed);// Задаем движение по вектору
                if (IsShift && (rb.velocity.magnitude < Accelerate_lim))
                {
                    rb.AddForce(rb.velocity * Accelerate);//ускорение
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vturn * -Speed * Turn);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vturn * Speed * Turn);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(VCam * -Speed);
            }
            if (Input.GetKey(KeyCode.LeftControl))// торможение
            {
                if (rb.velocity.magnitude < Speed)
                {
                    rb.AddForce(-rb.velocity * Slow);
                }
            }

        }
        
    }

}

