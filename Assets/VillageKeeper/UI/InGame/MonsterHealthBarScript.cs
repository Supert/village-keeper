using System;

namespace VillageKeeper.UI
{
    public class MonsterHealthBarScript : BarScript
    {
        private OffScreenMenuScript offscreenMenu;

        protected override void Awake()
        {
            base.Awake();
            offscreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
        }
        
        public void Show()
        {
            offscreenMenu.Show();
            MaxValue = CoreScript.Instance.Monster.maxHealth;
            minValue = 0;
        }

        public void Hide()
        {
            offscreenMenu.Hide();
        }

        protected void Update()
        {
            MaxValue = CoreScript.Instance.Monster.maxHealth;
            CurrentValue = CoreScript.Instance.Monster.Health;
        }
    }
}