using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{

    public int Immunopoints = 20;
    public int CurrentWave = 0;
    public float EndAt;
    public Pathogen CurrentPathogen;
    public List<Pathogen> Stage1Pathogens;
    public List<Pathogen> Stage2Pathogens;
    public bool isIntermission = true;

    public List<Defense> InternalDefense;
    public List<Defense> ExternalDefense;

    public UIManager uim;
    public Transform bodyAreas;

    // Start is called before the first frame update
    void Start()
    {

        //TODO
        //FINISH ALL PATHOGENS
        Stage1Pathogens = new List<Pathogen> {
            new Pathogen(1000, "Athletes foot", new Dictionary<string, int>{ { "Skin",  30} }, "Fungi", "This fungus casues a skin infection turning the skin red in the feet and between the toes. It is transmitted by close contact with infected person or area.", 50, 50),
        };

        Stage2Pathogens = new List<Pathogen> {
        };

        //TODO
        //FINISH ALL DEFENSE COMPONENTS AND THEIR UPGRADES

        //INITIALIZE THE DEFENSE COMPONENTS
        InternalDefense = new List<Defense> {
            new Defense("Eye", false, 1000, new List<Upgrade>{
                new Upgrade("Increase tears", new List<int> { 300, 150}, 0, "Supply the surface of the eye with oxygen and nutrients (since there aren't any blood vessels present)")
            }, new List<Upgrade>{}, "The eyes produce tears to flush out microbes and foreign objects."),
            new Defense("Nose", false, 1000, new List<Upgrade>{
                new Upgrade("Increase tears", new List<int> { 300, 150}, 0, "Supply the surface of the eye with oxygen and nutrients (since there aren't any blood vessels present)")
            }, new List<Upgrade>{}, "The nose traps microbes with hairs and secretes mucus."),
            new Defense("Mouth", false, 1000, new List<Upgrade>{
                new Upgrade("Increase tears", new List<int> { 300, 150}, 0, "Supply the surface of the eye with oxygen and nutrients (since there aren't any blood vessels present)")
            }, new List<Upgrade>{}, "The mouth contains many different microbes and produces saliva containing enzymes and antibodies to destroy them."),
            new Defense("Skin", false, 1000, new List<Upgrade>{
                new Upgrade("Increase tears", new List<int> { 300, 150}, 0, "Supply the surface of the eye with oxygen and nutrients (since there aren't any blood vessels present)")
            }, new List<Upgrade>{}, "Physical barrier that blocks microbes from entering the body")
        };

        ExternalDefense = new List<Defense> {
            //TODO
        };


        //START THE GAME
        StartCoroutine(NewWave(ChooseRandomPathogen(1)));

    }

    Pathogen ChooseRandomPathogen(int pathogenStage) {
        if (pathogenStage == 1)
        {
            return Stage1Pathogens[Random.Range(0, Stage1Pathogens.Count - 1)];
        }
        else if (pathogenStage == 2)
        {
            return Stage2Pathogens[Random.Range(0, Stage2Pathogens.Count - 1)];
        }
        else if (pathogenStage == 3)
        {
            return Stage2Pathogens[Random.Range(0, Stage2Pathogens.Count - 1)];
        }
        else {
            return Stage2Pathogens[Random.Range(0, Stage2Pathogens.Count - 1)];
        }
    }

    void HealDefenses(GameObject btn)
    {
        Destroy(btn.gameObject);
        int healAmount = 75;
        for (int x = 0; x < CurrentPathogen.ComponentDamages.Count; x++)
        {
            var cDamage = CurrentPathogen.ComponentDamages.ElementAt(x);
            Defense foundDefense = GetDefenseByName(cDamage.Key);
            foundDefense.Health = Mathf.Min(foundDefense.MaxHealth, foundDefense.Health+healAmount);
        }

        Immunopoints++;
    }

    void DamagePathogen(GameObject btn)
    {
        Destroy(btn.gameObject);
        CurrentPathogen.PathogenHealth = Mathf.Max(0, CurrentPathogen.PathogenHealth - Random.Range(40, 80));
        Immunopoints++;
    }

    IEnumerator DestroyAfter(GameObject obj, float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(obj);
    }

    IEnumerator NewWave(Pathogen ChosenPathogen, int waveLength = 300) {

        foreach (Defense d in InternalDefense) {
            d.IsUnderAttack = false;
        }
        foreach (Defense d in ExternalDefense)
        {
            d.IsUnderAttack = false;
        }

        uim.selectedDefense = ChosenPathogen.ComponentDamages.ElementAt(0).Key;

        if (CurrentWave < 20) {
            CurrentPathogen = ChosenPathogen;
            CurrentWave++;
            EndAt = Time.time + waveLength;
            isIntermission = false;

            //print(CurrentPathogen.PathogenName);

            for (int i = 0; i < waveLength; i++)
            {
                float tt = Time.time;
                yield return new WaitForSeconds(1);
                //print(Time.time - tt);
                for (int x = 0; x < CurrentPathogen.ComponentDamages.Count; x++) {
                    var cDamage = CurrentPathogen.ComponentDamages.ElementAt(x);
                    Defense foundDefense = GetDefenseByName(cDamage.Key);
                    foundDefense.Health -= cDamage.Value;
                    foundDefense.IsUnderAttack = true;

                    float Chance = Random.Range(0.0f, 1.0f);
                    if (Chance < .3f)
                    {
                        //dont spawn
                    }else if (Chance < .63f)
                    {
                        //spawn green bubble
                        GameObject bubble = (GameObject)Instantiate(Resources.Load("GreenHP"), bodyAreas.Find("Bottom"));
                        bubble.name = "greenBubble";
                        StartCoroutine(DestroyAfter(bubble, 3f));
                        bubble.GetComponent<Button>().onClick.AddListener(delegate { HealDefenses(bubble); });

                    }
                    else
                    {
                        //spawn yellow bubble
                        GameObject bubble = (GameObject)Instantiate(Resources.Load("YellowHP"), bodyAreas.Find("Bottom"));
                        bubble.name = "yellowBubble";
                        StartCoroutine(DestroyAfter(bubble, 1.5f));
                        bubble.GetComponent<Button>().onClick.AddListener(delegate { DamagePathogen(bubble); });
                    }

                    //print(foundDefense.DefenseName + ": " + foundDefense.Health);
                }

                if (CurrentPathogen.PathogenHealth <= 0) {
                    isIntermission = true;
                    StartCoroutine(uim.ShowSlain(CurrentPathogen.PathogenName));
                    CurrentPathogen.PathogenHealth = CurrentPathogen.PathogenMaxHealth;
                    break;
                }
            }
            
        }


        foreach (Defense d in InternalDefense)
        {
            d.IsUnderAttack = false;
        }
        foreach (Defense d in ExternalDefense)
        {
            d.IsUnderAttack = false;
        }

        isIntermission = true;
        EndAt = Time.time + 15;
        yield return new WaitForSeconds(15);

        //TODO CHANGE PATHOGEN STAGE DEPENDNG ON WAVE
        StartCoroutine(NewWave(ChooseRandomPathogen(1)));

    }

    public Defense GetDefenseByName(string DefenseName) {
        foreach (Defense d in InternalDefense) {
            if (d.DefenseName == DefenseName) {
                return d;
            }
        }

        foreach (Defense d in ExternalDefense)
        {
            if (d.DefenseName == DefenseName)
            {
                return d;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class Pathogen {
        public int PathogenHealth;
        public int PathogenMaxHealth;
        public string PathogenName;
        public Dictionary<string, int> ComponentDamages;
        public string PathogenType;
        public string PathogenInfo;
        public int YellowDamage;
        public int GreenHealth;

        public Pathogen(int pathogenHealth, string pathogenName, Dictionary<string, int> componentDamages, string pathogenType, string pathogenInfo, int yellowDamage, int greenHealth) {
            PathogenHealth = pathogenHealth;
            PathogenMaxHealth = pathogenHealth;
            PathogenName = pathogenName;
            ComponentDamages = componentDamages;
            PathogenType = pathogenType;
            PathogenInfo = pathogenInfo;
            YellowDamage = yellowDamage;
            GreenHealth = greenHealth;
        }

    }

    public class Upgrade {

        public string UpgradeName;
        public List<int> LevelPrices;
        public int UpgradeLevel;
        public string UpgradeDescription;

        public Upgrade(string upgradeName, List<int> levelPrices, int upgradeLevel = 0, string upgradeDescription = "No description") {
            UpgradeName = upgradeName;
            LevelPrices = levelPrices;
            UpgradeLevel = upgradeLevel;
            UpgradeDescription = upgradeDescription;
        }
    }

    public class Defense{

        public string DefenseName;
        public bool IsInternal;
        public int Health;
        public int MaxHealth;
        public List<Upgrade> Upgrades;
        public List<Upgrade> UnlockedUpgrades;
        public string DefenseDescription;
        public bool IsUnderAttack;

        public Defense(string defenseName, bool isInternal, int health, List<Upgrade> upgrades, List<Upgrade> unlockedUpgrades, string defenseDesc = "No description", bool isUnderAttack = false) {
            DefenseName = defenseName;

            IsInternal = isInternal;
            Health = health;
            MaxHealth = health;
            Upgrades = upgrades;
            UnlockedUpgrades = unlockedUpgrades;
            DefenseDescription = defenseDesc;
            IsUnderAttack = isUnderAttack;
        }
    }

}
