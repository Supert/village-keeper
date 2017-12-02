namespace VillageKeeper.UI
{
    public class SpecialImageDataBindView : ImageDataBindView
    {
        protected override string FullResourcePath
        {
            get
            {
                return resourcePath + "/" + CoreScript.Instance.TodaySpecial + " /" + GetValue();
            }
        }
    }
}