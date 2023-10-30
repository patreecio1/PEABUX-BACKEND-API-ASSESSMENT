using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Utilities
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (Enum.TryParse(value, out T result))
            {
                return result;
            }
            else
            {
                return default(T);
            }
        }
        public static bool IsValidEmail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        /// <summary>
        /// Update properties with properties of the object Supplied (typically anonymous)
        /// </summary>
        /// <typeparam name="T">Type of Source Object</typeparam>
        /// <param name="destination">Object whose property you want to update</param>
        /// <param name="source">destination object (typically anonymous) you want to take values from</param>
        /// <param name="ignoredProperties"></param>
        /// <returns>Update reference to same Object</returns>
        public static T Assign<T>(this T destination, object source, params string[] ignoredProperties)
        {
            if (ignoredProperties == null) ignoredProperties = new string[0];
            if (destination != null && source != null)
            {
                var query = from sourceProperty in source.GetType().GetProperties()
                            join destProperty in destination.GetType().GetProperties()
                            on sourceProperty.Name.ToLower() equals destProperty.Name.ToLower()             //Case Insensitive Match
                            where !ignoredProperties.Contains(sourceProperty.Name)
                            where destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)   //Properties can be Assigned
                            where destProperty.GetSetMethod() != null                                       //Destination Property is not Readonly
                            select new { sourceProperty, destProperty };


                foreach (var pair in query)
                {
                    //Go ahead and Assign the value on the destination
                    pair.destProperty
                        .SetValue(destination,
                            value: pair.sourceProperty.GetValue(obj: source, index: new object[] { }),
                            index: new object[] { });
                }
            }
            return destination;
        }
        public static string GetEnumDescription(this Enum GenericEnum) //Hint: Change the method signature and input paramter to use the type parameter T
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}
