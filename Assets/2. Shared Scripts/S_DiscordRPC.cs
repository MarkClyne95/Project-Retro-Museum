using System;
using System.Threading;
using Discord;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_DiscordRPC : MonoBehaviour
{
    public Discord.Discord discord;
    private float playTime = 0.0f;
    private Discord.Activity activity;
    private Discord.ActivityManager activityManager;
    private int currentEpochTime;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        playTime = 0.0f;
        discord = new Discord.Discord(1107751061067927582, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        activityManager = discord.GetActivityManager();
        
        playTime += Time.deltaTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        var levelName = "";

        switch (scene.name)
        {
            case "L_MainMenu":
                levelName = "Main Menu";
                break;
            case "L_70sFloor":
                levelName = "The 70's Floor";
                break;
            case "AtariGame":
                levelName = "Atari Vs. Badtari";
                break;
            case "L_80sFloor":
                levelName = "The 80's Floor";
                break;
            case "L_90sFloor":
                levelName = "The 90's Floor";
                break;
            case "L_MDMainMenu":
                levelName = "Mega Dude Main Menu";
                break;
            case "L_Level1":
                levelName = "Mega Dude";
                break;
            case "menu":
                levelName = "Firewall Main Menu";
                break;
            case "game":
                levelName = "Firewall";
                break;
            case "L_Saturn":
                levelName = "Saturn Siege";
                break;
            case "DoomMenu":
                levelName = "Froom Main Menu";
                break;
            case "DoomLevel":
                levelName = "Froom";
                break;
            case "MainMenu":
                levelName = "Randy The Roadrunner Main Menu";
                break;
            case "Temple":
                levelName = "Randy The Roadrunner: Temple";
                break;
            case "Jungle":
                levelName = "Randy The Roadrunner: Jungle";
                break;
            case "L_Credits":
                levelName = "Credits";
                break;
        }
        activity = new Discord.Activity
        {
            State = levelName,
            Timestamps = { Start = currentEpochTime, End = 0 },
            Assets =
            {
                LargeImage = "randy",
                LargeText = "Randy!",
                SmallImage = "randy",
                SmallText = "Randy!"
            }
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                //all good
            }
        });
    }

    private void Update()
    {
        discord.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        discord.Dispose();
    }
}
