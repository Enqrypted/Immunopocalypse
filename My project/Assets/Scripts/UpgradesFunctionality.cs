using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesFunctionality
{
    public Dictionary<string, int> DamageToDefense_Upgrades = new Dictionary<string, int>();
    public Dictionary<string, int> HealDefense_Upgrades = new Dictionary<string, int>();
    public Dictionary<string, int> DamagePathogen_Upgrades = new Dictionary<string, int>();

    public int ProcessDamageDefenseUpgrades(int currentVal, String upg, int upgLevel = 1) {
        if (DamageToDefense_Upgrades.ContainsKey(upg))
        {
            return Mathf.RoundToInt((float)currentVal * (1f - ((DamageToDefense_Upgrades[upg] * upgLevel) / 100f)));
        }
        else {
            if (DamagePathogen_Upgrades.ContainsKey(upg) == false && HealDefense_Upgrades.ContainsKey(upg) == false)
            {
                Debug.Log("WARNING : " + upg + " does not exist as upgrade functionality");
            }
            return currentVal;
        }
    }

    public int ProcessHealDefenseUpgrades(int currentVal, String upg, int upgLevel = 1) {
        if (HealDefense_Upgrades.ContainsKey(upg))
        {
            return Mathf.RoundToInt((float)currentVal * (1f + ((HealDefense_Upgrades[upg] * upgLevel) / 100f)));
        }
        else
        {
            if (DamageToDefense_Upgrades.ContainsKey(upg) == false && DamagePathogen_Upgrades.ContainsKey(upg) == false)
            {
                Debug.Log("WARNING : " + upg + " does not exist as upgrade functionality");
            }
            return currentVal;
        }
    }

    public int ProcessDamagePathogenUpgrades(int currentVal, String upg, int upgLevel = 1) {
        if (DamagePathogen_Upgrades.ContainsKey(upg))
        {
            return Mathf.RoundToInt((float)currentVal * (1f + ((DamagePathogen_Upgrades[upg] * upgLevel) / 100f)));
        }
        else
        {
            if (DamageToDefense_Upgrades.ContainsKey(upg) == false && HealDefense_Upgrades.ContainsKey(upg) == false)
            {
                Debug.Log("WARNING : " + upg + " does not exist as upgrade functionality");
            }
            return currentVal;
        }
    }

    public UpgradesFunctionality()
    {
        //Reduce damage to defense
        DamageToDefense_Upgrades["Thick skin"] = 10;
        DamageToDefense_Upgrades["Iron skin"] = 15;
        DamageToDefense_Upgrades["Increase tears"] = 10;
        DamageToDefense_Upgrades["More mucus"] = 10;
        DamageToDefense_Upgrades["Harden hairs"] = 15;
        DamageToDefense_Upgrades["Acid mantle"] = 20;
        DamageToDefense_Upgrades["Filter microbes"] = 15;
        DamageToDefense_Upgrades["Mature B-cells"] = 15;
        DamageToDefense_Upgrades["Produce B-cells"] = 15;
        DamageToDefense_Upgrades["Neutralise toxins"] = 20;
        DamageToDefense_Upgrades["Produce helper T-cells"] = 15;
        DamageToDefense_Upgrades["Acidic discharge"] = 15;

        //Increase pathogen damage
        DamagePathogen_Upgrades["Reinforce enzymes"] = 15;
        DamagePathogen_Upgrades["Activate antibodies"] = 20;
        DamagePathogen_Upgrades["Secrete sweat"] = 20;
        DamagePathogen_Upgrades["Bacterial antibodies"] = 20;
        DamagePathogen_Upgrades["Fungal antibodies"] = 20;
        DamagePathogen_Upgrades["Viral antibodies"] = 20;
        DamagePathogen_Upgrades["Increase white blood cells"] = 10;
        DamagePathogen_Upgrades["Produce killer T-cells"] = 15;
        
        


        //Increase defense healing
        HealDefense_Upgrades["Boost cornea"] = 25;
        HealDefense_Upgrades["Secrete saliva"] = 15;
        HealDefense_Upgrades["Regulate temperature"] = 20;
        HealDefense_Upgrades["Strengthen cilia"] = 20;
        HealDefense_Upgrades["Produce phagocytes"] = 20;
        HealDefense_Upgrades["Increase red blood cells"] = 10;
        HealDefense_Upgrades["Increase platelets"] = 10;
        HealDefense_Upgrades["Regenerate blood cells"] = 10;
        HealDefense_Upgrades["Produce neutrophils"] = 25;
        HealDefense_Upgrades["Bacterial neutrophils"] = 20;
        HealDefense_Upgrades["Fungal neutrophils"] = 20;
        HealDefense_Upgrades["Viral neutrophils"] = 20;
        HealDefense_Upgrades["Produce memory T-cells"] = 25;

    }
}
