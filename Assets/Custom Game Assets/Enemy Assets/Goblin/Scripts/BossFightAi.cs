using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightAi : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject weaponApearParticles;
    public Transform firepoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WeaponVanish(float delayTimer)
    {
        StartCoroutine(WeaponVanishEnumerator(delayTimer));
    }
    public void WeaponApear(float delayTimer)
    {
        StartCoroutine(WeaponApearEnumerator(delayTimer));
    }
    IEnumerator WeaponVanishEnumerator(float delayTimer)
    {
        yield return new WaitForSeconds(delayTimer);
        Weapon.SetActive(false);

    }
    IEnumerator WeaponApearEnumerator(float delayTimer)
    {
        yield return new WaitForSeconds(delayTimer);
        Destroy(Instantiate(weaponApearParticles, firepoint),5f);
        Weapon.SetActive(true);

    }
}
