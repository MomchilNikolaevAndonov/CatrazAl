using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CatrazAl.Business;
using CatrazAl.Data.Models;

namespace CatrazAl_Form
{
    public class MainForm : Form
    {
        private Panel sidebarPanel;
        private Panel contentPanel;
        private Label logoLabel;
        private Button currentActiveButton;

        public MainForm()
        {
            InitializeComponent();
            SetupModules();
            ShowWelcomeScreen();
        }

        private void InitializeComponent()
        {
            this.Text = "CatrazAl - Prison Management System";
            this.Size = new Size(1250, 750);
            this.MinimumSize = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 240,
                BackColor = Color.FromArgb(35, 40, 45),
                ForeColor = Color.White
            };

            logoLabel = new Label
            {
                Text = "CatrazAl\nManager",
                Dock = DockStyle.Top,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                BackColor = Color.FromArgb(23, 27, 31),
                ForeColor = Color.LightGray
            };
            sidebarPanel.Controls.Add(logoLabel);

            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 246, 250)
            };

            this.Controls.Add(contentPanel);
            this.Controls.Add(sidebarPanel);
        }

        private void ShowWelcomeScreen()
        {
            contentPanel.Controls.Clear();
            Label welcomeLabel = new Label
            {
                Text = "Welcome to CatrazAl Prison Management System\n\nPlease select a module from the sidebar to begin.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16F, FontStyle.Regular),
                ForeColor = Color.DimGray
            };
            contentPanel.Controls.Add(welcomeLabel);
        }

        private void SetupModules()
        {
            var prisonerBiz = new PrisonerBusiness();
            var cellBiz = new CellBusiness();
            var blockBiz = new PrisonBlockBusiness();
            var guardBiz = new GuardBusiness();
            var shiftBiz = new ShiftBusiness();
            var crimeBiz = new CrimeBusiness();
            var punishBiz = new PunishmentBusiness();
            var medBiz = new MedicalRecordBusiness();
            var visitBiz = new VisitBusiness();

            AddNavigationButton("👥 Prisoners", new CrudPanel<Prisoner>("Manage Prisoners",
                prisonerBiz.GetAll, prisonerBiz.Add, prisonerBiz.Update, prisonerBiz.Delete, p => p.PrisonerId));

            AddNavigationButton("🛏️ Cells", new CrudPanel<Cell>("Manage Cells",
                cellBiz.GetAll, cellBiz.Add, cellBiz.Update, cellBiz.Delete, c => c.CellId));

            AddNavigationButton("🏢 Prison Blocks", new CrudPanel<PrisonBlock>("Manage Prison Blocks",
                blockBiz.GetAll, blockBiz.Add, blockBiz.Update, blockBiz.Delete, b => b.PrisonBlockId));

            AddNavigationButton("👮 Guards", new CrudPanel<Guard>("Manage Guards",
                guardBiz.GetAll, guardBiz.Add, guardBiz.Update, guardBiz.Delete, g => g.GuardId));

            AddNavigationButton("⏱️ Shifts", new CrudPanel<Shift>("Manage Shifts",
                shiftBiz.GetAll, shiftBiz.Add, shiftBiz.Update, shiftBiz.Delete, s => s.ShiftId));

            AddNavigationButton("⚖️ Crimes", new CrudPanel<Crime>("Manage Crimes",
                crimeBiz.GetAll, crimeBiz.Add, crimeBiz.Update, crimeBiz.Delete, c => c.CrimeId));

            AddNavigationButton("⚠️ Punishments", new CrudPanel<Punishment>("Manage Punishments",
                punishBiz.GetAll, punishBiz.Add, punishBiz.Update, punishBiz.Delete, p => p.PunishmentId));

            AddNavigationButton("⚕️ Medical Records", new CrudPanel<MedicalRecord>("Manage Medical Records",
                medBiz.GetAll, medBiz.Add, medBiz.Update, medBiz.Delete, m => m.RecordId));

            AddNavigationButton("🤝 Visits", new CrudPanel<Visit>("Manage Visits",
                visitBiz.GetAll, visitBiz.Add, visitBiz.Update, visitBiz.Delete, v => v.VisitId));
        }

        private void AddNavigationButton(string title, UserControl targetPanel)
        {
            Button btn = new Button
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 55,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 56, 62) },
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular)
            };

            btn.Click += (s, e) =>
            {
                if (currentActiveButton != null)
                {
                    currentActiveButton.BackColor = Color.FromArgb(35, 40, 45);
                    currentActiveButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                }
                currentActiveButton = btn;
                btn.BackColor = Color.FromArgb(0, 122, 204);
                btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

                contentPanel.Controls.Clear();
                targetPanel.Dock = DockStyle.Fill;
                contentPanel.Controls.Add(targetPanel);
            };

            sidebarPanel.Controls.Add(btn);
            btn.BringToFront();
        }
    }

    public class CrudPanel<T> : UserControl where T : class, new()
    {
        private Func<List<T>> _getAll;
        private Action<T> _add;
        private Action<T> _update;
        private Action<int> _delete;
        private Func<T, int> _getId;

        private DataGridView grid;
        private Panel dynamicFormPanel;
        private Label lblInstruction;
        private Dictionary<string, Control> inputControls;
        private T currentEditItem;

        public CrudPanel(string title, Func<List<T>> getAll, Action<T> add, Action<T> update, Action<int> delete, Func<T, int> getId)
        {
            _getAll = getAll;
            _add = add;
            _update = update;
            _delete = delete;
            _getId = getId;

            inputControls = new Dictionary<string, Control>();

            InitializeLayout(title);
            BuildDynamicForm();
            RefreshData();
        }

        private void InitializeLayout(string title)
        {
            Label lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI Semibold", 18F),
                Height = 60,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 45, 48)
            };
            this.Controls.Add(lblTitle); 

            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 650,
                BackColor = Color.FromArgb(230, 230, 230),
                Padding = new Padding(10)
            };

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing,
                ColumnHeadersHeight = 40
            };

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 232, 255);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            grid.SelectionChanged += Grid_SelectionChanged;

            Panel editContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            Button btnNew = CreateActionButton("➕ Create New Record", Color.SteelBlue);
            btnNew.Dock = DockStyle.Top;
            btnNew.Height = 50;
            btnNew.Margin = new Padding(0, 0, 0, 10);
            btnNew.Click += BtnNew_Click;

            lblInstruction = new Label
            {
                Text = "Creating New Record",
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 11F),
                BackColor = Color.FromArgb(220, 245, 230)
            };

            dynamicFormPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(15) };

            TableLayoutPanel toolBarLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(5)
            };
            toolBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            toolBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            Button btnSave = CreateActionButton("💾 Save Changes", Color.SeaGreen);
            btnSave.Dock = DockStyle.Fill;
            btnSave.Click += BtnSave_Click;

            Button btnDelete = CreateActionButton("🗑️ Delete Selected", Color.IndianRed);
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.Click += BtnDelete_Click;

            toolBarLayout.Controls.Add(btnSave, 0, 0);
            toolBarLayout.Controls.Add(btnDelete, 1, 0);

            editContainer.Controls.Add(dynamicFormPanel);
            editContainer.Controls.Add(lblInstruction);
            editContainer.Controls.Add(btnNew);
            editContainer.Controls.Add(toolBarLayout);

            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(editContainer);

            this.Controls.Add(split); 
            split.BringToFront();     
        }

        private void BuildDynamicForm()
        {
            dynamicFormPanel.Controls.Clear();
            inputControls.Clear();
            int yPos = 15;

            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Type type = prop.PropertyType;
                Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

                if ((underlyingType.IsClass && underlyingType != typeof(string)) || underlyingType.IsInterface)
                    continue;

                Label lbl = new Label
                {
                    Text = prop.Name,
                    Left = 15,
                    Top = yPos,
                    Width = 250,
                    Font = new Font("Segoe UI Semibold", 9.5F),
                    ForeColor = Color.FromArgb(60, 60, 60)
                };
                dynamicFormPanel.Controls.Add(lbl);

                Control inputCtrl;
                if (underlyingType == typeof(bool))
                {
                    inputCtrl = new CheckBox { Left = 15, Top = yPos + 25, Width = 250, Text = "Enabled" };
                }
                else if (underlyingType == typeof(DateTime) || underlyingType == typeof(DateOnly))
                {
                    inputCtrl = new DateTimePicker { Left = 15, Top = yPos + 25, Width = 250, Format = DateTimePickerFormat.Short };
                }
                else
                {
                    inputCtrl = new TextBox { Left = 15, Top = yPos + 25, Width = 250 };
                    if (prop.Name.Equals(typeof(T).Name + "Id", StringComparison.OrdinalIgnoreCase))
                    {
                        inputCtrl.Enabled = false;
                        inputCtrl.BackColor = Color.FromArgb(235, 235, 235);
                    }
                }

                dynamicFormPanel.Controls.Add(inputCtrl);
                inputControls[prop.Name] = inputCtrl;
                yPos += 65;
            }
        }

        private Button CreateActionButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 10F),
                Cursor = Cursors.Hand,
                Margin = new Padding(3)
            };
        }

        private void RefreshData()
        {
            try
            {
                grid.DataSource = null;
                grid.DataSource = _getAll();

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    var prop = typeof(T).GetProperty(col.Name);
                    if (prop != null)
                    {
                        Type type = prop.PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;
                        if ((underlyingType.IsClass && underlyingType != typeof(string)) || underlyingType.IsInterface)
                            col.Visible = false;
                    }
                }

                grid.ClearSelection();
                PopulateForm(new T()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateForm(T item)
        {
            currentEditItem = item ?? new T();
            int id = _getId(currentEditItem);

            if (id == 0)
            {
                lblInstruction.Text = "Creating New Record";
                lblInstruction.BackColor = Color.FromArgb(220, 245, 230); 
            }
            else
            {
                lblInstruction.Text = $"Editing Record ID: {id}";
                lblInstruction.BackColor = Color.FromArgb(245, 246, 250);
            }

            foreach (var prop in typeof(T).GetProperties())
            {
                if (!inputControls.TryGetValue(prop.Name, out Control ctrl)) continue;

                var val = prop.GetValue(currentEditItem);

                if (ctrl is CheckBox cb)
                {
                    cb.Checked = val != null && (bool)val;
                }
                else if (ctrl is DateTimePicker dtp)
                {
                    DateTime safeDate = DateTime.Now;

                    if (val is DateTime dt && dt >= dtp.MinDate && dt <= dtp.MaxDate)
                    {
                        safeDate = dt;
                    }
                    else if (val is DateOnly doVal)
                    {
                        DateTime dtConverted = doVal.ToDateTime(TimeOnly.MinValue);
                        if (dtConverted >= dtp.MinDate && dtConverted <= dtp.MaxDate)
                            safeDate = dtConverted;
                    }

                    dtp.Value = safeDate;
                }
                else if (ctrl is TextBox txt)
                {
                    txt.Text = val?.ToString() ?? "";
                }
            }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count > 0)
            {
                PopulateForm((T)grid.SelectedRows[0].DataBoundItem);
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            grid.ClearSelection();
            PopulateForm(new T());
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (currentEditItem == null) return;

            try
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (!inputControls.TryGetValue(prop.Name, out Control ctrl)) continue;
                    if (!ctrl.Enabled) continue; 

                    Type type = prop.PropertyType;
                    Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

                    if (ctrl is CheckBox cb)
                    {
                        prop.SetValue(currentEditItem, cb.Checked);
                    }
                    else if (ctrl is DateTimePicker dtp)
                    {
                        if (underlyingType == typeof(DateTime))
                            prop.SetValue(currentEditItem, dtp.Value);
                        else if (underlyingType == typeof(DateOnly))
                            prop.SetValue(currentEditItem, DateOnly.FromDateTime(dtp.Value));
                    }
                    else if (ctrl is TextBox txt)
                    {
                        if (string.IsNullOrWhiteSpace(txt.Text))
                        {
                            if (!type.IsValueType || Nullable.GetUnderlyingType(type) != null)
                                prop.SetValue(currentEditItem, null);
                            else if (underlyingType == typeof(int))
                                prop.SetValue(currentEditItem, 0);
                        }
                        else
                        {
                            if (underlyingType == typeof(int))
                                prop.SetValue(currentEditItem, int.Parse(txt.Text));
                            else if (underlyingType == typeof(string))
                                prop.SetValue(currentEditItem, txt.Text);
                        }
                    }
                }

                int id = _getId(currentEditItem);
                if (id == 0)
                    _add(currentEditItem);
                else
                    _update(currentEditItem);

                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data. Ensure numerical boxes contain valid numbers.\n\nDetails: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentEditItem == null) return;
            var id = _getId(currentEditItem);
            if (id == 0) return;

            var result = MessageBox.Show($"Are you sure you want to delete this record (ID: {id})?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _delete(id);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not delete record. It may be linked to other records.\n\nDetails: {ex.Message}", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}