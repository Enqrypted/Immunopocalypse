using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static RoundManager;

public class UpgradesUIManager : MonoBehaviour
{

    public GameObject UpgradesPanel;
    public UIManager UIManager;
    public RoundManager roundManager;
    public List<GameObject> UpgradePanels = new List<GameObject>();
    public Button upgradeButton, backToGame;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(OpenDefenseUpgrades);
        backToGame.onClick.AddListener(CloseDefenseUpgrades);
    }

    IEnumerator openUpgradeInfo(GameObject foundPanel)
    {
        yield return new WaitForSeconds(.2f);

        if (foundPanel != null)
        {
            foreach (Transform upg in foundPanel.transform)
            {
                if (upg.GetComponent<UpgradeInfoManager>().unlocked == true)
                {
                    upg.GetComponent<UpgradeInfoManager>().DisplayUpgrade();
                }
            }
        }

    }

    public void OpenDefenseUpgrades() {


        GameObject foundPanel = null;
        foreach (GameObject panel in UpgradePanels)
        {
            if (panel != null && panel.name != null)
            {
                if (panel.name.Contains(UIManager.selectedDefense))
                {
                    panel.SetActive(true);

                    foundPanel = panel;

                }
                else
                {
                    panel.SetActive(false);
                }
            }
        }

        UpgradesPanel.SetActive(true);


        UpgradesPanel.transform.Find("UpgradesTitle").Find("title").GetComponent<TextMeshProUGUI>().text = UIManager.selectedDefense + " Upgrades";

        StartCoroutine(openUpgradeInfo(foundPanel));


    }

    public void CloseDefenseUpgrades() {
        UpgradesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UpgradesPanel.activeSelf) {
            Defense currentDefense = roundManager.GetDefenseByName(UIManager.selectedDefense);
            UpgradesPanel.transform.Find("dt").GetComponent<TextMeshProUGUI>().text = currentDefense.DefenseName;
            UpgradesPanel.transform.Find("dHealth").Find("Bar").transform.localScale = new Vector3((float)currentDefense.Health/(float)currentDefense.MaxHealth, 1f, 1f);
        }
    }
}
