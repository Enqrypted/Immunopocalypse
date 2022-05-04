using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesUIManager : MonoBehaviour
{

    public GameObject UpgradesPanel;
    public UIManager UIManager;
    public List<GameObject> UpgradePanels = new List<GameObject>();
    public Button upgradeButton, backToGame;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(OpenDefenseUpgrades);
        backToGame.onClick.AddListener(CloseDefenseUpgrades);
    }

    public void OpenDefenseUpgrades() {



        foreach (GameObject panel in UpgradePanels)
        {
            if (panel != null && panel.name != null)
            {
                if (panel.name.Contains(UIManager.selectedDefense))
                {
                    panel.SetActive(true);
                }
                else
                {
                    panel.SetActive(false);
                }
            }
        }

        UpgradesPanel.SetActive(true);

        UpgradesPanel.transform.Find("UpgradesTitle").Find("title").GetComponent<TextMeshProUGUI>().text = UIManager.selectedDefense + " Upgrades";
    }

    public void CloseDefenseUpgrades() {
        UpgradesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
