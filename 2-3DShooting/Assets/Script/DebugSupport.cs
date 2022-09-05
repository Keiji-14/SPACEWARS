using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSupport : MonoBehaviour
{
	private float timeSpeed = 1.0f;

	// timeScale‚Ìİ’èi“Á’è‚Ì“®ì‚Ü‚Å‘‘—‚è‚È‚Çj
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
