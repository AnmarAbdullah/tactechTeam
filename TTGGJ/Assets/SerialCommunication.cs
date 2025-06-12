using System;
using System.IO.Ports;
using UnityEngine;

public class SerialCommunication : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM6"; // Replace with your port name
    public int baudRate = 9600;

    // Actions to send accelerometer, gyroscope, and flex sensor data
    public static Action<Vector3, Vector3> OnSensorDataReceived;
    public static Action<int> OnFlexSensorDataReceived;

    private void Start()
    {
        try
        {
            // Initialize SerialPort
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            serialPort.ReadTimeout = 100; // Set timeout for reading
            Debug.Log("Serial port opened successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to open serial port: {e.Message}");
        }
    }

    private void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine(); // Read data from the serial port
                ParseAndInvokeAction(data); // Parse and invoke action
            }
            catch (TimeoutException)
            {
                // Ignore timeout exceptions (normal if no data available)
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading from serial port: {e.Message}");
            }
        }
    }

    private void ParseAndInvokeAction(string data)
    {
        // Example input: "A:100,-200,300;G:-50,20,90;B:180;"
        try
        {
            Vector3 accelerometer = Vector3.zero;
            Vector3 gyroscope = Vector3.zero;
            int flexSensorValue = 0;

            string[] parts = data.Split(';');
            foreach (string part in parts)
            {
                if (part.StartsWith("A:"))
                {
                    string[] accel = part.Substring(2).Split(',');
                    accelerometer = new Vector3(
                        float.Parse(accel[0]),
                        float.Parse(accel[1]),
                        float.Parse(accel[2])
                    );
                }
                else if (part.StartsWith("G:"))
                {
                    string[] gyro = part.Substring(2).Split(',');
                    gyroscope = new Vector3(
                        float.Parse(gyro[0]),
                        float.Parse(gyro[1]),
                        float.Parse(gyro[2])
                    );
                }
                else if (part.StartsWith("B:"))
                {
                    Debug.Log("fuck");
                    flexSensorValue = int.Parse(part.Substring(2));
                }
            }

            // Invoke the actions with the parsed data
            if (accelerometer != Vector3.zero || gyroscope != Vector3.zero)
            {
                OnSensorDataReceived?.Invoke(accelerometer, gyroscope);
                Debug.Log($"Accelerometer: {accelerometer}, Gyroscope: {gyroscope}");
            }

            if (flexSensorValue != 0)
            {
                OnFlexSensorDataReceived?.Invoke(flexSensorValue-180);
                Debug.Log($"Flex Sensor: {flexSensorValue-180}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing data: {e.Message}. Raw data: {data}");
        }
    }

    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close(); // Close the serial port when the application quits
            Debug.Log("Serial port closed.");
        }
    }
}
