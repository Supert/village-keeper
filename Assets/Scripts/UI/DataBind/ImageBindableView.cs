﻿using Shibari.UI;
using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageBindableView : BindableView
    {
        private Image image;
        
        protected void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override void OnValueChanged()
        {
            image.sprite = (Sprite) Fields[0].GetValue();
        }
    }
}