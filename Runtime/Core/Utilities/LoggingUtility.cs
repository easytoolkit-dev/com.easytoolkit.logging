using EasyToolkit.Serialization;

namespace EasyToolkit.Logging.Core
{
    public static class LoggingUtility
    {
        private static readonly SerializationContext SerializationContext = new()
        {
            MemberFlags = SerializableMemberFlags.AllPublic
        };

        public static string ConvertContextToJson(object context)
        {
            return EasySerializer.SerializeToJson(context, context: SerializationContext);
        }
    }
}
