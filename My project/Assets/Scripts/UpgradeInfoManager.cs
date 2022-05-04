using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoManager : MonoBehaviour
{

    RoundManager.Defense targDefense;
    RoundManager.Upgrade targUpgrade;
    public string upgradeName;
    public List<string> upgradeReqs = new List<string>();
    bool unlocked = false;
    public List<GameObject> lvl2UI;

    // Start is called before the first frame update
    void Start()
    {
        targDefense = GameObject.Find("GameManager").GetComponent<RoundManager>().GetDefenseByName(transform.parent.name.Replace("Upgrades", ""));
        targUpgrade = FindUpgrade(upgradeName, targDefense.Upgrades);
        GetComponent<Button>().onClick.AddListener(DisplayUpgrade);

        GameObject infoPanel = GameObject.FindGameObjectWithTag("UpgradesInfo");
        lvl2UI.Add(infoPanel.transform.Find("Lvl2Text").gameObject);
        lvl2UI.Add(infoPanel.transform.Find("Lvl2Desc").gameObject);
        lvl2UI.Add(infoPanel.transform.Find("Lvl2Buy").gameObject);
    }

    RoundManager.Upgrade FindUpgrade(string findWithName, List<RoundManager.Upgrade> upgradeList)
    {
        foreach (RoundManager.Upgrade upgrade in upgradeList)
        {
            if (upgrade.UpgradeName == findWithName)
            {
                return upgrade;
            }
        }

        return null;
    }

    void DisplayUpgrade()
    {
        if (targUpgrade != null)
        {
            GameObject infoPanel = GameObject.FindGameObjectWithTag("UpgradesInfo");
            infoPanel.transform.Find("UpgradeName").GetComponent<TextMeshProUGUI>().text = upgradeName;
            infoPanel.transform.Find("InfoPanel").Find("upgradeDesc").GetComponent<TextMeshProUGUI>().text = targUpgrade.UpgradeDescription;

            infoPanel.transform.Find("Lvl1Buy").Find("text").GetComponent<TextMeshProUGUI>().text = "Buy - " + targUpgrade.LevelPrices[0].ToString();

            if (targUpgrade.LevelPrices.Count > 1)
            {
                foreach(GameObject obj in lvl2UI)
                {
                    obj.SetActive(true);
                }

                infoPanel.transform.Find("Lvl2Buy").Find("text").GetComponent<TextMeshProUGUI>().text = "Buy - " + targUpgrade.LevelPrices[1].ToString();

            }
            else
            {
                foreach (GameObject obj in lvl2UI)
                {
                    obj.SetActive(false);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targDefense != null && targUpgrade != null)
        {

            bool hasAllUpgs = true;
            foreach (string upgradeReq in upgradeReqs)
            {
                if (FindUpgrade(upgradeReq, targDefense.UnlockedUpgrades) == null)
                {
                    
                    hasAllUpgs = false;
                    break;
                }
            }

            unlocked = hasAllUpgs;

            
        }

        if (unlocked)
        {
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = Color.white;
            }
        }
        else
        {
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = Color.black;
            }
        }
    }
}
