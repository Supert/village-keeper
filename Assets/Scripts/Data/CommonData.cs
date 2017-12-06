using System;
using Shibari;
using VillageKeeper.Balance;
using Shibari;

namespace VillageKeeper.Data
{
    public class CommonData : IBindableData
    {
        public enum Specials
        {
            None,
            Winter,
        }

        public BindableField<Specials> Special { get; private set; }

        public BindableField<int> MonsterBonusGold { get; private set; }
        public BindableField<int> MaxVillageLevel { get; private set; }

        public BindableField<float> Wind { get; private set; }

        public void Init(string prefix)
        {
            Special.Set(GetTodaySpecial());

            MonsterBonusGold.Set(BalanceData.MonsterBonusGold);
            MaxVillageLevel.Set(BalanceData.MaxVillageLevel);
        }

        private Specials GetTodaySpecial()
        {
            var winterSpecialBeginning = new DateTime(2012, 12, 24); //December 24th
            var winterSpecialEnd = new DateTime(2012, 1, 14); // January 14th
            if (CheckIfDateIsInPeriodOfYear(DateTime.Today, winterSpecialBeginning, winterSpecialEnd))
                return Specials.Winter;
            else
                return Specials.None;
        }

        //Day and Month
        private bool CheckIfDateIsInPeriodOfYear(DateTime date, DateTime beginning, DateTime end)
        {
            var testDate = new DateTime(2012, date.Month, date.Day);
            var testBeginning = new DateTime(2012, beginning.Month, beginning.Day);
            var testEnd = new DateTime(2012, end.Month, end.Day);
            if (testBeginning > testEnd)
                testEnd = testEnd.AddYears(1);
            if (testDate >= testBeginning && testDate <= testEnd)
                return true;
            testDate = testDate.AddYears(1);
            if (testDate >= testBeginning && testDate <= testEnd)
                return true;
            return false;
        }
    }
}