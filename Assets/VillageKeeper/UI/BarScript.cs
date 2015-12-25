﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BarScript : MonoBehaviour {
	public Image barFillerImage;
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
			if (value >= MaxValue) {
				_currentValue = MaxValue;
				barFillerImage.fillAmount = 1f;
		}
			else if (value <= minValue){
				_currentValue = minValue;
				barFillerImage.fillAmount = 0f;
			}
			else if (MaxValue - minValue != 0) {
				_currentValue = value;
				barFillerImage.fillAmount = RelativeCurrentValue;
			}
		}
	}
	protected virtual void Awake () {
		barFillerImage.color = new Color (0, 0, 0, 0);
	}
	protected virtual void Start () {
		barFillerImage.color = new Color (1, 1, 1, 1);
	}
	protected virtual void Update () {
	}
}
