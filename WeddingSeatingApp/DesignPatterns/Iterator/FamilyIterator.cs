
using System.Collections.Generic;
using WeddingSeatingApp;
using WeddingSeatingApp.DesignPatterns.Composite;

namespace WeddingSeatingApp.DesignPatterns.Iterator
{
    public static class FamilyIterator
    {
        public static IEnumerator<Guest> Iterate(
            IEnumerable<Guest> members)
        {
            foreach (var member in members)
                yield return member;
        }
    }
}
