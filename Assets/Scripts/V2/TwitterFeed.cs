using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SimpleJSON;
using UnityEngine.UI;

public class TwitterFeed : MonoBehaviour {

    public float marginBetweenTweets = 50.0f;
    public float speedScrolling = 16.0f;

    public float refreshingRateWhenNoTweets = 2.0f; // minimum because Twitter API limits to 450 searchs / 15 minutes
    private float timerRefreshingRateWhenNoTweets = 0.0f;
    public float positionToSearchForNewTweet = 0.0f;
    public Transform endScrollingPosition;
    public Text prefabTextTweet;

    public string reminderText = "Tweet your reactions to <color=#55ACEE>@MBL_GAME</color> !";
    public float refreshingRateForReminderWhenNoTweets = 10.0f;
    private float timerRefreshingRateForReminderWhenNoTweets = 0.0f;

    private string URL_TO_ENCODE_KEY_AND_SECRET = "4ylCpeQw4NK2fg2WPfEgY0j53:425RIsoJwCCH2yC2PKc7iDM8KLMjctb2z3goxuazZ9YffEO8DZ"; // consumerKey : consumerSecret
 
    private string accessToken;
    private bool authFinished = false;

    private JSONNode searchResponse;
    private bool newResponse = false;

    private List<Text> displayedTweets = new List<Text>();
    private bool searchingNewTweet = false;
    private string lastTweetDisplayed;
    //rajouter message tous les X temps pour rappeler l'adresse
    //optimiser renderTexture

    // Use this for initialization
    void Start()
    {

        StartCoroutine(GetTwitterAccessToken());

        DisplayTweet(reminderText);

    }

    // Update is called once per frame
    void Update()
    {

        timerRefreshingRateWhenNoTweets += Time.deltaTime;
        timerRefreshingRateForReminderWhenNoTweets += Time.deltaTime;

        if (newResponse)
        {
            newResponse = false;

            string textOnOneLine = MakeAllOnOneLine(searchResponse["statuses"][0]["text"]);
            string finalText = "<color=#FF0033>" + searchResponse["statuses"][0]["user"]["name"] + "</color> <size=40>@" + searchResponse["statuses"][0]["user"]["screen_name"] + "</size> : " + textOnOneLine;

            Debug.Log(finalText);

            if (lastTweetDisplayed != finalText)
            {
                lastTweetDisplayed = finalText;
                DisplayTweet(finalText);
            }
            else if (timerRefreshingRateForReminderWhenNoTweets > refreshingRateForReminderWhenNoTweets)
            {
                timerRefreshingRateForReminderWhenNoTweets = 0.0f;
                DisplayTweet(reminderText);
            }
            
        }

        float lastPixelPositionDisplayed = positionToSearchForNewTweet - 0.01f;

        foreach (Text tweet in displayedTweets)
        {

            if (tweet.gameObject.activeSelf)
            {

                // move if displayed
                if (transform.localPosition.x + tweet.transform.localPosition.x + tweet.preferredWidth * tweet.rectTransform.localScale.x < endScrollingPosition.localPosition.x)
                {
                    tweet.gameObject.SetActive(false);
                }
                else
                {
                    tweet.transform.Translate(Time.deltaTime * speedScrolling, 0, 0);
                }

                // search for last displaying tweet
                if (tweet.transform.localPosition.x + tweet.preferredWidth * tweet.rectTransform.localScale.x > lastPixelPositionDisplayed)
                {
                    lastPixelPositionDisplayed = tweet.transform.localPosition.x + tweet.preferredWidth * tweet.rectTransform.localScale.x;
                }

            }

        }

        // search for new tweet if place available
        if (lastPixelPositionDisplayed < positionToSearchForNewTweet && authFinished && !searchingNewTweet && timerRefreshingRateWhenNoTweets > refreshingRateWhenNoTweets)
        {
            StartCoroutine(SearchForTweets());
            searchingNewTweet = true;
        }

    }

    private string MakeAllOnOneLine(string oldText) {

        string textOnOneLine = oldText.Replace("\n", " ; ");
        return textOnOneLine;

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
            //Debug.Log("Waiting for authentification...");
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

        parameters["q="] = "#gamdev -enculé -pd -putain -pute -salope -fuck"; // or to:MBL_GAME or @MBL_GAME or a special # for the occasion
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
            //Debug.Log("Waiting for request...");
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
        timerRefreshingRateWhenNoTweets = 0.0f;
        searchingNewTweet = false;
        newResponse = true;
        
    }

    void DisplayTweet(string text)
    {

        bool hasRecycled = false;

        foreach (Text tweet in displayedTweets)
        {

            if (!tweet.gameObject.activeSelf)
            {
                tweet.gameObject.SetActive(true);

                tweet.transform.SetParent(transform, false);
                tweet.transform.position = transform.position;
                tweet.transform.Translate(-marginBetweenTweets, 0, 0);

                tweet.text = text;

                hasRecycled = true;
                break;
            }

        }

        if (!hasRecycled)
        {
            Text newTweet = Instantiate(prefabTextTweet);

            newTweet.transform.SetParent(transform, false);
            newTweet.transform.position = transform.position;
            newTweet.transform.Translate(-marginBetweenTweets, 0, 0);

            newTweet.text = text;

            displayedTweets.Add(newTweet);

        }

    }

}
