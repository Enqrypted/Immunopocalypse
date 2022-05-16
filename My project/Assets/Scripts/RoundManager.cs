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
    public Upgrade selectedUpgrade;

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
            //TODO
            new Defense("Tonsils", false, 1000, new List<Upgrade>{
                new Upgrade("Filter microbes", new List<int> { 300, 150}, 0, "Filters microbes entering through the nose and mouth."),
                new Upgrade("Activate antibodies", new List<int> { 300, 150}, 0, "Proteins called antibodies are produced by white blood cells in the tonsils to destroy microbes."),
                new Upgrade("Bacterial antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy bacteria better."),
                new Upgrade("Fungal antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy fungi better."),
                new Upgrade("Viral antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to taget and destroy viruses better."),
                new Upgrade("Heal the tonsils", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "Tonsils help trap microbes that enter through the mouth and nose. They have specialised cells that produce antibodies to destroy microbes."),
            new Defense("Trachea & Lungs", false, 1000, new List<Upgrade>{
                new Upgrade("More mucus", new List<int> { 300, 150}, 0, "Mucus layer traps microbes preventing them from reaching the lungs."),
                new Upgrade("Strenghten cilia", new List<int> { 300, 150}, 0, "Tiny hair-like projections in tracheal cells beat over 1000 times a minute to push out mucus with any trapped microbes and dirt."),
                new Upgrade("Produce phagocytes", new List<int> { 300, 150}, 0, "White blood cells that travel in the blood of the alveoli and engulf any microbes they come accross."),
                new Upgrade("Activate antibodies", new List<int> { 300, 150}, 0, "Proteins called antibodies are produced by white blood cells in the lungs to destroy microbes."),
                new Upgrade("Bacterial antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy bacteria better."),
                new Upgrade("Fungal antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy fungi better."),
                new Upgrade("Viral antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to taget and destroy viruses better."),
                new Upgrade("Heal the trachea & lungs", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "Trachea and lungs produce a mucus to trap microbes. White blood cells engulf remaining microbes and produce antibodies."),
            new Defense("Stomach & Gut", false, 1000, new List<Upgrade>{
                new Upgrade("Acidic discharge", new List<int> { 300, 150}, 0, "Produce hydrochloric acid to destroy microbes and provide stomach enzymes with optimal pH for them to function."),
                new Upgrade("Reinforce enzymes", new List<int> { 300, 150}, 0, "Enzymes are produced to digest food and kill bacteria and fungi by destroying their cell wall in the gut"),
                new Upgrade("More mucus", new List<int> { 300, 150}, 0, "Mucus layer traps microbes in the intestines and also protects the stomach walls."),
                new Upgrade("Activate antibodies", new List<int> { 300, 150}, 0, "Proteins called antibodies are produced by white blood cells in the lungs to destroy microbes."),
                new Upgrade("Bacterial antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy bacteria better."),
                new Upgrade("Fungal antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy fungi better."),
                new Upgrade("Viral antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to taget and destroy viruses better."),
                new Upgrade("Heal the stomach and gut", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The stomach produces strong acids that destroy microbes. White blood cells in the gut (intestines) also produce antibodies that fight off microbes."),
            new Defense("Bone marrow", false, 1000, new List<Upgrade>{
                new Upgrade("Increase red blood cells", new List<int> { 300, 150}, 0, "Boost the production of red blood cells to transport oxygen to body cells."),
                new Upgrade("Increase platelets", new List<int> { 300, 150}, 0, "Boost the production of platelets to clot blood and heal injuries. "),
                new Upgrade("Increase white blood cells", new List<int> { 300, 150}, 0, "Boost the production of white blood cells to fight microbes."),
                new Upgrade("Mature B-cells", new List<int> { 300, 150}, 0, "Allow B-cells to grow and mature. B-cells are specialised white blood cells that produce antibodies."),
                new Upgrade("Regenerate blood cells", new List<int> { 300, 150}, 0, "Replace damaged cells to keep the blood healthy."),
                new Upgrade("Heal the bone marrow", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The bone marrow is responsible for the production of red blood cells, white blood cells and platelets."),
            new Defense("Blood", false, 1000, new List<Upgrade>{
                new Upgrade("Produce neutrophils", new List<int> { 300, 150}, 0, "Increase production of neutrophils which engulf and destroy microbes."),
                new Upgrade("Bacterial neutrophils", new List<int> { 300, 150}, 0, "Neutrophils are able to detect and destroy bacteria more efficiently."),
                new Upgrade("Fungal neutrophils", new List<int> { 300, 150}, 0, "Neutrophils are able to detect and destroy fungi more efficiently."),
                new Upgrade("Viral neutrophils", new List<int> { 300, 150}, 0, "Neutrophils are able to detect and destroy viruses more efficiently."),
                new Upgrade("Activate antibodies", new List<int> { 300, 150}, 0, "Proteins called antibodies are produced by white blood cells in the lungs to destroy microbes."),
                new Upgrade("Bacterial antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy bacteria better."),
                new Upgrade("Fungal antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to target and destroy fungi better."),
                new Upgrade("Viral antibodies", new List<int> { 300, 150}, 0, "Antibodies are able to taget and destroy viruses better."),
                new Upgrade("Neutralise toxins", new List<int> { 300, 150}, 0, "B-cells produce antitoxins to neutralise toxins secreted by microbes."),
                new Upgrade("Produce helper T-cells", new List<int> { 300, 150}, 0, "Helper T-cells stimulate B-cells to produce more antibodies."),
                new Upgrade("Produce killer T-cells", new List<int> { 300, 150}, 0, "Killer T-cells destroy our own body cells when they become infected. This slows down the spread of the disease."),
                new Upgrade("Produce memory T-cells", new List<int> { 300, 150}, 0, "Memory T-cells are able to remember how to combat microbes that have already attacked the body. When a familiar microbe enters the body they call on B-cells to produce antibodies immediately to combat the microbe."),
                new Upgrade("Produce B-cells", new List<int> { 300, 150}, 0, "Increase production of white blood cells called B-cells which produce antibodies."),
                new Upgrade("Heal the blood", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The blood is the most important part of the immune system. White blood cells fight off microbes by engulfing them, producing antibodies and antitoxins, and also destroying infected human cells."),
            new Defense("Brain", false, 1000, new List<Upgrade>{
                new Upgrade("Heal the brain", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The brain is not part of the immune system. It controls thought, memory, emotion and movement."),
            new Defense("Kidneys & bladder", false, 1000, new List<Upgrade>{
                new Upgrade("Heal the kidneys & bladder", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The kidneys and bladder are not a part of the immune system. Kidneys filter blood to remove waste and transport it and excess water to the bladder."),
            new Defense("Reproductive organs", false, 1000, new List<Upgrade>{
                new Upgrade("Heal the reproductive organs", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The reproductive organs are not part of the immune system. They differ between males and females and produce sex cells for reproduction."),
        };

        ExternalDefense = new List<Defense> {
            //TODO
            new Defense("Eye", false, 1000, new List<Upgrade>{
                new Upgrade("Increase tears", new List<int> { 300, 150}, 0, "Supply the surface of the eye with oxygen and nutrients (since there aren't any blood vessels present)"),
                new Upgrade("Reinforce enzymes", new List<int> { 300, 150}, 0, "The enzyme lysozyme is produced which kills bacteria and fungi by destroying their cell wall"),
                new Upgrade("More mucus", new List<int> { 300, 150}, 0, "Prevents dryness by lubricating and coating the surface of the eye, and traps specks of dirt."),
                new Upgrade("Boost cornea", new List<int> { 300, 150}, 0, "Tears help to heal the cornea of the eye"),
                new Upgrade("Heal the eye", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The eyes produce tears to flush out microbes and foreign objects."),
            new Defense("Nose", false, 1000, new List<Upgrade>{
                new Upgrade("More mucus", new List<int> { 300, 150}, 0, "Traps microbes and prevents them from entering the respiratory system"),
                new Upgrade("Reinforce enzymes", new List<int> { 300, 150}, 0, "The enzyme lysozyme is produced which kills bacteria and fungi by destroying their cell wall"),
                new Upgrade("Harden hairs", new List<int> { 300, 150}, 0, "Filtering airborne microbes as air enters the nose"),
                new Upgrade("Heal the nose", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The nose traps microbes with hairs and secretes mucus."),
            new Defense("Mouth", false, 1000, new List<Upgrade>{
                new Upgrade("Secrete saliva", new List<int> { 300, 150}, 0, "Saliva washes away food in the mouth which are breeding gounds for microbes"),
                new Upgrade("Reinforce enzymes", new List<int> { 300, 150}, 0, "The enzyme lysozyme is produced which kills bacteria and fungi by destroying their cell wall"),
                new Upgrade("Activate antibodies", new List<int> { 300, 150}, 0, "Antibodies called immunoglobulins are produced by white blood cells in the mouth to fight off microbes"),
                new Upgrade("More mucus", new List<int> { 300, 150}, 0, "Mucus is produced to trap microbes"),
                new Upgrade("Heal the mouth", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "The mouth contains many different microbes and produces saliva containing enzymes and antibodies to destroy them."),
            new Defense("Skin", false, 1000, new List<Upgrade>{
                new Upgrade("Thick skin", new List<int> { 300, 150}, 0, "Acts as a physical barrier to prevent the entry of microbes"),
                new Upgrade("Iron skin", new List<int> { 300, 150}, 0, "Improved physical barrier for a short period of time"),
                new Upgrade("Secrete sweat", new List<int> { 300, 150}, 0, "Sweat acts as a natural antibiotic, destroying bacteria"),
                new Upgrade("Acid mantle", new List<int> { 300, 150}, 0, "Covers the skin in a low pH to prevent microbes from growing on it"),
                new Upgrade("Regulate temperature", new List<int> { 300, 150}, 0, "The skin increases the temperature by increasing the blood supply."),
                new Upgrade("Heal the skin", new List<int> { 150}, 0, "Use IP to heal component")
            }, new List<Upgrade>{ }, "Physical barrier that blocks microbes from entering the body"),

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

    int CalcHealAmount()
    {
        //75
        int heal = 75;

        return heal;

    }

    int CalcDamageAmount()
    {
        //40-80
        int dmg = Random.Range(40, 80);

        return dmg;
    }

    void HealDefenses(GameObject btn)
    {
        Destroy(btn.gameObject);
        int healAmount = CalcHealAmount();
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
        CurrentPathogen.PathogenHealth = Mathf.Max(0, CurrentPathogen.PathogenHealth-CalcDamageAmount());
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
                    foundDefense.Health = Mathf.Max(0, foundDefense.Health - cDamage.Value);
                    foundDefense.IsUnderAttack = true;


                    //SPAWN BUBBLES
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
