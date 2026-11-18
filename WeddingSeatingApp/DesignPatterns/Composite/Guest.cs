#nullable disable
using WeddingSeatingApp;

namespace WeddingSeatingApp.DesignPatterns.Composite
{
 public class Guest : WeddingComponent
    {
        public string FamilyName { get; set; }

        public Guest(string name, string familyName)
        {
            Name = name;
            FamilyName = familyName;
        }

        public override int GetCount() => 1;
    }
}