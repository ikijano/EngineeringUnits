﻿using EngineeringUnits.Units;


namespace EngineeringUnits
{
    public partial class ForcePerLength : BaseUnit
    {

        public ForcePerLength() { }
        public ForcePerLength(decimal value, ForcePerLengthUnit selectedUnit) : base(value, selectedUnit.Unit) { }
        public ForcePerLength(double value, ForcePerLengthUnit selectedUnit) : base(value, selectedUnit.Unit) { }
        public ForcePerLength(int value, ForcePerLengthUnit selectedUnit) : base(value, selectedUnit.Unit) { }
        public ForcePerLength(UnknownUnit value) : base(value) { }

        public ForcePerLength(UnknownUnit value, ForcePerLengthUnit selectedUnit) : base(value, selectedUnit.Unit) { }

        public static ForcePerLength From(double value, ForcePerLengthUnit unit) => new(value, unit);
        public double As(ForcePerLengthUnit ReturnInThisUnit) => ToTheOutSideDouble(ReturnInThisUnit.Unit);
        public ForcePerLength ToUnit(ForcePerLengthUnit selectedUnit) => new(ToTheOutSide(selectedUnit.Unit), selectedUnit);
        public static ForcePerLength Zero => new(0, ForcePerLengthUnit.SI);

        public static implicit operator ForcePerLength(UnknownUnit Unit) => new(Unit);

        public static implicit operator ForcePerLength(int zero)
        {
            if (zero != 0)
                throw new WrongUnitException($"You need to give it a unit unless you set it to 0 (zero)!");

            return Zero;
        }

    }
}
