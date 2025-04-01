using Formularz_Pracownicy.Model;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace Formularz_Pracownicy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TeamCollection _sfCollection = new TeamCollection();
        private EmployeeGroup _group = new EmployeeGroup("Grupa 1");
        public MainWindow()
        {
            InitializeComponent();

            // podpięcie pod cb kierunków studiów
            cb_team.ItemsSource = _sfCollection;
            cb_team.DisplayMemberPath = "Name";

            // podpięcie pod lb studentów z grupy
            lb_pracownicy.ItemsSource = _group.Employee;
        }

        private void btn_dodaj_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<string> { };
            
            if (string.IsNullOrEmpty(tbx_imie.Text))
            {
                list.Add("Imie");
            }
            if (string.IsNullOrEmpty(tbx_nazwisko.Text))
            {
                list.Add("Nazwisko");
            }
            if (dp_data.SelectedDate == null)
            {
                list.Add("Data urodzenia");
            }
            if (cb_team.SelectedItem == null)
            {
                list.Add("Stanowisko");
            }
            if (rb_contract1.IsChecked == false && rb_contract2.IsChecked == false && rb_contract3.IsChecked == false)
            {
                list.Add("Typ umowy");
            }
            
            if (string.IsNullOrEmpty(tbx_salary.Text))
            {
                list.Add("Pensja");
            }

           
            
                string message = string.Join("\n", list);
            if (string.IsNullOrEmpty(tbx_imie.Text) ||
                string.IsNullOrEmpty(tbx_nazwisko.Text) ||
                string.IsNullOrEmpty(tbx_salary.Text) ||
                cb_team.SelectedItem == null ||
                dp_data.SelectedDate == null ||
                (rb_contract1.IsChecked==false && rb_contract2.IsChecked == false && rb_contract3.IsChecked == false)
                ) 
            {
                MessageBox.Show("Niewypełnione pola: \n"+message, "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedContract = "";

            if (rb_contract1.IsChecked == true)
            {
                selectedContract = rb_contract1.Content.ToString();
            }
            else if (rb_contract2.IsChecked == true)
            {
                selectedContract = rb_contract2.Content.ToString();
            }
            else if (rb_contract3.IsChecked == true)
            {
                selectedContract = rb_contract3.Content.ToString();
            }
            Employee nowyStudent = new Employee(tbx_imie.Text, tbx_nazwisko.Text,
                dp_data.SelectedDate, tbx_salary.Text, (Team)cb_team.SelectedItem, selectedContract);

            _group.AddStudent(nowyStudent);
            clear_fields();
            
        }
        private void clear_fields() {
            tbx_imie.Text = "";
            tbx_nazwisko.Text = "";
            dp_data.SelectedDate = null;
            tbx_salary.Text = "";
            rb_contract1.IsChecked=false;
            rb_contract3.IsChecked = false;
            rb_contract2.IsChecked = false;
            cb_team.Text = null;
        }

        private void tbx_id_PreviewTextInput(object sender,
            TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void btn_zapisz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
                Title = "Zapisz plik"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                // Pobranie kolekcji pracowników
                var employees = lb_pracownicy.ItemsSource as ObservableCollection<Employee>;

                if (employees != null)
                {
                    // Tworzenie listy linii do zapisania
                    List<string> lines = employees.Select(emp => emp.ToString()).ToList();

                    // Zapis do pliku
                    File.WriteAllLines(saveFileDialog.FileName, lines);

                    MessageBox.Show("Plik zapisany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btn_wczytaj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
                Title = "Zapis studentów"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string lines = File.ReadAllText(openFileDialog.FileName);
                MessageBox.Show(lines);
            }
        }
        private void btn_edytuj_Click(object sender, RoutedEventArgs e) {
            clear_fields();
        }
    }
}
