using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BarScript : MonoBehaviour {
	public RectTransform barFillerRT;
	private float _maxValue = 1f;
	public float MaxValue {
		get {
			return _maxValue;
		}
		set {
			_maxValue = value;
			this.CurrentValue = CurrentValue;
		}
	}
	public float minValue;
	private float _currentValue;
	public float RelativeCurrentValue {
		get {
			return (_currentValue - minValue) / (MaxValue - minValue);
		}
		set {
			CurrentValue = value * (MaxValue - minValue) + minValue;
		}
	}
	public float CurrentValue {
		get {
			return _currentValue;
		}
		set {
			var s = this.barFillerRT.localScale;
			if (value >= MaxValue) {
				_currentValue = MaxValue;
				s.x = 1f;
		}
			else if (value <= minValue){
				_currentValue = minValue;
				s.x = 0f;
			}
			else if (MaxValue - minValue != 0) {
				_currentValue = value;
				s.x = RelativeCurrentValue;
			}
			this.barFillerRT.localScale = s;
		}
	}
}
