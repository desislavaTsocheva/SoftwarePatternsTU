
#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WeddingSeatingApp.DesignPatterns.Composite;
using WeddingSeatingApp.DesignPatterns.Strategy;

namespace WeddingSeatingApp
{
    public partial class Form1 : Form
    {
        private readonly List<Family> allFamilies = new();
        private readonly List<Table> allTables = new();

        private readonly Color background = Color.FromArgb(255, 248, 251);
        private readonly Color burgundy = Color.FromArgb(111, 54, 70);
        private readonly Color rose = Color.FromArgb(184, 112, 131);
        private readonly Color roseHover = Color.FromArgb(151, 78, 99);
        private readonly Color lightRose = Color.FromArgb(252, 231, 240);
        private readonly Color paleRose = Color.FromArgb(255, 242, 247);
        private readonly Color darkText = Color.FromArgb(65, 48, 53);
        private readonly Color mutedText = Color.FromArgb(145, 125, 130);
        private readonly Color border = Color.FromArgb(240, 213, 224);

        private TextBox txtFamilyName;
        private TextBox txtGuestName;
        private ComboBox cmbGuestFamily;
        private ComboBox cmbRestrictionFirst;
        private ComboBox cmbRestrictionSecond;

        private FlowLayoutPanel familiesPanel;
        private FlowLayoutPanel tablesPanel;
        private FlowLayoutPanel restrictionsPanel;

        private Label lblFamilyCount;
        private Label lblGuestCount;
        private Label lblTableCount;
        private Label lblSelectedTable;
        private Label lblSelectedDetails;

        private Panel selectedTablePanel;
        private int selectedTableNumber = -1;

