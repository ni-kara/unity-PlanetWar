using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftBehaviour : MonoBehaviour
{
    private GameObject planet;
    private int speed;
    private Vector3 directionRot;
    private float refreshΙnstructionTimer;
    void Awake() =>    
        FindPlanet();
    
    // Start is called before the first frame update
    void Start()
    {
        SetSpeed();
        SetDirection();

        StartCoroutine(RefreshSpacecraftInstruction());
    }

    // Update is called once per frame
    void Update()=>  
        SpacecraftRotate();
    
    // Find the planet
    private void FindPlanet() => planet = GameObject.Find("Planet");
    // Speed Spacecraft
    private void SetSpeed() => speed = Random.Range(20, 80);
    // Direction Rotate Spacecraft
    private void SetDirection() => directionRot = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    // How long time is refreshed speed and direction
    private void SetRefreshInstructionTimer() => refreshΙnstructionTimer = Random.Range(10, 16);
    // Rotate Spacecraft
    private void SpacecraftRotate() => gameObject.transform.RotateAround(planet.transform.position, directionRot, speed * Time.deltaTime);
    // Change speed and direction of Spacecraft
    private IEnumerator RefreshSpacecraftInstruction() 
    {
        for (;;)
        {
            SetRefreshInstructionTimer();
            yield return new WaitForSeconds(refreshΙnstructionTimer);

            SetSpeed();
            SetDirection();
        }
    }
}
