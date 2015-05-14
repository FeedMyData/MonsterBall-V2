using UnityEngine;
using System.Collections;

public class DemoSound : MonoBehaviour {

    public MonsterControllerF monster;
    public GameManagerF manager;
    public PlayerControllerF player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            monster.PlayRandomSound(AbstractSound.Action.Impact);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            monster.PlayRandomSound(AbstractSound.Action.Grognement);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            monster.PlayRandomSound(AbstractSound.Action.Course);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            manager.PlayRandomSound(AbstractSound.Action.TroisDeuxUn);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            manager.PlayRandomSound(AbstractSound.Action.But);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            manager.PlayRandomSound(AbstractSound.Action.Debut);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            manager.PlayRandomSound(AbstractSound.Action.Dialogue);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            manager.PlayRandomSound(AbstractSound.Action.Match);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            player.PlayRandomSound(AbstractSound.Action.MarqueBut);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.PlayRandomSound(AbstractSound.Action.Poursuivi);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.PlayRandomSound(AbstractSound.Action.Victoire);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.PlayRandomSound(AbstractSound.Action.WilhemScream);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.PlayRandomSound(AbstractSound.Action.Yeah);
        }
	}
}
