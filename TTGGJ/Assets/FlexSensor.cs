using UnityEngine;
using UnityEngine.UI;
using System;

public class FlexSensor : MonoBehaviour
{
    public Slider flexSlider;
    public int minFlexValue = 0;
    public int maxFlexValue = 1023;
    public float pullThreshold = 0.8f;
    public float releaseThreshold = 0.2f;
    public float pullDuration = 2f;

    private float pullStartTime;
    private bool isBeingPulled = false;
    private bool shotDetected = false;

    public static event Action OnSlingshotShot;
    public static event Action<float> OnSlingshotValue;

    private void OnEnable()
    {
        SerialCommunication.OnFlexSensorDataReceived += UpdateSlider;
    }

    private void OnDisable()
    {
        SerialCommunication.OnFlexSensorDataReceived -= UpdateSlider;
    }

    private void UpdateSlider(int flexValue)
    {
        if (flexSlider == null)
        {
            Debug.LogError("Flex Slider is not assigned!");
            return;
        }

        float normalizedValue = Mathf.InverseLerp(minFlexValue, maxFlexValue, flexValue);
        flexSlider.value = normalizedValue;
        OnSlingshotValue?.Invoke(normalizedValue);
        DetectSlingshotAction(normalizedValue);
    }

    private void DetectSlingshotAction(float normalizedValue)
    {
        if (normalizedValue >= pullThreshold && !isBeingPulled)
        {
            isBeingPulled = true;
            pullStartTime = Time.time;
            shotDetected = false;
            Debug.Log("Slingshot pull started.");
        }
        else if (isBeingPulled && normalizedValue < releaseThreshold)
        {
            if (Time.time - pullStartTime >= pullDuration && !shotDetected)
            {
                shotDetected = true;
                isBeingPulled = false;
                OnSlingshotShot?.Invoke();
                Debug.Log("Slingshot shot detected!");
            }
            else
            {
                isBeingPulled = false;
                Debug.Log("Pull was too short; no shot detected.");
            }
        }
    }
}
