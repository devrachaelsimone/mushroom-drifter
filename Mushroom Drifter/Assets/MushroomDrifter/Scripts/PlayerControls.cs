using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Player speed")]
    //==============================VERTICAL AND HORIZONTAL MOVEMENT===============================

    //the speed of vertical and horizontal movement
    [Tooltip("how fast ship moves up and down based upon player input")][SerializeField] float xControlSpeed = 60f;
    [SerializeField] float yControlSpeed = 12f;
    float xThrow;
    float yThrow;
    //the clamped movement
    [Header("range of movement along x and y")]
    [SerializeField] float xRange = 15f;
    float yMin = -1f, yMax = 6.5f;
    //========================================ROTATION=============================================
    [Header("pitch tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [Header("yaw tuning")]
    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float controlRollFactor = -10f;
    //========================================PARTICLES=============================================
    [Header("player laser gun array")]
    [SerializeField] GameObject[] lasers;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
        quitGame();
    }

    //========================================METHODS=============================================

    void ProcessTranslation()
    {
        //horizontal movement (x)
        xThrow = Input.GetAxis("Horizontal");//get the input for x axis
        float xOffset = xThrow * Time.deltaTime * xControlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        //vertical movement
        yThrow = Input.GetAxis("Vertical"); //get the input for y axis
        float yOffset = yThrow * Time.deltaTime * yControlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    //

    void ProcessRotation()
    {
        //pitch is determined by player position and player input (control throw) together
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        //
        //float yaw = transform.localPosition.z * positionYawFactor;
        float yaw = transform.localPosition.x * positionYawFactor;

        //
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //x y and z correspond to pitch, yaw, and roll
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            setLasersActive(true);
        }
        else
        {
            setLasersActive(false);
        }
    }

    void setLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers){
          var emissionModule = laser.GetComponent<ParticleSystem>().emission;
          emissionModule.enabled = isActive;
        }
        
    }

    void quitGame(){
      if(Input.GetKey(KeyCode.Q)){
        Application.Quit();
      }
    }

}
//========================================================