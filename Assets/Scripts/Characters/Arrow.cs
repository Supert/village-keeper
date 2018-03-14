using UnityEngine;

namespace VillageKeeper.Game
{
    public abstract class Arrow : Projectile
    {
        protected RectTransform Shadow { get; private set; }
        
        protected virtual void Awake()
        {
            Shadow = transform.Find("Shadow") as RectTransform;

        }
    }
}