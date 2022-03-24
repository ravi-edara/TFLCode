using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TFLCodeChallenge.Tests.Acceptance
{
    public static class HttpWebResponseExtensions
    {
        public static string AsString(this HttpWebResponse httpResponse)
        {
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var response = streamReader.ReadToEnd();
                return response;
            }
        }

        public static T As<T>(this string value, bool ignoreTopLevelProperties = false)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.None,
                        ContractResolver = new DictionaryAsArrayResolver(ignoreTopLevelProperties)
                    });
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }

    public class DictionaryAsArrayResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly bool _ignoreTopLevelProperties;

        public DictionaryAsArrayResolver(bool ignoreTopLevelProperties)
        {
            _ignoreTopLevelProperties = ignoreTopLevelProperties;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType.GetInterfaces().Any(i => i == typeof(IDictionary) ||
                                                    (i.IsGenericType &&
                                                     i.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
            {
                return base.CreateArrayContract(objectType);
            }

            if (!_ignoreTopLevelProperties && (objectType == typeof(IDictionary) ||
                                               (objectType.IsGenericType &&
                                                objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
            {
                return base.CreateArrayContract(typeof(Dictionary<,>).MakeGenericType(objectType.GetGenericArguments()[0], objectType.GetGenericArguments()[1]));
            }

            return base.CreateContract(objectType);
        }
    }
}