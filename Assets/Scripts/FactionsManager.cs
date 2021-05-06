using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factions
{
    float _approval;

    public float approval
    {
        set
        {
            _approval = Mathf.Clamp(value, -1, 1);
        }
        get
        {
            return _approval;
        }
    } 
}

public class FactionsManager : MonoBehaviour
{
    Dictionary<string, Factions> factions;

    public static FactionsManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        factions = new Dictionary<string, Factions>();
        factions.Add("Clan", new Factions());
    }

    // float ? makes it nulliable variable
    float? factionsApproval(string factionName, float value)
    {
        if(factions.ContainsKey(factionName))
        {
            factions[factionName].approval += value;
            return factions[factionName].approval;
        }
        return null;
    }

    float? getFactionsApproval(string factionName)
    {
        if (factions.ContainsKey(factionName))
        {
            return factions[factionName].approval;
        }
        return null;
    }
}
