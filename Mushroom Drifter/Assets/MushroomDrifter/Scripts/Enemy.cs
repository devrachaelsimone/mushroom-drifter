using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX; 
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int enemyHitPoints = 2;

    GameObject parentGameObject;//empty gameobject holding particles
    ScoreBoard scoreBoard;
/* ===============================ON START============================================ */
    void Start(){
      scoreBoard = FindObjectOfType<ScoreBoard>();
      parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
      AddRigidBody();
    }
    void AddRigidBody(){
      Rigidbody rb = gameObject.AddComponent<Rigidbody>();
      rb.isKinematic = true;
      rb.useGravity = false;
    }
/* ===============================ON COLLISION============================================ */
    void OnParticleCollision(GameObject other){
    ProcessHit();
    if (enemyHitPoints < 1)
    {
    EnemyDeathExplosion();
    Destroy(gameObject);
    
    }
  }

  private void ProcessHit()
  {
    scoreBoard.IncreaseScore(scorePerHit);
    enemyHitPoints --;
  }

  void EnemyDeathExplosion(){
      GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);//explosion particle
      vfx.transform.parent = parentGameObject.transform;//empty gameobject that holds particles so they do not clog hierarchy
    }

  void DestroyEnemy(){
        Destroy(gameObject);

    }

  void StopEnemyAnimation() {
      GetComponent<Animator>().enabled = false;

    }
}
