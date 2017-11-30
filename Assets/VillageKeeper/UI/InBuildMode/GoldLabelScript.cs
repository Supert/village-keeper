using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Text))]
    public class GoldLabelScript : MonoBehaviour
    {
        private Text text;

        public void SetText()
        {
            text.text = CoreScript.Instance.Data.Gold.Get().ToString();
        }

        void Init()
        {
            text = GetComponent<Text>() as Text;
            CoreScript.Instance.Data.Gold.OnValueChanged += (i) => SetText();
        }
    }
}