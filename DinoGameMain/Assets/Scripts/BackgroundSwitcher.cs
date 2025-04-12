using System.Collections;
using UnityEngine;

public class BackgroundSwitcher : MonoBehaviour
{
    public GameObject[] backgrounds; // Assign your 3 backgrounds here
    public float switchInterval = 10f; // How often to switch (in seconds)

    private int currentIndex = 0;

    void Start()
    {
        ShowOnlyCurrent();
        StartCoroutine(SwitchBackgrounds());
    }

    IEnumerator SwitchBackgrounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            currentIndex = (currentIndex + 1) % backgrounds.Length;
            ShowOnlyCurrent();
        }
    }

    void ShowOnlyCurrent()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == currentIndex);
        }
    }
}
