using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class Word : INotifyPropertyChanged
    {
        public Word()
        {
            name = "unknown";
            description = "unknown";
            photoName = "default.jpg";
            CategoryWord = new Category();
        }

        public Word(string name, string description, Category category, string photoName = "default.jpg")
        {
            this.name = name;
            this.description = description;
            this.photoName = photoName;
            this.categoryWord = category;
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private Category categoryWord;
        public Category CategoryWord
        {
            get
            {
                return categoryWord;
            }
            set
            {
                categoryWord = value;
                NotifyPropertyChanged("Category");
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string photoName;
        public string PhotoName
        {
            get
            {
                return photoName;
            }
            set
            {
                photoName = value;
                NotifyPropertyChanged("PhotoName");
            }
        }

        public override bool Equals(object obj)
        {

            return (obj is Word) && name.ToLower().Equals((obj as Word).Name.ToLower());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return name;
        }
    }

    public class Category : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                categories.Add(value);
                NotifyPropertyChanged("Name");
            }
        }
        private static SetCollection<String> categories;
        public static SetCollection<String> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                // NotifyPropertyChanged("Categories");
            }
        }

        public Category()
        {
            categories = new SetCollection<string>();
            name = "unknown";
            Default();
        }

        public Category(string nameCategory)
        {
            categories = new SetCollection<string>();
            name = nameCategory.ToLower();
            Default();
            categories.Add(name);
        }

        private void Default()
        {
            categories.Add("fruits");
            categories.Add("vegetables");
            categories.Add("clothes");
            categories.Add("drinks");
            categories.Add("electronic");
            categories.Add("other");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WordVM
    {
        public SetCollection<Word> Words { get; set; }
        public Category CategoryWord { get; set; }

        public WordVM()
        {
            Words = new SetCollection<Word>();
            CategoryWord = new Category();
            Words = ReadAndWrite.ReadWords();
        }
    }

    public class SetCollection<T> : ObservableCollection<T>
    {
        public new void Add(T item)
        {
            if (Contains(item))
                return;

            base.Add(item);
        }

        public override string ToString()
        {
            return "empty";
        }
    }
}
