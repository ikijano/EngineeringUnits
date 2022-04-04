﻿using EngineeringUnits.Units;
using Fractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System;


namespace EngineeringUnits
{

    public class UnitSystem 
    {      
        public static readonly UnitSystem UnitsystemForDouble = new();


        public string Symbol { get; init; }     

        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public readonly IReadOnlyList<RawUnit> ListOfUnits = new List<RawUnit>();


        public UnitSystem() {}

        public UnitSystem(List<RawUnit> LocalUnitList, string symbol = null)
        {
            ListOfUnits = ReduceUnits(LocalUnitList);
            Symbol = symbol;
        }

        public UnitSystem(RawUnit unit) : this(new List<RawUnit>() {unit}) { }
        public UnitSystem(decimal unit, string symbol)
        {
            //Adding just a dimensionless unit
            var dimensionless = new RawUnit()
            {
                Symbol=symbol,
                A = new(unit),
                UnitType = BaseunitType.CombinedUnit,
                B = 0,
                Count = 1,
            };

            ListOfUnits = new List<RawUnit>() {dimensionless};
        }

        public UnitSystem(UnitSystem unit, string symbol)
        {
            ListOfUnits = new List<RawUnit>(unit.ListOfUnits);
            Symbol = symbol;
        }

        public List<(BaseunitType Key, int Value)> UnitsCount()
        {
            if (_UnitsCount is null)
            {
                _UnitsCount = ListOfUnits
                                .Where(x => x.UnitType != BaseunitType.CombinedUnit)
                                .GroupBy(x => x.UnitType)
                                .Select(x => (x.Key, x.Sum(x => x.Count)))
                                .Where(x => x.Item2 != 0)
                                .ToList();
            }

            return _UnitsCount;
        }

        public static bool operator ==(UnitSystem a, UnitSystem b)
        {
            return a.GetHashCodeForUnitCompare() == b.GetHashCodeForUnitCompare();
        }
        public static bool operator !=(UnitSystem a, UnitSystem b)
        {
            return !(a == b);
        }

        
        public Fraction SumConstant()
        {
            if (_sumConstant == Fraction.Zero)            
                _sumConstant = ListOfUnits.Aggregate(Fraction.One, (x, y) => x * y.TotalConstant);            

            return _sumConstant;
        }
        public Fraction SumOfBConstants()
        {
            return ListOfUnits.Aggregate(Fraction.Zero, (x, y) => x + (Fraction)y.B);
        }
        public Fraction ConvertionFactor(UnitSystem To)
        {
            return To.SumConstant() / SumConstant();
        }

        public static UnitSystem operator +(UnitSystem left, UnitSystem right)
        {
            if (left != right)
                throw new WrongUnitException($"Failed to do: {left} + {right}. Expected both units to be the same!");

            //Using left unitsystem as the final system
            return left;
        }
        public static UnitSystem operator -(UnitSystem left, UnitSystem right)
        {
            if (left != right)
                throw new WrongUnitException($"Failed to do: {left} - {right}. Expected both units to be the same!");

            //Using left unitsystem as the final system
            return left;



        }
        public static UnitSystem operator *(UnitSystem left, UnitSystem right)
        {
            int hashCode;
            unchecked
            {
                hashCode = left.GetHashCode() * 11270411 + right.GetHashCode() * 18403087;
            }


            if (CacheMultiply.TryGetValue(hashCode, out UnitSystem local))
            {
                return local;
            }

            var test2 = new UnitSystem(
                        new List<RawUnit>(
                            left.ListOfUnits.Concat(
                            right.ListOfUnits)));

            CacheMultiply.Add(hashCode, test2);

            return test2;


        }
        public static UnitSystem operator *(decimal left, UnitSystem right)
        {
            return right * left;
        }
        public static UnitSystem operator *(UnitSystem left, decimal right)
        {
            if (right == 1)
                return left;



            List<RawUnit> LocalUnitList = new();

            LocalUnitList.AddRange(left.ListOfUnits);
            //LocalUnitList.Add(new CombinedUnit(constant));

            var dimensionless = new RawUnit()
            {
                Symbol=null,
                A = new(right),
                UnitType = BaseunitType.CombinedUnit,
                B = 0,
                Count = 1,

            };
            LocalUnitList.Add(dimensionless);


            return new UnitSystem(LocalUnitList, left.Symbol);



        }
        public static UnitSystem operator /(UnitSystem left, UnitSystem right)
        {
            int hashCode = 512265997;
            unchecked
            {
                hashCode = (hashCode * 18403087) ^ left.GetHashCode();
                hashCode = (hashCode * 11270411) ^ right.GetHashCode();
            }




            if (CacheDivide.TryGetValue(hashCode, out UnitSystem local))
            {
                return local;
            }


            List<RawUnit> LocalUnitList = new(left.ListOfUnits);

            foreach (var item in right.ListOfUnits)
                LocalUnitList.Add(item.CloneAndReverseCount());


            var test2 = new UnitSystem(LocalUnitList);

            CacheDivide.Add(hashCode, test2);

            return test2;

        }


