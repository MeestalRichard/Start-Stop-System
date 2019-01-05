using GTA;
using System;
using GTA.Native;


public class Engine_stop__Start_system : Script
{
    // Where you initialize all your variables for use.
    bool VehicleIsStopped = false;
    private int GameTimeRef = Game.GameTime + 40000;

    private Ped PlayerPed = Game.Player.Character;
    private Vehicle Vehicle = Game.Player.Character.CurrentVehicle;


    public Engine_stop__Start_system()
    {
        
        Tick += OnTick;
    }

    public int GameTimeRef1 { get => GameTimeRef; set => GameTimeRef = value; }

    // This is where loops/things are run every frame.
    void OnTick(object sender, EventArgs e)
    {
        if (VehicleIsStopped)
        {
            if (Game.IsControlPressed(2, GTA.Control.VehicleAccelerate) || Game.IsControlPressed(2, GTA.Control.VehicleBrake) || Game.IsControlPressed(2, GTA.Control.VehicleMoveLeft) || Game.IsControlPressed(2, GTA.Control.VehicleMoveRight)
                || Game.Player.Character.IsRagdoll)
            {
                Game.Player.Character.Task.ClearAll();
                VehicleIsStopped = false;
                GameTimeRef1 = Game.GameTime;

                Function.Call(Hash.SET_VEHICLE_ENGINE_ON, Game.Player.Character.CurrentVehicle);
                Game.Player.Character.CurrentVehicle.IsDriveable = true;
            }

        }
        else
        {
            if (PlayerPed.IsInVehicle())
            {
                Vehicle = PlayerPed.CurrentVehicle;

                if (Vehicle.Model.IsCar || Vehicle.Model.IsBike)
                {

                    if (!Game.Player.Character.IsStopped) GameTimeRef1 = Game.GameTime;
                    else if (Game.GameTime > GameTimeRef1 + 4000)
                    {
                        VehicleIsStopped = true;
                        Function.Call(Hash.SET_VEHICLE_ENGINE_ON, Game.Player.Character.CurrentVehicle, false);
                        Game.Player.Character.CurrentVehicle.IsDriveable = false;
                    }
                }


            }
        }
    }
}
