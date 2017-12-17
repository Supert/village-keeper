namespace VillageKeeper.Data
{
    public abstract class SerializableArray<T>
    {
        public abstract T[] Values { get; set; }
        public T this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }
    }
}