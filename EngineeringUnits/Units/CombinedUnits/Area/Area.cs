﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringUnits
{
    public partial class Area : BaseUnit
    {

        public Area()
        {
            Name = "Area";
        }

        public Area(double value, AreaUnit SquaredlengthUnit) : this()
        {

            Unit = SquaredlengthUnit.Unit;

            SetLocalValue((decimal)value);
        }

        public decimal As(LengthUnit SquaredlengthUnit)
        {
            UnitSystem ReturnInThisUnitSystem = new UnitSystem();

            ReturnInThisUnitSystem.Length = SquaredlengthUnit;
            ReturnInThisUnitSystem.Length.Count = 2;

            return ToTheOutSide(ReturnInThisUnitSystem);
        }

        //Every units needs this
        public static implicit operator Area(UnknownUnit Unit)
        {
            Area local = new Area(0, AreaUnit.SI);

            local.Transform(Unit);
            return local;
        }




    }
}
