using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    public Vector3 randomPos;
    // Start is called before the first frame update
    void Start()
    {
        randomPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = randomPos + new Vector3(0f, Mathf.Sin(Time.time*2)*6);
        transform.rotation = Quaternion.Euler(0,0, Mathf.Sin(Time.time * 1.5f));
    }
}
