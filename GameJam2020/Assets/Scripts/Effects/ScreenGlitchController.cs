using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScreenGlitching))]
[ExecuteInEditMode]
public class ScreenGlitchController : MonoBehaviour
{
    private ScreenGlitching[] screenGlitchers;
    private HealthComponent playerHealth;
    private GameObject player;
    private PopupCreator popupCreator;
    private GameObject canvas;

    private GameObject deathScreen = null;

    [SerializeField]
    private GameObject gameOverScreenPrefab;

    [SerializeField]
    private bool playerHealthDependant = false;
    [SerializeField]
    [Range(0, 1)]
    private float glitch = 0;

    //Default values
    [SerializeField]
    private List<float> defaultStripeAmounts = new List<float>();
    [SerializeField]
    private List<float> defaultSeedSpeeds = new List<float>();
    [SerializeField]
    private List<float> defaultDistances = new List<float>();

    private void OnEnable()
    {
        screenGlitchers = GetComponents<ScreenGlitching>();
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<HealthComponent>();
        popupCreator = GameObject.Find("PopupSpawner").GetComponent<PopupCreator>();
        canvas = GameObject.Find("Canvas");

        if (playerHealthDependant)
        {
            player.GetComponent<Player.Player>().DamagedPlayer += UpdateGlitching;
            Player.Player.OnPlayerDeath += DeadPlayer;
        }

        if (screenGlitchers != null && screenGlitchers.Length > 0)
        {
            while (defaultStripeAmounts.Count < screenGlitchers.Length)
            {
                defaultStripeAmounts.Add(0);
            }

            while (defaultSeedSpeeds.Count < screenGlitchers.Length)
            {
                defaultSeedSpeeds.Add(0);
            }

            while (defaultDistances.Count < screenGlitchers.Length)
            {
                defaultDistances.Add(0);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < screenGlitchers.Length; i++)
        {
            UpdateStripeAmount(i, glitch);
            UpdateSeedSpeed(i, glitch);
            UpdateDistances(i, glitch);
        }
    } 

    private void UpdateGlitching(float currentHealth)
    {
        glitch = (1 - Mathf.Clamp01(currentHealth / playerHealth.GetMaxHealth())) * 0.5f;
    }

    private void UpdateStripeAmount(int _scriptIndex, float glitchValue)
    {
        float val = 0;
        if (glitchValue < 0.5) 
            val = Mathf.Lerp(0, defaultStripeAmounts[_scriptIndex], glitchValue * 2);
        else
            val = Mathf.Lerp(defaultStripeAmounts[_scriptIndex], 1, (glitchValue - 0.5f) * 2);
        screenGlitchers[_scriptIndex].GetMaterial().SetFloat("_StripesAmount", val);
    }

    private void UpdateSeedSpeed(int _scriptIndex, float glitchValue)
    {
        float val = 0;
        if (glitchValue < 0.5)
            val = Mathf.Lerp(0, defaultSeedSpeeds[_scriptIndex], glitchValue * 2);
        else
            val = Mathf.Lerp(defaultSeedSpeeds[_scriptIndex], 1, (glitchValue - 0.5f) * 2);
        screenGlitchers[_scriptIndex].GetMaterial().SetFloat("_NewSeedSpeed", val);
    }

    private void UpdateDistances(int _scriptIndex, float glitchValue)
    {
        float val = 0;
        if (glitchValue < 0.5)
            val = Mathf.Lerp(0, defaultDistances[_scriptIndex], glitchValue * 2);
        else
            val = defaultDistances[_scriptIndex];
        screenGlitchers[_scriptIndex].GetMaterial().SetFloat("_StripesDistance", val);
    }




    //Death
    private void DeadPlayer()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Player.Player>().DamagedPlayer -= UpdateGlitching;
        StartCoroutine(GlitchOut());

        deathScreen = Instantiate(gameOverScreenPrefab, canvas.transform);
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(deathScreen);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //GO TO MAIN MENU
    }

    private IEnumerator GlitchOut()
    {
        while(glitch < 0.8f)
        {
            glitch += 0.01f;
            yield return null;
        }

        glitch = 1.0f;
        yield return null;
    }

    private void OnDisable()
    {
        Player.Player.OnPlayerDeath -= DeadPlayer;
    }
}
