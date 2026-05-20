using System;
using System.Collections.Generic;
using System.Linq;

namespace ChorusTrainer.Core;

/// <summary>
/// Analyzes pitch from microphone input using a simple FFT-based approach.
/// For demonstration, uses a simulated pitch detection (real impl would use NAudio, etc.).
/// </summary>
public class PitchAnalyzer
{
    private readonly Dictionary<string, double> _noteFrequencies;

    public PitchAnalyzer()
    {
        // Standard A4 = 440 Hz, compute others via equal temperament
        _noteFrequencies = new Dictionary<string, double>();
        string[] notes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        for (int octave = 2; octave <= 6; octave++)
        {
            for (int i = 0; i < 12; i++)
            {
                int semitoneIndex = (octave - 4) * 12 + i; // relative to A4
                double freq = 440.0 * Math.Pow(2.0, semitoneIndex / 12.0);
                _noteFrequencies[notes[i] + octave] = freq;
            }
        }
    }

    /// <summary>
    /// Simulates pitch detection from a real-time audio buffer.
    /// Returns the detected note name (e.g., "A4") or null if not detected.
    /// </summary>
    /// <param name="audioSamples">Float array of audio samples (e.g., from microphone).</param>
    /// <returns>Detected note as string, or "Unknown".</returns>
    public string DetectPitch(float[] audioSamples)
    {
        // In a real project, this would perform FFT and peak detection.
        // Here we simulate by returning a random nearby note for demonstration.
        if (audioSamples.Length == 0)
            return "Unknown";

        // Simulated: find the closest note to a random frequency based on sample mean
        double simulatedFreq = 400.0 + (new Random().NextDouble() * 200.0); // 400-600 Hz
        return FindClosestNote(simulatedFreq);
    }

    /// <summary>
    /// Finds the note name closest to a given frequency.
    /// </summary>
    private string FindClosestNote(double frequency)
    {
        string closestNote = "Unknown";
        double minDiff = double.MaxValue;

        foreach (var kvp in _noteFrequencies)
        {
            double diff = Math.Abs(frequency - kvp.Value);
            if (diff < minDiff)
            {
                minDiff = diff;
                closestNote = kvp.Key;
            }
        }

        return closestNote;
    }

    /// <summary>
    /// Returns the frequency of a given note (e.g., "A4" -> 440.0).
    /// </summary>
    public double GetFrequency(string note)
    {
        return _noteFrequencies.TryGetValue(note, out double freq) ? freq : 0.0;
    }

    /// <summary>
    /// Calculates the pitch difference in cents between a detected note and a target note.
    /// Positive means detected is sharp, negative means flat.
    /// </summary>
    public double CentsDifference(string detectedNote, string targetNote)
    {
        if (!_noteFrequencies.ContainsKey(detectedNote) || !_noteFrequencies.ContainsKey(targetNote))
            return double.NaN;

        double freqDetected = _noteFrequencies[detectedNote];
        double freqTarget = _noteFrequencies[targetNote];
        return 1200.0 * Math.Log2(freqDetected / freqTarget);
    }
}
