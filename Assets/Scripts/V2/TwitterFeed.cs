using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SimpleJSON;
using UnityEngine.UI;

public class TwitterFeed : MonoBehaviour {

    private string URL_TO_ENCODE_KEY_AND_SECRET = "4ylCpeQw4NK2fg2WPfEgY0j53:425RIsoJwCCH2yC2PKc7iDM8KLMjctb2z3goxuazZ9YffEO8DZ"; // consumerKey : consumerSecret
 
    private string accessToken;
    private bool authFinished = false;

    private JSONNode searchResponse;
    private bool newResponse = false;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(GetTwitterAccessToken());

    }

    // Update is called once per frame
    void Update()
    {

        if (authFinished) 
        {
            authFinished = false;
            StartCoroutine(SearchForTweets());
        }
            

        if (newResponse)
        {
            newResponse = false;
            Debug.Log(searchResponse["statuses"][0]["user"]["name"] + " (@" + searchResponse["statuses"][0]["user"]["screen_name"] + ") : " + searchResponse["statuses"][0]["text"]);

            GetComponent<Text>().text = searchResponse["statuses"][0]["user"]["name"] + " (@" + searchResponse["statuses"][0]["user"]["screen_name"] + ") : " + searchResponse["statuses"][0]["text"];
        }

    }

    IEnumerator GetTwitterAccessToken() {

        byte[] bytesToEncode = Encoding.UTF8.GetBytes(URL_TO_ENCODE_KEY_AND_SECRET);
        string encodedText = System.Convert.ToBase64String(bytesToEncode);

        byte[] body;	
	    body = Encoding.UTF8.GetBytes("grant_type=client_credentials");
	
	    Dictionary<string, string> headers = new Dictionary<string, string>();
        headers["Authorization"] = "Basic " + encodedText;

        headers["Content-Type"] = "application/x-www-form-urlencoded;charset=UTF-8";
	
	    WWW web = new WWW("https://api.twitter.com/oauth2/token", body, headers);

        //yield return web;

        float elapsedTimeAuth = 0.0f;

        while (!web.isDone)
        {
            Debug.Log("Waiting for authentification...");
            elapsedTimeAuth += Time.deltaTime;
            if (elapsedTimeAuth >= 10.0f) break;
            yield return null;
        }

        if (!web.isDone || !string.IsNullOrEmpty(web.error))
        {
            Debug.LogError(string.Format("Fail :\n{0}", web.error));
            yield break;
        }

        //SimpleJSON.JSONNode JSONresponse = JSON.Parse(web.text);

        accessToken = JSON.Parse(web.text)["access_token"];

        authFinished = true;

    }
 
    IEnumerator SearchForTweets() {
	
        Dictionary<string, string> headers = new Dictionary<string, string>();

	    headers["Authorization"] = "Bearer " + accessToken;

        string url = "https://api.twitter.com/1.1/search/tweets.json?";

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters["q="] = "@MBL_GAME -enculé -pd -putain -pute -salope -fuck";
        parameters["result_type="] = "recent";
        parameters["count"] = "1";
        //parameters["geocode="] = "45.6537200,0.1486590,10km";

        for (int i = 0; i < parameters.Count; i++)
        {

            parameters[parameters.Keys.ElementAt(i)] = WWW.EscapeURL(parameters.Values.ElementAt(i), System.Text.Encoding.UTF8);
            url += parameters.Keys.ElementAt(i) + parameters.Values.ElementAt(i);
            if(i < parameters.Count -1)
                url += "&";

        }
 
	    WWW web = new WWW(url, null, headers);

        float elapsedTime = 0.0f;

        while (!web.isDone)
        {
            Debug.Log("Waiting for request...");
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 4.0f) break;
            yield return null;
        }

        if (!web.isDone || !string.IsNullOrEmpty(web.error))
        {
            Debug.LogError(string.Format("Fail :\n{0}", web.error));
            yield break;
        }

        searchResponse = JSON.Parse(web.text);
        newResponse = true;
        
    }

}
