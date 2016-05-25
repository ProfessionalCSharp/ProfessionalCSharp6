using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using Wrox.ProCSharp.WCF.Contracts;

namespace Wrox.ProCSharp.WCF
{
    [DataContract]
    public class GetRoomReservationsRequest
    {
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
    }


}
