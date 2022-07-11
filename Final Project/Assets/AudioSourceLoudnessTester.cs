using UnityEngine;

public class AudioSourceLoudnessTester : MonoBehaviour
{
	public AudioSource audioSource;
	public float updateStep;
	public int sampleDataLength = 1024;

	private float currentUpdateTime;

	public float clipLoudness;
	private float[] clipSampleData;

	public GameObject cube;
	public GameObject cube1;
	public GameObject cube2;
	public GameObject cube3;
	public float sizeFactor;

	public float minSize;
	public float maxSize = 500;

	// Use this for initialization
	private void Awake()
	{
		clipSampleData = new float[sampleDataLength];
	}

	// Update is called once per frame
	private void Update()
	{
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep)
		{
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
			clipLoudness = 0f;
			foreach (var sample in clipSampleData)
			{
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for

			clipLoudness *= sizeFactor;
			clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
			cube.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
			cube1.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
			cube2.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
			cube3.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
		}
	}
}