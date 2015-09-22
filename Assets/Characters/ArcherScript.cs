using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class ArcherScript : MonoBehaviour
{
	public bool IsLoaded {
		get;
		private set;
	}

	private Image image;
	public Sprite archerUnloadedSprite;
	public Sprite archerReadyAimingUpSprite;
	public Sprite archerReadyAimingStraightSprite;
	public Sprite archerReadyAimingDownSprite;
	// Use this for initialization
	void Start ()
	{
		this.IsLoaded = true;
		this.image = GetComponent<Image> () as Image;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (IsLoaded) {
			if (CoreScript.Instance.Monster.Sector == MonsterScript.SectorValues.Close)
				this.image.sprite = archerReadyAimingDownSprite;
			else {
				if (CoreScript.Instance.Monster.Sector == MonsterScript.SectorValues.Middle)
					this.image.sprite = archerReadyAimingStraightSprite;
				else
					this.image.sprite = archerReadyAimingUpSprite;
			}
		} else
			this.image.sprite = archerUnloadedSprite;
	}
}
