using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    //will be passed to the game
    static public Spell[] spellsPlayer1 = new Spell[3];
    static public Spell[] spellsPlayer2 = new Spell[3];

    static public Dictionary<string, KeyCode> player1Keys = new Dictionary<string, KeyCode>();
    static public Dictionary<string, KeyCode> player2Keys = new Dictionary<string, KeyCode>();

    static public string prefix1 = "P1_K_";
    static public string prefix2 = "P2_K_";
    //

    private static bool isInstantiate = false;

    private Transform canvas;
    private Transform mainMenu;
    private Transform fightMenu;
    private Transform settingsMenu;

    private bool onMainMenu = false;
    private bool onFightMenu = false;
    private bool onSettingsMenu = false;

    private GameObject currentKey; int currentPlayerKey;

    private Spell[,] spellsGrid = new Spell[6, 4];
    private Dictionary<string, int> currentSpell1 = new Dictionary<string, int>();
    private Dictionary<string, int> currentSpell2 = new Dictionary<string, int>();

    private float gcdUi = 0;
    private Transform selector1;
    private Transform selector2;

    private bool ready = false;
    private float delay = 0.5f;
    private Selectable currentButton;

    void Start()
    {
        if (!isInstantiate)
        {
            initKeys();
            isInstantiate = true;
        }

        canvas = GameObject.Find("Canvas").transform;

        mainMenu = canvas.Find("MainMenu");
        fightMenu = canvas.Find("FightMenu");
        settingsMenu = canvas.Find("SettingsMenu");

        initSpellsGrid();

        setSpellsBar();

        mainMenu.gameObject.SetActive(true);
        fightMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        onMainMenu = true;

        currentButton = mainMenu.Find("btnPlay").gameObject.GetComponent<Button>();
    }

    public void onClick_main_Fight()
    {
        fightMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        onMainMenu = false;
        onFightMenu = true;

        canvas.transform.Find("background").gameObject.GetComponent<Animator>().SetTrigger("green-sand");

        currentButton = fightMenu.Find("Fight").gameObject.GetComponent<Button>();
    }

    public void onClick_main_Settings()
    {
        settingsMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        onMainMenu = false;
        onSettingsMenu = true;

        currentButton = settingsMenu.Find("ToggleGroup").transform.Find("key1key2").gameObject.GetComponent<Toggle>();
    }

    public void onToggle_onSettings(GameObject g)
    {
        string value1 = "P1_K_";
        string value2 = "P2_K_";
        if(g.GetComponent<Toggle>().isOn && g.name == "key1key2")
        {
            value1 = "P1_K_";
            value2 = "P2_K_";
        }
        if (g.GetComponent<Toggle>().isOn && g.name == "key1joy2")
        {
            value1 = "P1_K_";
            value2 = "P1_J_";
        }
        if (g.GetComponent<Toggle>().isOn && g.name == "joy1key2")
        {
            value1 = "P1_J_";
            value2 = "P1_K_";
        }
        if (g.GetComponent<Toggle>().isOn && g.name == "joy1joy2")
        {
            value1 = "P1_J_";
            value2 = "P2_J_";
        }
        prefix1 = value1;
        prefix2 = value2;
        Debug.Log(prefix1 + " " + prefix2);
    }

    private void toggleOff(GameObject g)
    {
        settingsMenu.Find("key1key2").gameObject.GetComponent<Toggle>().isOn = false;
        settingsMenu.Find("joy1key2").gameObject.GetComponent<Toggle>().isOn = false;
        settingsMenu.Find("joy1key2").gameObject.GetComponent<Toggle>().isOn = false;
        settingsMenu.Find("joy1joy2").gameObject.GetComponent<Toggle>().isOn = false;
    }

    public void onClick_fight_Fight()
    {
        if(ready)
        {
            SceneManager.LoadScene("Duel");
            onFightMenu = false;
        }
    }

    public void onClick_fight_Back()
    {
        mainMenu.gameObject.SetActive(true);
        fightMenu.gameObject.SetActive(false);
        onFightMenu = false;
        onMainMenu = true;

        canvas.transform.Find("background").gameObject.GetComponent<Animator>().SetTrigger("sand-green");

        currentButton = mainMenu.Find("btnPlay").gameObject.GetComponent<Button>();
    }

    public void onClick_settings_Back()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        onSettingsMenu = false;
        onMainMenu = true;

        currentButton = mainMenu.Find("btnPlay").gameObject.GetComponent<Button>();
    }

    public void onClick_quit()
    {
        Application.Quit();
    }


    void OnGUI()
    {
        if (gcdUi <= 0)
        {
            if (onMainMenu)
            {
                if (Input.GetAxisRaw(prefix1 + "Horizontal") >= 0.5f)
                {
                    if (currentButton.navigation.selectOnRight != null)
                        currentButton = currentButton.navigation.selectOnRight;
                }
                if (Input.GetAxisRaw(prefix1 + "Horizontal") <= -0.5f)
                {
                    if (currentButton.navigation.selectOnLeft != null)
                        currentButton = currentButton.navigation.selectOnLeft;
                }
                if (Input.GetAxisRaw(prefix1 + "Vertical") <= -0.5f)
                {
                    if (currentButton.navigation.selectOnDown != null)
                        currentButton = currentButton.navigation.selectOnDown;
                }
                if (Input.GetAxisRaw(prefix1 + "Vertical") >= 0.5f)
                {
                    if (currentButton.navigation.selectOnUp != null)
                        currentButton = currentButton.navigation.selectOnUp;
                }
                currentButton.Select();

                if (Input.GetButton("Cancel"))
                {
                    currentButton = mainMenu.Find("Quit").transform.GetComponent<Button>();
                    mainMenu.Find("Quit").transform.GetComponent<Button>().Select();
                }
            }

            if(onSettingsMenu)
            {
                if (Input.GetAxisRaw(prefix1 + "Horizontal") >= 0.5f)
                {
                    if (currentButton.navigation.selectOnRight != null)
                        currentButton = currentButton.navigation.selectOnRight;
                }
                if (Input.GetAxisRaw(prefix1 + "Horizontal") <= -0.5f)
                {
                    if (currentButton.navigation.selectOnLeft != null)
                        currentButton = currentButton.navigation.selectOnLeft;
                }
                if (Input.GetAxisRaw(prefix1 + "Vertical") <= -0.5f)
                {
                    if (currentButton.navigation.selectOnDown != null)
                        currentButton = currentButton.navigation.selectOnDown;
                }
                if (Input.GetAxisRaw(prefix1 + "Vertical") >= 0.5f)
                {
                    if (currentButton.navigation.selectOnUp != null)
                        currentButton = currentButton.navigation.selectOnUp;
                }

                
                currentButton.Select();
                   

                if (Input.GetButton("Cancel"))
                {
                    currentButton = settingsMenu.Find("Back").transform.GetComponent<Button>();
                    settingsMenu.Find("Back").transform.GetComponent<Button>().Select();
                }
                    
            }

            if (onFightMenu)
            {
            
                    //player1
                    if (Input.GetAxisRaw(prefix1 + "Horizontal") >= 0.5f)
                    {
                        if (currentSpell1["x"] != spellsGrid.GetLength(0) - 1)
                            currentSpell1["x"] ++;
                    }
                    if (Input.GetAxisRaw(prefix1 + "Horizontal") <= -0.5f)
                    {
                        if (currentSpell1["x"] != 0)
                            currentSpell1["x"] --;
                    }
                    if (Input.GetAxisRaw(prefix1 + "Vertical") <= -0.5f)
                    {
                        if (currentSpell1["y"] != 0)
                            currentSpell1["y"] --;
                    }
                    if (Input.GetAxisRaw(prefix1 + "Vertical") >= 0.5f)
                    {
                        if (currentSpell1["y"] != spellsGrid.GetLength(1) - 1)
                            currentSpell1["y"] ++;
                    }


                    while (spellsGrid[currentSpell1["x"], currentSpell1["y"]] == null)
                    { currentSpell1["x"]--; }

                    selector1.transform.position = fightMenu.Find("spellsGrid").transform.Find(spellsGrid[currentSpell1["x"], currentSpell1["y"]].name).transform.position;

                    if (Input.GetAxisRaw(prefix1 + "Fire1") == 1)
                    {
                        refreshSpell(1, 0, currentSpell1["x"], currentSpell1["y"]);
                        Debug.Log("Player1 : spell1 = " + spellsPlayer1[0]);
                    }
                    if (Input.GetAxisRaw(prefix1 + "Fire2") == 1)
                    {
                        refreshSpell(1, 1, currentSpell1["x"], currentSpell1["y"]);
                        Debug.Log("Player1 : spell2 = " + spellsPlayer1[1]);
                    }
                    if (Input.GetButton(prefix1 + "Fire3"))
                    {
                        refreshSpell(1, 2, currentSpell1["x"], currentSpell1["y"]);
                        Debug.Log("Player1 : spell3 = " + spellsPlayer1[2]);
                    }

                    //player2
                    if (Input.GetAxisRaw(prefix2 + "Horizontal") >= 0.5f)
                    {
                        if (currentSpell2["x"] != spellsGrid.GetLength(0) - 1)
                            currentSpell2["x"] ++;
                    }
                    if (Input.GetAxisRaw(prefix2 + "Horizontal") <= -0.5f)
                    {
                        if (currentSpell2["x"] != 0)
                            currentSpell2["x"] --;
                    }
                    if (Input.GetAxisRaw(prefix2 + "Vertical") <= -0.5f)
                    {
                        if (currentSpell2["y"] != 0)
                            currentSpell2["y"] --;
                    }
                    if (Input.GetAxisRaw(prefix2 + "Vertical") >= 0.5f)
                    {
                        if (currentSpell2["y"] != spellsGrid.GetLength(1) - 1)
                            currentSpell2["y"] ++;
                    }

                    while (spellsGrid[currentSpell2["x"], currentSpell2["y"]] == null)
                    { currentSpell2["x"]--; }

                    selector2.transform.position = fightMenu.Find("spellsGrid").transform.Find(spellsGrid[currentSpell2["x"], currentSpell2["y"]].name).transform.position;
               
                    if (Input.GetAxisRaw(prefix2 + "Fire1") == 1)
                    {
                        refreshSpell(2, 0, currentSpell2["x"], currentSpell2["y"]);
                        Debug.Log("Player2 : spell1 = " + spellsPlayer2[0]);
                    }
                    if (Input.GetAxisRaw(prefix2 + "Fire2") == 1)
                    {
                        refreshSpell(2, 1, currentSpell2["x"], currentSpell2["y"]);
                        Debug.Log("Player2 : spell2 = " + spellsPlayer2[1]);
                    }
                    if (Input.GetButton(prefix2 + "Fire3"))
                    {
                        refreshSpell(2, 2, currentSpell2["x"], currentSpell2["y"]);
                        Debug.Log("Player2 : spell3 = " + spellsPlayer2[2]);
                    }

                    //sprites
                    for(int i = 0; i < fightMenu.Find("spellsGrid").transform.childCount; i++)
                    {
                        GameObject g = fightMenu.Find("spellsGrid").transform.GetChild(i).gameObject;
                        if (g.name == spellsGrid[currentSpell1["x"], currentSpell1["y"]].name 
                            || g.name == spellsGrid[currentSpell2["x"], currentSpell2["y"]].name)
                        {
                            g.GetComponent<Button>().interactable = true;
                            g.GetComponent<Image>().sprite = g.GetComponent<Button>().spriteState.highlightedSprite;
                        } else
                        {
                            g.GetComponent<Button>().interactable = false;
                            g.GetComponent<Image>().sprite = g.GetComponent<Button>().spriteState.disabledSprite;
                        }
                    }

                    
                fightMenu.Find("Player1").transform.Find("Description").transform.Find("Text").gameObject.GetComponent<Text>().text
                    = spellsGrid[currentSpell1["x"], currentSpell1["y"]].spellName;
                fightMenu.Find("Player2").transform.Find("Description").transform.Find("Text").gameObject.GetComponent<Text>().text
                    = spellsGrid[currentSpell2["x"], currentSpell2["y"]].spellName;

                //if everyone has 3 spells
                if ((spellsPlayer1[0] != null && spellsPlayer2[0] != null) &&
                    (spellsPlayer1[1] != null && spellsPlayer2[1] != null) &&
                    (spellsPlayer1[2] != null && spellsPlayer2[2] != null))
                    ready = true;
                else
                    ready = false;
                fightMenu.transform.Find("Fight").transform.GetComponent<Button>().interactable = ready;


                //inputs
                
                if (delay <= 0 && ready)
                {
                    if (Input.GetButton("Submit"))
                    {
                        fightMenu.Find("Fight").transform.GetComponent<Button>().Select();
                        delay = 0.5f;
                    }
                }
                else
                    delay -= Time.deltaTime;

                if (Input.GetButton("Cancel"))
                {
                    Debug.Log("cancel");
                    fightMenu.Find("Back").transform.GetComponent<Button>().Select();
                }
                    
            
            }
            gcdUi = 0.3f;
        }
        else
            gcdUi -= Time.deltaTime;

    }

    public void changeKey(GameObject button)
    {
        currentKey = button;
        if (currentKey.transform.parent.name == "Player1")
            currentPlayerKey = 1;
        if (currentKey.transform.parent.name == "Player2")
            currentPlayerKey = 2;
    }

    public void initKeys()
    {
        player1Keys.Add("up", KeyCode.Z);
        player1Keys.Add("down", KeyCode.S);
        player1Keys.Add("left", KeyCode.Q);
        player1Keys.Add("right", KeyCode.D);
        player1Keys.Add("facingup", KeyCode.I);
        player1Keys.Add("facingdown", KeyCode.K);
        player1Keys.Add("facingleft", KeyCode.J);
        player1Keys.Add("facingright", KeyCode.L);
        player1Keys.Add("spell1", KeyCode.Alpha7);
        player1Keys.Add("spell2", KeyCode.Alpha8);
        player1Keys.Add("spell3", KeyCode.Alpha9);

        player2Keys.Add("up", KeyCode.UpArrow);
        player2Keys.Add("down", KeyCode.DownArrow);
        player2Keys.Add("left", KeyCode.LeftArrow);
        player2Keys.Add("right", KeyCode.RightArrow);
        player2Keys.Add("facingup", KeyCode.Keypad5);
        player2Keys.Add("facingdown", KeyCode.Keypad2);
        player2Keys.Add("facingleft", KeyCode.Keypad1);
        player2Keys.Add("facingright", KeyCode.Keypad3);
        player2Keys.Add("spell1", KeyCode.Keypad7);
        player2Keys.Add("spell2", KeyCode.Keypad8);
        player2Keys.Add("spell3", KeyCode.Keypad9);
        

    }

    public void initSpellsGrid()
    {
        spellsGrid[0, 3] = Resources.Load<GameObject>("Prefab/Spells/fireball").GetComponent<Spell>();
        spellsGrid[1, 3] = Resources.Load<GameObject>("Prefab/Spells/lightning").GetComponent<Spell>();
        spellsGrid[2, 3] = Resources.Load<GameObject>("Prefab/Spells/defrag").GetComponent<Spell>();
        spellsGrid[3, 3] = Resources.Load<GameObject>("Prefab/Spells/grab").GetComponent<Spell>();
        spellsGrid[4, 3] = Resources.Load<GameObject>("Prefab/Spells/boomerang").GetComponent<Spell>();
        spellsGrid[5, 3] = Resources.Load<GameObject>("Prefab/Spells/guided").GetComponent<Spell>();

        spellsGrid[0, 2] = Resources.Load<GameObject>("Prefab/Spells/cone").GetComponent<Spell>();
        spellsGrid[1, 2] = Resources.Load<GameObject>("Prefab/Spells/laser").GetComponent<Spell>();
        spellsGrid[2, 2] = Resources.Load<GameObject>("Prefab/Spells/fissure").GetComponent<Spell>();
        spellsGrid[3, 2] = Resources.Load<GameObject>("Prefab/Spells/link").GetComponent<Spell>();
        spellsGrid[4, 2] = Resources.Load<GameObject>("Prefab/Spells/nova").GetComponent<Spell>();
        spellsGrid[5, 2] = Resources.Load<GameObject>("Prefab/Spells/counter").GetComponent<Spell>();
        
        spellsGrid[0, 1] = Resources.Load<GameObject>("Prefab/Spells/blink").GetComponent<Spell>();
        spellsGrid[1, 1] = Resources.Load<GameObject>("Prefab/Spells/charge").GetComponent<Spell>();
        spellsGrid[2, 1] = Resources.Load<GameObject>("Prefab/Spells/teleport").GetComponent<Spell>();
        spellsGrid[3, 1] = Resources.Load<GameObject>("Prefab/Spells/clone").GetComponent<Spell>();
        spellsGrid[4, 1] = Resources.Load<GameObject>("Prefab/Spells/iceblock").GetComponent<Spell>();
        spellsGrid[5, 1] = Resources.Load<GameObject>("Prefab/Spells/invisibility").GetComponent<Spell>();

        spellsGrid[0, 0] = Resources.Load<GameObject>("Prefab/Spells/circle").GetComponent<Spell>();
        spellsGrid[1, 0] = Resources.Load<GameObject>("Prefab/Spells/meteor").GetComponent<Spell>();
        spellsGrid[2, 0] = Resources.Load<GameObject>("Prefab/Spells/requiem").GetComponent<Spell>();

        currentSpell1.Add("x", 0); currentSpell1.Add("y", 0);
        currentSpell2.Add("x", 0); currentSpell2.Add("y", 0);
        selector1 = fightMenu.Find("selector1");
        selector2 = fightMenu.Find("selector2");

        Debug.Log(Screen.width + " " + Screen.height);


        currentSpell1["x"] = 0;
        currentSpell1["y"] = 3;

        currentSpell2["x"] = 0;
        currentSpell2["y"] = 3;
    }
    
    private void refreshSpell(int numPlayer, int numSpell, int x, int y)
    {
        Transform spellBar = null;
        if (numPlayer == 1)
        {
            spellBar = fightMenu.Find("Player1");

            for (int i = 0; i < spellsPlayer1.Length; i++)
            {
                if (spellsPlayer1[i] != null && (spellsPlayer1[i].spellName == spellsGrid[x, y].spellName))
                {
                    spellsPlayer1[i] = null;
                    spellBar.Find("Spell" + (i + 1).ToString()).gameObject.GetComponent<Image>().overrideSprite =
                        Resources.Load<Sprite>("Icons/tile");
                }
            }

            spellsPlayer1[numSpell] = spellsGrid[x, y];
            spellBar.Find("Spell" + (numSpell + 1).ToString()).gameObject.GetComponent<Image>().overrideSprite
                = spellsPlayer1[numSpell].icon;
        }

        if (numPlayer == 2)
        {
            spellBar = fightMenu.Find("Player2");

            for (int i = 0; i < spellsPlayer2.Length; i++)
                if (spellsPlayer2[i] != null && (spellsPlayer2[i].spellName == spellsGrid[x, y].spellName))
                {
                    spellsPlayer2[i] = null;
                    spellBar.Find("Spell" + (i + 1).ToString()).gameObject.GetComponent<Image>().overrideSprite =
                        Resources.Load<Sprite>("Icons/tile");
                }

            spellsPlayer2[numSpell] = spellsGrid[x, y];
            spellBar.Find("Spell" + (numSpell + 1).ToString()).gameObject.GetComponent<Image>().overrideSprite
                = spellsPlayer2[numSpell].icon;

        }
    }

    private void setSpellsBar()
    {
        Sprite icon1 = Resources.Load<Sprite>("Icons/tile");
        Sprite icon2 = Resources.Load<Sprite>("Icons/tile");

        for (int i = 0; i < 3; i++)
        {
            if (spellsPlayer1[i] != null)
                icon1 = spellsPlayer1[i].icon;
            if (spellsPlayer2[i] != null)
                icon2 = spellsPlayer2[i].icon;

            fightMenu.Find("Player1").Find("Spell" + (i + 1).ToString()).gameObject.GetComponent<Image>().overrideSprite =
                icon1;
            fightMenu.Find("Player2").Find("Spell" + (i + 1).ToString()).gameObject.GetComponent<Image>().overrideSprite =
                icon2;
        }
    }
}
