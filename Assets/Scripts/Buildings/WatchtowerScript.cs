using UnityEngine;
using System.Collections;
using VillageKeeper.Data;

namespace VillageKeeper.Game
{
    public class WatchtowerScript : Building
    {
        private bool isLoaded = true;
        public void Shoot()
        {
            if (isLoaded)
            {
                var targetPosition = Core.Instance.Monster.transform.localPosition;
                var arrow = new GameObject("arrow", typeof(ArrowScript)).GetComponent<ArrowScript>();
                var initialPosition = (Vector2)transform.position;
                var vectorToCalcAngle = (Vector2)targetPosition + new Vector2(0, Core.Instance.BuildingsArea.CellWorldSize.y) - (Vector2)initialPosition;
                var angle = Mathf.Atan2(vectorToCalcAngle.y, vectorToCalcAngle.x);
                arrow.Init(initialPosition, targetPosition, angle);
                isLoaded = false;
                StartCoroutine(ReloadCoroutine());
            }
        }

        private IEnumerator ReloadCoroutine()
        {
            switch (type)
            {
                case BuildingTypes.WatchtowerWooden:
                    yield return new WaitForSeconds(3f);
                    break;
                case BuildingTypes.WatchtowerStone:
                    yield return new WaitForSeconds(1.5f);
                    break;
                default:
                    throw new System.Exception($"Watchtower has building type {type}");
            }

            isLoaded = true;
        }

        protected override void Update()
        {
            base.Update();
            if (Core.Instance.FSM.Current == FSM.States.Battle
                && isLoaded
                && Vector2.Distance(transform.position, Core.Instance.Monster.transform.localPosition) < Core.Instance.BuildingsArea.CellWorldSize.x * 4)
            {
                Shoot();
            }
        }
    }
}