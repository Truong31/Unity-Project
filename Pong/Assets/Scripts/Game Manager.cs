using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text playerText;
    public Text computerText;
    public Ball ball;
    private int playerScore = 0;
    private int computerScore = 0;

    public void PlayerScore()
    {
        playerScore++;
        playerText.text = playerScore.ToString();
        this.ball.ResetPosition();
    }
    public void ComputerScore()
    {
        computerScore++;
        computerText.text = computerScore.ToString();
        this.ball.ResetPosition();
    }
}
