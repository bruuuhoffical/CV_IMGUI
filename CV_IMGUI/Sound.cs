public static class SoundManager
{
    public static bool IsSoundEnabled { get; set; } = true;

    public static void PlaySound(System.IO.UnmanagedMemoryStream sound)
    {
        if (IsSoundEnabled)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(sound);
            player.Play();
        }
    }
}
