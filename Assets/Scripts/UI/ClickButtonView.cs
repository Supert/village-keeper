using Shibari.UI;

namespace VillageKeeper.UI
{
    public class ClickButtonView : ButtonView
    {
        protected override void Invoke()
        {
            base.Invoke();
            Core.Instance.AudioManager.PlayClick();
        }
    }
}