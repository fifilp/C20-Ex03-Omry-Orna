using System;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        private readonly eGasType r_GasType;
        
        internal GasEngine(
            eGasType i_GasType,
            float i_MaxAmountOfGas,
            float i_CurrentGas)
            : base(i_MaxAmountOfGas, i_CurrentGas)
        {
            r_GasType = i_GasType;
        }

        public float CurrentAmountOfGas
        {
            get
            {
                return CurrentValue;
            }
        }

        public float MaxAmountOfGas
        {
            get
            {
                return MaxCapacity;
            }
        }

        public eGasType GasType
        {
            get
            {
                return r_GasType;
            }
        }

        public void FillUpTank(eGasType i_GasType, float i_GasLitresToFill)
        {
            if (i_GasType != GasType)
            {
                throw new ArgumentException("wrong gas type");
            }

            FillUp(i_GasLitresToFill);
        }
    }
}
