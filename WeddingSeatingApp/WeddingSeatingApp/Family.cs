namespace WeddingSeatingApp
{
     public class Family : WeddingComponent, IEnumerable<Guest>
    {
        private List<Guest> members = new List<Guest>();
        public HashSet<string> ForbiddenFamilies { get; set; } = new HashSet<string>();

        public Family(string familyName)
        {
            Name = familyName;
        }

        public override void Add(WeddingComponent component)
        {
            if (component is Guest guest)
            {
                guest.FamilyName = this.Name;
                members.Add(guest);
            }
        }

        public override void Remove(WeddingComponent component)
        {
            if (component is Guest guest)
            {
                members.Remove(guest);
            }
        }

        public override int GetCount() => members.Count;

        public void AddForbiddenFamily(string familyName)
        {
            ForbiddenFamilies.Add(familyName);
        }

        public IEnumerator<Guest> GetEnumerator()
        {
            foreach (var member in members)
            {
                yield return member;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}