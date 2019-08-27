﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStab : MonoBehaviour
{
    public AudioSource knifeStabSound;
    private int baseDamage = 10;
    public float stabRate;
	private ParticleDecalPool particleDecalPool;
    
	private Animator anim;
	
	private int startShootHash = Animator.StringToHash("StartShoot");
	private int switchWeaponHash = Animator.StringToHash("SwitchWeapon");
	private int LowerHash = Animator.StringToHash("Lower");

    private void Start()
    {
	    particleDecalPool = GameObject.FindGameObjectWithTag("BloodParticles").GetComponent<ParticleDecalPool>();
	    anim = transform.GetComponentInParent<Animator>();
    }

    void Update()
    {
	    if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
	        anim.SetTrigger(startShootHash);
        } else if (Input.GetButtonUp("Fire1"))
        {
	        anim.SetBool(startShootHash, false);
        }
    }

	private void stab()
    {
        knifeStabSound.Play();

        int newDamage = 1 * Random.Range(1, 21);

        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out raycastHit, 0.6f, ~(1 << 2)))
        {
	        if (raycastHit.transform.gameObject.layer == 10)
	        {
		        raycastHit.transform.parent.GetComponent<Enemy>().Damage(newDamage);
                particleDecalPool.spawnBloodParticles(newDamage);
            }
        }
    }
}
