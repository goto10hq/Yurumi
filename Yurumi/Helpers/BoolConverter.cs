using System;
using Newtonsoft.Json;

namespace Yurumi.Helpers
{
    /// <summary>
    /// Bool converter.
    /// </summary>
    class BoolConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(((bool)value) ? 1 : 0);

        /// <summary>
        /// Reads the JSON.
        /// </summary>
        /// <returns>The JSON.</returns>
        /// <param name="reader">Reader.</param>
        /// <param name="objectType">Object type.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <param name="serializer">Serializer.</param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => reader.Value.ToString() == "1";

        /// <summary>
        /// Gets a flag if it can be converted.
        /// </summary>
        /// <returns><c>true</c>, if convert was allowed, <c>false</c> otherwise.</returns>
        /// <param name="objectType">Object type.</param>
        public override bool CanConvert(Type objectType) => objectType == typeof(bool);
    }
}
