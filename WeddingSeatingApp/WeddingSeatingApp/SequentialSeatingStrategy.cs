namespace WeddingSeatingApp
{
    public class SequentialSeatingStrategy : ISeatingStrategy
    {
        public void SeatGuests(List<Family> families, List<Table> tables)
        {
            int tableIndex = 0;
            foreach (var family in families)
            {
                bool seated = false;
                while (tableIndex < tables.Count)
                {
                    try
                    {
                        tables[tableIndex].Add(family);
                        seated = true;
                        break;
                    }
                    catch
                    {
                        tableIndex++;
                    }
                }
                if (!seated)
                {
                    throw new Exception($"There is not enough free tables for {family.Name} family!");
                }
            }
        }
    }
}