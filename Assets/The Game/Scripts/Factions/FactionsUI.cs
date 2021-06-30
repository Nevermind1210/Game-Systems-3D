using System;
using UnityEngine;
using UnityEngine.UI;

namespace Faction
{
    public class FactionsUI : MonoBehaviour
    {
        [SerializeField] private Text minuteMenApprovalText;
        [SerializeField] private Text vibesMenApprovalText;
        private float minuteMenApproval;
        private float vibesApproval;

        private void Update()
        {
            minuteMenApproval = (float) FactionsManager.instance.FactionsApproval("Minute Men");
            minuteMenApprovalText.text = "Minute Men Faction Approval: " + minuteMenApproval.ToString();
            
            vibesApproval = (float) FactionsManager.instance.FactionsApproval("Vibes");
            vibesMenApprovalText.text = "Vibes Faction Approval: " + vibesApproval.ToString();
        }
    }
}