using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverStats : MonoBehaviour
{

    public TextMeshProUGUI pathogen, rounds, component;

    // Start is called before the first frame update
    void Start()
    {
        pathogen.text = GameObject.Find("GameStats").GetComponent<RoundStats>().pathogenDeath;
        rounds.text = GameObject.Find("GameStats").GetComponent<RoundStats>().wavesSurvived + "/20";
        component.text = GameObject.Find("GameStats").GetComponent<RoundStats>().componentDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
