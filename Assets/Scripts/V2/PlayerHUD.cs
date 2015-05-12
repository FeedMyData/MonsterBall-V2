using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

    public GameControllerF.Jersey jersey;
    private GameObject player;
    private Renderer rendPlayer;


	// Use this for initialization
	void Start () {
        switch (jersey)
        {
            case GameControllerF.Jersey.player1:
                player = GameControllerF.GetPlayer(1);
                break;
            case GameControllerF.Jersey.player2:
                player = GameControllerF.GetPlayer(2);
                break;
            case GameControllerF.Jersey.player3:
                player = GameControllerF.GetPlayer(3);
                break;
            case GameControllerF.Jersey.player4:
                player = GameControllerF.GetPlayer(4);
                break;
        }

        rendPlayer = player.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 posHUD = Vector3.zero;

        Vector3 v3Pos = Camera.main.WorldToViewportPoint(player.transform.position);


        if (v3Pos.x >= 0.0f && v3Pos.x <= 1.0f && v3Pos.y >= 0.0f && v3Pos.y <= 1.0f)
        {
            posHUD.x = rendPlayer.transform.position.x;
            posHUD.y = 0.2f;
            posHUD.z = rendPlayer.transform.position.z;

            
           
        }
        else
        {
            v3Pos.x -= 0.5f;  // Translate to use center of viewport
            v3Pos.y -= 0.5f;
            v3Pos.z = 0;      // I think I can do this rather than do a 
            //   a full projection onto the plane

            float fAngle = Mathf.Atan2(v3Pos.x, v3Pos.y);
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);

            v3Pos.x = 0.5f * Mathf.Sin(fAngle) + 0.5f;  // Place on ellipse touching 
            v3Pos.y = 0.5f * Mathf.Cos(fAngle) + 0.5f;  //   side of viewport
            v3Pos.z = Camera.main.nearClipPlane + 0.01f;  // Looking from neg to pos Z;
            posHUD = Camera.main.ViewportToWorldPoint(v3Pos);

            //posHUD.y = 0.2f;
        }

        transform.position = posHUD;
         
	}
}
