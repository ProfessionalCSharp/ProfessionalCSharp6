using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Wrox.ProCSharp.WCF.Contracts;
using Wrox.ProCSharp.WCF.Data;

namespace Wrox.ProCSharp.WCF
{
    public class RoomReservationService : IRoomService
    {

        public bool ReserveRoom(RoomReservation roomReservation)
        {
            try
            {
                var data = new RoomReservationRepository();
                data.ReserveRoom(roomReservation);
            }
            catch (Exception ex)
            {
                RoomReservationFault fault = new RoomReservationFault { Message = ex.Message };
                throw new FaultException<RoomReservationFault>(fault);
            }
            return true;

        }

        [WebGet(UriTemplate = "Reservations?From={fromTime}&To={toTime}")]
        public RoomReservation[] GetRoomReservations(DateTime fromTime, DateTime toTime)
        {
            var data = new RoomReservationRepository();
            return data.GetReservations(fromTime, toTime);

        }
    }
}
