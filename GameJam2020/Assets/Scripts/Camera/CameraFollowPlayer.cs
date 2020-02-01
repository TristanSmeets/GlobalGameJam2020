using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 defaultOffset;
    private Vector3 adjustedOffset;
    private bool screenShake = false;

    [SerializeField]
    private float cameraSpeed = 10;
    [SerializeField]
    private bool shake = false;
    [SerializeField]
    private float shakeTime = 1.0f;
    [SerializeField]
    private float screenShakeSpeed = 25;
    [SerializeField]
    private float screenShakeDistance = 1;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        defaultOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveOffset();
        MoveCamera();
    }

    private void OnValidate()
    {
        if(shake)
        {
            shake = false;
            ShakeCamera(shakeTime);
        }
    }

    private void MoveOffset()
    {
        if(screenShake)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)).normalized * screenShakeDistance;
            adjustedOffset = Vector3.Lerp(adjustedOffset, defaultOffset + randomOffset, Time.fixedDeltaTime * screenShakeSpeed);
        }
        else
        {
            adjustedOffset = defaultOffset;
        }
    }

    private void MoveCamera()
    {
        Vector3 intendedPosition = player.transform.position + adjustedOffset;

        Vector3 lerpedPos = Vector3.Lerp(transform.position, intendedPosition, Time.fixedDeltaTime * cameraSpeed);
        transform.position = new Vector3(lerpedPos.x, transform.position.y, lerpedPos.z);
    }

    public void ShakeCamera(float seconds)
    {
        screenShake = true;
        StartCoroutine(ShakeCameraIE(seconds));
    }

    IEnumerator ShakeCameraIE(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        screenShake = false;
    }
}
