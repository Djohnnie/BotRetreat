using System;
using BotRetreat.Domain;
using BotRetreat.Enums;

namespace BotRetreat.Business.Interfaces
{
    public interface ICoreGlobals
    {
        /// <summary>
        /// The width in units of the current arena.
        /// </summary>
        Int16 Width { get; }

        /// <summary>
        /// The height in units of the current arena.
        /// </summary>
        Int16 Height { get; }

        /// <summary>
        /// Your current position in units from the left and units from the top.
        /// </summary>
        Position Location { get; }

        /// <summary>
        /// The maximum physical health.
        /// </summary>
        Int16 MaximumPhysicalHealth { get; }

        /// <summary>
        /// The current remaining physical health.
        /// A physical health of 0 causes death.
        /// </summary>
        Int16 PhysicalHealth { get; }

        /// <summary>
        /// The amount of drain on your physical health since the last iteration.
        /// </summary>
        Int16 PhysicalHealthDrain { get; }

        /// <summary>
        /// The maximum stamina.
        /// </summary>
        Int16 MaximumStamina { get; }

        /// <summary>
        /// The current remaining stamina.
        /// A stamina of 0 causes unability to move forward.
        /// </summary>
        Int16 Stamina { get; }

        /// <summary>
        /// The amount of drain on your stamina since the last iteration.
        /// </summary>
        Int16 StaminaDrain { get; }

        /// <summary>
        /// Your current orientation using wind directions.
        /// </summary>
        Orientation Orientation { get; }

        /// <summary>
        /// Your previous action.
        /// </summary>
        LastAction LastAction { get; }

        /// <summary>
        /// Your field of view.
        /// </summary>
        IFieldOfView Vision { get; }

        /// <summary>
        /// Makes you move one unit forward based on your current orientation.
        /// </summary>
        void MoveForward();

        /// <summary>
        /// Makes you turn 90° to the left based on your current orientation.
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Makes you turn 90° to the right based on your current orientation.
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Makes you turn 180° around based on your current orientation.
        /// </summary>
        void TurnAround();

        /// <summary>
        /// Attacks an opponent using a melee strike.
        /// Does damage to an opponent in front of you.
        /// Does 5 damage if blown in an opponents back, 3 damage in an opponents side or face.
        /// </summary>
        void MeleeAttack();

        /// <summary>
        /// Attacks an opponent using a ranged strike.
        /// Does 1 damage to a visible opponent within 5 units of distance from you.
        /// </summary>
        /// <param name="x">The number of units from the left to strike.</param>
        /// <param name="y">The number of units from the top to strike.</param>
        void RangedAttack(Int16 x, Int16 y);

        /// <summary>
        /// Self destructs and does 10 damage to all surrounding opponents.
        /// </summary>
        void SelfDestruct();
    }
}