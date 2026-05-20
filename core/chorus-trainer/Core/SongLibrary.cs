using System.Collections.Generic;

namespace ChorusTrainer.Core;

/// <summary>
/// Represents a musical song with its harmony parts.
/// </summary>
public class Song
{
    /// <summary>Title of the song.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Artist or group name.</summary>
    public string Artist { get; set; } = string.Empty;

    /// <summary>List of harmony notes (e.g., "C4", "Eb4") for the chorus part.</summary>
    public List<string> ChorusNotes { get; set; } = new();

    /// <summary>Timing in beats for each note (relative to BPM).</summary>
    public List<double> NoteTimings { get; set; } = new();

    /// <summary>Beats per minute for the song.</summary>
    public int Bpm { get; set; } = 120;
}

/// <summary>
/// Provides a library of preloaded songs for chorus practice.
/// </summary>
public class SongLibrary
{
    private readonly List<Song> _songs;

    public SongLibrary()
    {
        _songs = new List<Song>
        {
            new Song
            {
                Title = "Bohemian Rhapsody",
                Artist = "Queen",
                Bpm = 72,
                ChorusNotes = new List<string> { "F4", "G4", "A4", "F4", "G4", "A4", "Bb4", "C5" },
                NoteTimings = new List<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5 }
            },
            new Song
            {
                Title = "Hallelujah",
                Artist = "Leonard Cohen",
                Bpm = 80,
                ChorusNotes = new List<string> { "C4", "E4", "G4", "A4", "G4", "E4", "C4" },
                NoteTimings = new List<double> { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 }
            },
            new Song
            {
                Title = "Africa",
                Artist = "Toto",
                Bpm = 93,
                ChorusNotes = new List<string> { "D4", "F#4", "A4", "B4", "A4", "F#4", "D4" },
                NoteTimings = new List<double> { 0.0, 0.75, 1.5, 2.25, 3.0, 3.75, 4.5 }
            }
        };
    }

    /// <summary>Returns all songs in the library.</summary>
    public List<Song> GetAllSongs() => _songs;

    /// <summary>Finds a song by its title (case-insensitive).</summary>
    public Song? FindByTitle(string title) =>
        _songs.Find(s => s.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));
}
