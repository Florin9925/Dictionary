using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for PageDelete.xaml
    /// </summary>
    public partial class PageDelete : Page
    {
        public PageDelete()
        {
            InitializeComponent();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Word currentItem = (Word)this.ListBoxOfWords.SelectedItem;
            if (currentItem != null)
            {
                (this.DataContext as WordVM).Words.Remove(currentItem);

                ReadAndWrite.WriteWords((this.DataContext as WordVM).Words);
                Switcher.Switch(new PageDelete());
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageAdmin());
        }

        private void ListBoxOfWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Word currentItem = (Word)this.ListBoxOfWords.SelectedItem;
            if (currentItem != null)
            {
                this.TextBoxName.Text = currentItem.Name;
                this.TextBoxDescription.Text = currentItem.Description;
                this.TextBoxCategory.Text = currentItem.CategoryWord.Name;
                ImageWord.Source = new BitmapImage(new Uri(@"D:\\Info Unitbv 2020-2021\\Semestrul II\\MVP\\Laborator\\Dictionary\\Dictionary\\Resources\\Pictures\\" + currentItem.PhotoName, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
