using WeddingSeatingApp.DesignPatterns.Composite;

namespace WeddingSeatingApp.DesignPatterns.Strategy
{
    public interface ISeatingStrategy
    {
        void SeatGuests(List<Family> families, List<Table> tables);
    }
}