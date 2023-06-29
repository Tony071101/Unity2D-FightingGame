using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameOverScreen : MonoBehaviour
{
    public Text text;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        text.text = score.ToString();
    }
}
