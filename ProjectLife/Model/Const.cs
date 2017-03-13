using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProjectLife.Model
{
    public class UsersConst
    {
        public const string Diane = "Diane";
        public const string Clem = "Clément";
        public const string All = "Tous les deux";
        public const string AnyWay = "Tous";
    }

    public class MaiLConst
    {
        public const string Diane = "dianedossou@live.fr";
        public const string Clem = "borgnon.clement@hotmail.fr";
        public const string All = "dianedossou@live.fr;borgnon.clement@hotmail.fr";

        public static string GetConst(string value)
        {
            var result = string.Empty;

            foreach (var prop in typeof(MaiLConst).GetFields())
            {

                if (value.Equals(prop.Name))
                    result = prop.GetValue(new MaiLConst()).ToString();

            }
            return result;

        }
    }
    public class TypeConst
    {
        public const string Travel = "Voyage";
        public const string Informatic = "Informatique";
        public const string Appliances = "ElectroMénager";
        public const string Baby = "Enfant";
        public const string LifeStyle = "Hygiène de vie";
        public const string Estate = "Immobilier";
    }
}
