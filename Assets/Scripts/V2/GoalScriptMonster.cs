using UnityEngine;
using System.Collections;

public class GoalScriptMonster : MonoBehaviour {

    private GameManagerF manager;
    
    //feedbacks
    private GuiEffects guiEffectsScript;
    private TextCommentaries commentariesScript;

    // Use this for initialization
    void Start()
    {
        manager = GameControllerF.getManager();
        guiEffectsScript = GameObject.Find("CanvasFeedbacks").GetComponent<GuiEffects>();
        commentariesScript = GameObject.Find("Commentaries").GetComponent<TextCommentaries>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
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
            if (player.IsProjectionInGoal())
            {
                //feedbacks goal joueur
                Camera.main.GetComponent<CameraShake>().shake(1.0f, 1.0f, 1.0f);
                guiEffectsScript.flashGoal(tag);
                commentariesScript.WriteCommentary(tag, "monsterG");

                manager.AddScore(tag);
                player.Respawn();

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
}
