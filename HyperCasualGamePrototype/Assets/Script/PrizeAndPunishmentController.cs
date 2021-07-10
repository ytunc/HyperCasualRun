using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeAndPunishmentController : MonoBehaviour
{
    [SerializeField] private GameObject particleObject;



    public void StartParticle()
    {
        particleObject.gameObject.SetActive(true);
        StartCoroutine(nameof(StopParticle));
    }

    private IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(1);
        particleObject.gameObject.SetActive(false);
    }

}
