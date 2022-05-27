using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningFlash : MonoBehaviour
{

    public bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            if (Time.time % 1f <= .5f)
            {
                GetComponent<Image>().enabled = false;
                transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().enabled = false;
            }
            else
            {

                GetComponent<Image>().enabled = true;
                transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().enabled = true;

            }
        }
        else
        {
            GetComponent<Image>().enabled = true;
            transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
