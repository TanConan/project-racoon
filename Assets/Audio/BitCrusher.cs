using UnityEngine;

public class BitCrusher : MonoBehaviour
{
    [Range(1, 32)] public float bitDepth = 8; // Simulate 8-bit audio
    
    void OnAudioFilterRead(float[] data, int channels)
    {
        float numLevels = Mathf.Pow(2, bitDepth);
        
        for (int i = 0; i < data.Length; i++)
        {
            // The "Crush" math:
            data[i] = Mathf.Floor(data[i] * numLevels) / numLevels;
        }
    }
}