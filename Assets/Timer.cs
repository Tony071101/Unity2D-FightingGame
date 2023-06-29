using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public float WaitSec;
    private int WaitSecInt;
    public Text text;
    public GameObject retryUI;
    public Text text2;
    [SerializeField] Player player;
    [SerializeField] Player2 player2;
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] Player2Combat player2Combat;
    [SerializeField] MoveASWD movePlayer;
    [SerializeField] MoveArrows movePlayer2;
    [SerializeField] Enemy enemy;
    public bool notdie = true;
    private void FixedUpdate()
    {
        if(notdie == true)
        {
            if (WaitSec > 0)
            {
                WaitSec -= Time.fixedDeltaTime;
                WaitSecInt = (int)WaitSec;
                text.text = WaitSecInt.ToString();
            }
            else
            {
                retryUI.SetActive(true);
                playerCombat.cantAttack = false;
                player2Combat.cantAttack = false;
                movePlayer.cantMove = false;
                movePlayer2.cantMove2 = false;
                enemy.cantMove = false;
                enemy.cantAtttack = false;
                Score();
            }
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Score()
    {
        if (player.currentHealth > player2.currentHealth)
        {
            text2.text = "Player " + Player2.playernamestr +" win!";
        }
        if (player.currentHealth < player2.currentHealth)
        {
            text2.text = "Player " + Player.player2namestr + " win!";
        }
        if (player.currentHealth == player2.currentHealth)
        {
            text2.text = "Draw!";
        }
    }
}
