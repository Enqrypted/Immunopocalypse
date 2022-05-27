using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStats : MonoBehaviour
{

    public int wavesSurvived;
    public string pathogenDeath;
    public string componentDeath;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
