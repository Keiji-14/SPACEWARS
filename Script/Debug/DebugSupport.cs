using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �f�o�b�O�p��TimeScale�̉����E�ᑬ�̐ݒ�
/// </summary>
public class DebugSupport : MonoBehaviour
{
	private float timeSpeed = 1.0f;

	// TimeScale�̐ݒ�i����̓���܂ő�����Ȃǁj
	void Update()
    {
		if (Input.GetKeyDown("h"))
		{
			timeSpeed += 0.2f;
			Debug.Log(timeSpeed);
		}
		if (Input.GetKeyDown("l"))
		{
			timeSpeed -= 0.2f;
			Debug.Log(timeSpeed);
		}

		Time.timeScale = timeSpeed;
	}
}
