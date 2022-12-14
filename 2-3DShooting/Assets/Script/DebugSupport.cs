using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSupport : MonoBehaviour
{
	private float timeSpeed = 1.0f;

	// timeScaleの設定（特定の動作まで早送りなど）
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
