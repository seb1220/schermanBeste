using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Windows;
using DataModel;
using LinqToDB;
using static PA3_Klauninger.TransferManager;

namespace PA3_Klauninger;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : INotifyPropertyChanged {
    private readonly WordsDb _db = new(new DataOptions<WordsDb>(
        new DataOptions().UseSQLite("Data Source=../../../words.db") // relative to executable in build dir
    ));

    private readonly Logger _logger = new("MAIN");
    private int _attempts;
    private TransferManager _transfer;
    private string _word = "";
    private string _userWord = "";

    public MainWindow() {
        InitializeComponent();
        UpdateStats();

        DataContext = this;
        GuessButton.IsEnabled = false;

        var connected = false;
        while (!connected)
            try {
                _transfer =
                    new TransferManager(new TcpClient("localhost", 12345), OnReceiveMessage, OnDisconnect);
                connected = true;
                _logger.Info("Connected to Server!");
            }
            catch (SocketException e) {
                _logger.Info("Could not connect to server: " + e.Message);

                var result =
                    MessageBox.Show("Could not connect to server. Do you want to retry?", "Error",
                        MessageBoxButton.YesNo);
                if (result != MessageBoxResult.No) {
                    continue;
                }

                Application.Current.Shutdown();
                break;
            }
    }

    public ObservableCollection<Cell> Letters { get; set; } =  [];
    public ObservableCollection<string> Suggestions { get; set; } =  [];
    public ObservableCollection<string> Statistic { get; set; } =  [];


    public string UserWord {
        get => _userWord.ToLower();
        set {
            if (_userWord == value)
                return;
            _userWord = value;
            NotifyPropertyChanged(nameof(UserWord));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnReceiveMessage(MSG msg) {
        if (msg.Word == null) {
            return;
        }

        _word = msg.Word;
        _attempts = 0;
        Dispatcher.Invoke(() => { Letters.Clear(); });
    }

    private void OnDisconnect() {
        _logger.Info("Disconnected from Server!");
        var connected = false;
        while (!connected)
            try {
                _transfer =
                    new TransferManager(new TcpClient("localhost", 12345), OnReceiveMessage, OnDisconnect);
                connected = true;
                _logger.Info("Connected to Server!");
            }
            catch (SocketException e) {
                _logger.Info("Disconnected from the server!");

                var result =
                    MessageBox.Show("Lost connection to the server. Do you want to reestablish a connection?",
                        "Error",
                        MessageBoxButton.YesNo);
                if (result != MessageBoxResult.No) continue;

                Application.Current.Shutdown();
                break;
            }
    }


    private void OnNewGame(object sender, RoutedEventArgs e) {
        GuessButton.IsEnabled = true;
        _transfer.Send(new MSG {
            NewGame = true
        });
    }

    private void OnGuess(object sender, RoutedEventArgs e) {
        _attempts++;
        if (UserWord.Length != 5) {
            MessageBox.Show("Word must be 5 characters long!");
            return;
        }

        for (var i = 0; i < 5; i++)
            if (!char.IsLetter(UserWord[i])) {
                MessageBox.Show("Word must contain only letters!");
                return;
            }

        for (var i = 0; i < 5; i++)
            if (_word.ToLower().Contains(UserWord[i])) {
                Letters.Add(_word.ToLower()[i] == UserWord[i]
                    ? new Cell { Letter = UserWord[i].ToString(), CorrectPosition = true }
                    : new Cell { Letter = UserWord[i].ToString(), WrongPosition = true });
            }
            else {
                Letters.Add(new Cell { Letter = UserWord[i].ToString(), Incorrect = true });
            }

        UpdateSuggestions();
        if (UserWord == _word.ToLower()) {
            switch (_attempts) {
                case 1:
                    _db.Words.Where(w => w.Word1 == _word).Set(w => w.FirstGuess, w => w.FirstGuess + 1).Update();
                    break;
                case 2:
                    _db.Words.Where(w => w.Word1 == _word).Set(w => w.SecondGuess, w => w.SecondGuess + 1).Update();
                    break;
                case 3:
                    _db.Words.Where(w => w.Word1 == _word).Set(w => w.ThirdGuess, w => w.ThirdGuess + 1).Update();
                    break;
                case 4:
                    _db.Words.Where(w => w.Word1 == _word).Set(w => w.FourthGuess, w => w.FourthGuess + 1).Update();
                    break;
                case 5:
                    _db.Words.Where(w => w.Word1 == _word).Set(w => w.FifthGuess, w => w.FifthGuess + 1).Update();
                    break;
                case 6:
                    _db.Words.Where(w => w.Word1 == _word).Set(w => w.SixthGuess, w => w.SixthGuess + 1).Update();
                    break;
            }

            MessageBox.Show("You won!");
            Suggestions.Clear();
            GuessButton.IsEnabled = false;
        }
        else {
            if (_attempts < 6) {
                return;
            }

            _db.Words.Where(w => w.Word1 == _word).Set(w => w.NoGuessed, w => w.NoGuessed + 1).Update();

            MessageBox.Show("You lost! The word was: " + _word);
            Suggestions.Clear();

            GuessButton.IsEnabled = false;
        }

        UpdateStats();
    }

    private void UpdateStats() {
        Statistic.Clear();
        Statistic.Add("Failed: " + _db.Words.Sum(w => w.NoGuessed));
        Statistic.Add("1st try: " + _db.Words.Sum(w => w.FirstGuess));
        Statistic.Add("2nd try: " + _db.Words.Sum(w => w.SecondGuess));
        Statistic.Add("3rd try: " + _db.Words.Sum(w => w.ThirdGuess));
        Statistic.Add("4th try: " + _db.Words.Sum(w => w.FourthGuess));
        Statistic.Add("5th try: " + _db.Words.Sum(w => w.FifthGuess));
        Statistic.Add("6th try: " + _db.Words.Sum(w => w.SixthGuess));
    }

    private void UpdateSuggestions() {
        // create lists that store correct, incorrect and wrong position letters
        var wrongPositionLetters = new List<char>();
        var correctLetters = new Dictionary<int, char>();
        var incorrectLetters = new List<char>();

        foreach (var cell in Letters) {
            if (cell.WrongPosition && !wrongPositionLetters.Contains(cell.Letter[0]))
                wrongPositionLetters.Add(cell.Letter[0]);

            if (cell.CorrectPosition && !correctLetters.ContainsValue(cell.Letter[0]))
                correctLetters.Add(Letters.IndexOf(cell) % 5, cell.Letter[0]);

            if (cell.Incorrect && !incorrectLetters.Contains(cell.Letter[0])) incorrectLetters.Add(cell.Letter[0]);
        }

        // select only words that contain all letters where the position may be wrong and none of the incorrect letters
        var words = _db.Words
            .Where(w => wrongPositionLetters.All(l => w.Word1.Contains(l)))
            .Where(w => incorrectLetters.All(l => !w.Word1.Contains(l)));

        Suggestions.Clear();
        foreach (var word in words) {
            var valid = correctLetters.All(letter => word.Word1[letter.Key] == letter.Value);

            if (valid) {
                Suggestions.Add(word.Word1);
            }
        }
    }

    private void NotifyPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}