using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRenderre : MonoBehaviour
{
    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //カメラに表示されているか
    private bool isRendered = false;

    private void Update()
    {

        if (isRendered)
        {
            Debug.Log("カメラに映ってる");
        }
        else
        {
            Debug.Log("カメラに映っていない");
        }

        //カメラに写っていれば当り判定有効
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = isRendered;
        }

        isRendered = false;
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            isRendered = true;
        }
    }
}
