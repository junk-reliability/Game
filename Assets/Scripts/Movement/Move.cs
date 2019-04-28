using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed = 15f;//скорость
    public float Turn = 1f;// скорость поворота
    public float Slow = 0.2f;
    public float Accelerate = 2.5f;
    public float Accelerate_lim;// предел ускорения
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
        Accelerate_lim = Speed * 2;
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGround && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
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
            if (Input.GetKey(KeyCode.W) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(VCam * Speed);// Задаем движение по вектору
                if (IsShift && (rb.velocity.magnitude < Accelerate_lim))
                {
                    rb.AddForce(rb.velocity * Accelerate);//ускорение
                }
            }
            if (Input.GetKey(KeyCode.A) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(Vturn * -Speed * Turn);
            }
            if (Input.GetKey(KeyCode.D) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(Vturn * Speed * Turn);
            }
            if (Input.GetKey(KeyCode.S) && Input.anyKey != Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(VCam * -Speed);
            }
            if (Input.GetKey(KeyCode.LeftControl))// торможение
            {
                rb.AddForce(VCam * Speed);
                rb.AddForce(rb.velocity* -Slow);
            }

        }
        
    }

}

