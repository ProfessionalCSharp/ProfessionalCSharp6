using System.Runtime.Serialization;

namespace Wrox.ProCSharp.WCF.Contracts
{
    [DataContract]
    public class RoomReservationFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
