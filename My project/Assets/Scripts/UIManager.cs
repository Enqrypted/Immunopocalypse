using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static RoundManager;

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
    public GameObject changeDefenseWarning;

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

        attackPanel.Find("HealButton").GetComponent<Button>().onClick.AddListener(QuickHeal);

    }

    void SelectDefense(Transform t) {
        selectedDefense = t.parent.name;
    }

    void QuickHeal()
    {
        if (roundManager.Immunopoints >= 100)
        {
            roundManager.Immunopoints -= 100;
            ((GameObject)Instantiate(Resources.Load("UpgradeAudio"))).GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.75f, 1.25f);
            Defense healingDefense = roundManager.GetDefenseByName(selectedDefense);
            healingDefense.Health = Mathf.Min(healingDefense.MaxHealth, healingDefense.Health+200);
        }
        else
        {
            roundManager.ShowTip("Not enough immunopoints to heal!");
        }
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

        //check internal/external defense attack
        if (btnText.text == "Go to external")
        {

            bool warn = false;
            bool warnUrgent = false;

            //check external components
            foreach(Defense d in roundManager.ExternalDefense)
            {
                if (d.IsUnderAttack)
                {

                    warn = true;

                    if (d.Health <= (d.MaxHealth / 2f))
                    {
                        warnUrgent = true;
                    }


                }
            }

            changeDefenseWarning.SetActive(warn);
            changeDefenseWarning.GetComponent<WarningFlash>().isFlashing = warnUrgent;

        }
        else
        {

            bool warn = false;
            bool warnUrgent = false;

            //check internal components
            foreach (Defense d in roundManager.InternalDefense)
            {
                if (d.IsUnderAttack)
                {

                    warn = true;

                    if (d.Health <= (d.MaxHealth / 2f))
                    {
                        warnUrgent = true;
                    }


                }
            }

            changeDefenseWarning.SetActive(warn);
            changeDefenseWarning.GetComponent<WarningFlash>().isFlashing = warnUrgent;

        }

            //Update points
            if (lastImmunopoints != roundManager.Immunopoints){
            lastImmunopoints = roundManager.Immunopoints;
            immunopointsText.text = lastImmunopoints.ToString();
        }

        //Update pathogen stats
        if (roundManager.isIntermission)
        {
            pathogenPanel.transform.Find("attackingUpcoming").GetComponent<TextMeshProUGUI>().text = "Upcoming disease:";
            pathogenPanel.Find("currentPathogen").GetComponent<TextMeshProUGUI>().text = roundManager.upcomingPathogen.PathogenName;
            pathogenPanel.Find("pathogenDesc").GetComponent<TextMeshProUGUI>().text = roundManager.upcomingPathogen.PathogenInfo;
        }
        else
        {
            pathogenPanel.transform.Find("attackingUpcoming").GetComponent<TextMeshProUGUI>().text = "Attacking pathogen!";
            pathogenPanel.Find("currentPathogen").GetComponent<TextMeshProUGUI>().text = roundManager.CurrentPathogen.PathogenName;
            pathogenPanel.Find("pathogenDesc").GetComponent<TextMeshProUGUI>().text = roundManager.CurrentPathogen.PathogenInfo;
        }

        

        if (roundManager.isPaused)
        {
            waveStatus.text = "GAME PAUSED";
            currentWave.text = "Intermission";
            waveTimer.text = "00:00";
        }
        else
        {
            //Update round stats
            if (roundManager.isIntermission)
            {
                waveStatus.text = "NEXT WAVE IN:";
                currentWave.text = "Intermission";
                TimeSpan t = TimeSpan.FromSeconds(Mathf.Ceil(roundManager.EndAt - Time.time));
                waveTimer.text = t.ToString(@"mm\:ss");
            }
            else
            {
                
                waveStatus.text = "WAVE ENDS:";
                currentWave.text = roundManager.CurrentWave.ToString() + "/20";
                TimeSpan t = TimeSpan.FromSeconds(Mathf.Ceil(roundManager.EndAt - Time.time));

                if (roundManager.CurrentWave == 1 && t.Seconds > 50f)
                {
                    Alert.gameObject.SetActive(true);
                    Alert.Find("text").GetComponent<TextMeshProUGUI>().text = "Quickly tap the green bubbles to heal and the yellow bubbles to damage the pathogen!";
                }
                else
                {
                    Alert.gameObject.SetActive(false);
                }

                waveTimer.text = t.ToString(@"mm\:ss");
            }
        }

        //Add warning signs to parts under attack
        foreach(Transform t in internalDefense.transform)
        {
            Defense checkingDefense = GetDefense(t.name);
            if (checkingDefense != null)
            {
                t.GetComponent<Image>().color = Color.Lerp(new Color(.5f, 1f, 1f), new Color(1, 0, 0), 1f - ( (float)checkingDefense.Health / (float)checkingDefense.MaxHealth));
            }

            if (checkingDefense != null && checkingDefense.IsUnderAttack)
            {
                if ((t.Find("warning") == null) && internalDefense.gameObject.activeSelf==true)
                {
                    GameObject warning = (GameObject)Instantiate(Resources.Load("warning"), t);
                    warning.name = "warning";
                    warning.transform.localPosition = new Vector3(26, 26);
                }

                if ((t.Find("warning") != null))
                {
                    t.Find("warning").GetComponent<WarningFlash>().isFlashing = checkingDefense.Health <= (checkingDefense.MaxHealth/2f);
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

            Defense checkingDefense = GetDefense(t.name);
            if (checkingDefense != null)
            {
                t.GetComponent<Image>().color = Color.Lerp(new Color(.5f, 1f, 1f), new Color(1, 0, 0), 1f - ((float)checkingDefense.Health / (float)checkingDefense.MaxHealth));
            }

            if (checkingDefense != null && checkingDefense.IsUnderAttack)
            {
                if ((t.Find("warning") == null) && externalDefense.gameObject.activeSelf == true)
                {
                    GameObject warning = (GameObject)Instantiate(Resources.Load("warning"), t);
                    warning.name = "warning";
                    warning.transform.localPosition = new Vector3(26, 26);
                }

                if ((t.Find("warning") != null))
                {
                    t.Find("warning").GetComponent<WarningFlash>().isFlashing = checkingDefense.Health <= (checkingDefense.MaxHealth / 2f);
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
