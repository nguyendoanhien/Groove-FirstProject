using System;

namespace GrooveMessengerDAL.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapBy : Attribute
    {
        public MapBy(Type enumType)
        {
        }
    }
}