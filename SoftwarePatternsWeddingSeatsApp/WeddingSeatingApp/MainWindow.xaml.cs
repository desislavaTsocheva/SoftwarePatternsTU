using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeddingSeatingApp;

public partial class MainWindow : Window
{
public interface IGuestComponent
        {
            string Name { get; }
            int Count { get; }
        }

        public class Guest : IGuestComponent
        {
            public string Name { get; }
            public Guest(string name) => Name = name;
            public int Count => 1;
        }

        public class Family : IGuestComponent
        {
            public string Name { get; }
            private readonly List<IGuestComponent> _members = new List<IGuestComponent>();

            public Family(string familyName) => Name = familyName;

            public void AddMember(IGuestComponent component) => _members.Add(component);

            public int Count => _members.Sum(m => m.Count);
            public IReadOnlyList<IGuestComponent> Members => _members;
        }

        // --- DOMAIN MODEL ---
        public class WeddingTable
        {
            public int TableNumber { get; }
            public List<Family> SeatedFamilies { get; } = new List<Family>();
            public int TotalGuests => SeatedFamilies.Sum(f => f.Count);

            public WeddingTable(int tableNumber) => TableNumber = tableNumber;

            public bool CanSeat(Family family, HashSet<string> conflicts)
            {
                if (SeatedFamilies.Count >= 2) return false;
                if (TotalGuests + family.Count > 10) return false;

                // Проверка за забранени комбинации (конфликти)
                foreach (var seated in SeatedFamilies)
                {
                    if (conflicts.Contains($"{family.Name}-{seated.Name}") ||
                        conflicts.Contains($"{seated.Name}-{family.Name}"))
                    {
                        return false;
                    }
                }
                return true;
            }

            public void SeatFamily(Family family) => SeatedFamilies.Add(family);
        }

        // --- STATE ---
        private readonly List<Family> _families = new List<Family>();
        private readonly HashSet<string> _conflicts = new HashSet<string>();
        private readonly List<WeddingTable> _tables = new List<WeddingTable>();

        // UI Controls
        private TextBox txtFamilyName, txtMembers, txtFam1, txtFam2;
        private TextBox txtOutput;

        public MainWindow()
        {
            Title = "Wedding Seating Organizer (C# / WPF) - Отличен (6)";
            Width = 750;
            Height = 650;

            BuildUI();
        }

