using UnityEngine;
using System.Collections;

public class GoalScriptMonster : MonoBehaviour {

    private GameManagerF manager;

    private Renderer goalRendererToFlash;

    //feedbacks
    private GuiEffects guiEffectsScript;
    private TextCommentaries commentariesScript;

    public float durationSwitch = 2.0f;
    public float durationReturnSwitch = 1.0f;
    private float timeSwitch;
    private bool returnSwitch = false;

    // Use this for initialization
    void Start()
    {
        manager = GameControllerF.getManager();
        guiEffectsScript = GameObject.Find("CanvasFeedbacks").GetComponent<GuiEffects>();
        commentariesScript = GameObject.Find("Commentaries").GetComponent<TextCommentaries>();

        if (tag == "TeamRed")
        {
            goalRendererToFlash = GameObject.Find("redGoal").GetComponent<Renderer>();
        }
        else
        {
            goalRendererToFlash = GameObject.Find("blueGoal").GetComponent<Renderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (returnSwitch)
        {
            float delayReturnSwitch = Mathf.Abs(((timeSwitch - Time.time) / durationReturnSwitch) - 1); ;
            goalRendererToFlash.material.SetFloat("_Switch_goal", Mathf.Lerp(1, 0, delayReturnSwitch));

            if (delayReturnSwitch >= 1)
                returnSwitch = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        PlayerControllerF player = other.GetComponent<PlayerControllerF>();

        //if (other.gameObject.tag == "Monster")
        //{
        //    if (!other.gameObject.GetComponent<MonsterControllerF>().IsMonsterForm())
        //    {
        //        //feedbacks goal balle
        //        Camera.main.GetComponent<CameraShake>().shake(1, 1, 1);

        //        other.gameObject.GetComponent<MonsterControllerF>().PlayRandomSound(AbstractSound.Action.But);
        //        manager.AddScore(tag);
        //        //tp au centre + invul de 3 secondes
        //        other.gameObject.GetComponent<MonsterControllerF>().Respawn();
        //    }
        //}

        if (player != null)
        {
            if (player.IsProjectionInGoal() && player.canCount)
            {
                //feedbacks goal joueur
                player.GetComponent<SoundManager>().PlayEvent("SFX_But",player.gameObject);
                player.GetComponent<SoundManager>().PlayEvent("Public_But", Camera.main.gameObject);
                Camera.main.GetComponent<CameraShake>().shake(1.0f, 1.0f, 1.0f);
                guiEffectsScript.flashGoal(tag);
                commentariesScript.WriteCommentary(tag, "monsterG");

                goalRendererToFlash.material.SetFloat("_Switch_goal", 1);
                StartCoroutine(StopSwitchGoal());

                manager.AddScore(tag);
                StartCoroutine(DezRez(player.gameObject));
                player.canCount = false;

            }
            //else
            //{
            //    player.callStun(player.stunGoal);
            //    Vector3 dirImpact;

            //    player.PlayRandomSound(AbstractSound.Action.EjectBut);

            //    if (other.transform.position.x > transform.position.x)
            //        dirImpact = new Vector3(-1, 0.1f, 0);
            //    else
            //        dirImpact = new Vector3(1, 0.1f, 0);
            //    player.AddImpact(dirImpact * 200);
            //}
        }
    }

    IEnumerator StopSwitchGoal()
    {

        yield return new WaitForSeconds(durationSwitch);

        timeSwitch = Time.time + durationReturnSwitch;
        returnSwitch = true;
    }

    IEnumerator DezRez(GameObject player)
    {
        PlayerControllerF playerController = player.GetComponent<PlayerControllerF>();
        TeleportationF telPlayer = player.GetComponentInChildren<TeleportationF>();
        telPlayer.InstantTP(true);
        telPlayer.SetTeleportation(false);
        playerController.Respawn();
        yield return new WaitForSeconds(telPlayer.durationTP);
        playerController.canCount = true;

    }
}
