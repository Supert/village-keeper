﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageBindableView : ResourceBindableView<Sprite>
    {
        private Image image;

        public override Sprite GetResource()
        {
            return ResourceMock.GetSprite(FullResourcePath);
        }

        protected void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override void OnValueChanged()
        {
            image.sprite = GetResource();
        }
    }
}