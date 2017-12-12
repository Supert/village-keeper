namespace VillageKeeper.Data
{
    public abstract class SerializableArray<T>
    {
        protected abstract T[] Values { get; }
        public T this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }
    }
}