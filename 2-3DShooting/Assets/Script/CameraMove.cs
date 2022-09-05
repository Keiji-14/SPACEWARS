using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private int phaseCamera;
    private float moveStep;
    private float rotationStep;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    private enum Phase
    {
        Phase1,
        Phase2,
        Phase3,
    }

    void Update()
    {
        phaseCamera = PlayerPrefs.GetInt("PHASE", 0);
        moveStep = moveSpeed * Time.deltaTime;
        rotationStep = rotationSpeed * Time.deltaTime;

        switch (phaseCamera)
        {
            case (int)Phase.Phase1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 10, 5), moveStep);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90.0f, 0.0f, 0), rotationStep);
                break;
            case (int)Phase.Phase2:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(10, 0, 5), moveStep);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), rotationStep);
                break;
            case (int)Phase.Phase3:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -6), moveStep);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), rotationStep);
                break;
        }
    }
}