        public override string ToString()
        {

            if (Symbol is not null)
                return Symbol;


            //Creates all positive symbols
            string local = ListOfUnits
                .Where(x => x.Count > 0)
                .Aggregate("", (x, y) => x += $"{y.Symbol}{y.Count.ToSuperScript()}");

            //If any negative values are present create a '/'
            if (ListOfUnits.Any(x => x.Count < 0))
                local += "/";

            //Creates all negative symbols
            local += ListOfUnits
                .Where(x => x.Count < 0)
                .Aggregate("", (x, y) => x += $"{y.Symbol}{(y.Count * -1).ToSuperScript()}");


            return local;
        }

        public static List<RawUnit> ReduceUnits(List<RawUnit> ListToBeReduced)
        {

           var test = ListToBeReduced.GroupBy(x => x.UnitType);

            var NewUnitList = new List<RawUnit>();

            foreach (var GroupOfTypes in test)
            {

                if (GroupOfTypes.Count() <= 1)
                {
                    //just add the unit
                    NewUnitList.Add(GroupOfTypes.First());
                }
                else
                {

                    var groupOfSameConstant = GroupOfTypes
                        .Select(x => x)
                        .GroupBy(x => x.A);

     
                    foreach (var item in groupOfSameConstant)
                    {

                        RawUnit NewUnit = item.First().CloneWithNewCount(item.Sum(x => x.Count));

                        NewUnitList.Add(NewUnit);

                    }
                }           

            }

            return NewUnitList;
        }
        public UnitSystem Sqrt()
        {

            var NewUnitList = new List<RawUnit>();

            foreach (var item in ListOfUnits.Where(x => x.UnitType is not BaseunitType.CombinedUnit))
            {
                if (item.Count % 2 != 0)                
                    throw new WrongUnitException($"We can't handle taking the square root of your unit! If the resulting unit ends in ex. [meter^0.5] you get this error.");

                NewUnitList.Add(item.CloneWithNewCount(item.Count/2));
            }

            var combinedUnit = ListOfUnits.Where(x => x.UnitType is BaseunitType.CombinedUnit).FirstOrDefault();

            if (combinedUnit is not null)
            {

                var dimensionless = new RawUnit()
                {
                    Symbol=null,
                    A = combinedUnit.A.Sqrt(),
                    UnitType = BaseunitType.CombinedUnit,
                    B = 0,
                    Count = 1,
                };

                NewUnitList.Add(dimensionless);


            }
            






            return new(NewUnitList);       
        }
       
        public override int GetHashCode()
        {
            if (HashCode == 0)
            {
                HashCode = (int)795945743;              

                foreach (var item in ListOfUnits.OrderBy(x => x.UnitType))
                {
                    HashCode = (HashCode * 512265997) ^ item.GetHashCode();
                }
            }

            return HashCode;
        }
        public static bool EqualWithoutHash(UnitSystem a, UnitSystem b)
        { 
            int aCount;
            decimal aB = 0;
            Fraction aNewC;
            BaseunitType aType;            

            bool equal = false;


            if (a.ListOfUnits.Count != b.ListOfUnits.Count)
            {
                equal = false;
            }
            else
            {
                for (int i = 0; i < a.ListOfUnits.Count(); i++)
                {
                    aCount = a.ListOfUnits[i].Count;
                    aB = a.ListOfUnits[i].B;
                    aNewC = a.ListOfUnits[i].A;
                    aType = a.ListOfUnits[i].UnitType;
                    for ( int j=0; j <b.ListOfUnits.Count(); j++)
                    {

                        if (aCount == b.ListOfUnits[i].Count &&
                            aB == b.ListOfUnits[i].B &&
                            aNewC == b.ListOfUnits[i].A &&
                            aType == b.ListOfUnits[i].UnitType)
                        {
                            equal = true;
                        }

                    }
              
                }

            }
            
            return equal;

           
        }
        public int GetHashCodeForUnitCompare()
        {
            if (HashCodeForUnitCompare == 0)
            {
                var test = UnitsCount().OrderBy(x => x.Key)
                                       .ThenBy(x => x.Value);

                HashCode hashCode = new();                

                //Debug.Print(hashCode.ToHashCode().ToString()); 

                foreach (var (Key, Value) in test)
                {
                    hashCode.Add(Key);
                    hashCode.Add(Value);
                }

                HashCodeForUnitCompare = hashCode.ToHashCode();
            }

            return HashCodeForUnitCompare;
        }


        public bool IsSIUnit()
        {
          return ListOfUnits.All(x=> x.IsSI);
        }

        public bool DoesIncludeTemperature()
        {
            return ListOfUnits.Any(x => x.UnitType is BaseunitType.temperature);
        }

        //Cache
        private static readonly Dictionary<int, UnitSystem> CacheMultiply = new();
        private static readonly Dictionary<int, UnitSystem> CacheDivide = new();
        private List<(BaseunitType Key, int Value)> _UnitsCount;
        private int HashCode;
        private Fraction _sumConstant;
        private int HashCodeForUnitCompare;
    }
}
