using EasyToolkit.Serialization;
using EasyToolkit.Serialization.Formatters;

namespace EasyToolkit.Logging.Core
{
    public static class LoggingUtility
    {
        private static readonly SerializationContext SerializationContext = new()
        {
            MemberFlags = SerializableMemberFlags.AllPublic,
            AllowAnonymousTypes = true,
            AllowNonSerializableTypes = true
        };

        private static readonly SerializationSettings SerializationSettings = new()
        {
            JsonFormatterSettings = new JsonFormatterSettings()
            {
                Options = JsonFormatterOptions.None
            }
        };

        public static string ConvertContextToJson(object context)
        {
            return EasySerializer.SerializeToJson(context, SerializationSettings, SerializationContext);
        }
    }
}
