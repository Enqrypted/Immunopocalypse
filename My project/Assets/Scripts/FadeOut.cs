using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOut : MonoBehaviour
{

    float opacity = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        opacity -= Time.deltaTime*1.5f;
        gameObject.GetComponent<Image>().color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, opacity);

        TextMeshProUGUI tmp = transform.Find("txt").GetComponent<TextMeshProUGUI>();

        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, opacity);
        transform.position += new Vector3(0, Time.time/50, 0);
    }
}
