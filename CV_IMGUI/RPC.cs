using CV_IMGUI;
using DiscordRPC;
using System;
using System.Web.UI.WebControls;

public class RPC
{
    public static DiscordRpcClient client;
    public static Timestamps rpctimestamp { get; set; }
    private static RichPresence presence;

    public static void InitializeRPC()
    {
        client = new DiscordRpcClient("1284640392829993072");
        client.Initialize();

        DiscordRPC.Button[] buttons = {
            new DiscordRPC.Button() { Label = "FREE PANEL", Url = "https://discord.gg/hDTF97EauR" },
            new DiscordRPC.Button() { Label = "PURCHASE INSTEAD", Url = "https://discord.gg/hDTF97EauR" },
        };

        presence = new RichPresence()
        {
            Buttons = buttons,
            Timestamps = rpctimestamp,
            Assets = new Assets()
            {
                //LargeImageKey = "https://media.giphy.com/media/KLLCeBZIqApC1jg22R/giphy.gif",
                LargeImageKey = "https://media.giphy.com/media/1yffXI7T0zEizHwCRx/giphy.gif",
                LargeImageText = "BRUUUH CHEATS",
                SmallImageKey = "https://media4.giphy.com/media/xmOMPI63SsyZyKz2Tx/giphy.gif?cid=790b7611485d6e9b471bcd8f93609e96f8a02c35a7e05685&rid=giphy.gif&ct=s",
                SmallImageText = "Trusted & Tested"
            }
        };

        client.SetPresence(presence);
        UpdateDiscordPresence();
    }

    public static void SetState(string state, bool watching = false)
    {
        if (watching)
            state = "Looking at " + state;

        presence.State = state;
        client.SetPresence(presence);
    }

    private static void UpdateDiscordPresence()
    {
        if (Form1.KeyAuthApp.user_data != null)
        {
            string username = Form1.KeyAuthApp.user_data.username;
            DateTime expiryDateTime = UnixTimeToDateTime(long.Parse(Form1.KeyAuthApp.user_data.subscriptions[0].expiry));

            presence.Details = $"User: {username}";
            presence.State = $"Expiry Date: {expiryDateTime:yyyy-MM-dd HH:mm}";
        }
        else
        {
            presence.Details = "USER";
            presence.State = "";
        }

        client.SetPresence(presence);
    }

    private static DateTime UnixTimeToDateTime(long unixTime)
    {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixStart.AddSeconds(unixTime).ToLocalTime();
    }

    public static void StopRPC()
    {
        if (client != null)
        {
            client.ClearPresence();
            client.Dispose();
        }
    }
}
