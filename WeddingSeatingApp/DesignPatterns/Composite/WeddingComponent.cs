#nullable disable

using WeddingSeatingApp;

namespace WeddingSeatingApp.DesignPatterns.Composite
{
    public abstract class WeddingComponent
    {
        public string Name { get; set; }
        public virtual void Add(WeddingComponent component) => throw new NotImplementedException();
        public virtual void Remove(WeddingComponent component) => throw new NotImplementedException();
        public virtual int GetCount() => 0;
    }
}