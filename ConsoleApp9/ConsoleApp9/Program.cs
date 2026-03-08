using System;
using System.Collections.Generic;

interface ICommand
{
    void Execute();
    void Undo();
}

class Light
{
    public void On() => Console.WriteLine("Light ON");
    public void Off() => Console.WriteLine("Light OFF");
}

class Door
{
    public void Open() => Console.WriteLine("Door OPENED");
    public void Close() => Console.WriteLine("Door CLOSED");
}

class Thermostat
{
    public int temperature = 20;

    public void Increase()
    {
        temperature++;
        Console.WriteLine("Temperature increased to " + temperature);
    }

    public void Decrease()
    {
        temperature--;
        Console.WriteLine("Temperature decreased to " + temperature);
    }
}

class TV
{
    public void On() => Console.WriteLine("TV ON");
    public void Off() => Console.WriteLine("TV OFF");
}

class LightOnCommand : ICommand
{
    Light light;

    public LightOnCommand(Light l) { light = l; }

    public void Execute() => light.On();
    public void Undo() => light.Off();
}

class LightOffCommand : ICommand
{
    Light light;

    public LightOffCommand(Light l) { light = l; }

    public void Execute() => light.Off();
    public void Undo() => light.On();
}

class DoorOpenCommand : ICommand
{
    Door door;

    public DoorOpenCommand(Door d) { door = d; }

    public void Execute() => door.Open();
    public void Undo() => door.Close();
}

class DoorCloseCommand : ICommand
{
    Door door;

    public DoorCloseCommand(Door d) { door = d; }

    public void Execute() => door.Close();
    public void Undo() => door.Open();
}

class TempUpCommand : ICommand
{
    Thermostat thermostat;

    public TempUpCommand(Thermostat t) { thermostat = t; }

    public void Execute() => thermostat.Increase();
    public void Undo() => thermostat.Decrease();
}

class TempDownCommand : ICommand
{
    Thermostat thermostat;

    public TempDownCommand(Thermostat t) { thermostat = t; }

    public void Execute() => thermostat.Decrease();
    public void Undo() => thermostat.Increase();
}

class TVOnCommand : ICommand
{
    TV tv;

    public TVOnCommand(TV t) { tv = t; }

    public void Execute() => tv.On();
    public void Undo() => tv.Off();
}

class RemoteControl
{
    Stack<ICommand> history = new Stack<ICommand>();
    public void PressButton(ICommand command)
    {
        command.Execute();
        history.Push(command);
    }
    public void Undo()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("No commands to undo");
            return;
        }

        ICommand command = history.Pop();
        command.Undo();
    }
}

class Program
{
    static void Main()
    {
        Light light = new Light();
        Door door = new Door();
        Thermostat thermostat = new Thermostat();
        TV tv = new TV();

        RemoteControl remote = new RemoteControl();

        remote.PressButton(new LightOnCommand(light));
        remote.PressButton(new DoorOpenCommand(door));
        remote.PressButton(new TempUpCommand(thermostat));
        remote.PressButton(new TVOnCommand(tv));

        Console.WriteLine("\nUndo actions:");

        remote.Undo();
        remote.Undo();
        remote.Undo();
        remote.Undo();
        remote.Undo(); // ошибка — нечего отменять
    }
}