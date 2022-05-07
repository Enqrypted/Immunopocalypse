using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    Button ChangeDefenseBtn;

    TextMeshProUGUI btnText;
    TextMeshProUGUI immunopointsText;
    TextMeshProUGUI currentWave;
    TextMeshProUGUI waveTimer;
    TextMeshProUGUI waveStatus;

    public Transform internalDefense;
    public Transform externalDefense;
    public Transform[] defenseButtons;

    private int lastImmunopoints;
    public RoundManager roundManager;
    public string selectedDefense = "Nose";
    public Transform defensePanel;
    public Transform Alert;

    public Transform infoPanel, attackPanel, pathogenPanel;

    public GameObject Human, Organs, OpaqueHuman, TransparentHuman;

    // Start is called before the first frame update
    void Start()
    {
        ChangeDefenseBtn = GameObject.Find("ChangeDefense").GetComponent<Button>();
        btnText = ChangeDefenseBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        ChangeDefenseBtn.onClick.AddListener(ChangeDefense);

        ChangeDefense();

        lastImmunopoints = roundManager.Immunopoints;
        immunopointsText = GameObject.Find("ImmunopointsPanel").transform.Find("imAmount").GetComponent<TextMeshProUGUI>();

        Transform statsPanel = GameObject.Find("StatsPanel").transform.Find("WaveStats");

        currentWave = statsPanel.Find("WaveNum").GetComponent<TextMeshProUGUI>();
        waveTimer = statsPanel.Find("Timer").GetComponent<TextMeshProUGUI>();
        waveStatus = statsPanel.Find("CurrentStatus").GetComponent<TextMeshProUGUI>();

        foreach(Transform t in defenseButtons)
        {

            t.GetComponent<Button>().onClick.AddListener(delegate { SelectDefense(t); });

        }

    }

    void SelectDefense(Transform t) {
        selectedDefense = t.parent.name;
    }

    void ChangeDefense(){
        if (btnText.text == "Go to external"){

            GameObject.Find("DefenseTitle").transform.Find("text").gameObject.GetComponent<TextMeshProUGUI>().text = "External Defense";

            btnText.text = "Go to internal";
            internalDefense.gameObject.SetActive(false);
            externalDefense.gameObject.SetActive(true);

            OpaqueHuman.SetActive(true);
            TransparentHuman.SetActive(false);
            Organs.SetActive(false);

        }
        else{

            GameObject.Find("DefenseTitle").transform.Find("text").gameObject.GetComponent<TextMeshProUGUI>().text = "Internal Defense";

            btnText.text = "Go to external";
            internalDefense.gameObject.SetActive(true);
            externalDefense.gameObject.SetActive(false);

            OpaqueHuman.SetActive(false);
            TransparentHuman.SetActive(true);
            Organs.SetActive(true);

        }
    }

    RoundManager.Defense GetDefense(string defenseName)
    {
        foreach(RoundManager.Defense d in roundManager.ExternalDefense)
        {
            if (d.DefenseName == defenseName) { return d; }
        }

        foreach (RoundManager.Defense d in roundManager.InternalDefense)
        {
            if (d.DefenseName == defenseName) { return d; }
        }

        return null;
    }

    public IEnumerator ShowSlain(string pathogenName)
    {
        Alert.gameObject.SetActive(true);
        Alert.Find("text").GetComponent<TextMeshProUGUI>().text = pathogenName + " slain!";
        yield return new WaitForSeconds(5f);
        Alert.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Update points
        if (lastImmunopoints != roundManager.Immunopoints){
            lastImmunopoints = roundManager.Immunopoints;
            immunopointsText.text = lastImmunopoints.ToString();
        }

        //Update pathogen stats
        if (roundManager.isIntermission)
        {
            pathogenPanel.gameObject.SetActive(false);
        }
        else
        {
            pathogenPanel.gameObject.SetActive(true);
            pathogenPanel.Find("currentPathogen").GetComponent<TextMeshProUGUI>().text = roundManager.CurrentPathogen.PathogenName;
            pathogenPanel.Find("pathogenDesc").GetComponent<TextMeshProUGUI>().text = roundManager.CurrentPathogen.PathogenInfo;
        }

        //Update round stats
        if (roundManager.isIntermission)
        {
            waveStatus.text = "NEXT WAVE IN:";
            currentWave.text = "Intermission";
            TimeSpan t = TimeSpan.FromSeconds(Mathf.Ceil(roundManager.EndAt - Time.time));
            waveTimer.text = t.ToString(@"mm\:ss");
        }
        else {
            Alert.gameObject.SetActive(true);
            Alert.Find("text").GetComponent<TextMeshProUGUI>().text = "Quickly tap the green bubbles to heal and the yellow bubbles to damage the pathogen!";
            waveStatus.text = "WAVE ENDS:";
            currentWave.text = roundManager.CurrentWave.ToString() + "/20";
            TimeSpan t = TimeSpan.FromSeconds(Mathf.Ceil(roundManager.EndAt - Time.time));
            waveTimer.text = t.ToString(@"mm\:ss");
        }

        //Add warning signs to parts under attack
        foreach(Transform t in internalDefense.transform)
        {
            if (GetDefense(t.name) != null && GetDefense(t.name).IsUnderAttack)
            {
                if (t.Find("warning") == null)
                {
                    GameObject warning = (GameObject)Instantiate(Resources.Load("warning"), t);
                    warning.transform.localPosition = new Vector3(26, 26);
                }
            }
            else
            {
                if (t.Find("warning"))
                {
                    Destroy(t.Find("warning").gameObject);
                }
            }
        }

        foreach (Transform t in externalDefense.transform)
        {
            if (GetDefense(t.name) != null && GetDefense(t.name).IsUnderAttack)
            {
                if (t.Find("warning") == null)
                {
                    GameObject warning = (GameObject)Instantiate(Resources.Load("warning"), t);
                    warning.name = "warning";
                    warning.transform.localPosition = new Vector3(26, 26);
                }
            }
            else
            {
                if (t.Find("warning"))
                {
                    Destroy(t.Find("warning").gameObject);
                }
            }
        }

        //Update selected defense stats
        defensePanel.Find("DefenseSelected").GetComponent<TextMeshProUGUI>().text = selectedDefense;
        RoundManager.Defense csDefense = GetDefense(selectedDefense);
        if (csDefense != null)
        {
            if (csDefense.IsUnderAttack)
            {
                attackPanel.Find("dt").GetComponent<TextMeshProUGUI>().text = selectedDefense + " Health";
                defensePanel.Find("AttackStatus").GetComponent<TextMeshProUGUI>().text = "Under attack!";
                //print(csDefense.Health + " / " + csDefense.MaxHealth);
                attackPanel.Find("dHealth").Find("Bar").localScale = Vector3.Lerp(attackPanel.Find("dHealth").Find("Bar").localScale, new Vector3((float)csDefense.Health / (float)csDefense.MaxHealth, 1, 0), 4f * Time.deltaTime);
                attackPanel.Find("pHealth").Find("Bar").localScale = Vector3.Lerp(attackPanel.Find("pHealth").Find("Bar").localScale, new Vector3((float)roundManager.CurrentPathogen.PathogenHealth / (float)roundManager.CurrentPathogen.PathogenMaxHealth, 1, 0), 4f * Time.deltaTime);
                attackPanel.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(false);
            }
            else
            {
                defensePanel.Find("AttackStatus").GetComponent<TextMeshProUGUI>().text = "";
                defensePanel.Find("InfoPanel").Find("defenseDesc").GetComponent<TextMeshProUGUI>().text = csDefense.DefenseDescription;
                attackPanel.gameObject.SetActive(false);
                infoPanel.gameObject.SetActive(true);
            }
        }

    }
}
