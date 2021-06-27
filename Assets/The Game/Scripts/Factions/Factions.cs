using UnityEngine;

namespace Faction
{
    [System.Serializable]
    public class Factions
    {
        public string factionName;
        [SerializeField,Range(-1,1)] float _approval;

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

        public Factions(float initialApproval)
        {
            approval = initialApproval;
        }
    }
}