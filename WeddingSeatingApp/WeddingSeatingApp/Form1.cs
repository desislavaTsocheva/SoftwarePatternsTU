#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WeddingSeatingApp
{
    public partial class Form1 : Form
    {
        private List<Family> allFamilies = new List<Family>();
        private List<Table> allTables = new List<Table>();

        private TextBox txtFamilyName;
        private TextBox txtGuestName;

        private ListBox lstFamilies;
        private ListBox lstTables;

        private Button btnAddFamily;
        private Button btnAddGuest;
        private Button btnAddTable;
        private Button btnAssign;
        private Button btnAddRestriction;

        private Label lblFamilies;
        private Label lblTables;
        private Label lblHeader;

        private readonly Color background =
            Color.FromArgb(250, 246, 243);

        private readonly Color burgundy =
            Color.FromArgb(111, 54, 70);

        private readonly Color rose =
            Color.FromArgb(184, 112, 131);

        private readonly Color roseHover =
            Color.FromArgb(151, 78, 99);

        private readonly Color darkText =
            Color.FromArgb(65, 48, 53);

        private readonly Color mutedText =
            Color.FromArgb(145, 125, 130);

        public Form1()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Wedding Seating Manager";
            this.ClientSize = new Size(1000, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = background;
            this.Font = new Font("Segoe UI", 10);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // HEADER
            Panel header = new Panel
            {
                Left = 0,
                Top = 0,
                Width = 1000,
                Height = 125,
                BackColor = background
            };

            Label lblSmall = new Label
            {
                Text = "THE PERFECT DAY STARTS HERE",
                Left = 35,
                Top = 17,
                Width = 400,
                Height = 20,
                ForeColor = rose,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            lblHeader = new Label
            {
                Text = "Wedding Seating Planner",
                Left = 32,
                Top = 39,
                Width = 700,
                Height = 48,
                Font = new Font("Georgia", 25),
                ForeColor = burgundy
            };

            Label subtitle = new Label
            {
                Text = "Organize your guests and create the perfect seating arrangement.",
                Left = 36,
                Top = 88,
                Width = 750,
                Height = 22,
                ForeColor = mutedText,
                Font = new Font("Segoe UI", 9.5f)
            };

            header.Controls.Add(lblSmall);
            header.Controls.Add(lblHeader);
            header.Controls.Add(subtitle);

            this.Controls.Add(header);

            Panel familyCard = CreateCard(25, 145, 440, 470);

            Label familyTitle = CreateSectionTitle(
                "01  |  Families & Guests", 25, 20, 380);

            Label familyDescription = new Label
            {
                Text = "Manage your wedding guest list",
                Left = 25,
                Top = 53,
                Width = 380,
                Height = 22,
                ForeColor = mutedText
            };

            txtFamilyName = CreateStyledTextBox(25, 95, 250);
            txtFamilyName.PlaceholderText = "Enter family name...";

            btnAddFamily = CreateStyledButton(
                "+ New Family",
                285, 94, 130,
                rose, roseHover);

            btnAddFamily.Click += BtnAddFamily_Click;

            txtGuestName = CreateStyledTextBox(25, 145, 250);
            txtGuestName.PlaceholderText = "Enter guest name...";

            btnAddGuest = CreateStyledButton(
                "+ Add Guest",
                285, 144, 130,
                burgundy,
                Color.FromArgb(85, 38, 53));

            btnAddGuest.Click += BtnAddGuest_Click;

            Label guestLabel = new Label
            {
                Text = "REGISTERED FAMILIES",
                Left = 25,
                Top = 195,
                Width = 380,
                Height = 22,
                ForeColor = burgundy,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            lstFamilies = CreateStyledListBox(
                25, 225, 390, 160);

            btnAddRestriction = CreateStyledButton(
                "Set Family Restriction",
                25, 410, 390,
                Color.FromArgb(232, 220, 222),
                Color.FromArgb(215, 193, 198));

            btnAddRestriction.ForeColor = burgundy;

            btnAddRestriction.Click += BtnAddRestriction_Click;

            familyCard.Controls.Add(familyTitle);
            familyCard.Controls.Add(familyDescription);
            familyCard.Controls.Add(txtFamilyName);
            familyCard.Controls.Add(btnAddFamily);
            familyCard.Controls.Add(txtGuestName);
            familyCard.Controls.Add(btnAddGuest);
            familyCard.Controls.Add(guestLabel);
            familyCard.Controls.Add(lstFamilies);
            familyCard.Controls.Add(btnAddRestriction);

            this.Controls.Add(familyCard);

            Panel tableCard = CreateCard(485, 145, 490, 470);

            Label tableTitle = CreateSectionTitle(
                "02  |  Wedding Tables", 25, 20, 430);

            Label tableDescription = new Label
            {
                Text = "Create tables and assign your guests",
                Left = 25,
                Top = 53,
                Width = 430,
                Height = 22,
                ForeColor = mutedText
            };

            btnAddTable = CreateStyledButton(
                "+ Create New Table",
                25, 95, 430,
                rose, roseHover);

            btnAddTable.Click += BtnAddTable_Click;

            Label tableLabel = new Label
            {
                Text = "SEATING ARRANGEMENT",
                Left = 25,
                Top = 150,
                Width = 430,
                Height = 22,
                ForeColor = burgundy,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            lstTables = CreateStyledListBox(
                25, 180, 430, 205);

            btnAssign = CreateStyledButton(
                "Create Seating Arrangement",
                25, 410, 430,
                burgundy,
                Color.FromArgb(85, 38, 53));

            btnAssign.Height = 42;
            btnAssign.Font = new Font(
                "Segoe UI", 10, FontStyle.Bold);

            btnAssign.Click += BtnAssign_Click;

            tableCard.Controls.Add(tableTitle);
            tableCard.Controls.Add(tableDescription);
            tableCard.Controls.Add(btnAddTable);
            tableCard.Controls.Add(tableLabel);
            tableCard.Controls.Add(lstTables);
            tableCard.Controls.Add(btnAssign);

            this.Controls.Add(tableCard);

            Label footer = new Label
            {
                Text = "Made with love  |  Wedding Reception Planner",
                Left = 25,
                Top = 630,
                Width = 950,
                Height = 22,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = mutedText,
                Font = new Font("Segoe UI", 8.5f)
            };

            this.Controls.Add(footer);
        }

        private Panel CreateCard(
            int left, int top, int width, int height)
        {
            Panel card = new Panel
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height,
                BackColor = Color.White
            };

            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(
                    Color.FromArgb(235, 225, 226)))
                {
                    e.Graphics.DrawRectangle(
                        pen,
                        0, 0,
                        card.Width - 1,
                        card.Height - 1);
                }
            };

            return card;
        }


        private Label CreateSectionTitle(
            string text,
            int left,
            int top,
            int width)
        {
            return new Label
            {
                Text = text,
                Left = left,
                Top = top,
                Width = width,
                Height = 30,
                Font = new Font("Georgia", 15),
                ForeColor = burgundy
            };
        }

        private TextBox CreateStyledTextBox(
            int left, int top, int width)
        {
            return new TextBox
            {
                Left = left,
                Top = top,
                Width = width,
                Height = 32,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(250, 247, 246),
                ForeColor = darkText,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private Button CreateStyledButton(
            string text,
            int left,
            int top,
            int width,
            Color backColor,
            Color hoverColor)
        {
            Button btn = new Button
            {
                Text = text,
                Left = left,
                Top = top,
                Width = width,
                Height = 36,
                Font = new Font(
                    "Segoe UI", 9, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = hoverColor;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = backColor;
            };

            return btn;
        }

        private ListBox CreateStyledListBox(
            int left,
            int top,
            int width,
            int height)
        {
            return new ListBox
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(250, 247, 246),
                ForeColor = darkText,
                BorderStyle = BorderStyle.None,
                IntegralHeight = false
            };
        }

        private void BtnAddFamily_Click(
            object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFamilyName.Text))
            {
                string familyName = txtFamilyName.Text.Trim();

                var fam = new Family(familyName);

                allFamilies.Add(fam);

                lstFamilies.Items.Add(
                    $"Family: {fam.Name}");

                txtFamilyName.Clear();
            }
            else
            {
                MessageBox.Show(
                    "Please enter a family name!",
                    "Notice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnAddGuest_Click(
            object sender, EventArgs e)
        {
            if (lstFamilies.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(txtGuestName.Text))
            {
                int index = lstFamilies.SelectedIndex;

                var family = allFamilies[index];

                var guest = new Guest(
                    txtGuestName.Text.Trim(),
                    family.Name);

                family.Add(guest);

                MessageBox.Show(
                    $"Successfully added guest '{guest.Name}' to family '{family.Name}'!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtGuestName.Clear();
            }
            else
            {
                MessageBox.Show(
                    "Please select a family from the list and enter a guest name!",
                    "Notice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnAddTable_Click(
            object sender, EventArgs e)
        {
            int tableNumber = allTables.Count + 1;

            var table = new Table(tableNumber);

            allTables.Add(table);

            lstTables.Items.Add(
                $"Table #{tableNumber} (Available)");
        }

        private void BtnAddRestriction_Click(
            object sender, EventArgs e)
        {
            if (allFamilies.Count >= 2)
            {
                allFamilies[0].AddForbiddenFamily(
                    allFamilies[1].Name);

                MessageBox.Show(
                    $"Restriction set: Family '{allFamilies[0].Name}' cannot sit with '{allFamilies[1].Name}'.",
                    "Restriction",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "At least 2 families are required to add a restriction.",
                    "Notice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnAssign_Click(
            object sender, EventArgs e)
        {
            if (allTables.Count == 0)
            {
                MessageBox.Show(
                    "Please add at least one table before assignment!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                ISeatingStrategy strategy =
                    new SequentialSeatingStrategy();

                strategy.SeatGuests(
                    allFamilies,
                    allTables);

                lstTables.Items.Clear();

                foreach (var table in allTables)
                {
                    string tableInfo =
                        $"Table {table.TableNumber} (Total: {table.GetCount()}): ";

                    foreach (var fam in table)
                    {
                        tableInfo += $"[{fam.Name}: ";

                        foreach (var g in fam)
                        {
                            tableInfo += $"{g.Name} ";
                        }

                        tableInfo += "] ";
                    }

                    lstTables.Items.Add(tableInfo);
                }

                MessageBox.Show(
                    "Guests were successfully assigned to tables!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Assignment error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
