using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialouge
{
    public class Dialougue : MonoBehaviour
    {
        [TextArea(3, 6)]
        public string greeting;
        public string faction;
        public LineOfDialouge goodbye;
        public LineOfDialouge[] dialougeOptions;
    }
}