using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChorusTrainer.Core;

/// <summary>
/// Engine that runs a training session for a given song.
/// Listens to microphone (simulated), compares pitch to expected notes, and gives feedback.
/// </summary>
public class ChorusTrainerEngine
{
    private readonly Song _song;
    private readonly PitchAnalyzer _analyzer;
    private readonly Random _random = new();

    public ChorusTrainerEngine(Song song)
    {
        _song = song ?? throw new ArgumentNullException(nameof(song));
        _analyzer = new PitchAnalyzer();
    }

    /// <summary>
    /// Runs the training session asynchronously. Simulates real-time feedback.
    /// </summary>
    public async Task RunTrainingSessionAsync()
    {
        Console.WriteLine("Starting training session. Simulating microphone input...\n");

        double beatDuration = 60.0 / _song.Bpm; // seconds per beat

        for (int i = 0; i < _song.ChorusNotes.Count; i++)
        {
            string expectedNote = _song.ChorusNotes[i];
            double timing = _song.NoteTimings[i];

            // Wait for the note's timing (simulate real-time)
            await Task.Delay((int)(timing * beatDuration * 1000));

            // Simulate microphone input (random float array)
            float[] audioBuffer = new float[1024];
            for (int j = 0; j < audioBuffer.Length; j++)
            {
                audioBuffer[j] = (float)(_random.NextDouble() * 2.0 - 1.0);
            }

            string detectedNote = _analyzer.DetectPitch(audioBuffer);
            double cents = _analyzer.CentsDifference(detectedNote, expectedNote);

            // Provide feedback
            Console.Write($"Beat {i + 1}: Expected {expectedNote}, Detected {detectedNote} ");
            if (double.IsNaN(cents))
            {
                Console.WriteLine("[No pitch detected]");
            }
            else if (Math.Abs(cents) < 10.0)
            {
                Console.WriteLine($"[Perfect! ({cents:F1} cents)]");
            }
            else if (cents > 0)
            {
                Console.WriteLine($"[Sharp by {cents:F1} cents]");
            }
            else
            {
                Console.WriteLine($"[Flat by {Math.Abs(cents):F1} cents]");
            }
        }

        Console.WriteLine("\nTraining session complete!");
    }
}
