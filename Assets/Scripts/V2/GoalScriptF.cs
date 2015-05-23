using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalScriptF : MonoBehaviour {

    private GameManagerF manager;

    //feedbacks
    private GuiEffects guiEffectsScript;


	// Use this for initialization
	void Start () {
	    manager = GameControllerF.getManager();
        guiEffectsScript = GameObject.Find("CanvasFeedbacks").GetComponent<GuiEffects>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other) // old was ontriggerenter, changé pour détecter but d'une balle sur les mesh de buts du stade, par contre ça ne détecte pas les niveks, donc utilisation des anciens mesh colliders GoalMonsterRed et GoalMonsterBlue avec un nouveau script
    {
        //PlayerControllerF player = other.gameObject.GetComponent<PlayerControllerF>();

        

        if (other.gameObject.tag == "Monster")
        {
            if (!other.gameObject.GetComponent<MonsterControllerF>().IsMonsterForm())
            {
                //feedbacks goal balle
                Camera.main.GetComponent<CameraShake>().shake(0.8f, 0.6f, 1.0f);
                guiEffectsScript.flashGoal(tag);

                manager.AddScore(tag);
                //tp au centre + invul de 3 secondes
                other.gameObject.GetComponent<MonsterControllerF>().Respawn();
            }
        }

        //if (player != null)
        //{
        //    if (player.IsProjectionInGoal())
        //    {
        //        //feedbacks goal joueur
        //        Camera.main.GetComponent<CameraShake>().shake(1, 1, 1);
        //        manager.AddScore(tag);
        //        player.Respawn();

        //    }
        //    else
        //    {
        //        player.callStun(player.stunGoal);
        //        Vector3 dirImpact;

        //        player.PlayRandomSound(AbstractSound.Action.EjectBut);

        //        if (other.transform.position.x > transform.position.x)
        //            dirImpact = new Vector3(-1, 0.1f, 0);
        //        else
        //            dirImpact = new Vector3(1, 0.1f, 0);
        //        player.AddImpact(dirImpact * 200);
        //    }
        //}
    }
}
