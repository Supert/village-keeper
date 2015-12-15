using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {
	public Sprite inBattleSprite;
	public Sprite inBattlePressedSprite;
	public Sprite inBuildModeSprite;
	public Sprite inBuildModePressedSprite;
	private Button _button;
	// Use this for initialization
	void Start () {
		var _button = GetComponent<Button> () as Button;
		_button.onClick.AddListener (() => {
			switch (CoreScript.Instance.GameState) {
			case CoreScript.GameStates.InBattle:
				CoreScript.Instance.GameState = CoreScript.GameStates.Paused;
				break;
			case CoreScript.GameStates.InBuildMode:
				CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
				break;
			}
		});
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			var s = _button.spriteState;
			switch (e.NewState) {
			case CoreScript.GameStates.InBattle:
				_button.image.sprite = inBattleSprite;
				s.pressedSprite = inBattlePressedSprite;
				_button.spriteState = s;
				break;
			case CoreScript.GameStates.InBuildMode:
				_button.image.sprite = inBuildModeSprite;
				s = _button.spriteState;
				s.pressedSprite = inBuildModePressedSprite;
				_button.spriteState = s;
				break;
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
