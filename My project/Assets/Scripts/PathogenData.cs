using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public enum pathogenType
{
    Fungi,
    Virus,
    Bacteria
}

public enum pathogenArea
{
    top,
    mid,
    bot
}

public class PathogenData : MonoBehaviour
{

    

    public int health = 1000;

    [SerializeField]
    public List<String> componentDamages = new List<String>();

    [SerializeField]
    public pathogenType VirusType = pathogenType.Fungi;

    [SerializeField]
    public String desc;

    [SerializeField]
    public int yellowDamage = 50;
    public int greenHealth = 50;

    [SerializeField]
    public pathogenArea attackArea = pathogenArea.bot;

}
