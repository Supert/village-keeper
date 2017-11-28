using UnityEngine;

namespace VillageKeeper.UI
{
    public class UiManager : MonoBehaviour
    {
        private CastleScript castle;

        public void OnBuildModeEntered()
        {
            castle.SetSprite();
        }
    }
}