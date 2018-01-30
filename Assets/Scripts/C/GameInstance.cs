using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    Transform canvas;
    Text countdownGO; bool onCountdown = true; float timeLeft = 3.5f;
    Text time;
    float timer = 0f;

    public ButtonsBehaviour bb;

    public bool isInstantiate = false;
    bool onPause = false;

    void Start()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").transform.Find("winInterface").gameObject.SetActive(false);
        

        player1 = GameObject.Find("Game").transform.Find("GameInstance").transform.Find("Player1").gameObject;
        player2 = GameObject.Find("Game").transform.Find("GameInstance").transform.Find("Player2").gameObject;

        player1.GetComponent<Player>().inputPrefix = Manager.prefix1;
        player2.GetComponent<Player>().inputPrefix = Manager.prefix2;

        canvas = GameObject.Find("Canvas").transform;
        countdownGO = canvas.Find("countdown").transform.Find("Title").transform.Find("Text").gameObject.GetComponent<Text>();
        time = canvas.Find("header").transform.Find("Time").transform.Find("Text").gameObject.GetComponent<Text>();

        player1.GetComponent<Player>().setSpells(Manager.spellsPlayer1[0], Manager.spellsPlayer1[1], Manager.spellsPlayer1[2]);

        player2.GetComponent<Player>().setSpells(Manager.spellsPlayer2[0], Manager.spellsPlayer2[1], Manager.spellsPlayer2[2]);

    }
    public GameObject[] getPlayers()
    { return new GameObject[2] { player1, player2 }; }

    public GameObject getPlayer1()
    { return player1; }
    
    public GameObject getPlayer2()
    { return player2; }

    void Update()
    {
        Time.timeScale = 1;
        
        if (onCountdown)
            countdown();
        

        if (player1.GetComponent<Player>().getHealth() == 0 && player2.GetComponent<Player>().getHealth() == 0)
            setWinScreen(0);
        if (player1.GetComponent<Player>().getHealth() == 0)
            setWinScreen(2);
        if (player2.GetComponent<Player>().getHealth() == 0)
            setWinScreen(1);

        if(Input.GetButtonDown("Cancel") && onPause)
        {
            backToGame();
        } else if (Input.GetButtonDown("Cancel") && !onPause)
        {
            Time.timeScale = 0;
            player1.GetComponent<Player>().activate = false;
            player2.GetComponent<Player>().activate = false;
            canvas.Find("Menu").gameObject.SetActive(true);
            onPause = true;
        }

        if (Input.GetButtonDown("Submit") && onPause)
        {
            bb.onClick_MainMenu();
        }

        //Timer
        if(!onPause)
        {
            timer += Time.deltaTime;
            time.text = timer.ToString("000");
        }
            
    }

    void countdown()
    {
        //countdown
        Time.timeScale = 1;
        timeLeft -= Time.deltaTime;

        if (Mathf.Round(timeLeft) == 3)
        {
            player1.GetComponent<Player>().activate = false;
            player2.GetComponent<Player>().activate = false;
            canvas.Find("countdown").transform.gameObject.SetActive(true);
            countdownGO.text = "THREE...";
        }

        if (Mathf.Round(timeLeft) == 2)
            countdownGO.text = "TWO...";

        if (Mathf.Round(timeLeft) == 1)
            countdownGO.text = "ONE...";

        if (Mathf.Round(timeLeft) == 0)
        {
            countdownGO.text = "GO!";
            player1.GetComponent<Player>().activate = true;
            player2.GetComponent<Player>().activate = true;
        }

        if (Mathf.Round(timeLeft) == -1f)
        {
            canvas.Find("countdown").transform.gameObject.SetActive(false);
            timeLeft = 3.5f;
            timer -= timeLeft;
            onCountdown = false;
        }

    }

    void setWinScreen(int playerId)
    {
        onPause = true;
        string res = "";
        if (playerId == 1)
            res = "One";
        if (playerId == 2)
            res = "Two";

        player1.GetComponent<Player>().activate = false;
        player2.GetComponent<Player>().activate = false;

        Transform winInterface = canvas.Find("winInterface");
        Transform title = winInterface.Find("Title");
        title.Find("Text").gameObject.GetComponent<Text>().text = "Player " + res + " WINS";

        if(playerId == 0)
            title.Find("Text").gameObject.GetComponent<Text>().text = "DRAW";

        winInterface.gameObject.SetActive(true);

        if (Input.GetButtonDown("Submit"))
        {
            bb.onClick_MainMenu();
        }
    }

    public void backToGame()
    {
        player1.GetComponent<Player>().activate = true;
        player2.GetComponent<Player>().activate = true;
        canvas.Find("Menu").gameObject.SetActive(false);
        onPause = false;
        Time.timeScale = 1;
        //onCountdown = true;
    }
}
