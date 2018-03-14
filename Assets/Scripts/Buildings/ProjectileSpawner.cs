using UnityEngine;
using Shibari.UI;

namespace VillageKeeper.Game
{
    public class ProjectileSpawner : BindableView
    {
        private static BindableValueRestraint[] bindableValueRestraints = new BindableValueRestraint[1] { new BindableValueRestraint(typeof(Projectile)) };
        public override BindableValueRestraint[] BindableValueRestraints { get { return bindableValueRestraints; } }

        public void Shoot(Vector2 target, float angle)
        {
            var arrow = Instantiate(BindedValues[0].GetValue() as Projectile, transform);
            var initialPosition = (Vector2)transform.position;
            arrow.Initialize(initialPosition, target, angle);
        }

        protected override void OnValueChanged()
        {

        }
    }
}