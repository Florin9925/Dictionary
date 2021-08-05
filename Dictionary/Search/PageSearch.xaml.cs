using Dictionary;
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
    /// Interaction logic for PageSearch.xaml
    /// </summary>
    public partial class PageSearch : Page
    {
        public PageSearch()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageMain());
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private Word selectedWord = null;

        public Word SelectedWord
        {
            get
            {
                return selectedWord;
            }
            set
            {
                selectedWord = value;
            }
        }


        public class Data
        {
            public static List<Word> getData()
            {
                List<Word> data = new List<Word>();
                WordVM words = new WordVM();
                foreach (var obj in words.Words)
                {
                    data.Add(obj);
                }

                return data;
            }
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool isFound = false;

            var boder = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Data.getData();

            string find = (sender as TextBox).Text;
            if (find.Length == 0)
            {
                resultStack.Children.Clear();
                resultStack.Background.Opacity = 0;
                boder.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Enter)
            {
                textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            else
            {
                resultStack.Background.Opacity = 0.5;
                boder.Visibility = Visibility.Visible;
            }

            resultStack.Children.Clear();

            foreach (var obj in data)
            {
                if (obj.Name.ToLower().StartsWith(find.ToLower()))
                {
                    if (ComboBoxCategory.Text.Equals(obj.CategoryWord.Name.ToLower()) || ComboBoxCategory.Text.Equals("other"))
                    {
                        AddItem(obj);
                        int wordsMatching = resultStack.Children.Count;
                        if (wordsMatching <= 4)
                        {
                            resultStack.Height = ScrollViewer.Height = BorderAuto.Height = wordsMatching * 30;
                        }
                        isFound = true;
                    }
                }
            }

            if (!isFound)
            {
                resultStack.Background.Opacity = 0;
            }

        }

        public void AddItem(Word obj)
        {
            Label block = new Label();

            block.HorizontalContentAlignment = HorizontalAlignment.Center;
            block.Content = obj;
            block.FontSize = 16;
            block.Height = 30;
            block.Foreground = new SolidColorBrush(Colors.Black);
            block.VerticalAlignment = VerticalAlignment.Center;
            block.HorizontalContentAlignment = HorizontalAlignment.Left;

            block.MouseLeftButtonDown += (sender, e) =>
            {
                Label lb = sender as Label;
                textBox.Text = lb.Content.ToString();
                SelectedWord = lb.Content as Word;
                lb.Background = new SolidColorBrush(Colors.Transparent);
            };

            block.MouseEnter += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.WhiteSmoke);
                lb.Foreground = new SolidColorBrush(Colors.Black);
            };

            block.MouseLeave += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.Transparent);
                lb.Foreground = new SolidColorBrush(Colors.Black);
            };

            resultStack.Children.Add(block);
        }


        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            //resultStack.Children.Clear();
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            resultStack.Children.Clear();
            resultStack.Background.Opacity = 0;


            Word currentItem = new Word();
            currentItem.Name = textBox.Text;
            foreach(var index in Data.getData())
            {
                if (index.Equals(currentItem))
                {
                    this.TextBoxName.Text = index.Name;
                    this.TextBoxDescription.Text = index.Description;
                    this.TextBoxCategory.Text = index.CategoryWord.Name;
                    ImageWord.Source = new BitmapImage(new Uri(@"D:\\Info Unitbv 2020-2021\\Semestrul II\\MVP\\Laborator\\Dictionary\\Dictionary\\Resources\\Pictures\\" + index.PhotoName, UriKind.RelativeOrAbsolute));
                    break;
                }
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            resultStack.Background = new SolidColorBrush(Colors.AntiqueWhite);
            resultStack.Background.Opacity = 0;
        }

        
    }
}
