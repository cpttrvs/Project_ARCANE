using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonsBehaviour : MonoBehaviour {

	public void onClick_MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void onClick_Return()
    {
        GameObject.Find("Game").transform.Find("GameInstance").transform.gameObject.GetComponent<GameInstance>().backToGame();
    }
}
