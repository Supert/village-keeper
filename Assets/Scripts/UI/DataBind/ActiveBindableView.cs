using UnityEngine;

namespace VillageKeeper.UI
{
    public class ActiveBindableView : BindableView
    {
        [SerializeField]
        protected bool isInversed;
        [SerializeField]
        protected string equalsTo;
        protected override void OnValueChanged()
        {
            gameObject.SetActive(equalsTo.Equals(Fields[0].GetValue().ToString()) != isInversed);
        }
    }
}