using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Forms;
using System.Drawing;
using Image = System.Windows.Controls.Image;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for PageAddWord.xaml
    /// </summary>
    public partial class PageAddWord : Page
    {
        public PageAddWord()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageAdmin());
        }

        private void ButtonAddWord_Click(object sender, RoutedEventArgs e)
        {
            Category category;
            string photoName = "default.jpg";
            if (!this.TextBoxAddPhoto.Text.Equals(""))
            {
                 BitmapImage image = new BitmapImage(new Uri(this.TextBoxAddPhoto.Text));


                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var fileStream = new System.IO.FileStream("..//..//Resources\\Pictures\\"+this.TextBoxName.Text+".jpg", System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
                photoName = this.TextBoxName.Text + ".jpg";

            }
            if (!this.ComboBoxCategory.Text.Equals("other"))
            {
                category = new Category(this.ComboBoxCategory.Text);
            }
            else
            {
                if (this.TextBoxCategoryOther.Text.Equals("")) return;
                category = new Category(this.TextBoxCategoryOther.Text);
            }

            (this.DataContext as WordVM).Words.Add(new Word()
            {
                Name = this.TextBoxName.Text,
                Description = this.TextBoxDescription.Text,
                CategoryWord = category,
                PhotoName = photoName
            }
            );
            ReadAndWrite.WriteWords((this.DataContext as WordVM).Words);
            Switcher.Switch(new PageAddWord());
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
            this.TextBoxAddPhoto.Text = filePath;
            ImageWord.Source = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
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

    }

}