        private void BuildUI()
        {
            StackPanel root = new StackPanel { Margin = new Thickness(15) };

            // Добавяне на семейство
            root.Children.Add(new TextBlock { Text = "Добавяне на семейство:", FontWeight = FontWeights.Bold });
            
            var sp1 = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 10) };
            sp1.Children.Add(new TextBlock { Text = "Семейство: ", Width = 90, VerticalAlignment = VerticalAlignment.Center });
            txtFamilyName = new TextBox { Width = 150, VerticalAlignment = VerticalAlignment.Center };
            sp1.Children.Add(txtFamilyName);
            sp1.Children.Add(new TextBlock { Text = " Гости (запетая): ", Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center });
            txtMembers = new TextBox { Width = 200, VerticalAlignment = VerticalAlignment.Center };
            sp1.Children.Add(txtMembers);
            
            var btnAddFamily = new Button { Content = "Добави", Width = 80, Margin = new Thickness(10, 0, 0, 0) };
            btnAddFamily.Click += (s, e) => {
                string fName = txtFamilyName.Text.Trim();
                string[] memberNames = txtMembers.Text.Split(',');
                if (!string.IsNullOrEmpty(fName))
                {
                    Family family = new Family(fName);
                    foreach (var m in memberNames)
                    {
                        if (!string.IsNullOrWhiteSpace(m))
                            family.AddMember(new Guest(m.Trim()));
                    }
                    _families.Add(family);
                    txtOutput.AppendText($"Добавено семейство: {fName} ({family.Count} души)\n");
                    txtFamilyName.Clear();
                    txtMembers.Clear();
                }
            };
            sp1.Children.Add(btnAddFamily);
            root.Children.Add(sp1);

            root.Children.Add(new Separator { Margin = new Thickness(0, 5, 0, 10) });

            // Забрана (Конфликт)
            root.Children.Add(new TextBlock { Text = "Забрана за сядане на една маса:", FontWeight = FontWeights.Bold });
            var sp2 = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 10) };
            sp2.Children.Add(new TextBlock { Text = "Конфликт: ", Width = 90, VerticalAlignment = VerticalAlignment.Center });
            txtFam1 = new TextBox { Width = 150, VerticalAlignment = VerticalAlignment.Center };
            sp2.Children.Add(txtFam1);
            sp2.Children.Add(new TextBlock { Text = " не може с ", Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center });
            txtFam2 = new TextBox { Width = 150, Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            sp2.Children.Add(txtFam2);

            var btnAddConflict = new Button { Content = "Забрани", Width = 80, Margin = new Thickness(10, 0, 0, 0) };
            btnAddConflict.Click += (s, e) => {
                string f1 = txtFam1.Text.Trim();
                string f2 = txtFam2.Text.Trim();
                if (!string.IsNullOrEmpty(f1) && !string.IsNullOrEmpty(f2))
                {
                    _conflicts.Add($"{f1}-{f2}");
                    txtOutput.AppendText($"ЗАБРАНА: {f1} не може да е на маса с {f2}\n");
                    txtFam1.Clear();
                    txtFam2.Clear();
                }
            };
            sp2.Children.Add(btnAddConflict);
            root.Children.Add(sp2);

            root.Children.Add(new Separator { Margin = new Thickness(0, 5, 0, 10) });

            // Бутон за стартиране на разпределението
            var btnOrganize = new Button 
            { 
                Content = "Разпределение по маси (Старт)", 
                Height = 35, 
                Background = new SolidColorBrush(Color.FromRgb(76, 175, 80)),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold
            };
            btnOrganize.Click += (s, e) => OrganizeSeating();
            root.Children.Add(btnOrganize);

            root.Children.Add(new TextBlock { Text = "Резултати:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 10, 0, 5) });
            
            txtOutput = new TextBox 
            { 
                Height = 250, 
                IsReadOnly = true, 
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                AcceptsReturn = true
            };
            root.Children.Add(txtOutput);

            Content = root;
        }

        // --- 2. ITERATOR PATTERN & LOGIC ---
        private void OrganizeSeating()
        {
            _tables.Clear();
            txtOutput.AppendText("\n--- Започва автоматично разпределение по маси ---\n");

            var currentTable = new WeddingTable(1);
            _tables.Add(currentTable);

            // Използване на Iterator pattern чрез стандартния IEnumerable / IEnumerator на C# колекциите
            using (IEnumerator<Family> iterator = _families.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    Family family = iterator.Current;
                    bool seated = false;

                    foreach (var table in _tables)
                    {
                        if (table.CanSeat(family, _conflicts))
                        {
                            table.SeatFamily(family);
                            seated = true;
                            break;
                        }
                    }

                    if (!seated)
                    {
                        var newTable = new WeddingTable(_tables.Count + 1);
                        if (newTable.CanSeat(family, _conflicts))
                        {
                            newTable.SeatFamily(family);
                            _tables.Add(newTable);
                        }
                        else
                        {
                            txtOutput.AppendText($"ГРЕШКА: Семейство '{family.Name}' е твърде голямо за празна маса!\n");
                        }
                    }
                }
            }

            // Извеждане на крайния резултат
            foreach (var table in _tables)
            {
                txtOutput.AppendText($"Маса № {table.TableNumber} (Общо гости: {table.TotalGuests}):\n");
                foreach (var f in table.SeatedFamilies)
                {
                    txtOutput.AppendText($"   -> Семейство: {f.Name} [Брой: {f.Count}]\n");
                }
            }
            txtOutput.AppendText("--- Разпределението приключи ---\n\n");
        }
}