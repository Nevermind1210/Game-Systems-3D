using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Faction
{
    public class FactionsManager : MonoBehaviour
    {
        Dictionary<string, Factions> factions;
        [SerializeField] List<Factions> initialiseFactions;
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
            foreach (Factions faction in initialiseFactions)
            {
                factions.Add(faction.factionName, faction);
            }
        }

        // float ? makes it nulliable variable
        public float? FactionsApproval(string factionName, float value)
        {
            if(factions.ContainsKey(factionName))
            {
                factions[factionName].approval += value;
                return factions[factionName].approval;
            }
            return null;
        }

        public float? FactionsApproval(string factionName)
        {
            if (factions.ContainsKey(factionName))
            {
                return factions[factionName].approval;
            }
            return null;
        }
    }
}