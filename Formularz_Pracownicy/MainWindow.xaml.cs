using lab_1.Model;
using Microsoft.Win32;
using Microsoft.Win32;
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
        private StudyFieldCollection _sfCollection = new StudyFieldCollection();
        private StudentGroup _group = new StudentGroup("Grupa 1");
        public MainWindow()
        {
            InitializeComponent();

            // podpięcie pod cb kierunków studiów
            cb_kierunek.ItemsSource = _sfCollection;
            cb_kierunek.DisplayMemberPath = "Name";

            // podpięcie pod lb studentów z grupy
            lb_studenci.ItemsSource = _group.Students;
        }

        private void btn_dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbx_imie.Text) ||
                string.IsNullOrEmpty(tbx_nazwisko.Text) ||
                string.IsNullOrEmpty(tbx_id.Text) ||
                cb_kierunek.SelectedItem == null ||
                dp_data.SelectedDate == null)
            {
                MessageBox.Show("Pola muszą być wypełnione", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Student nowyStudent = new Student(tbx_imie.Text, tbx_nazwisko.Text,
                dp_data.SelectedDate, tbx_id.Text, (StudyField)cb_kierunek.SelectedItem);

            _group.AddStudent(nowyStudent);
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
                File.WriteAllText(saveFileDialog.FileName, "Przykładowa zawartość pliku.");
                MessageBox.Show("Plik zapisany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
