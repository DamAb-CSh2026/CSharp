using System;
using System.Collections.Generic;

interface ICommand
{
    void Execute();
    void Undo();
}

class Light
{
    public void On() => Console.WriteLine("Свет включен");
    public void Off() => Console.WriteLine("Свет выключен");
}

class Door
{
    public void Open() => Console.WriteLine("Дверь открыта");
    public void Close() => Console.WriteLine("Дверь закрыта");
}

class Thermostat
{
    public int Temperature = 20;

    public void Increase()
    {
        Temperature++;
        Console.WriteLine("Температура: " + Temperature);
    }

    public void Decrease()
    {
        Temperature--;
        Console.WriteLine("Температура: " + Temperature);
    }
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
    Thermostat t;
    public TempUpCommand(Thermostat t1) { t = t1; }

    public void Execute() => t.Increase();
    public void Undo() => t.Decrease();
}

class TempDownCommand : ICommand
{
    Thermostat t;
    public TempDownCommand(Thermostat t1) { t = t1; }

    public void Execute() => t.Decrease();
    public void Undo() => t.Increase();
}

class TV
{
    public void On() => Console.WriteLine("Телевизор включен");
    public void Off() => Console.WriteLine("Телевизор выключен");
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

    public void Press(ICommand command)
    {
        command.Execute();
        history.Push(command);
    }

    public void Undo()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("Нет команд для отмены");
            return;
        }

        ICommand cmd = history.Pop();
        cmd.Undo();
    }
}

class Program
{
    static void Main()
    {
        Light light = new Light();
        Door door = new Door();
        Thermostat thermo = new Thermostat();
        TV tv = new TV();

        RemoteControl remote = new RemoteControl();

        remote.Press(new LightOnCommand(light));
        remote.Press(new DoorOpenCommand(door));
        remote.Press(new TempUpCommand(thermo));
        remote.Press(new TVOnCommand(tv));

        Console.WriteLine("Отмена:");
        remote.Undo();
        remote.Undo();
    }
}