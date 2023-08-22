using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRenderre : MonoBehaviour
{
    //���C���J�����ɕt���Ă���^�O��
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //�J�����ɕ\������Ă��邩
    private bool isRendered = false;

    private void Update()
    {

        if (isRendered)
        {
            Debug.Log("�J�����ɉf���Ă�");
        }
        else
        {
            Debug.Log("�J�����ɉf���Ă��Ȃ�");
        }

        //�J�����Ɏʂ��Ă���Γ��蔻��L��
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = isRendered;
        }

        isRendered = false;
    }

    //�J�����ɉf���Ă�ԂɌĂ΂��
    private void OnWillRenderObject()
    {
        //���C���J�����ɉf����������_isRendered��L����
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            isRendered = true;
        }
    }
}
