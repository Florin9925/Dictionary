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
    /// Interaction logic for PageEntertainment.xaml
    /// </summary>
    public partial class PageEntertainment : Page
    {
        private static int correctAnswer = 0;
        private List<Word> entertainmentWords;
        private bool button1 = false;
        private bool button2 = false;
        private bool button3 = false;
        public PageEntertainment()
        {
            InitializeComponent();
            correctAnswer = 0;
            entertainmentWords = new List<Word>();
            Random rnd = new Random();
            HashSet<int> rand = new HashSet<int>();
            while(rand.Count<5)
            {
                rand.Add(rnd.Next(0, (this.DataContext as WordVM).Words.Count));
            }
            foreach(var index in rand)
            {
                entertainmentWords.Add((this.DataContext as WordVM).Words[index]);
            }
            GenerateQuestion(0);
        }

        private void GenerateQuestion(int indexQuestion = 0)
        {
            this.TextBoxQuestion.Text = entertainmentWords[indexQuestion].Description;
            Random rnd = new Random();
            Word answer1 = entertainmentWords[indexQuestion];
            Word answer2 = entertainmentWords[indexQuestion];
            Word answer3 = entertainmentWords[indexQuestion];

            switch (rnd.Next(1, 4))
            {
                case 1:
                    button1 = true;
                    RadioButtonAnswer1.Content = entertainmentWords[indexQuestion].Name;
                    do
                    {
                        answer2 = (this.DataContext as WordVM).Words[rnd.Next(0, (this.DataContext as WordVM).Words.Count)];
                        answer3 = (this.DataContext as WordVM).Words[rnd.Next(0, (this.DataContext as WordVM).Words.Count)];
                    } while (entertainmentWords[indexQuestion].Equals(answer2)&& entertainmentWords[indexQuestion].Equals(answer3)&&answer2.Equals(answer3));
                    RadioButtonAnswer2.Content = answer2.Name;
                    RadioButtonAnswer3.Content = answer3.Name;
                    break;
                case 2:
                    button2 = true;
                    RadioButtonAnswer2.Content = entertainmentWords[indexQuestion].Name;

                    do
                    {
                        answer1 = (this.DataContext as WordVM).Words[rnd.Next(0, (this.DataContext as WordVM).Words.Count)];
                        answer3 = (this.DataContext as WordVM).Words[rnd.Next(0, (this.DataContext as WordVM).Words.Count)];
                    } while (entertainmentWords[indexQuestion].Equals(answer1) && entertainmentWords[indexQuestion].Equals(answer3) && answer1.Equals(answer3));
                    RadioButtonAnswer1.Content = answer1.Name;
                    RadioButtonAnswer3.Content = answer3.Name;

                    break;
                case 3:
                    button3 = true;
                    RadioButtonAnswer3.Content = entertainmentWords[indexQuestion].Name;

                    do
                    {
                        answer2 = (this.DataContext as WordVM).Words[rnd.Next(0, (this.DataContext as WordVM).Words.Count)];
                        answer1 = (this.DataContext as WordVM).Words[rnd.Next(0, (this.DataContext as WordVM).Words.Count)];
                    } while (entertainmentWords[indexQuestion].Equals(answer2) && entertainmentWords[indexQuestion].Equals(answer1) && answer2.Equals(answer1));
                    RadioButtonAnswer2.Content = answer2.Name;
                    RadioButtonAnswer1.Content = answer1.Name;

                    break;
                default:
                    break;
            }
        }

        private bool CheckAnswer()
        {
            if (RadioButtonAnswer1.IsChecked.Equals(button1) && button1 == true) return true;
            if (RadioButtonAnswer2.IsChecked.Equals(button2) && button2 == true) return true;
            if (RadioButtonAnswer3.IsChecked.Equals(button3) && button3 == true) return true;
            return false;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAnswer())
            {
                ++correctAnswer;
            }

            int numberQuestion = int.Parse(TextBoxQuestionNumber.Text);

            if(int.Parse(TextBoxQuestionNumber.Text) == int.Parse(TextBoxQuestionMaximNumber.Text))
            {
                MessageBox.Show(correctAnswer+" answers correct");
                Switcher.Switch(new PageMain());
                return;
            }

            button1 = false;
            button2 = false;
            button3 = false;

            GenerateQuestion(numberQuestion);
            ++numberQuestion;
            TextBoxQuestionNumber.Text = numberQuestion.ToString();
            if(numberQuestion == int.Parse( TextBoxQuestionMaximNumber.Text))
            {
                ButtonNext.Content = "Finish";
            }
            
            RadioButtonAnswer1.IsChecked = false;
            RadioButtonAnswer2.IsChecked = false;
            RadioButtonAnswer3.IsChecked = false;
        }
    }
}
