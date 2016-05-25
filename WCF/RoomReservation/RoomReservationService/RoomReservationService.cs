using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Wrox.ProCSharp.WCF.Contracts;
using Wrox.ProCSharp.WCF.Data;
using static System.Console;

namespace Wrox.ProCSharp.WCF
{
    public class RoomReservationService : IRoomService
    {

        public bool ReserveRoom(RoomReservation roomReservation)
        {
            try
            {
                WriteLine($"received room reservation for room {roomReservation.RoomName}");
                var data = new RoomReservationRepository();
                data.ReserveRoom(roomReservation);
            }
            catch (Exception ex)
            {
                WriteLine($"error {ex.Message}");
                RoomReservationFault fault = new RoomReservationFault { Message = ex.Message };
                throw new FaultException<RoomReservationFault>(fault);
            }
            return true;

        }

        [WebGet(UriTemplate = "Reservations?From={fromTime}&To={toTime}")]
        public RoomReservation[] GetRoomReservations(DateTime fromTime, DateTime toTime)
        {
            WriteLine("received call to return reservations");
            var data = new RoomReservationRepository();
            return data.GetReservations(fromTime, toTime);

        }
    }
}
