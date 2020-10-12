using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        internal ElectricEngine(float i_MaxTimeInHours, float i_RemainingTime)
            : base(i_MaxTimeInHours, i_RemainingTime)
        {
        }

        public float RemainingTimeInHours
        {
            get
            {
                return CurrentValue;
            }
        }

        public float MaxTimeInHours
        {
            get
            {
                return MaxCapacity;
            }
        }

        internal void RechargeBattery(float i_NumberOfHoursToAdd)
        {
            FillUp(i_NumberOfHoursToAdd);
        }
    }
}
