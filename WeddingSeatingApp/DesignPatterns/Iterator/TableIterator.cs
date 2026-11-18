
using System.Collections.Generic;
using WeddingSeatingApp;
using WeddingSeatingApp.DesignPatterns.Composite;

namespace WeddingSeatingApp.DesignPatterns.Iterator
{
    public static class TableIterator
    {
        public static IEnumerator<Family> Iterate(
            IEnumerable<Family> families)
        {
            foreach (var family in families)
                yield return family;
        }
    }
}
