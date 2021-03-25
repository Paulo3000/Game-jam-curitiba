using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class GameController : MonoBehaviour
{
    public static int numOfBrokenObjs;
    public static int numOfRepairedObjs;

    public static float howMuchFixed;
    public static float playerHealth;
    public static float maxHealth;

    public float saturation = 0f;

    public static bool hasWon = false;
    public GameObject winMessage;

    ColorGrading colorGradingLayer = null;
    public TextMeshProUGUI howMuchFixedText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI innerMonstersText;

    public AudioClip battleSong;
    public AudioClip winSong;
    public AudioSource audioSource;

    bool shouldRiseVolume = false;

    float volume = 1;

    private void Awake()
    {
        numOfBrokenObjs = 0;
        numOfRepairedObjs = 0;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = battleSong;
        audioSource.Play();
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
    }

    private void Update()
    {
        print(hasWon);

        audioSource.volume = volume;

        if (hasWon)
        {
            print("test");

            winMessage.SetActive(true);

            if (audioSource.volume <= 1f && !shouldRiseVolume)
            {
                volume -= Time.deltaTime * 0.5f;
            }
            
            if (audioSource.volume <= 0f && !shouldRiseVolume)
            {
                shouldRiseVolume = true;
                audioSource.clip = winSong;
                audioSource.Play();
            }


            if (audioSource.volume <= 1f && shouldRiseVolume)
            {
                volume += Time.deltaTime * 0.5f;
            }

        }

        howMuchFixedText.text = "Boxes fixed: " + howMuchFixed + "%";
        //healthText.text = "Health: " + Mathf.Round(playerHealth);
        float healthPercentage = playerHealth / maxHealth;
        if (healthPercentage >= 1.0f)
        {
            healthText.text = "Health: " + "<color=#7CFC00>||||||||||";
        }
        else if (healthPercentage > 0.9f)
        {
            healthText.text = "Health: " + "<color=#7CFC00>|||||||||";
        }
        else if(healthPercentage > 0.8f)
        {
            healthText.text = "Health: " + "<color=#7CFC00>||||||||";
        }
        else if(healthPercentage > 0.7f)
        {
            healthText.text = "Health: " + "<color=#7CFC00>|||||||";
        }
        else if(healthPercentage > 0.6f)
        {
            healthText.text = "Health: " + "<color=#ffff00>||||||";
        }
        else if(healthPercentage > 0.5f)
        {
            healthText.text = "Health: " + "<color=#ffff00>|||||";
        }
        else if(healthPercentage > 0.4f)
        {
            healthText.text = "Health: " + "<color=#ffff00>||||";
        }
        else if (healthPercentage > 0.3f)
        {
            healthText.text = "Health: " + "<color=#ff0000>|||";
        }
        else if (healthPercentage > 0.2f)
        {
            healthText.text = "Health: " + "<color=#ff0000>||";
        }
        else if (healthPercentage > 0f)
        {
            healthText.text = "Health: " + "<color=#ff0000>|";
        }
        else if (healthPercentage <= 0f)
        {
            healthText.text = "Health: " + "";
        }

        innerMonstersText.text = "";


        colorGradingLayer.enabled.value = true;

        howMuchFixed = Mathf.Round(
            (float)numOfRepairedObjs / (float)numOfBrokenObjs * 100f
            );

        if (numOfRepairedObjs == numOfBrokenObjs)
        {
            colorGradingLayer.saturation.value = 6f;
            hasWon = true;
        } else
        {
            colorGradingLayer.saturation.value = -80f + (((float)numOfRepairedObjs / (float)numOfBrokenObjs) * 60f);
        }
    }
}
