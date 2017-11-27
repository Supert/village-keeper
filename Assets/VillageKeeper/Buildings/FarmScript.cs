using UnityEngine;

public class FarmScript : BuildingScript
{
    public override BuildingTypes Type
    {
        get
        {
            return BuildingTypes.Farm;
        }
    }

    public SpriteRenderer cropsSR;
    public Sprite winterSpecialCropsSprite;

    protected void Start()
    {
        if (CoreScript.Instance.TodaySpecial == CoreScript.Specials.Winter)
            cropsSR.sprite = winterSpecialCropsSprite;
    }

}