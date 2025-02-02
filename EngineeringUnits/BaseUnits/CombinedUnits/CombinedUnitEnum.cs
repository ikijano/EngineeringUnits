﻿using EngineeringUnits.Units;
using Fractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EngineeringUnits
{



    public record CombinedUnit : UnitTypebase
    {




        public CombinedUnit() { }


        public CombinedUnit(string symbol, Fraction Constant)
        {
            var unit = new RawUnit()
            {
                Symbol=symbol,
                A = Constant,
                UnitType = BaseunitType.CombinedUnit,
                B = 0,
                Count = 1,

            };


            Unit = new UnitSystem(unit);
        }
        public CombinedUnit(decimal Constant)
        {
            var unit = new RawUnit()
            {
                Symbol=null,
                A = new(Constant),
                UnitType = BaseunitType.CombinedUnit,
                B = 0,
                Count = 1,

            };

            Unit = new UnitSystem(unit);
        }

        public override string ToString()
        {
            if (Unit.Symbol is not null)
            {
                return $"{Unit.Symbol}";
            }


            return $"{Unit}";
        }


    }



}