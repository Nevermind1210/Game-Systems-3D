using UnityEngine;

namespace Quests
{
    public class FetchQuest : MonoBehaviour
    {
        public bool gotItem = false;

        public bool CheckQuestCompletion()
        {
            if (gotItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}