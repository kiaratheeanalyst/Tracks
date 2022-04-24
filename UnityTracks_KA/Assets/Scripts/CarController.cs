using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelFrontRight;
    public WheelCollider wheelBackLeft;
    public WheelCollider wheelBackRight;

    public float steerMax;
    public float motorMax;
    public float brakeMax;

    float steer = 0.0f;
    float motor = 0.0f;
    float brake = 0.0f;
    public float shiftMultiplier = 3f;
    public float controlMultiplier = 0.7f;

    // Use this for initialization


    // Update is called once per frame

    void Update()

    {
        wheelFrontLeft.gameObject.transform.Rotate(0, wheelFrontLeft.rpm / 60 * 360 * Time.deltaTime, 0);
        wheelFrontRight.gameObject.transform.Rotate(0, wheelFrontRight.rpm / 60 * 360 * Time.deltaTime, 0);
        wheelBackLeft.gameObject.transform.Rotate(0, wheelBackLeft.rpm / 60 * 360 * Time.deltaTime, 0);
        wheelBackRight.gameObject.transform.Rotate(0, wheelBackRight.rpm / 60 * 360 * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {

            motor = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        steer = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);

        /*determines whether we should reverse or not!
        If we do click the specific key to reverse, then our car will gradually slow down, i think. */


        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //simply reverse the car.
            Debug.LogWarning("reversing car!");
            //minusing the motor so we get a positive value
            motor = Mathf.Clamp(-Input.GetAxis("Vertical"), 0, 1);
            wheelBackLeft.motorTorque = 1 * motorMax * motor;
            wheelBackRight.motorTorque = 1 * motorMax * motor;

        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //simply normalise the car.
            Debug.LogWarning("Normalising car.");
            wheelBackLeft.motorTorque = -1 * motorMax * motor;
            wheelBackRight.motorTorque = -1 * motorMax * motor;


        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            motor = motor * shiftMultiplier;
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            motor = motor * controlMultiplier;
        }




        if (Input.GetKey(KeyCode.Space))
        {
            //we brake if we press [Space]
            wheelBackLeft.brakeTorque = brakeMax;
            wheelBackRight.brakeTorque = brakeMax;
        } else
        {
            wheelBackLeft.brakeTorque = 0;
            wheelBackRight.brakeTorque = 0;
        }

        wheelFrontLeft.steerAngle = steerMax * steer;
        wheelFrontRight.steerAngle = steerMax * steer;

        

    }
}
