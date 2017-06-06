using System.Collections.Generic;
using System.Dynamic;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Controls;

namespace RadDataGridTest
{
    public class EyeColor
    {
        public string DisplayName { get; set; }
    }

    public class Person
    {
        public string PersonName { get; set; }
        public EyeColor EyeColor { get; set; }
        public string EyeColorName { get; set; }
        public string EyeColorNameFromStringList { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var eyeColorList = new List<EyeColor>();
            eyeColorList.Add(new EyeColor() { DisplayName = "Red" });
            eyeColorList.Add(new EyeColor() { DisplayName = "Green" });
            eyeColorList.Add(new EyeColor() { DisplayName = "Blue" });

            var eyeColorStringList = new List<string>();
            foreach (var eyeColor in eyeColorList) eyeColorStringList.Add(eyeColor.DisplayName);
                     
            // Make a set of rows that are class instances
            var classInstanceRows = new List<Person>();
            classInstanceRows.Add(new Person() { PersonName = "Bill", EyeColor = eyeColorList[0], EyeColorName = eyeColorList[0].DisplayName, EyeColorNameFromStringList = eyeColorList[0].DisplayName });
            classInstanceRows.Add(new Person() { PersonName = "Jane", EyeColor = eyeColorList[1], EyeColorName = eyeColorList[1].DisplayName, EyeColorNameFromStringList = eyeColorList[1].DisplayName });

            // Make a set of rows that are dynamic object instances
            var dynamicRows = new List<ExpandoObject>();
            foreach(var classInstanceRow in classInstanceRows)
            {
                dynamic row = new ExpandoObject();
                row.PersonName = classInstanceRow.PersonName;
                row.EyeColor = classInstanceRow.EyeColor;
                row.EyeColorName = classInstanceRow.EyeColorName;
                row.EyeColorNameFromStringList = classInstanceRow.EyeColorNameFromStringList;
                dynamicRows.Add(row);
            }

            if (false)
            {
                // Use the class instances
                this.RadDataGrid.ItemsSource = classInstanceRows;
            }
            else
            {
                // Use the dynamic object instances
                // !!!!! An exception will be thrown when you cancel an edit in the UI
                this.RadDataGrid.ItemsSource = dynamicRows;
            }


            // Setup the RadDataGrid
            this.RadDataGrid.FrozenColumnCount = 1;
            this.RadDataGrid.AutoGenerateColumns = false;
            this.RadDataGrid.UserEditMode = DataGridUserEditMode.Inline;

            {
                var textColumn = new DataGridTextColumn();
                textColumn.Header = "Person Name";
                textColumn.PropertyName = "PersonName";
                this.RadDataGrid.Columns.Add(textColumn);
            }

            { // Column that gets its items from an array of classes and stores a class reference in the row data
                var comboBoxColumn = new DataGridComboBoxColumn();
                comboBoxColumn.Header = "Stored as reference";
                comboBoxColumn.ItemsSource = eyeColorList;
                comboBoxColumn.DisplayMemberPath = "DisplayName";
                comboBoxColumn.PropertyName = "EyeColor";
                this.RadDataGrid.Columns.Add(comboBoxColumn);
            }

            { // Column that gets its items from an array of classes and stores a string row data
                var comboBoxColumn = new DataGridComboBoxColumn();
                comboBoxColumn.Header = "Stored as string";
                comboBoxColumn.ItemsSource = eyeColorList;
                comboBoxColumn.DisplayMemberPath = "DisplayName";
                comboBoxColumn.SelectedValuePath = "DisplayName";
                comboBoxColumn.PropertyName = "EyeColorName";
                this.RadDataGrid.Columns.Add(comboBoxColumn);
            }

            if(false) // !!! setting this to true will throw an exception on the first Measure operation
            { // Column that gets its items from an array of strings and stores a string in the row data
                var comboBoxColumn = new DataGridComboBoxColumn();
                comboBoxColumn.Header = "Stored as string from list of strings"; // "Eye Color by string from string list";
                comboBoxColumn.ItemsSource = eyeColorStringList;
                comboBoxColumn.PropertyName = "EyeColorNameFromStringList";
                this.RadDataGrid.Columns.Add(comboBoxColumn);
            }
        }
    }
}
