namespace WeddingSeatingApp
{
    public interface ISeatingStrategy
    {
        void SeatGuests(List<Family> families, List<Table> tables);
    }
}