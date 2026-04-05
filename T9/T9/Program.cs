using System;

class TV
{
    public void On() => Console.WriteLine("TV on");
    public void Off() => Console.WriteLine("TV off");
    public void SetChannel(string channel) => Console.WriteLine($"TV channel set: {channel}");
    public void SetInput(string input) => Console.WriteLine($"TV input set: {input}");
}
class AudioSystem
{
    private int volume = 10;

    public void On() => Console.WriteLine("Audiosystem on");
    public void Off() => Console.WriteLine("Audiosystem off");

    public void SetVolume(int level)
    {
        volume = level;
        Console.WriteLine($"Volume set on: {volume}");
    }
}
class DVDPlayer
{
    public void On() => Console.WriteLine("DVD On");
    public void Off() => Console.WriteLine("DVD off");

    public void Play() => Console.WriteLine("Play DVD");
    public void Pause() => Console.WriteLine("Pause DVD");
    public void Stop() => Console.WriteLine("Stop DVD");
}
class GameConsole
{
    public void On() => Console.WriteLine("Игровая консоль включена");
    public void Off() => Console.WriteLine("Игровая консоль выключена");

    public void StartGame() => Console.WriteLine("Игра запущена");
}
class HomeTheaterFacade
{
    private TV tv;
    private AudioSystem audio;
    private DVDPlayer dvd;
    private GameConsole game;

    public HomeTheaterFacade(TV tv, AudioSystem audio, DVDPlayer dvd, GameConsole game)
    {
        this.tv = tv;
        this.audio = audio;
        this.dvd = dvd;
        this.game = game;
    }
    public void WatchMovie()
    {
        Console.WriteLine("\n Mode:Movie On");
        tv.On();
        tv.SetInput("DVD");
        audio.On();
        audio.SetVolume(15);
        dvd.On();
        dvd.Play();
    }
    public void StopMovie()
    {
        Console.WriteLine("\n Movie Stop");
        dvd.Stop();
        dvd.Off();
        audio.Off();
        tv.Off();
    }
    public void PlayGame()
    {
        Console.WriteLine("\n Mode:Game");
        tv.On();
        tv.SetInput("Game");
        game.On();
        game.StartGame();
        audio.On();
        audio.SetVolume(20);
    }
    public void ListenMusic()
    {
        Console.WriteLine("\n Mode:Music");
        tv.On();
        tv.SetInput("Audio");
        audio.On();
        audio.SetVolume(25);
    }
    public void AllOff()
    {
        Console.WriteLine("\n All system's off");
        dvd.Off();
        game.Off();
        audio.Off();
        tv.Off();
    }
    public void SetVolume(int level)
    {
        audio.SetVolume(level);
    }
}
class Program
{
    static void Main()
    {
        TV tv = new TV();
        AudioSystem audio = new AudioSystem();
        DVDPlayer dvd = new DVDPlayer();
        GameConsole game = new GameConsole();

        HomeTheaterFacade home = new HomeTheaterFacade(tv, audio, dvd, game);

        home.WatchMovie();
        home.SetVolume(18);
        home.StopMovie();

        home.PlayGame();
        home.AllOff();

        home.ListenMusic();
        home.AllOff();
    }
}
