using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] PlayerCombat player;
    [SerializeField] Player2Combat player2;
    [SerializeField] Timer time;
    [SerializeField] Enemy enemy;
    //[SerializeField] EnemyMove enemyMove;
    public GameObject cdUI;
    public float WaitSec;
    private int WaitSecInt;
    public Text text;
    private void FixedUpdate()
    {
        if (WaitSec > 0)
        {
            time.enabled = false;
            player.enabled = false;
            player2.enabled = false;
            enemy.enabled = false;
            //enemyMove.enabled = false;
            WaitSec -= Time.fixedDeltaTime;
            WaitSecInt = (int)WaitSec;
            text.text = WaitSecInt.ToString();
            if (text.text == "1")
            {
                text.text = "Ready?";
            }
            if (text.text == "0")
            {
                text.text = "Fight!";
            }
        }
        else
        {
            time.enabled = true;
            player.enabled = true;
            player2.enabled = true;
            enemy.enabled = true;
            //enemyMove.enabled = true;
            cdUI.SetActive(false);
        }
    }
}
