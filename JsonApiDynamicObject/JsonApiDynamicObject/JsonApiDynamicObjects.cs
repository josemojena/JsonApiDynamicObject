using System;
using System.Collections.Generic;
using System.Dynamic;

namespace JsonApiDynamicObject
{

    /// <summary>
    /// Provides the necessary methods to standardize dynamic objects and that can be serialized according to JsonApi
    /// </summary>
    public static class JsonApiDynamicObjects
    {
        private static Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        private static Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        private static void StandardizeObject(IDictionary<string, object> obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (obj.Count == 0)
                throw new Exception("No information to process.");

            foreach (var property in obj)
            {
                // Format the keys to get a standard format
                string key = property.Key.Replace(" ", "").ToLower();

                if (string.IsNullOrWhiteSpace(key))
                    throw new Exception($"The key corresponding to the value \"{ property.Value }\" is not valid.");

                else if (key == "id")
                    Data["id"] = property.Value.ToString(); // The values of the id and type members MUST be strings.

                else if (key == "type")
                    Data["type"] = property.Value.ToString(); // The values of the id and type members MUST be strings.

                else
                    Attributes[key] = property.Value;
            }

            if (!Data.ContainsKey("id"))
                throw new Exception("Property \"id\" not found.");

            if (!Data.ContainsKey("type"))
                Data["type"] = obj.GetType().Name;

            if (Attributes.Count > 0)
                Data["attributes"] = Attributes;
        }

        /// <summary>
        /// Standardize, according to JsonApi, a dynamic object contained in a Dictionary
        /// </summary>
        /// <param name="obj">Dictionary containing the attributes of the dynamic object</param>
        /// <returns>
        /// Standardized dynamic object according to JsonApi
        /// </returns>
        public static dynamic getRootObject(IDictionary<string, object> obj)
        {
            StandardizeObject(obj);

            dynamic root = new ExpandoObject();
            root.data = Data;
            return root;
        }

        /// <summary>
        /// Standardize, according to JsonApi, a dynamic object contained in a ExpandoObject
        /// </summary>
        /// <param name="obj">ExpandoObject containing the attributes of the dynamic object</param>
        /// <returns>
        /// Standardized dynamic object according to JsonApi
        /// </returns>
        public static dynamic getRootObject(ExpandoObject obj)
        {
            var dic = (IDictionary<string, object>)obj;

            return getRootObject(dic);
        }
    }
}