using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private const float V = 15f;//скорость
    private Rigidbody rb;// получаем тело 
    private SphereCollider sc;
    Vector3 start;// координаты стартовой позиции
    Vector3 V3Move;// движение
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
            rb.AddForce(V3Move * -V);
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
        if (IsGround)
        {
            float V_Move = Input.GetAxis("Vertical");//вертикальное движение
            float H_Move = Input.GetAxis("Horizontal");// горизонтальное движение
            V3Move = new Vector3(H_Move, 0, V_Move);// создаем вектор 
            if (IsShift)
            {
                rb.AddForce(V3Move * V*shiftforce);// Задаем движение по вектору
            }
            else
            {
                rb.AddForce(V3Move * V);// Задаем движение по вектору
            }
       }
    }

}

