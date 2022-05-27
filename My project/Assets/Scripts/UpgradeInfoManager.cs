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
    public bool unlocked = false;
    public List<GameObject> lvl2UI;
    public List<GameObject> lvl1UI;
    RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        targDefense = GameObject.Find("GameManager").GetComponent<RoundManager>().GetDefenseByName(transform.parent.name.Replace("Upgrades", ""));
        targUpgrade = FindUpgrade(upgradeName, targDefense.Upgrades);

        GetComponent<Button>().onClick.AddListener(DisplayUpgrade);

        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();

        GameObject infoPanel = GameObject.FindGameObjectWithTag("UpgradesInfo");
        lvl2UI.Add(infoPanel.transform.Find("Lvl2Text").gameObject);
        lvl2UI.Add(infoPanel.transform.Find("Lvl2Desc").gameObject);
        lvl2UI.Add(infoPanel.transform.Find("Lvl2Buy").gameObject);

        lvl1UI.Add(infoPanel.transform.Find("Lvl1Text").gameObject);
        lvl1UI.Add(infoPanel.transform.Find("Lvl1Desc").gameObject);
        lvl1UI.Add(infoPanel.transform.Find("Lvl1Buy").gameObject);

        lvl2UI[1].SetActive(false);
        lvl1UI[1].SetActive(false);

        infoPanel.transform.Find("Lvl1Buy").gameObject.GetComponent<Button>().onClick.AddListener(BuyLvl1);
        infoPanel.transform.Find("Lvl2Buy").gameObject.GetComponent<Button>().onClick.AddListener(BuyLvl2);


    }

    RoundManager.Upgrade FindUpgrade(string findWithName, List<RoundManager.Upgrade> upgradeList)
    {
        foreach (RoundManager.Upgrade upgrade in upgradeList)
        {
            if (upgrade.UpgradeName.ToLower().Trim() == findWithName.ToLower().Trim())
            {
                return upgrade;
            }
        }

        return null;
    }

    void BuyLvl1()
    {
        if (targUpgrade == roundManager.selectedUpgrade)
        {
            if ((targUpgrade.LevelPrices.Count > 0) && (targUpgrade.UpgradeLevel == 0))
            {
                //BUY LVL 1
                if(targUpgrade.LevelPrices[0] <= roundManager.Immunopoints)
                {

                    ((GameObject)Instantiate(Resources.Load("UpgradeAudio"))).GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.75f, 1.25f);

                    if (targUpgrade.UpgradeName.ToLower().Contains("heal"))
                    {

                        roundManager.Immunopoints -= targUpgrade.LevelPrices[0];
                        targDefense.Health = Mathf.Min(targDefense.Health + 200, targDefense.MaxHealth);

                    }
                    else {

                        roundManager.Immunopoints -= targUpgrade.LevelPrices[0];
                        targDefense.UnlockedUpgrades.Add(targUpgrade);
                        targUpgrade.UpgradeLevel = 1;
                        DisplayUpgrade();
                        gameObject.GetComponent<Image>().color = new Color(.5f, 1f, 1f);

                    }

                }
                else
                {

                    roundManager.ShowTip("Not enough immunopoints!");

                }
            }
        }
    }
    
    void BuyLvl2()
    {
        if (targUpgrade == roundManager.selectedUpgrade)
        {
            if ((targUpgrade.LevelPrices.Count > 1) && (targUpgrade.UpgradeLevel == 1))
            {
                //BUY LVL 2
                if (targUpgrade.LevelPrices[1] <= roundManager.Immunopoints)
                {

                    ((GameObject)Instantiate(Resources.Load("UpgradeAudio"))).GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.75f, 1.25f);

                    roundManager.Immunopoints -= targUpgrade.LevelPrices[1];
                    targUpgrade.UpgradeLevel = 2;
                    DisplayUpgrade();
                    gameObject.GetComponent<Image>().color = new Color(.5f, 1f, .5f);

                }
                else
                {

                    roundManager.ShowTip("Not enough immunopoints!");

                }
            }
        }
    }

    public void DisplayUpgrade()
    {
        if (targUpgrade != null && unlocked)
        {
            roundManager.selectedUpgrade = targUpgrade;

            string foundUpgradeFunc = "";

            foreach (KeyValuePair<string, int> entry in roundManager.upgradesFunctionality.DamagePathogen_Upgrades) {
                if(entry.Key.ToLower().Trim() == targUpgrade.UpgradeName.ToLower().Trim())
                {
                    foundUpgradeFunc = "Increases damage done to pathogen";
                }
            }

            foreach (KeyValuePair<string, int> entry in roundManager.upgradesFunctionality.DamageToDefense_Upgrades)
            {
                if (entry.Key.ToLower().Trim() == targUpgrade.UpgradeName.ToLower().Trim())
                {
                    foundUpgradeFunc = "Decreases amount of damage taken to this body part";
                }
            }

            foreach (KeyValuePair<string, int> entry in roundManager.upgradesFunctionality.HealDefense_Upgrades)
            {
                if (entry.Key.ToLower().Trim() == targUpgrade.UpgradeName.ToLower().Trim())
                {
                    foundUpgradeFunc = "Increases amount of healing from green bubbles to this body part";
                }
            }


            GameObject infoPanel = GameObject.FindGameObjectWithTag("UpgradesInfo");
            infoPanel.transform.Find("UpgradeName").GetComponent<TextMeshProUGUI>().text = upgradeName;
            infoPanel.transform.Find("InfoPanel").Find("upgradeDesc").GetComponent<TextMeshProUGUI>().text = targUpgrade.UpgradeDescription;
            infoPanel.transform.Find("UpgradeDesc").GetComponent<TextMeshProUGUI>().text = foundUpgradeFunc;

            infoPanel.transform.Find("Lvl1Buy").Find("text").GetComponent<TextMeshProUGUI>().text = "Buy - " + targUpgrade.LevelPrices[0].ToString();

            if (targUpgrade.LevelPrices.Count > 1)
            {
                lvl2UI[0].SetActive(true);
                lvl2UI[2].SetActive(true);

                infoPanel.transform.Find("Lvl2Buy").Find("text").GetComponent<TextMeshProUGUI>().text = "Buy - " + targUpgrade.LevelPrices[1].ToString();

            }
            else
            {
                foreach (GameObject obj in lvl2UI)
                {
                    obj.SetActive(false);
                }
            }

            if (targUpgrade.UpgradeLevel == 0)
            {
                lvl1UI[2].SetActive(true);
                lvl2UI[2].SetActive(false);
            }
            else if (targUpgrade.UpgradeLevel == 1)
            {
                lvl1UI[2].SetActive(false);
                lvl2UI[2].SetActive(true);
            }
            else
            {
                lvl1UI[2].SetActive(false);
                lvl2UI[2].SetActive(false);
            }

        }
        else{
            if (targUpgrade != null) {
                roundManager.ShowTip("This upgrade required the following upgrades to be unlocked: " + string.Join(",", upgradeReqs));
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
                if (targUpgrade.UpgradeLevel == 0)
                {
                    img.color = Color.white;
                }
                
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
