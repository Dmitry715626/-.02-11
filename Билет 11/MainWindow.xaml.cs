using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;

namespace Билет_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public static double StartPrice = 1000;
        public double PriceNotDiscount = 0;
        public double LastPrice = 0;
        public double RadioButtonPrice = 0;
        public string path = "";
        Word._Application oWord = new Word.Application();
        public MainWindow()
        {
            InitializeComponent();
            WordWriteButton.IsEnabled = false;
            Names.Items.Add("Красная шапочка");
            Names.Items.Add("Летучий корабль");
            Names.Items.Add("Лебединое озеро");
            Names.Items.Add("Донкихот");
            Names.Items.Add("Алые паруса");
            Names.Items.Add("Щелкунчик");
            Names.SelectedIndex = 0;
        }

        private void Names_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Вывод картинки
            ImageAfish.Source = BitmapFrame.Create(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\Афиши\\" + Names.SelectedValue + ".jpg"));
            
            
            
            path = System.IO.Directory.GetCurrentDirectory() + "\\Афиши\\" + Names.SelectedValue + ".jpg";
        }
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            //Проверка выбранной радио кнопки для увеличения цены (VIP, Балкон, Партер)
            RadioButtonPrice radio = new RadioButtonPrice(((RadioButton)sender), StartPrice);
            PriceNotDiscount = radio.GetPrice();
        }
        private void Calculation_Click(object sender, RoutedEventArgs e)
        {
            if(TextBoxCount.Text.Length == 0)
            {
                MessageBox.Show("Выберите количество билетов!", "Info");
                return;
            }
            
            //Рассчет скидки по количеству выбранных билетов
            Discount discount = new Discount(Convert.ToInt32(TextBoxCount.Text));
            LastPrice = (PriceNotDiscount * Convert.ToInt32(TextBoxCount.Text)) - (((PriceNotDiscount * Convert.ToInt32(TextBoxCount.Text)) / 100) * discount.GetDiscdountSize());
            PrintInformation();
            if(discount.GetDiscdountSize() > 0)
            {
                DisplayDiscount(discount.GetDiscdountSize());
            }
            WordWriteButton.IsEnabled = true;

        }
        private void PrintInformation() //Вывод информации
        {
            InfoForSession.Text = "Представление: " + Names.SelectedValue + "\nКоличество билетов: " + TextBoxCount.Text + "\nСумма: " + LastPrice;
        }
        private void DisplayDiscount(int Discount) //Отображение старой зачеркнутой цены
        {
            OldPrice.Text = "Старая цена: ";
            TextBoxDiscount.Text = (PriceNotDiscount * Convert.ToInt32(TextBoxCount.Text)).ToString();
            TextBoxDiscountDisplay.Text = $" (Скидка {Discount}%)";
        }
        private void Count_PreviewTextInput(object sender, TextCompositionEventArgs e) //Ограничение ввода (только цифры)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
        private void WordWriteButton_Click(object sender, RoutedEventArgs e)
        {
            Write(); //Начинаем процесс записи данных в ворд
        }
        private void Write()
        {
            //Выбирает шаблон и создает на его основе ворд документ
            _Document oDoc = GetDoc(Environment.CurrentDirectory + "\\чек.dotx");
            oDoc.SaveAs(FileName: Environment.CurrentDirectory + "\\ЧекВыход.docx"); //Сохранение документа который вернул метод GetDoc
            oDoc.Close();
        }
        private _Document GetDoc(string path)
        {
            //Создай документ по шаблону пихаем в метод SetTemplate и возвращаем для сохранения
            _Document oDoc = oWord.Documents.Add(path);
            SetTemplate(oDoc);
            return oDoc;
        }

        private void SetTemplate(Word._Document oDoc) //Непосредственная запись по закладкам
        {
            oDoc.Bookmarks["Number"].Range.Text = (DateTime.Now.ToString().Replace(".", "").Replace(":", "") + LastPrice).Trim();
            oDoc.Bookmarks["Date"].Range.Text = DateTime.Now.ToString();
            oDoc.Bookmarks["Product"].Range.Text = Names.SelectedValue.ToString();
            oDoc.Bookmarks["Final"].Range.Text = LastPrice.ToString() + " руб.";

            MessageBox.Show("Чек сохранен!\nНазвание: ЧекВыход.docx", "Info");
        }
        private void AddAfishButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ImageAfish.Source = BitmapFrame.Create(new Uri(dialog.FileName));
            }
        }
    }
}
