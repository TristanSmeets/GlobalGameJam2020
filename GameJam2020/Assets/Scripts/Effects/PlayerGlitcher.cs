using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlitcher : MonoBehaviour
{
    private Renderer renderer;
    private Player.Player player;

    [SerializeField]
    private bool glitch = false;
    float glitchVal = 0;
    [SerializeField]
    private float glitchSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        player = GameObject.Find("Player").GetComponent<Player.Player>();
        player.DamagedPlayer += StartGlitch;
    }

    // Update is called once per frame
    void Update()
    {
        if(glitch)
        {
            glitchVal = Mathf.Lerp(glitchVal, 1, Time.deltaTime * glitchSpeed);

            if(glitchVal >= 0.99)
            {
                glitch = false;
                glitchVal = 0;
            }
        }
        renderer.material.SetFloat("_Value", glitchVal);
    }

    public void StartGlitch(float currentPlayerHealth)
    {
        glitch = true;
    }
}
