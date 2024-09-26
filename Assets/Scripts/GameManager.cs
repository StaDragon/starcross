using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float endTime = 4f;

    public GameObject endScreen;
    public GameObject pauseScreen;
    public Text scoreIndicator;

    private int Score = 0;

    private PlayerManager pManager;
    private Player[] players;

    private bool gameEndTime;
    private float endTimer;
    private float scoreTimer;

    private bool paused;


    // Start is called before the first frame update
    void Start()
    {
        pManager = FindObjectOfType<PlayerManager>();
        players = FindObjectsOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pManager.playersCombined)
        {
            scoreTimer += Time.deltaTime;

            Score = (int)scoreTimer;
            scoreIndicator.text = Score.ToString();
        }

        if (gameEndTime)
        {
            endTimer += Time.deltaTime;

            if(endTimer > endTime)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!paused)
            {
                Pause();
                paused = true;
            }
            else
            {
                UnPause();
                paused = false;
            }
        }

    }

    public void StartGame()
    {
        
    }

    public void EndGame()
    {
        Debug.Log("End Game");

        foreach(Player player in players)
        {
            Destroy(pManager.gameObject);
            Destroy(player.gameObject);
        }

        gameEndTime = true;
        endScreen.active = true;

    }

    public void Pause()
    {
        Time.timeScale = 0.0f;

        pauseScreen.active = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;

        pauseScreen.active = false;
    }

    
}
