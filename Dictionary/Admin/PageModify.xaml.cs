using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for PageModify.xaml
    /// </summary>
    public partial class PageModify : Page
    {
        private static string pathNewPhoto = "";

        public PageModify()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageAdmin());
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            Category category;
            if (!this.ComboBoxCategory.Text.Equals("other"))
            {
                category = new Category(this.ComboBoxCategory.Text);
            }
            else
            {
                if (this.TextBoxCategoryOther.Text.Equals("")) return;
                category = new Category(this.TextBoxCategoryOther.Text);
            }

            Word currentItem = (Word)this.ListBoxOfWords.SelectedItem;
            if (currentItem != null)
            {
                foreach (var word in (this.DataContext as WordVM).Words)
                {
                    if (word.Equals(currentItem))
                    {
                        currentItem.Name = this.TextBoxName.Text;
                        currentItem.Description = this.TextBoxDescription.Text;
                        currentItem.CategoryWord = category;
                        if (!pathNewPhoto.Equals(""))
                        {
                            currentItem.PhotoName = this.TextBoxName.Text + ".jpg";

                            BitmapImage image = new BitmapImage(new Uri(pathNewPhoto));
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(image));

                            using (var fileStream = new System.IO.FileStream("..//..//Resources\\Pictures\\" + this.TextBoxName.Text + ".jpg", System.IO.FileMode.Create))
                            {
                                encoder.Save(fileStream);
                            }
                        }
                        break;
                    }
                }
                ReadAndWrite.WriteWords((this.DataContext as WordVM).Words);
            }
            Switcher.Switch(new PageModify());

        }

        private void ListBoxOfWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Word currentItem = (Word)this.ListBoxOfWords.SelectedItem;
            if (currentItem != null)
            {
                this.TextBoxName.Text = currentItem.Name;
                this.TextBoxDescription.Text = currentItem.Description;
                this.ComboBoxCategory.Text = currentItem.CategoryWord.Name;
                ImageWord.Source = new BitmapImage(new Uri(@"D:\\Info Unitbv 2020-2021\\Semestrul II\\MVP\\Laborator\\Dictionary\\Dictionary\\Resources\\Pictures\\" + currentItem.PhotoName, UriKind.RelativeOrAbsolute));
            }
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxCategory.SelectedItem.ToString().Equals("other") && this.ComboBoxCategory != null && this.TextBoxCategoryOther != null)
            {
                this.TextBoxCategoryOther.Visibility = Visibility.Visible;
                this.LabelOtherCategory.Visibility = Visibility.Visible;
            }
            else if (this.ComboBoxCategory != null && this.TextBoxCategoryOther != null)
            {
                this.TextBoxCategoryOther.Visibility = Visibility.Hidden;
                this.LabelOtherCategory.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\";
                openFileDialog.Filter = "(*.png)|*.png|(*.jpg)|*.jpg";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            pathNewPhoto = filePath;
            ImageWord.Source = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
        }
    }
}
