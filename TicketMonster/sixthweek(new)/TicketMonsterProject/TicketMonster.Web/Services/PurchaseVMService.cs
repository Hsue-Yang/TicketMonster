using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using TicketMonster.ApplicationCore.Interfaces.PurchaseService;
using TicketMonster.ApplicationCore.Services;
using TicketMonster.Web.ViewModels.Purchase;
using TicketMonster.Infrastructure.Data;
using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.Web.Services.Cms
{
    public class PurchaseVMService : IPurchaseVMService
    {

        private readonly IPurchaseService _purchaseService;

        public PurchaseVMService(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }


        public async Task<PurchasePageViewModel> GetPurchasePageViewModel(int eventId)
        {
            var thisEvent = await _purchaseService.GetEvent(eventId);
            var thisPerformer = await _purchaseService.GetPerfomer(eventId);
            var thisVenue = await _purchaseService.GetVenue(eventId);
            var eventPic = await _purchaseService.GetEventPic(eventId);
            var performerPic = await _purchaseService.GetPerformerPic(eventId);



            PurchasePageViewModel viewModel = new PurchasePageViewModel()
            {
                EventsViewModel = new EventsViewModel
                {
                    ID = eventId,//這邊增加了id方便checkoutpage取用
                    EventDate = thisEvent.EventDate,
                    EventName = thisEvent.EventName,
                    TotalTime = thisEvent.TotalTime,
                    EventPic = eventPic
                },
                PerformersViewModel = new PerformersViewModel
                {
                    ID = thisPerformer.Select(p => p.Id).ToList(),
                    Name = thisPerformer.Select(p => p.Name).ToList(),
                    About = thisPerformer.Select(p => p.About).ToList(),
                    PerformerPic = performerPic,
                },
                VenuesViewModel = new VenuesViewModel
                {
                    ID = thisVenue.Id,
                    Location = thisVenue.Location,
                    Capacity = thisVenue.Capacity,
                    VenueName = thisVenue.VenueName,
                    Pic = thisVenue.Pic
                },

            };

            return (viewModel);
        }

        public async Task<string> GetTheaterSeat()
        {
            var theaterSeat = await _purchaseService.GetTheaterSeat();
            PurchasePageViewModel seatSectionModel = new PurchasePageViewModel
            {
                SeatSectionsViewModel = theaterSeat.Select(ss => new SeatSectionsViewModel
                {
                    ID = ss.Id,
                    VenueID = ss.VenueId,
                    SectionCapacity = ss.SectionCapacity,
                    SectionName = ss.SectionName,
                    SectionPrice = ss.SectionPrice,
                }).ToList(),
            };
            var theaterJson = JsonConvert.SerializeObject(seatSectionModel);


            return theaterJson;
        }

        public async Task<string> GetSeatSectionList(int eventId)
        {
            var seatSection = await _purchaseService.GetSeatSection(eventId);
           
            PurchasePageViewModel seatSectionModel = new PurchasePageViewModel
            {
                SeatSectionsViewModel = seatSection.Select(ss => new SeatSectionsViewModel
                {
                    ID = ss.Id,
                    VenueID = ss.VenueId,
                    SectionCapacity = ss.SectionCapacity,
                    SectionName = ss.SectionName,
                    SectionPrice = ss.SectionPrice,
                }).ToList(),
            };


            var seatSectionJson = JsonConvert.SerializeObject(seatSectionModel);


            return seatSectionJson;
        }
        public async Task<string> GetSeatNumList(int eventId)
        {

            var seatNum = await _purchaseService.GetSeatNum(eventId);
            PurchasePageViewModel seatNumModel = new PurchasePageViewModel
            {
                SeatNumsViewModel = seatNum.Select(sn => new SeatNumsViewModel
                {
                    SeatSectionID = sn.SeatSectionId,
                    SeatNum = sn.SeatNum1,
                    IsOrdered = sn.IsOrdered,
                    RetainTime = sn.RetainTime,
                }).ToList(),
            };


            var seatNumJson = JsonConvert.SerializeObject(seatNumModel);


            return seatNumJson;
        }

        public SeatNumsViewModel GetEventSeat(int eventId, string sectionName, string seatNum)
        {
            var eventSeat = _purchaseService.GetEventSeat(eventId, sectionName, seatNum);

            if (eventSeat is not null)
            {
                //eventSeat.IsOrdered = true;

                //await _seatNumRepository.UpdateAsync(eventSeat);

                SeatNumsViewModel seat = new SeatNumsViewModel
                {
                    SeatSectionID = eventSeat.SeatSectionId,
                    IsOrdered = eventSeat.IsOrdered,
                    RetainTime = eventSeat.RetainTime,
                    SeatNum = eventSeat.SeatNum1
                };

                return seat;
            }


            //SeatNum sn = new SeatNum
            //{
            //    SeatSectionId = eventSeat.SeatSectionId,
            //    IsOrdered = eventSeat.IsOrdered,
            //    RetainTime = eventSeat.RetainTime,
            //    SeatNum1 = eventSeat.SeatNum1
            //};
            //_seatNumRepository.EditSeatNum(eventSeat);

            return null;
        }


    }
}
