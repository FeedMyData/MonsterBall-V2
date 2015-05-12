using UnityEngine;
using System.Collections;

public class TwitterReading : MonoBehaviour {

    Twitter.RequestTokenResponse m_RequestTokenResponse;
    Twitter.AccessTokenResponse m_AccessTokenResponse;

    private string ACCESS_TOKEN = "3163963660-ltfVBJInXBn9aoDsM7Sqxk7vyIoymytGN1EXRni";
    private string ACCESS_TOKEN_SECRET = "3CM2S18donphlE5ydJXLCVd9j613R9w6bsBDOl1Jv1pfw";
    private string CONSUMER_KEY = "WraQZjnSCAbbRlZBF0eLpBFZV";
    private string CONSUMER_SECRET = "VLN5wX8GRmIhgn67zPkWfXpGndeQF7TU1M9st5OKQ6YEzapLIE";

    public static TwitterReading instance = null;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More then one instance of TwitterAPI: " + this.transform.name);
        }
    }
	// Use this for initialization
	void Start () {
	   /* 
        l = StdOutListener()
        auth = Twitter.API.OAuthHandler(consumer_key, consumer_secret)
        auth.set_access_token(access_token, access_token_secret)
        stream = Stream(auth, l)
        //*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
