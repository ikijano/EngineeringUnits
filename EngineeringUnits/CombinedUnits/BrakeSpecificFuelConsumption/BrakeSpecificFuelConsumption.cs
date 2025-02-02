
using EngineeringUnits.Units;


namespace EngineeringUnits
{
    //This class is auto-generated, changes to the file will be overwritten!
    public partial class BrakeSpecificFuelConsumption : BaseUnit
    {

        public BrakeSpecificFuelConsumption() { }
        public BrakeSpecificFuelConsumption(decimal value, BrakeSpecificFuelConsumptionUnit selectedUnit) : base(value, selectedUnit.Unit) { }
        public BrakeSpecificFuelConsumption(double value, BrakeSpecificFuelConsumptionUnit selectedUnit) : base(value, selectedUnit.Unit) { }
        public BrakeSpecificFuelConsumption(int value, BrakeSpecificFuelConsumptionUnit selectedUnit) : base(value, selectedUnit.Unit) { }
        public BrakeSpecificFuelConsumption(UnknownUnit value) : base(value) { }

        public static BrakeSpecificFuelConsumption From(double value, BrakeSpecificFuelConsumptionUnit unit) => new(value, unit);

        public static BrakeSpecificFuelConsumption From(double? value, BrakeSpecificFuelConsumptionUnit unit)
        {
            if (value is null || unit is null)
            {
                return null;
            }

            return From((double)value, unit);
        }
        public double As(BrakeSpecificFuelConsumptionUnit ReturnInThisUnit) => GetValueAsDouble(ReturnInThisUnit.Unit);
        public BrakeSpecificFuelConsumption ToUnit(BrakeSpecificFuelConsumptionUnit selectedUnit) => new(GetValueAs(selectedUnit.Unit), selectedUnit);
        public static BrakeSpecificFuelConsumption Zero => new(0, BrakeSpecificFuelConsumptionUnit.SI);

        public static implicit operator BrakeSpecificFuelConsumption(UnknownUnit Unit)
        {
            Unit.UnitCheck(BrakeSpecificFuelConsumptionUnit.SI);
            return new(Unit);        
        }

        public static implicit operator BrakeSpecificFuelConsumption(int zero)
        {
            if (zero != 0)
                throw new WrongUnitException("You need to give it a unit unless you set it to 0(zero)!");
			return Zero;
		}
	public override string GetStandardSymbol(UnitSystem _unit) => GetStandardSymbol<BrakeSpecificFuelConsumptionUnit>(_unit);
	}
}

