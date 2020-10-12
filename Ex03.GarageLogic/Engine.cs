namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaxCapacity;
        private float m_CurrentValue;

        public Engine(float i_MaxEnergyCapacity, float i_CurrentValue = 0)
        {
            r_MaxCapacity = i_MaxEnergyCapacity;

            // using fill up to ensure the current doesn't exceed the max
            FillUp(i_CurrentValue);
        }

        public float MaxCapacity
        {
            get
            {
                return r_MaxCapacity;
            }
        }

        public float CurrentValue
        {
            get
            {
                return m_CurrentValue;
            }
        }

        protected void FillUp(float i_ValueToFill)
        {
            if (m_CurrentValue + i_ValueToFill > r_MaxCapacity)
            {
                throw new ValueOutOfRangeException("Exceeds engine capacity", 0, r_MaxCapacity - m_CurrentValue);
            }

            if (i_ValueToFill < 0)
            {
                throw new ValueOutOfRangeException("value must be equal/ greater than 0", 0, r_MaxCapacity - m_CurrentValue);
            }

            m_CurrentValue += i_ValueToFill;
        }
    }
}
