﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TicketMasterDataAccess.Abstracts;
using TicketMasterDataAccess.DataAccess;
using TicketMasterDataAccess.Factories;
using TicketMasterDataAccess.UnitOfWork.IUnitOfWork;

namespace TicketMasterDataAccess.ConcreteRepositories
{
    public class EventRepository : AbstractTicketRepository<Event, int>, IEventRepositorySegregator
    {
        public EventRepository(TicketMasterEntities context):base(context)
        {
        }
        public override bool Add(Event instance)
        {
            var result = base.Add(instance);
            return result;
        }
        public override Event GetById(int key)
        {
            return DBContext.Events.SingleOrDefault(p => p.EventId == key);
        }

        public override bool Delete(int key)
        {
                try
                {
                    DBContext.Events.Remove(
                            DBContext.Events.SingleOrDefault(k => k.EventId == key));
                    return true;
                }
                catch (Exception e)
                {
                }
                return false;
        }
        public override bool Update(Event instance)
        {
                try
                {
                    var evt =
                        DBContext.Events.SingleOrDefault(k => k.EventId == instance.EventId);
                    evt.EventDescription = instance.EventDescription;
                    evt.EventId = instance.EventId;
                    evt.NumberOfTickets = instance.NumberOfTickets;
                    evt.PricePerTicket = instance.PricePerTicket;
                    evt.EventName = instance.EventName;
                    evt.Location = instance.Location;
                    evt.Tickets = instance.Tickets;
                    evt.EventDate = instance.EventDate;
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
        }

        public decimal? GetUnitPriceOfTicketByEventId(int eventId)
        {
            return
                DBContext.Events.SingleOrDefault(p => p.EventId == eventId).PricePerTicket;
        }
    }
    public interface IEventRepositorySegregator
    {

    }
}
