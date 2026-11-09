namespace WeddingSeatingApp{
public class Table : WeddingComponent, IEnumerable<Family>
    {
        public int TableNumber { get; set; }
        private List<Family> seatedFamilies = new List<Family>();

        public Table(int tableNumber)
        {
            TableNumber = tableNumber;
            Name = $"Маса {tableNumber}";
        }

        public override void Add(WeddingComponent component)
        {
            if (component is Family family)
            {
                if (seatedFamilies.Count >= 2)
                    throw new InvalidOperationException("На една маса могат да сядат максимум 2 семейства!");

                if (GetCount() + family.GetCount() > 10)
                    throw new InvalidOperationException("На една маса могат да сядат максимум 10 души!");

                foreach (var existingFamily in seatedFamilies)
                {
                    if (existingFamily.ForbiddenFamilies.Contains(family.Name) ||
                        family.ForbiddenFamilies.Contains(existingFamily.Name))
                    {
                        throw new InvalidOperationException($"Семейство '{family.Name}' има забрана да сяда с '{existingFamily.Name}'!");
                    }
                }

                seatedFamilies.Add(family);
            }
        }

        public override void Remove(WeddingComponent component)
        {
            if (component is Family family)
            {
                seatedFamilies.Remove(family);
            }
        }

        public override int GetCount()
        {
            return seatedFamilies.Sum(f => f.GetCount());
        }

        public IEnumerator<Family> GetEnumerator()
        {
            foreach (var fam in seatedFamilies)
            {
                yield return fam;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}