using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    public float scrollSpeed;
    public float borderGroundZ;
    public float setGroundX;
    public float setGroundY;
    public float setGroundZ;

    // �n�ʂ̃��[�v����
    void Update()
    {
        transform.position -= new Vector3(0.0f, 0.0f, scrollSpeed) * Time.deltaTime;

        if (Mathf.Floor(this.transform.position.z) <= borderGroundZ)
        {
            transform.position = new Vector3(setGroundX, setGroundY, setGroundZ);
        }
    }
}
