using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFloat : MonoBehaviour
{
    public Vector3 randomPos;
    // Start is called before the first frame update
    void Start()
    {
        randomPos = new Vector3(Random.Range(0f, 200f) - 100f, Random.Range(0f, 100f) - 50f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = randomPos + new Vector3(0f, Mathf.Sin(Time.time*4)*6);
        transform.rotation = Quaternion.Euler(0,0, Time.time*50f);
    }
}
