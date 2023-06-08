using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour
{
  int score;
  Text scoreText;

  void Start(){
    scoreText = GetComponent<Text>();
    scoreText.text = "Start";
  }
   public void IncreaseScore(int amountToIncrease){
    score += amountToIncrease;
    scoreText.text = score.ToString();

   }
}
