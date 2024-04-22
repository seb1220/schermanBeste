using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace PA3N_5B
{
    public class Cell
    {
        public bool CorrectPosition { get; set; } = false;
        public bool WrongPosition { get; set; } = false;
        public bool Incorrect { get; set; } = true;
        public String Letter { get; set; } = "";

    }

    public partial class MainWindow : Window
    {
        GameManager gameManager;
        List<string> Words { get; set; }

        Statistics statistics;

        int guessNumber = 0;

        public ObservableCollection<Cell> Cells { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Cells = new ObservableCollection<Cell>();

            gameManager = new GameManager(Cells);

            Guesses.ItemsSource = Cells;

            LoadWordsFromXMLFile();

            NotGussedText.DataContext = statistics;
            FirstTryText.DataContext = statistics;
            SecondTryText.DataContext = statistics;
            ThirdTryText.DataContext = statistics;
            FourthTryText.DataContext = statistics;
            FifthTryText.DataContext = statistics;
            SixthTryText.DataContext = statistics;

        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameManager.StartGame();

            GuessesButton.IsEnabled = true;
            GuessInput.IsEnabled = true;

            guessNumber = 0;
        }

        private void GuessesButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuessInput.Text.Length != 5)
            {
                MessageBox.Show("Please enter a 5 letter word");
                return;
            }

            if (gameManager.State != State.WaitingForUserInput)
                return;

            Debug.WriteLine($"Guess: {GuessInput.Text}");
            Debug.WriteLine($"Word: {gameManager.Word}");
            Debug.WriteLine($"Guessnumber: {guessNumber}");

            string input = GuessInput.Text.ToLower();
            int wordIndex = 0;
            for (int i = 5 * guessNumber; i < 5 * guessNumber + 5; i++)
            {
                Debug.WriteLine("Setting Cell " + i + " to " + input[wordIndex].ToString());
                Cells[i].Letter = input[wordIndex].ToString();

                if (gameManager.Word[wordIndex].ToString().Equals(input[wordIndex].ToString()))
                {
                    Cells[i].CorrectPosition = true;
                }
                else if (gameManager.Word.Contains(input[wordIndex].ToString()))
                {
                    Cells[i].WrongPosition = true;
                }
                else
                {
                    Cells[i].Incorrect = true;
                }

                wordIndex++;

                GuessInput.Text = "";
            }

            guessNumber++;

            Guesses.Items.Refresh();

            if (input.Equals(gameManager.Word))
            {
                gameManager.Finish(true);
                statistics.AddData(gameManager.Word, guessNumber);
                return;
            }
            if (guessNumber > 5)
            {
                gameManager.Finish(false);
                statistics.AddData(gameManager.Word, -1);
                return;
            }
        }

        private void LoadWordsFromXMLFile()
        {

            if (System.IO.File.Exists("statistics.xml"))
            {
                XmlSerializer statisticsSerializer = new XmlSerializer(typeof(Statistics));
                using (FileStream reader = new FileStream("statistics.xml", FileMode.Open))
                {
                    statistics = (Statistics)statisticsSerializer.Deserialize(reader);
                }
                Debug.WriteLine($"Statistics Words Count: {statistics.Words.Count}");
            }
            else if (System.IO.File.Exists("Words.xml"))
            {
                XmlSerializer wordsSerializer = new XmlSerializer(typeof(List<string>));
                using (FileStream reader = new FileStream("Words.xml", FileMode.Open))
                {
                    Words = (List<string>)wordsSerializer.Deserialize(reader);
                }
                statistics = new Statistics(Words);
                Debug.WriteLine($"Words Count: {Words.Count}");
            }
            else
            {
                Debug.WriteLine("Creating new Field");
                Words = new List<string>();
                statistics = new Statistics(Words);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            XmlSerializer statisticsSerializer = new XmlSerializer(typeof(Statistics));

            using (FileStream writer = new FileStream("statistics.xml", FileMode.Create))
            {
                statisticsSerializer.Serialize(writer, statistics);
            }

            Debug.WriteLine("Safed Game and Field");
        }
    }

    public class Statistics : INotifyPropertyChanged
    {
        public List<string> Words { get; set; }
        public List<WordStatistics> Tries { get; set; }

        public int FirstTry
        {
            get
            {
                int firstTries = 0;
                foreach (WordStatistics stats in Tries)
                {
                    firstTries += stats.FirstTry;
                }
                return firstTries;
            }
        }
        public int SecondTry { get { int secondTries = 0; foreach (WordStatistics stats in Tries) { secondTries += stats.SecondTry; } return secondTries; } }
        public int ThirdTry
        {
            get
            {
                int tirdTries = 0; foreach (WordStatistics stats in Tries) { tirdTries += stats.ThirdTry; }
                return tirdTries;
            }
        }
        public int FourthTry
        {
            get
            {
                int tirdTries = 0; foreach (WordStatistics stats in Tries) { tirdTries += stats.FourthTry; }
                return tirdTries;
            }
        }
        public int FifthTry
        {
            get
            {
                int tirdTries = 0; foreach (WordStatistics stats in Tries) { tirdTries += stats.FifthTry; }
                return tirdTries;
            }
        }
        public int SixthTry
        {
            get
            {
                int sixthTries = 0; foreach (WordStatistics stats in Tries) { sixthTries += stats.SixthTry; }
                return sixthTries;
            }
        }
        public int NotGuessed
        {
            get
            {
                int notGuessed = 0; foreach (WordStatistics stats in Tries) { notGuessed += stats.NotGuessed; }
                return notGuessed;
            }
        }

        public Statistics(List<string> words)
        {
            Words = new List<string>();
            Tries = new List<WordStatistics>();

            for (int i = 0; i < words.Count; i++)
            {
                Words.Add(words[i]);
                Tries.Add(new WordStatistics(i));
            }
        }

        public Statistics()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddData(string word, int tryNumber)
        {
            int index = Words.IndexOf(word);

            switch (tryNumber)
            {
                case 1:
                    Tries[index].FirstTry++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstTry"));
                    break;
                case 2:
                    Tries[index].SecondTry++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SecondTry"));
                    break;
                case 3:
                    Tries[index].ThirdTry++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThirdTry"));
                    break;
                case 4:
                    Tries[index].FourthTry++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FourthTry"));
                    break;
                case 5:
                    Tries[index].FifthTry++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FifthTry"));
                    break;
                case 6:
                    Tries[index].SixthTry++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SixthTry"));
                    break;
                default:
                    Tries[index].NotGuessed++;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NotGuessed"));
                    break;
            }
        }
    }

    public class WordStatistics
    {
        public int WordIndex { get; set; }
        public int FirstTry { get; set; }
        public int SecondTry { get; set; }
        public int ThirdTry { get; set; }
        public int FourthTry { get; set; }
        public int FifthTry { get; set; }
        public int SixthTry { get; set; }
        public int NotGuessed { get; set; }

        public WordStatistics(int index)
        {
            WordIndex = index;

            FirstTry = 0;
            SecondTry = 0;
            ThirdTry = 0;
            FourthTry = 0;
            FifthTry = 0;
            SixthTry = 0;
            NotGuessed = 0;

        }

        public WordStatistics()
        {
        }
    }
}
