using UnityEngine;

namespace Dialouge
{
    [System.Serializable]
    public class LineOfDialouge
    {
        [TextArea(3, 6)]
        public string topic, response;
        public Dialougue nextDialogue;

        public float minApproval = -1f;
        public float changeApproval = 0f;
    }
}