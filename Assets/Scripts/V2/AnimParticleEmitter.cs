using UnityEngine;
using System.Collections;

public class AnimParticleEmitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void EmitParticle(){

        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.name == "Course") ps.Play();
        }
	}
	public void EmitCryParticle(){
		
		foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
		{
			if (ps.name == "Larme" || ps.name == "LarmeGauche") ps.Play();
		}
	}
}
