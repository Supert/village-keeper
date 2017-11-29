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
            text.text = CoreScript.Instance.Data.Gold.ToString();
        }

        void Awake()
        {
            text = GetComponent<Text>() as Text;
        }

        void Start()
        {
            CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged(e);
        }

        void OnDataFieldChanged(DataScript.DataFieldChangedEventArgs e)
        {
            if (e.FieldChanged == DataScript.DataFields.Gold)
                SetText();
        }
    }
}