using Formularz_Pracownicy.Model;
using Microsoft.Win32;
using System;
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
        private Employee _editingEmployee = null;

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
            if (validate()==false) return;
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
            //MessageBox.Show(_editingEmployee.ToString());
            Employee nowyPracownik = new Employee(tbx_imie.Text, tbx_nazwisko.Text,
                    dp_data.SelectedDate, tbx_salary.Text, (Team)cb_team.SelectedItem, selectedContract);

            _group.AddEmployee(nowyPracownik);
            clear_fields();

        }
        private bool validate() {
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
            if (dp_data.SelectedDate.HasValue && dp_data.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Data urodzenia nie może być w przyszłości.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                dp_data.SelectedDate = DateTime.Today; 
            }
            else { return false; }

                string message = string.Join("\n", list);
            if (string.IsNullOrEmpty(tbx_imie.Text) ||
                string.IsNullOrEmpty(tbx_nazwisko.Text) ||
                string.IsNullOrEmpty(tbx_salary.Text) ||
                cb_team.SelectedItem == null ||
                dp_data.SelectedDate == null ||
                (rb_contract1.IsChecked == false && rb_contract2.IsChecked == false && rb_contract3.IsChecked == false)
                )
            {
                MessageBox.Show("Niewypełnione pola: \n" + message, "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }   
            return true;
        }
        private void clear_fields()
        {
            tbx_imie.Text = "";
            tbx_nazwisko.Text = "";
            dp_data.SelectedDate = null;
            tbx_salary.Text = "";
            rb_contract1.IsChecked = false;
            rb_contract3.IsChecked = false;
            rb_contract2.IsChecked = false;
            cb_team.Text = null;
        }

        private void tbx_salary_PreviewTextInput(object sender,
            TextCompositionEventArgs e)
        {
            
            TextBox textBox = sender as TextBox;
            // Sprawdzenie, czy próbujemy wpisać liczbę zmiennoprzecinkową z kropką
            bool isLeadingZero = textBox.Text.Length == 0 && e.Text == "0";
            bool isDecimalPoint = e.Text == "."; // Akceptujemy tylko kropkę jako separator dziesiętny

            // Walidacja: liczba musi być cyfrą, nie może zaczynać się od 0, i kropka jest dozwolona tylko raz
            e.Handled = !e.Text.All(char.IsDigit) && !isDecimalPoint || isLeadingZero ||
                        (textBox.Text.Contains(".") && isDecimalPoint);
        }
        private void tbx_name_PreviewTextInput(object sender,
            TextCompositionEventArgs e)
        {
            e.Handled = e.Text.All(char.IsDigit);
        }
        private void btn_usun_Click(object sender, RoutedEventArgs e) {
            if (lb_pracownicy.SelectedItem is Employee selectedEmployee)
            {
                _group.RemoveEmployee(selectedEmployee);
            }
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
                try
                {
                    List<Employee> loadedEmployees = new List<Employee>();

                    foreach (var line in File.ReadAllLines(openFileDialog.FileName))
                    {
                        try
                        {
                            // Rozbijamy linijkę na części
                            string[] parts = line.Split(',');

                            if (parts.Length == 4)
                            {
                                // Przetwarzamy imię i nazwisko
                                string[] nameParts = parts[0].Split(' ');
                                string firstName = nameParts[0];
                                string lastName = nameParts[1];

                                // Pobieramy wiek
                                int age = int.Parse(parts[0].Split(':')[1].TrimEnd(')'));

                                // Pobieramy stanowisko
                                string position = parts[1].Split(':')[1].Trim();

                                // Pobieramy typ umowy
                                string contract = parts[2].Split(':')[1].Trim();

                                // Pobieramy pensję
                                string salary = parts[3].Split(':')[1].Trim();

                                // Tworzymy obiekt Employee
                                Employee emp = new Employee(firstName, lastName, DateTime.Now.AddYears(-age), salary, new Team(position, $"{position}_Opis"), contract);
                                loadedEmployees.Add(emp);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Błąd parsowania linii:\n{line}\n\nSzczegóły: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    // Czyszczenie aktualnej listy i dodanie nowych pracowników
                    _group.Employee.Clear();
                    foreach (var emp in loadedEmployees)
                    {
                        _group.Employee.Add(emp);
                    }

                    MessageBox.Show("Dane zostały wczytane pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas wczytywania pliku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btn_edytuj_Click(object sender, RoutedEventArgs e) {
            if (lb_pracownicy.SelectedItem is Employee selectedEmployee)
            {
                if (validate() == false) return;
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
                _group.UpdateEmployee(
                    selectedEmployee,
                    tbx_imie.Text,
                    tbx_nazwisko.Text,
                    dp_data.SelectedDate,
                    tbx_salary.Text,
                    (Team)cb_team.SelectedItem,
                    selectedContract
                );
                lb_pracownicy.Items.Refresh();

            }
        }
        private void btn_employee_Click(object sender, RoutedEventArgs e)
        {
            clear_fields();
            if (lb_pracownicy.SelectedItem is Employee selectedEmployee)
            {
                _editingEmployee = selectedEmployee;
                // Wypełnienie pól formularza danymi zaznaczonego pracownika
                tbx_imie.Text = selectedEmployee.FirstName;
                tbx_nazwisko.Text = selectedEmployee.LastName;
                dp_data.SelectedDate = selectedEmployee.BirthDate;
                tbx_salary.Text = selectedEmployee.Salary;
                cb_team.SelectedItem = _sfCollection.FirstOrDefault(t => t.Name == selectedEmployee.Team1.Name);




                if (selectedEmployee.Contract == "Umowa zlecenie") {
                    rb_contract3.IsChecked = true;
                }
                else if (selectedEmployee.Contract =="Umowa na czas określony") {
                    rb_contract2.IsChecked = true;
                }
                else if (selectedEmployee.Contract == "Umowa na czas nieokreślony")
                {
                    rb_contract1.IsChecked = true;
                }

            }
        }
    }
}
