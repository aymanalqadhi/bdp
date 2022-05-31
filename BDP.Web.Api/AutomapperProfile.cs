using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Web.Dtos;

using AutoMapper;

namespace BDP.Web.Api;

/// <summary>
/// A profile class for Automapper
/// </summary>
public class AutomapperProfile : Profile
{
    /// <summary>
    /// Default constructor where types mapping occures
    /// </summary>
    public AutomapperProfile()
    {
        // users
        CreateMap<User, UserDto>()
            .ForMember(
                u => u.ProfilePictureUrl,
                o => o.MapFrom(s => s.ProfilePicture != null ? s.ProfilePicture.FullPath : null)
             )
            .ForMember(
                u => u.Groups,
                o => o.MapFrom(s => s.Groups.Select(g => g.Name))
            )
            .ForMember(
                u => u.PhoneNumbers,
                o => o.MapFrom(s => s.PhoneNumbers.Select(p => p.Number))
            );

        // sellables
        CreateMap<Sellable, SellableDto>()
            .ForMember(
                p => p.Attachments,
                o => o.MapFrom(s => s.Attachments.Select(a => a.FullPath))
            )
            .Include<Product, ProductDto>()
            .Include<Service, ServiceDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<Service, ServiceDto>();
        CreateMap<SellableReview, SellableReviewDto>();

        // finacial records
        CreateMap<FinancialRecord, FinancialRecordDto>();
        CreateMap<FinancialRecordVerification, FinancialRecordVerificationDto>()
            .ForMember(
                v => v.IsAccepted,
                o => o.MapFrom(v => v.Outcome == FinancialRecordVerificationOutcome.Accepted)
            )
            .ForMember(
                v => v.Document,
                o => o.MapFrom(v => v.Document == null ? null : v.Document.FullPath)
            );

        // transactions
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionConfirmation, TransactionConfirmationDto>()
            .ForMember(
                c => c.IsAccepted,
                o => o.MapFrom(c => c.Outcome == TransactionConfirmationOutcome.Confirmed)
            );

        // purchases
        CreateMap<Purchase, PurchaseDto>()
            .Include<ProductOrder, OrderDto>()
            .Include<ServiceReservation, ReservationDto>();
        CreateMap<ProductOrder, OrderDto>();
        CreateMap<ServiceReservation, ReservationDto>();

        // events
        CreateMap<Event, EventDto>()
            .ForMember(
                e => e.Pictures,
                o => o.MapFrom(s => s.Pictures.Select(i => i.FullPath))
            );
        CreateMap<EventType, EventTypeDto>();
    }
}