using UnityEngine;
using System.Collections;
using System;

public class CameraShake : MonoBehaviour
{
	public static CameraShake current;

	public float decreaseFactor = 1.0f;

	private Transform camTransform;
	private Vector3 originalPos;

	private HitStop hitStop;

	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount;
	// How long the object should shake for.
	private float shakeDuration;
	private bool shaking;

	void Awake()
	{
		camTransform = Camera.main.transform;

		hitStop = gameObject.AddComponent<HitStop>();

		shaking = false;
		shakeDuration = 0f;

		current = this;
		current.onCameraShake += Shake;
	}

    private void OnDestroy()
	{
		current.onCameraShake -= Shake;
	}


    //------------- Events -----------------
    public Action<float, float> onCameraShake;
	public void ShakeCamera(float duration, float amount)
	{
		if (onCameraShake != null)
		{
			onCameraShake(duration, amount);
		}
	}

	private void Shake(float duration, float amount)
	{
		if (shaking)
			return;

		shaking = true;

		originalPos = camTransform.localPosition;
		shakeDuration = duration;
		shakeAmount = amount;

		hitStop.Stop(0.01f);
	}

	void Update()
	{
		if (Time.timeScale != 0)
		{
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				shaking = false;
			}
		}
	}
}