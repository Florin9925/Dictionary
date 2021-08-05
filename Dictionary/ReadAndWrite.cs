using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dictionary
{
    class ReadAndWrite
    {
        public static void WriteWords(SetCollection<Word> words)
        {
            var filename = @"D:\Info Unitbv 2020-2021\Semestrul II\MVP\Laborator\Dictionary\Dictionary\Resources\WordsFile.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = Directory.GetParent(currentDirectory).FullName;
            currentDirectory = Directory.GetParent(currentDirectory).FullName;

            var wordsFilePath = Path.Combine(currentDirectory, filename);

            XmlTextWriter writer = new XmlTextWriter(wordsFilePath, null);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("words");

            foreach (var word in words)
            {
                writer.WriteStartElement("word");

                writer.WriteStartAttribute("name");
                writer.WriteString(word.Name);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("description");
                writer.WriteString(word.Description);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("photoName");
                writer.WriteString(word.PhotoName);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("category");
                writer.WriteString(word.CategoryWord.Name);
                writer.WriteEndAttribute();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
        }
        public static SetCollection<Word> ReadWords()
        {
            SetCollection<Word> result = new SetCollection<Word>();

            var filename = @"D:\Info Unitbv 2020-2021\Semestrul II\MVP\Laborator\Dictionary\Dictionary\Resources\WordsFile.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = Directory.GetParent(currentDirectory).FullName;
            currentDirectory = Directory.GetParent(currentDirectory).FullName;

            var wordsFilePath = Path.Combine(currentDirectory, filename);

            XmlReader reader = XmlReader.Create(wordsFilePath);

            reader.ReadToFollowing("word");

            do
            {

                reader.MoveToAttribute("name");
                string name = reader.ReadContentAsString();

                reader.MoveToAttribute("description");
                string description = reader.ReadContentAsString();

                reader.MoveToAttribute("photoName");
                string photoNme = reader.ReadContentAsString();

                reader.MoveToAttribute("category");
                Category category = new Category(reader.ReadContentAsString());

                result.Add(new Word(name, description, category, photoNme));

            } while (reader.ReadToFollowing("word"));

            reader.Close();

            return result;
        }

    }
}