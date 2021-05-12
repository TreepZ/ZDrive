using System;
using System.Collections.Generic;
using System.Linq;
using ZDrive.Models;

namespace ZDrive.Interfaces
{
    public interface IReserveService
    {
        void AddReservation(ReservedSeat seat);
        IEnumerable<ReservedSeat> GetReservedSeats();
    }
}