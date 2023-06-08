using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleCollision : MonoBehaviour{
  [SerializeField] float loadDelay = 1.2f;
  [SerializeField] ParticleSystem crashVFX;

  //trigger
  void OnTriggerEnter(Collider other) {  
    StartCrashSequence();
  } 

  //collision
  void OnCollisionEnter(Collision other) {  
    StartCrashSequence();
  } 

  void StartCrashSequence(){
    GetComponent<PlayerControls>().enabled = false; 
    Invoke("ReloadLevel", loadDelay);
    EnableGravity();
    StopAnimation();
    crashVFX.Play();
    StopAnimation();
  }

  void StopAnimation() {
        GetComponentInParent<Animator>().enabled = false;

  }

   void EnableGravity(){
    GetComponent<Rigidbody>().useGravity = true;
  }


  void ReloadLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
}

