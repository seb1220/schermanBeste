namespace PA3_Klauninger;

public class Logger(string name) {
    public string Name { get; } = name;

    public void Info(string message) {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] info [{Name}] {message}");
    }
}