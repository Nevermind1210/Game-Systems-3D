using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class KillQuest : Quest
    {
        public int requiredKills;
        public int killCount;
        // Some enemy type 

        public override bool CheckQuestCompletion()
        {
            return killCount == requiredKills;
        }
    }
}

