using ChorusTrainer.Core;

namespace ChorusTrainer;

/// <summary>
/// Main entry point for the Chorus Trainer application.
/// Trains users to sing harmonies by analyzing pitch and timing.
/// </summary>
internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to Chorus Trainer!");
        Console.WriteLine("Select a song to practice:");

        var songLibrary = new SongLibrary();
        var songs = songLibrary.GetAllSongs();

        for (int i = 0; i < songs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {songs[i].Title} by {songs[i].Artist}");
        }

        Console.Write("Enter song number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > songs.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        var selectedSong = songs[choice - 1];
        Console.WriteLine($"\nNow training on: {selectedSong.Title}");
        Console.WriteLine("Sing into your microphone. Press Ctrl+C to stop.\n");

        var trainer = new ChorusTrainerEngine(selectedSong);
        await trainer.RunTrainingSessionAsync();
    }
}
