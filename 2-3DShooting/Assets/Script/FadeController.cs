using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	float red, green, blue, alfa;

	public float fadeSpeed;
	public bool isFadeIn = false;
	public bool isFadeOut = false;

	Image fadeImage;

	void Start()
	{
		fadeImage = GetComponent<Image>();
		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;
	}

	void Update()
	{
		if (isFadeIn)
		{
			StartFadeIn();
		}

		if (isFadeOut)
		{
			StartFadeOut();
		}
	}

	// フェードインを行う処理
	public void StartFadeIn()
	{
		alfa -= fadeSpeed;
		SetAlpha();
		if (alfa <= 0)
		{
			isFadeIn = false;
			fadeImage.enabled = false;
		}
	}

	// フェードアウトを行う処理
	public void StartFadeOut()
	{
		fadeImage.enabled = true;
		alfa += fadeSpeed;
		SetAlpha();
		if (alfa >= 1)
		{
			isFadeOut = false;
		}
	}

	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}
}
