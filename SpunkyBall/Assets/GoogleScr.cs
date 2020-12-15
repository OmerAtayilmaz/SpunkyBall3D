using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
public class GoogleScr : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private void Start()
    {
        RequestInterstitial();


    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
       // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }


    public void GameOver()
    {
       
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            Debug.Log("yüklendi ama göstermiyor");


        }
        else
        {
            RequestInterstitial();
            Debug.Log("yüklenmedi");
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        interstitial.Destroy();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
        interstitial.Destroy();
    }

    

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

       
    }

    public void OdulluReklam()
    {
        CreateAndLoadRewardedAd();
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

  

 
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (GameObject.Find("GameManager").GetComponent<GameManagerScr>().OyunDurum == 2)
        {
            GameObject.Find("GameManager").GetComponent<GameManagerScr>().OyunDurum = 1;
            GameObject.Find("Character").gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameObject.Find("Character").gameObject.transform.position = new Vector3(0, 1, 0);
            GameObject.Find("PanelKaybettiniz").gameObject.SetActive(false);
        }
        if(GameObject.Find("GameManager").GetComponent<GameManagerScr>().OyunDurum ==1)
        {
            Debug.Log("Çalıştı");
            PlayerPrefs.SetInt("diamond", PlayerPrefs.GetInt("diamond") + PlayerPrefs.GetInt("toplanan"));
            GameObject.Find("AdsButton").gameObject.GetComponent<Button>().enabled = false ;
        }
        else if (PlayerPrefs.GetInt("beklenen_id")!=0)
        {
            GameObject.Find("WatchandEarn").gameObject.SetActive(false);
            PlayerPrefs.SetInt("alindi:" + PlayerPrefs.GetInt("beklenen_id"), 1);
        }
       
    }

}
