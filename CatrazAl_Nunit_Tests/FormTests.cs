using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using CatrazAl_Form;

namespace CatrazAl.Tests.Forms
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class FormTests
    {
        [Test]
        [Ignore("Integration test: MainForm automatically fetches from DB on startup via Business classes. Un-ignore when DB is running.")]
        public void MainForm_Initialization_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => {
                using var form = new MainForm();
                Assert.That(form.Text, Does.Contain("CatrazAl"));
                Assert.That(form.Controls.Count, Is.GreaterThan(0));
            });
        }

        public class DummyEntity
        {
            public int DummyEntityId { get; set; }
            public string Title { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public object ComplexObject { get; set; } 
        }

        [Test]
        public void CrudPanel_BuildDynamicForm_CreatesCorrectInputControls()
        {
            var mockData = new List<DummyEntity>();

            using var panel = new CrudPanel<DummyEntity>(
                "Dummy Panel",
                () => mockData,
                (m) => { },
                (m) => { },
                (id) => { },
                (m) => m.DummyEntityId
            );

            var type = panel.GetType();
            var inputControlsField = type.GetField("inputControls", BindingFlags.NonPublic | BindingFlags.Instance);
            var inputControls = (Dictionary<string, Control>)inputControlsField.GetValue(panel);

            Assert.That(inputControls.Count, Is.EqualTo(4));

            Assert.That(inputControls.ContainsKey("DummyEntityId"), Is.True);
            Assert.That(inputControls["DummyEntityId"].Enabled, Is.False, "Primary key text box should be disabled");

            Assert.That(inputControls.ContainsKey("Title"), Is.True);
            Assert.That(inputControls["Title"], Is.InstanceOf<TextBox>());

            Assert.That(inputControls.ContainsKey("IsActive"), Is.True);
            Assert.That(inputControls["IsActive"], Is.InstanceOf<CheckBox>());

            Assert.That(inputControls.ContainsKey("CreatedAt"), Is.True);
            Assert.That(inputControls["CreatedAt"], Is.InstanceOf<DateTimePicker>());

            Assert.That(inputControls.ContainsKey("ComplexObject"), Is.False, "Complex relational objects should be ignored by the UI builder");
        }

        [Test]
        public void CrudPanel_PopulateForm_BindsDataToUIControls()
        {
            var mockData = new List<DummyEntity>();
            using var panel = new CrudPanel<DummyEntity>("Dummy Panel", () => mockData, (m) => { }, (m) => { }, (id) => { }, (m) => m.DummyEntityId);

            var type = panel.GetType();
            var inputControlsField = type.GetField("inputControls", BindingFlags.NonPublic | BindingFlags.Instance);
            var inputControls = (Dictionary<string, Control>)inputControlsField.GetValue(panel);

            var dummyData = new DummyEntity
            {
                DummyEntityId = 5,
                Title = "Test Binding",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1)
            };

            var populateMethod = type.GetMethod("PopulateForm", BindingFlags.NonPublic | BindingFlags.Instance);
            populateMethod.Invoke(panel, new object[] { dummyData });

            Assert.That(((TextBox)inputControls["DummyEntityId"]).Text, Is.EqualTo("5"));
            Assert.That(((TextBox)inputControls["Title"]).Text, Is.EqualTo("Test Binding"));
            Assert.That(((CheckBox)inputControls["IsActive"]).Checked, Is.True);
            Assert.That(((DateTimePicker)inputControls["CreatedAt"]).Value, Is.EqualTo(new DateTime(2025, 1, 1)));
        }

        [Test]
        public void CrudPanel_ClearSelection_ResetsUIControls()
        {
            var mockData = new List<DummyEntity>();
            using var panel = new CrudPanel<DummyEntity>("Dummy Panel", () => mockData, (m) => { }, (m) => { }, (id) => { }, (m) => m.DummyEntityId);

            var type = panel.GetType();
            var inputControlsField = type.GetField("inputControls", BindingFlags.NonPublic | BindingFlags.Instance);
            var inputControls = (Dictionary<string, Control>)inputControlsField.GetValue(panel);

            var btnNewMethod = type.GetMethod("BtnNew_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            btnNewMethod.Invoke(panel, new object[] { null, EventArgs.Empty });

            Assert.That(((TextBox)inputControls["DummyEntityId"]).Text, Is.EqualTo("0"));
            Assert.That(((TextBox)inputControls["Title"]).Text, Is.EqualTo(string.Empty));
            Assert.That(((CheckBox)inputControls["IsActive"]).Checked, Is.False);
        }
    }
}