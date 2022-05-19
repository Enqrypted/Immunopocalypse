using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(StartGame);

        if (gameObject.transform.Find("MenuBtn") != null)
            gameObject.transform.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(BackToMenu);

    }

    void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
