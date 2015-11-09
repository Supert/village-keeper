using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BarScript : MonoBehaviour {
	public RectTransform barFillerRT;
	public float maxValue;
	public float minValue;
	private float _currentValue;
	public float RelativeCurrentValue {
		get {
			return (_currentValue - minValue) / (maxValue - minValue);
		}
		set {
			CurrentValue = value * (maxValue - minValue) + minValue;
		}
	}
	public float CurrentValue {
		get {
			return _currentValue;
		}
		set {
			var s = this.barFillerRT.localScale;
			if (value >= maxValue) {
				_currentValue = maxValue;
				s.x = 1f;
		}
			else if (value <= minValue){
				_currentValue = minValue;
				s.x = 0f;
			}
			else if (maxValue - minValue != 0) {
				_currentValue = value;
				s.x = RelativeCurrentValue;
			}
			this.barFillerRT.localScale = s;
		}
	}
}
