using VillageKeeper.Data;

namespace VillageKeeper.Balance
{
    public class BalanceField<T> : DataField<T>
    {
        protected override T GetDefaultValue()
        {
            return (default(T));
        }
    }
}