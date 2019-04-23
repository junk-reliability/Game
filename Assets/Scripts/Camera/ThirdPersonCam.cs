using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {

    public Camera cam;// камера
    public Transform target;// цель 
    public float speedX = 360f;
    public float speedY = 240f;
    public float limY = 40f;// лимит по y
    public float MinDist = 1.5f;
    public float hidePlayer = 2f;// дистанция скрытия персонажа 
    public LayerMask obstacles;// маска с припятствиями 
    public LayerMask noPlayer;// маска без персонажа
    private float MaxDist;
    private Vector3 LocalPosition;// позиция камеры в кокальных координатах цели
    private float CurrentYRotation;
    private LayerMask _camOrig;// оригинальная маска слоев камеры
    private Vector3 _position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    // Use this for initialization
    void Start()
    {
        LocalPosition = target.InverseTransformPoint(_position);
        MaxDist = Vector3.Distance(_position, target.position);
        _camOrig = cam.cullingMask;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _position = target.TransformPoint(LocalPosition);
        CameraRotation();
        ObstaclesReact();
        PlayerReact();
        LocalPosition = target.InverseTransformPoint(_position);
    }
    void CameraRotation()// поворот вокруг точки
    {
        var mx = Input.GetAxis("Mouse X");
        var my = Input.GetAxis("Mouse Y");

        if (my != 0)// поворот по У с лимитом 
        {
            var tmp = Mathf.Clamp(CurrentYRotation + my * speedY * Time.deltaTime, -limY, limY);
            if (tmp != CurrentYRotation)
            {
                var rot = tmp - CurrentYRotation;
                transform.RotateAround(target.position, transform.right, rot);
                CurrentYRotation = tmp;
            }
        }
        if (mx != 0)// поворот по Х
        {
            transform.RotateAround(target.position, Vector3.up, mx * speedX * Time.deltaTime);
        }

        transform.LookAt(target);// 
    }
    void ObstaclesReact()
    {
        var distance = Vector3.Distance(_position, target.position);
        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, MaxDist, obstacles))// проверка дистанции до стены - если лучь с чем то столкнулся то камера остается в точке где луч столкнулся
        {
            _position = hit.point;
        }
        else if (distance < MaxDist && !Physics.Raycast(_position, -transform.forward, .1f, obstacles))// если нет - пускаем луч от себя и камера отодвигается от себя
        {
            _position -= transform.forward * .05f;
        }
    }
    void PlayerReact()
    {
        var distance = Vector3.Distance(_position, target.position);
        if(distance < hidePlayer)// скрываем персонажа используя маску - cullingMask 
        {
            cam.cullingMask = noPlayer;
        }
        else
        {
            cam.cullingMask = _camOrig;// возвращаем персонажа на экран
        }
    }
}
