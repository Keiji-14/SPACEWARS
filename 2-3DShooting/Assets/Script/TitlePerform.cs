using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePerform : MonoBehaviour
{
    [SerializeField] GameObject titleShip;

    private float delta;

    void Update()
    {
        delta += Time.deltaTime;
        titleShip.transform.position = new Vector3(titleShip.transform.position.x, Mathf.Sin(delta) * 0.002f + titleShip.transform.position.y, titleShip.transform.position.z);
    }
}
