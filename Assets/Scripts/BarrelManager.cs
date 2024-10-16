using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> imagesJumpscareBarrel = new List<GameObject>();
    [SerializeField] List<InteractBarrel> interactBarrels = new List<InteractBarrel>();

    private void Start()
    {
        foreach (GameObject img in imagesJumpscareBarrel)
        {
            int randomIndex = Random.Range(0, interactBarrels.Count);
            interactBarrels[randomIndex].imgJumpscare = img;
            interactBarrels[randomIndex].jumpscare = true;
            interactBarrels[randomIndex].key = false;
            interactBarrels.RemoveAt(randomIndex);
        }
    }
}
