using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalScriptF : MonoBehaviour {

    private GameManagerF manager;

    //feedbacks
    private GuiEffects guiEffectsScript;
    private TextCommentaries commentariesScript;

    public float durationSwitch = 2.0f;
    public float durationReturnSwitch = 1.0f;
    private float timeSwitch;
    private bool returnSwitch = false;


	// Use this for initialization
	void Start () {
	    manager = GameControllerF.getManager();
        guiEffectsScript = GameObject.Find("CanvasFeedbacks").GetComponent<GuiEffects>();
        commentariesScript = GameObject.Find("Commentaries").GetComponent<TextCommentaries>();
	}
	
	// Update is called once per frame
	void Update () {
        if (returnSwitch)
        {
            float delayReturnSwitch = Mathf.Abs(((timeSwitch - Time.time) / durationReturnSwitch) - 1); ;
            GetComponent<Renderer>().material.SetFloat("_Switch_goal", Mathf.Lerp(1,0,delayReturnSwitch));

            if (delayReturnSwitch >= 1)
                returnSwitch = false;
        }
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

                GetComponent<Renderer>().material.SetFloat("_Switch_goal", 1);
                StartCoroutine(StopSwitchGoal());

                other.gameObject.GetComponent<SoundManager>().PlayEvent("VX_Balle_But",other.gameObject);



                PlayerControllerF striker = other.gameObject.GetComponent<MonsterControllerF>().GetStriker();

                bool goalInHisTeam = false;

                if (striker != null)
                {
                    if ((other.gameObject.GetComponent<MonsterControllerF>().GetStriker().team == GameControllerF.Team.Blu && tag == "TeamBlu") || (other.gameObject.GetComponent<MonsterControllerF>().GetStriker().team == GameControllerF.Team.Red && tag == "TeamRed"))
                    {
                        other.gameObject.GetComponent<SoundManager>().PlayEvent("VX_Niveks_ButGagnant", striker.gameObject);

                        goalInHisTeam = true;

                    }
                    else
                    {
                        other.gameObject.GetComponent<SoundManager>().PlayEvent("VX_Niveks_ButPerdant", striker.gameObject);
                    }

                    striker.marqueBut++;
                }

                if (goalInHisTeam)
                {
                    string tagCommentary = tag;
                    tagCommentary = (tagCommentary == "TeamBlu") ? "TeamRed" : "TeamBlu";
                    commentariesScript.WriteCommentary(tagCommentary, "playerOG");
                }
                else
                {
                    commentariesScript.WriteCommentary(tag, "playerG");
                }

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

    IEnumerator StopSwitchGoal(){

        yield return new WaitForSeconds(durationSwitch);

        timeSwitch = Time.time + durationReturnSwitch;
        returnSwitch = true;
    }
}