        public Form1()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            Text = "Wedding Seating Planner";
            ClientSize = new Size(1320, 850);
            MinimumSize = new Size(1100, 740);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = background;
            Font = new Font("Segoe UI", 10);
            AutoScaleMode = AutoScaleMode.Font;
            DoubleBuffered = true;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 18, 25, 18),
                ColumnCount = 1,
                RowCount = 3,
                BackColor = background
            };

            root.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 95));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 120));

            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            Controls.Add(root);

            BuildHeader(root);
            BuildStatistics(root);
            BuildWorkspace(root);

            RefreshFamilySelectors();
            RefreshFamilies();
            RefreshTables();
            RefreshRestrictions();
            RefreshStatistics();
        }

        private void BuildHeader(TableLayoutPanel root)
        {
            var header = new Panel
            {
                Dock = DockStyle.Fill
            };

            var eyebrow = CreateLabel(
                "THE PERFECT DAY STARTS HERE",
                9,
                true,
                rose);

            eyebrow.Location = new Point(5, 0);

            var title = CreateLabel(
                "Wedding Seating Planner",
                26,
                false,
                burgundy,
                "Georgia");

            title.Location = new Point(3, 23);

            var subtitle = CreateLabel(
                "Create beautiful seating arrangements for your special day",
                10,
                false,
                mutedText);

            subtitle.Location = new Point(5, 70);

            header.Controls.Add(eyebrow);
            header.Controls.Add(title);
            header.Controls.Add(subtitle);

            root.Controls.Add(header, 0, 0);
        }


        private void BuildStatistics(TableLayoutPanel root)
        {
            var stats = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 8)
            };

            for (int i = 0; i < 3; i++)
            {
                stats.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100f / 3));
            }

            lblFamilyCount = CreateStatistic(
                stats, 0, "FAMILIES", "0");

            lblGuestCount = CreateStatistic(
                stats, 1, "GUESTS", "0");

            lblTableCount = CreateStatistic(
                stats, 2, "TABLES", "0");

            root.Controls.Add(stats, 0, 1);
        }



        private Label CreateStatistic(
            TableLayoutPanel parent,
            int column,
            string caption,
            string value)
        {
            var card = CreateCard();

            card.Padding = new Padding(20, 8, 15, 8);

            card.Margin = new Padding(
                column == 0 ? 0 : 6,
                0,
                column == 2 ? 0 : 6,
                0);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 24));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var captionLabel = CreateLabel(
                caption, 9, true, mutedText);

            captionLabel.Dock = DockStyle.Fill;
            captionLabel.AutoSize = false;
            captionLabel.TextAlign = ContentAlignment.MiddleLeft;
            captionLabel.Margin = new Padding(0);

            var number = CreateLabel(
                value, 22, true, burgundy);

            number.Dock = DockStyle.Fill;
            number.AutoSize = false;
            number.TextAlign = ContentAlignment.MiddleLeft;
            number.Margin = new Padding(0);

            layout.Controls.Add(captionLabel, 0, 0);
            layout.Controls.Add(number, 0, 1);

            card.Controls.Add(layout);
            parent.Controls.Add(card, column, 0);

            return number;
        }


        private void BuildWorkspace(TableLayoutPanel root)
        {
            var workspace = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };

            workspace.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 43));

            workspace.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 57));

            var left = CreateCard();
            var right = CreateCard();

            left.Margin = new Padding(0, 0, 8, 0);
            right.Margin = new Padding(8, 0, 0, 0);

            workspace.Controls.Add(left, 0, 0);
            workspace.Controls.Add(right, 1, 0);

            BuildFamilies(left);
            BuildTables(right);

            root.Controls.Add(workspace, 0, 2);
        }

        private Panel CreateCard()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(18)
            };

            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode =
                    SmoothingMode.AntiAlias;

                using var pen = new Pen(border);

                e.Graphics.DrawRectangle(
                    pen,
                    0,
                    0,
                    panel.Width - 1,
                    panel.Height - 1);
            };

            return panel;
        }

        private Label CreateLabel(
            string text,
            float size = 10,
            bool bold = false,
            Color? color = null,
            string fontName = "Segoe UI")
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = color ?? darkText,
                Font = new Font(
                    fontName,
                    size,
                    bold ? FontStyle.Bold : FontStyle.Regular)
            };
        }

        private Button CreateButton(
            string text,
            bool secondary = false)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Height = 39,
                FlatStyle = FlatStyle.Flat,
                BackColor = secondary ? lightRose : rose,
                ForeColor = secondary ? burgundy : Color.White,
                Cursor = Cursors.Hand,
                Font = new Font(
                    "Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(3)
            };

            button.FlatAppearance.BorderSize = 0;

            button.MouseEnter += (s, e) =>
            {
                button.BackColor = secondary
                    ? Color.FromArgb(247, 205, 221)
                    : roseHover;
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = secondary
                    ? lightRose
                    : rose;
            };

            return button;
        }

        private TextBox CreateTextBox(string placeholder)
        {
            return new TextBox
            {
                PlaceholderText = placeholder,
                Dock = DockStyle.Fill,
                BackColor = paleRose,
                ForeColor = darkText,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(3, 7, 6, 4)
            };
        }

        private ComboBox CreateComboBox()
        {
            return new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = paleRose,
                ForeColor = darkText,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(3, 5, 6, 4)
            };
        }

        private TableLayoutPanel CreateRow(
            params float[] widths)
        {
            var row = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = widths.Length,
                RowCount = 1,
                Margin = new Padding(0)
            };

            foreach (float width in widths)
            {
                row.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, width));
            }

            return row;
        }

        private FlowLayoutPanel CreateScrollablePanel(
            bool wrap = false)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = wrap,
                BackColor = Color.White,
                Padding = new Padding(3)
            };

            return panel;
        }

        private void BuildFamilies(Panel parent)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 11
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            int[] heights =
            {
                42, 25, 45, 25, 45,
                35, 0, 32, 45, 35, 90
            };

            for (int i = 0; i < heights.Length; i++)
            {
                layout.RowStyles.Add(
                    i == 6
                        ? new RowStyle(SizeType.Percent, 100)
                        : new RowStyle(
                            SizeType.Absolute, heights[i]));
            }

            parent.Controls.Add(layout);

            layout.Controls.Add(
                CreateLabel(
                    "Families & Guests",
                    17, false, burgundy, "Georgia"),
                0, 0);

            layout.Controls.Add(
                CreateLabel("CREATE A FAMILY", 9, true, rose),
                0, 1);

            var familyRow = CreateRow(68, 32);

            txtFamilyName = CreateTextBox("Family name");

            var addFamily = CreateButton("+ Add Family");
            addFamily.Click += BtnAddFamily_Click;

            familyRow.Controls.Add(txtFamilyName, 0, 0);
            familyRow.Controls.Add(addFamily, 1, 0);

            layout.Controls.Add(familyRow, 0, 2);

            layout.Controls.Add(
                CreateLabel("ADD A GUEST", 9, true, rose),
                0, 3);

            var guestRow = CreateRow(34, 36, 30);

            cmbGuestFamily = CreateComboBox();
            txtGuestName = CreateTextBox("Guest name");

            var addGuest = CreateButton("+ Add Guest");
            addGuest.Click += BtnAddGuest_Click;

            guestRow.Controls.Add(cmbGuestFamily, 0, 0);
            guestRow.Controls.Add(txtGuestName, 1, 0);
            guestRow.Controls.Add(addGuest, 2, 0);

            layout.Controls.Add(guestRow, 0, 4);

            layout.Controls.Add(
                CreateLabel(
                    "REGISTERED FAMILIES",
                    10, true, burgundy),
                0, 5);

            familiesPanel = CreateScrollablePanel();

            familiesPanel.SizeChanged += (s, e) =>
            {
                foreach (Control control in familiesPanel.Controls)
                {
                    control.Width = Math.Max(
                        120,
                        familiesPanel.ClientSize.Width - 25);
                }
            };

            layout.Controls.Add(familiesPanel, 0, 6);

            layout.Controls.Add(
                CreateLabel(
                    "FAMILY RESTRICTIONS",
                    10, true, burgundy),
                0, 7);

            var restrictionRow = CreateRow(35, 35, 30);

            cmbRestrictionFirst = CreateComboBox();
            cmbRestrictionSecond = CreateComboBox();

            var restrict = CreateButton("Restrict", true);
            restrict.Click += BtnAddRestriction_Click;

            restrictionRow.Controls.Add(
                cmbRestrictionFirst, 0, 0);

            restrictionRow.Controls.Add(
                cmbRestrictionSecond, 1, 0);

            restrictionRow.Controls.Add(
                restrict, 2, 0);

            layout.Controls.Add(restrictionRow, 0, 8);

            layout.Controls.Add(
                CreateLabel(
                    "Select two families that cannot share a table",
                    8, false, mutedText),
                0, 9);

            restrictionsPanel = CreateScrollablePanel();

            layout.Controls.Add(restrictionsPanel, 0, 10);
        }

        private void BuildTables(Panel parent)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 48));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 115));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            parent.Controls.Add(layout);

            layout.Controls.Add(
                CreateLabel(
                    "Wedding Tables",
                    17, false, burgundy, "Georgia"),
                0, 0);

            var addTable = CreateButton("+ Create New Table");
            addTable.Click += BtnAddTable_Click;

            layout.Controls.Add(addTable, 0, 1);

            tablesPanel = CreateScrollablePanel(true);

            tablesPanel.FlowDirection =
                FlowDirection.LeftToRight;

            tablesPanel.WrapContents = true;

            layout.Controls.Add(tablesPanel, 0, 2);

            var detailsCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = paleRose,
                Padding = new Padding(12),
                Margin = new Padding(3, 5, 3, 5)
            };

            lblSelectedTable = CreateLabel(
                "Select a table",
                11, true, burgundy);

            lblSelectedTable.Dock = DockStyle.Top;
            lblSelectedTable.Height = 28;
            lblSelectedTable.AutoSize = false;

            lblSelectedDetails = new Label
            {
                Text = "Click on a table to view its guests.",
                Dock = DockStyle.Fill,
                ForeColor = mutedText,
                Font = new Font("Segoe UI", 9),
                AutoEllipsis = true
            };

            detailsCard.Controls.Add(lblSelectedDetails);
            detailsCard.Controls.Add(lblSelectedTable);

            layout.Controls.Add(detailsCard, 0, 3);

            var assign = CreateButton(
                "Generate Seating Arrangement");

            assign.Font = new Font(
                "Segoe UI", 11, FontStyle.Bold);

            assign.Click += BtnAssign_Click;

            layout.Controls.Add(assign, 0, 4);
        }

        private Panel CreateFamilyCard(Family family)
        {
            var guests = family.Select(g => g.Name).ToList();

            int height = Math.Max(
                85,
                64 + ((guests.Count + 2) / 3) * 24);

            var card = new Panel
            {
                Width = 350,
                Height = height,
                BackColor = paleRose,
                Margin = new Padding(0, 0, 0, 9),
                Padding = new Padding(12),
                Cursor = Cursors.Hand
            };

            var title = CreateLabel(
                family.Name,
                11, true, burgundy);

            title.Location = new Point(12, 9);

            var count = CreateLabel(
                $"{family.GetCount()} guests",
                9, false, rose);

            count.Location = new Point(12, 35);

            var guestLabel = new Label
            {
                Text = guests.Count == 0
                    ? "No guests yet"
                    : string.Join("  •  ", guests),
                Location = new Point(12, 58),
                Width = 325,
                Height = Math.Max(20, height - 62),
                ForeColor = mutedText,
                Font = new Font("Segoe UI", 9),
                AutoEllipsis = true
            };

            card.Controls.Add(title);
            card.Controls.Add(count);
            card.Controls.Add(guestLabel);

            EventHandler selectFamily = (s, e) =>
            {
                cmbGuestFamily.SelectedItem = family.Name;
                txtGuestName.Focus();
            };

            card.Click += selectFamily;
            title.Click += selectFamily;
            count.Click += selectFamily;
            guestLabel.Click += selectFamily;

            card.MouseEnter += (s, e) =>
                card.BackColor = lightRose;

            card.MouseLeave += (s, e) =>
                card.BackColor = paleRose;

            return card;
        }

        private void RefreshFamilies()
        {
            familiesPanel.SuspendLayout();
            familiesPanel.Controls.Clear();

            foreach (var family in allFamilies)
            {
                var card = CreateFamilyCard(family);

                card.Width = Math.Max(
                    120,
                    familiesPanel.ClientSize.Width - 25);

                familiesPanel.Controls.Add(card);
            }

            familiesPanel.ResumeLayout();
            RefreshStatistics();
        }

        private void RefreshFamilySelectors()
        {
            string guest =
                cmbGuestFamily.SelectedItem?.ToString();

            string first =
                cmbRestrictionFirst.SelectedItem?.ToString();

            string second =
                cmbRestrictionSecond.SelectedItem?.ToString();

            string[] names = allFamilies
                .Select(f => f.Name)
                .ToArray();

            foreach (var combo in new[]
            {
                cmbGuestFamily,
                cmbRestrictionFirst,
                cmbRestrictionSecond
            })
            {
                combo.Items.Clear();
                combo.Items.AddRange(names);
            }

            RestoreSelection(cmbGuestFamily, guest);
            RestoreSelection(cmbRestrictionFirst, first);
            RestoreSelection(cmbRestrictionSecond, second);

            if (cmbRestrictionSecond.Items.Count > 1 &&
                cmbRestrictionSecond.SelectedIndex ==
                cmbRestrictionFirst.SelectedIndex)
            {
                cmbRestrictionSecond.SelectedIndex = 1;
            }
        }

        private void RestoreSelection(
            ComboBox combo,
            string previous)
        {
            if (previous != null &&
                combo.Items.Contains(previous))
            {
                combo.SelectedItem = previous;
            }
            else if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        private void RefreshRestrictions()
        {
            restrictionsPanel.Controls.Clear();

            var displayed = new HashSet<string>();

            foreach (var family in allFamilies)
            {
                foreach (string other in family.ForbiddenFamilies)
                {
                    var names = new[]
                    {
                        family.Name,
                        other
                    };

                    Array.Sort(
                        names,
                        StringComparer.OrdinalIgnoreCase);

                    string key = string.Join("|", names);

                    if (!displayed.Add(key))
                        continue;

                    var label = new Label
                    {
                        Text = names[0] + "  ×  " + names[1],
                        Width = Math.Max(
                            150,
                            restrictionsPanel.ClientSize.Width - 25),
                        Height = 29,
                        BackColor = lightRose,
                        ForeColor = burgundy,
                        Padding = new Padding(9, 5, 0, 0),
                        Font = new Font(
                            "Segoe UI", 9, FontStyle.Bold),
                        Margin = new Padding(0, 0, 0, 5)
                    };

                    restrictionsPanel.Controls.Add(label);
                }
            }
        }

        private Panel CreateTableVisual(Table table)
        {
            var panel = new Panel
            {
                Width = 195,
                Height = 210,
                BackColor = Color.White,
                Margin = new Padding(7),
                Cursor = Cursors.Hand,
                Tag = table.TableNumber
            };

            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode =
                    SmoothingMode.AntiAlias;

                bool selected =
                    selectedTableNumber == table.TableNumber;

                int occupied = table.GetCount();

                Color tableColor = selected
                    ? burgundy
                    : occupied > 0 ? rose : lightRose;

                Color seatColor = occupied > 0
                    ? rose
                    : Color.FromArgb(244, 211, 225);

                using var tableBrush =
                    new SolidBrush(tableColor);

                using var seatBrush =
                    new SolidBrush(seatColor);

                using var outline =
                    new Pen(border, 2);

                float centerX = panel.Width / 2f;
                float centerY = 89f;

                for (int i = 0; i < 10; i++)
                {
                    double angle =
                        (2 * Math.PI * i / 10) - Math.PI / 2;

                    float x = centerX +
                        (float)Math.Cos(angle) * 76;

                    float y = centerY +
                        (float)Math.Sin(angle) * 76;

                    bool filled = i < occupied;

                    using var brush = new SolidBrush(
                        filled ? rose : lightRose);

                    e.Graphics.FillEllipse(
                        brush, x - 10, y - 10, 20, 20);

                    e.Graphics.DrawEllipse(
                        outline, x - 10, y - 10, 20, 20);
                }

                e.Graphics.FillEllipse(
                    tableBrush,
                    centerX - 51,
                    centerY - 51,
                    102,
                    102);

                using var titleFont = new Font(
                    "Segoe UI", 12, FontStyle.Bold);

                using var countFont = new Font(
                    "Segoe UI", 9);

                using var titleBrush = new SolidBrush(
                    selected || occupied > 0
                        ? Color.White
                        : burgundy);

                using var countBrush = new SolidBrush(
                    selected || occupied > 0
                        ? Color.White
                        : mutedText);

                var centered = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(
                    "Table " + table.TableNumber,
                    titleFont,
                    titleBrush,
                    new RectangleF(
                        centerX - 49, centerY - 23, 98, 28),
                    centered);

                e.Graphics.DrawString(
                    occupied + " / 10",
                    countFont,
                    countBrush,
                    new RectangleF(
                        centerX - 45, centerY + 5, 90, 23),
                    centered);

                using var bottomFont = new Font(
                    "Segoe UI", 9, FontStyle.Bold);

                using var bottomBrush =
                    new SolidBrush(burgundy);

                string status = occupied == 0
                    ? "Available"
                    : table.Count() + " families";

                e.Graphics.DrawString(
                    status,
                    bottomFont,
                    bottomBrush,
                    new RectangleF(
                        0, 179, panel.Width, 25),
                    centered);

                if (selected)
                {
                    using var selectedPen =
                        new Pen(rose, 2);

                    e.Graphics.DrawRectangle(
                        selectedPen,
                        1, 1,
                        panel.Width - 3,
                        panel.Height - 3);
                }
            };

            panel.Click += (s, e) =>
                SelectTable(table.TableNumber);

            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = paleRose;
                panel.Invalidate();
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = Color.White;
                panel.Invalidate();
            };

            return panel;
        }

        private void SelectTable(int tableNumber)
        {
            selectedTableNumber = tableNumber;

            var table = allTables.FirstOrDefault(
                t => t.TableNumber == tableNumber);

            if (table == null)
                return;

            lblSelectedTable.Text =
                $"Table {table.TableNumber}  •  " +
                $"{table.GetCount()} / 10 guests";

            var families = table.ToList();

            lblSelectedDetails.Text = families.Count == 0
                ? "This table is currently available."
                : string.Join(
                    Environment.NewLine,
                    families.Select(f =>
                        f.Name + ": " +
                        string.Join(", ",
                            f.Select(g => g.Name))));

            foreach (Control control in tablesPanel.Controls)
                control.Invalidate();
        }

        private void RefreshTables()
        {
            tablesPanel.SuspendLayout();
            tablesPanel.Controls.Clear();

            foreach (var table in allTables)
            {
                tablesPanel.Controls.Add(
                    CreateTableVisual(table));
            }

            tablesPanel.ResumeLayout();

            if (selectedTableNumber > 0)
                SelectTable(selectedTableNumber);

            RefreshStatistics();
        }

        private void RefreshStatistics()
        {
            lblFamilyCount.Text = allFamilies.Count.ToString();

            lblGuestCount.Text = allFamilies
                .Sum(f => f.GetCount())
                .ToString();

            lblTableCount.Text = allTables.Count.ToString();
        }

        private void BtnAddFamily_Click(
            object sender, EventArgs e)
        {
            string name = txtFamilyName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowWarning("Enter a family name.");
                return;
            }

            if (allFamilies.Any(f =>
                string.Equals(
                    f.Name,
                    name,
                    StringComparison.OrdinalIgnoreCase)))
            {
                ShowWarning("This family already exists.");
                return;
            }

            allFamilies.Add(new Family(name));

            txtFamilyName.Clear();

            RefreshFamilySelectors();
            cmbGuestFamily.SelectedItem = name;

            RefreshFamilies();
            txtGuestName.Focus();
        }

        private void BtnAddGuest_Click(
            object sender, EventArgs e)
        {
            if (cmbGuestFamily.SelectedItem == null)
            {
                ShowWarning("Select a family first.");
                return;
            }

            string name = txtGuestName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowWarning("Enter a guest name.");
                return;
            }

            var family = allFamilies.First(f =>
                f.Name == cmbGuestFamily.SelectedItem.ToString());

            family.Add(new Guest(name, family.Name));

            txtGuestName.Clear();
            RefreshFamilies();
            txtGuestName.Focus();
        }

        private void BtnAddTable_Click(
            object sender, EventArgs e)
        {
            var table = new Table(allTables.Count + 1);

            allTables.Add(table);

            RefreshTables();
            SelectTable(table.TableNumber);
        }

        private void BtnAddRestriction_Click(
            object sender, EventArgs e)
        {
            string first =
                cmbRestrictionFirst.SelectedItem?.ToString();

            string second =
                cmbRestrictionSecond.SelectedItem?.ToString();

            if (first == null || second == null)
            {
                ShowWarning("Select two families.");
                return;
            }

            if (first == second)
            {
                ShowWarning(
                    "Select two different families.");
                return;
            }

            var familyA = allFamilies.First(f =>
                f.Name == first);

            var familyB = allFamilies.First(f =>
                f.Name == second);

            if (familyA.ForbiddenFamilies.Contains(second))
            {
                ShowWarning(
                    "This restriction already exists.");
                return;
            }

            familyA.AddForbiddenFamily(second);
            familyB.AddForbiddenFamily(first);

            RefreshRestrictions();
        }

        private void BtnAssign_Click(
            object sender, EventArgs e)
        {
            if (allTables.Count == 0)
            {
                ShowWarning("Create at least one table.");
                return;
            }

            if (allFamilies.Count == 0)
            {
                ShowWarning("Add at least one family.");
                return;
            }

            foreach (var table in allTables)
            {
                foreach (var family in table.ToList())
                    table.Remove(family);
            }

            try
            {
                ISeatingStrategy strategy =
                    new SequentialSeatingStrategy();

                strategy.SeatGuests(
                    allFamilies,
                    allTables);

                RefreshTables();

                if (allTables.Count > 0)
                    SelectTable(allTables[0].TableNumber);

                MessageBox.Show(
                    "Your seating arrangement is ready!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                foreach (var table in allTables)
                {
                    foreach (var family in table.ToList())
                        table.Remove(family);
                }

                RefreshTables();

                MessageBox.Show(
                    ex.Message,
                    "Seating Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(
                message,
                "Wedding Seating Planner",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
