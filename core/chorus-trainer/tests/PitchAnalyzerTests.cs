using Xunit;
using ChorusTrainer.Core;

namespace ChorusTrainer.Tests;

/// <summary>
/// Unit tests for the PitchAnalyzer class.
/// </summary>
public class PitchAnalyzerTests
{
    [Fact]
    public void GetFrequency_A4_Returns440()
    {
        // Arrange
        var analyzer = new PitchAnalyzer();

        // Act
        double freq = analyzer.GetFrequency("A4");

        // Assert
        Assert.Equal(440.0, freq, 1); // Allow small floating point tolerance
    }

    [Fact]
    public void GetFrequency_C4_ReturnsApproximately261Point63()
    {
        // Arrange
        var analyzer = new PitchAnalyzer();

        // Act
        double freq = analyzer.GetFrequency("C4");

        // Assert
        Assert.Equal(261.63, freq, 1);
    }

    [Fact]
    public void CentsDifference_SameNote_ReturnsZero()
    {
        // Arrange
        var analyzer = new PitchAnalyzer();

        // Act
        double cents = analyzer.CentsDifference("A4", "A4");

        // Assert
        Assert.Equal(0.0, cents, 1);
    }

    [Fact]
    public void CentsDifference_A4ToA5_Returns1200()
    {
        // Arrange
        var analyzer = new PitchAnalyzer();

        // Act
        double cents = analyzer.CentsDifference("A5", "A4"); // A5 is 1200 cents above A4

        // Assert
        Assert.Equal(1200.0, cents, 1);
    }

    [Fact]
    public void DetectPitch_EmptyArray_ReturnsUnknown()
    {
        // Arrange
        var analyzer = new PitchAnalyzer();
        float[] empty = Array.Empty<float>();

        // Act
        string result = analyzer.DetectPitch(empty);

        // Assert
        Assert.Equal("Unknown", result);
    }

    [Fact]
    public void DetectPitch_ValidArray_ReturnsNote()
    {
        // Arrange
        var analyzer = new PitchAnalyzer();
        float[] samples = new float[1024];
        for (int i = 0; i < samples.Length; i++)
            samples[i] = 0.5f; // Simulated constant signal

        // Act
        string result = analyzer.DetectPitch(samples);

        // Assert
        Assert.NotEqual("Unknown", result);
    }
}
