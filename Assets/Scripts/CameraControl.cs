using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject Player;
    private Vector3 Offset;

	// Use this for initialization
	void Start ()
    {
        Offset = transform.position - Player.transform.position;// transform.position - позиция объекта на котором скрипт; Player.transform.position - позиция игрока
    }
	
	// Update is called once per frame
	void LateUpdate ()// LateUpdate - камера будет двигаться после движения персонажа, обновляется после Update
    {
        transform.position = Player.transform.position + Offset;

	}
}
