using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private const float V = 15f;//скорость
    private Rigidbody rb;// получаем тело 
    private SphereCollider sc;
    Vector3 start;// координаты стартовой позиции
    Vector3 VCam;// вектор движения в сторону камеры
    public GameObject Camera;
    public int jumpforce;
    public int shiftforce;
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
            rb.AddForce(VCam* -V);
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
        if (IsGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(VCam * V);// Задаем движение по вектору
                if (IsShift)
                {
                    rb.AddForce(VCam * V * shiftforce);// Задаем движение по вектору
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
              
            }
            if (Input.GetKey(KeyCode.D))
            {

            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(VCam * -V);// Задаем движение по вектору
            }


        }
        
    }

}

