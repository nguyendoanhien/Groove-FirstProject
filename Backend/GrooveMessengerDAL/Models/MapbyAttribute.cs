using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MapBy : Attribute
    {
        public MapBy(Type enumType)
        {

        }
    }
}
