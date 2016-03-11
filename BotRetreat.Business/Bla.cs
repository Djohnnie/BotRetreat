using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotRetreat.Business.Interfaces;
using BotRetreat.Enums;

namespace BotRetreat.Business
{
    //public class Bla : ICoreGlobals
    //{
    //    public short XPosition { get; }
    //    public short YPosition { get; }
    //    public short Width { get; }
    //    public short Height { get; }
    //    public short PhysicalHealth { get; }
    //    public short MentalHealth { get; }
    //    public short Stamina { get; }
    //    public IFieldOfView Vision { get; }
    //    public Orientation Orientation { get; set; }

    //    public void MoveForward()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void TurnLeft()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void TurnRight()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void TurnAround()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void A()
    //    {
    //        bool aboutToColideWithEnemyBot = false;
    //        foreach (var visibleBot in Vision.FriendlyBots)
    //        {
    //            switch (Orientation)
    //            {
    //                case Orientation.North:
    //                    if (visibleBot.LocationX == XPosition && visibleBot.LocationY == YPosition - 1)
    //                    {
    //                        aboutToColideWithEnemyBot = true;
    //                    }
    //                    break;
    //                case Orientation.East:
    //                    if (visibleBot.LocationX == XPosition + 1 && visibleBot.LocationY == YPosition)
    //                    {
    //                        aboutToColideWithEnemyBot = true;
    //                    }
    //                    break;
    //                case Orientation.South:
    //                    if (visibleBot.LocationX == XPosition && visibleBot.LocationY == YPosition + 1)
    //                    {
    //                        aboutToColideWithEnemyBot = true;
    //                    }
    //                    break;
    //                case Orientation.West:
    //                    if (visibleBot.LocationX == XPosition - 1 && visibleBot.LocationY == YPosition)
    //                    {
    //                        aboutToColideWithEnemyBot = true;
    //                    }
    //                    break;
    //            }
    //        }
    //        if (XPosition == 0 || XPosition == Width - 1 || YPosition == 0 || YPosition == Height - 1)
    //        {
    //            TurnAround();
    //        }
    //        else
    //        {
    //            if (aboutToColideWithEnemyBot)
    //            {
    //                TurnLeft();
    //            }
    //            else
    //            {
    //                MoveForward();
    //            }
    //        }
    //    }
    //}
}