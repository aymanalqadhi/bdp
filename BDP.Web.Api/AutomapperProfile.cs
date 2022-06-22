using AutoMapper;
using BDP.Domain.Entities;
using BDP.Web.Api.Auth;
using BDP.Web.Dtos;
using System;

using System;

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
        // categories
        CreateMap<Category, CategoryDto>();

        // events
        CreateMap<Event, EventDto>()
            .ForMember(
                e => e.Pictures,
                o => o.MapFrom(s => s.Pictures.Select(i => i.FullPath))
            );
        CreateMap<EventType, EventTypeDto>();

        // finacial records
        CreateMap<FinancialRecord, FinancialRecordDto>();
        CreateMap<FinancialRecordVerification, FinancialRecordVerificationDto>()
            .ForMember(
                v => v.Document,
                o => o.MapFrom(v => v.Document == null ? null : v.Document.FullPath)
            );

        // products
        CreateMap<Product, ProductDto>()
            .ForMember(
                p => p.Categories,
                o => o.MapFrom(s => s.Categories.Select(a => a.Name))
            );
        CreateMap<ProductVariant, ProductVariantDto>()
            .ForMember(
                p => p.Attachments,
                o => o.MapFrom(s => s.Attachments.Select(a => a.FullPath))
            );

        // purchases
        CreateMap<Order, OrderDto>();
        CreateMap<Reservation, ReservationDto>();

        // product variant type-specific classes
        CreateMap<ReservationWindow, ReservationWindowDto>();
        CreateMap<StockBatch, StockBatchDto>();

        // transactions
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionConfirmation, TransactionConfirmationDto>();

        // users
        CreateMap<User, UserDto>()
            .ForMember(
                u => u.Role,
                o => o.MapFrom(s => UserRoleConverter.FromRole(s.Role))
            );

        // user profiles
        CreateMap<UserProfile, UserProfileDto>()
            .ForMember(
                u => u.ProfilePicture,
                o => o.MapFrom(s => s.ProfilePicture != null ? s.ProfilePicture.FullPath : null)
             )
            .ForMember(
                u => u.CoverPicture,
                o => o.MapFrom(s => s.CoverPicture != null ? s.CoverPicture.FullPath : null)
             );
    }
}