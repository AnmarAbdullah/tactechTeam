using UnityEngine;

public class RotationCursorController : MonoBehaviour
{
    public GameObject cursor;
    public Camera mainCamera;

    public float rotationSensitivity = 2f;
    public float gyroscopeThreshold = 0.1f;

    private Vector2 screenBounds;
    private Vector3 previousGyroscope = Vector3.zero;

    private void Start()
    {
        screenBounds = new Vector2(Screen.width, Screen.height);
        SerialCommunication.OnSensorDataReceived += HandleSensorData;
        cursor.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
    }

    private void HandleSensorData(Vector3 accelerometer, Vector3 gyroscope)
    {
        Vector3 gyroscopeDelta = gyroscope - previousGyroscope;

        if (Mathf.Abs(gyroscopeDelta.x) < gyroscopeThreshold) gyroscopeDelta.x = 0;
        if (Mathf.Abs(gyroscopeDelta.y) < gyroscopeThreshold) gyroscopeDelta.y = 0;

        previousGyroscope = gyroscope;

        if (gyroscopeDelta == Vector3.zero) return;

        Vector3 cursorDelta = new Vector3(
            gyroscopeDelta.y,
            -gyroscopeDelta.x,
            0
        ) * rotationSensitivity;

        Vector3 newCursorPosition = cursor.transform.position + cursorDelta;
        newCursorPosition = ClampToScreenBounds(newCursorPosition);
        cursor.transform.position = newCursorPosition;
    }

    private Vector3 ClampToScreenBounds(Vector3 position)
    {
        Vector3 screenMin = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 10));
        Vector3 screenMax = mainCamera.ScreenToWorldPoint(new Vector3(screenBounds.x, screenBounds.y, 10));

        position.x = Mathf.Clamp(position.x, screenMin.x, screenMax.x);
        position.y = Mathf.Clamp(position.y, screenMin.y, screenMax.y);

        return position;
    }

    private void OnDestroy()
    {
        SerialCommunication.OnSensorDataReceived -= HandleSensorData;
    }
}
