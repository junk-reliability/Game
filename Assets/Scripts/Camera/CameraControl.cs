using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public float limit = 80; // ограничение вращения по Y
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 10; // макс. увеличение
    public float zoomMin = 3; // мин. увеличение
    private float X, Y;
    public float MouseSense = 3; // чувствительность мышки

    // Use this for initialization
    void Start ()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.position = target.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()// LateUpdate - камера будет двигаться после движения персонажа, обновляется после Update
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;// приблизить
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;// отдалить
        offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));// значение между мин и макс
        X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSense;
        Y += Input.GetAxis("Mouse Y") * MouseSense;
        Y = Mathf.Clamp(Y, -limit, limit);
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;

    }
}
