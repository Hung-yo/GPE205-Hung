using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float noiseVolume;
    public float decayRate;
    public float maxNoise;

    void Update()
    {
        // have noise slowly decay back to 0
        if (noiseVolume > 0f)
        {
            noiseVolume -= decayRate * Time.deltaTime;
            if (noiseVolume < 0f) noiseVolume = 0f;
        }

        noiseVolume = Mathf.Clamp(noiseVolume, 0f, maxNoise);
    }

    public void AddNoise(float amount)
    {
        noiseVolume = Mathf.Clamp(noiseVolume + amount, 0f, maxNoise);
    }
}
