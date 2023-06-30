using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject player2;
    public GameObject hpBar;
    public GameObject hpBar2;
    public GameObject skillbar;
    public GameObject skillbar2;
    public GameObject timer;
    public GameObject counting;
    public InputField playerName;
    public InputField player2Name;
    public GameObject inputNameUI;
    [SerializeField] CountDown cd;
    [SerializeField] Timer time;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] MoveASWD moveASWD;
    [SerializeField] MoveArrows moveArrows;
    void Start()
    {
        inputNameUI.SetActive(true);
        player.SetActive(false);
        player2.SetActive(false);
        hpBar.SetActive(false);
        hpBar2.SetActive(false);
        skillbar.SetActive(false);
        skillbar2.SetActive(false);
        timer.SetActive(false);
        counting.SetActive(false);
        cd.enabled = false;
        time.enabled = false;
        pauseMenu.enabled = false;
        moveArrows.enabled = false;
        moveASWD.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Player.player2namestr = player2Name.text;
        Player2.playernamestr = playerName.text;
    }
    public void Play()
    {
        inputNameUI.SetActive(false);
        player.SetActive(true);
        player2.SetActive(true);
        hpBar.SetActive(true);
        hpBar2.SetActive(true);
        skillbar.SetActive(true);
        skillbar2.SetActive(true);
        timer.SetActive(true);
        counting.SetActive(true);
        cd.enabled = true;
        time.enabled = true;
        pauseMenu.enabled = true;
        moveASWD.enabled = true;
        moveArrows.enabled = true;
    }
}
