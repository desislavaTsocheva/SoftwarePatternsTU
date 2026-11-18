
using System;
using System.Collections.Generic;
using System.Linq;
using WeddingSeatingApp.DesignPatterns.Composite;
using WeddingSeatingApp.DesignPatterns.Strategy;

namespace WeddingSeatingApp
{
    public class SequentialSeatingStrategy : ISeatingStrategy
    {
        public void SeatGuests(
            List<Family> families,
            List<Table> tables)
        {
            foreach (var table in tables)
            {
                foreach (var family in table.ToList())
                    table.Remove(family);
            }

            if (families.Any(f => f.GetCount() > 10))
            {
                throw new InvalidOperationException(
                    "A family exceeds the maximum table capacity of 10 guests.");
            }

            if (!AssignFamily(0, families, tables))
            {
                throw new InvalidOperationException(
                    "Unable to seat all families with the current tables and restrictions. Add more tables or change the restrictions.");
            }
        }

        private bool AssignFamily(
            int index,
            List<Family> families,
            List<Table> tables)
        {
            if (index >= families.Count)
                return true;

            Family family = families[index];

            foreach (Table table in tables)
            {
                bool added = false;

                try
                {
                    table.Add(family);
                    added = true;
                }
                catch (InvalidOperationException)
                {
                    continue;
                }

                if (added)
                {
                    if (AssignFamily(
                        index + 1, families, tables))
                    {
                        return true;
                    }

                    table.Remove(family);
                }
            }

            return false;
        }
    }
}
