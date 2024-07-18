using Alarm_Project.DTOs;
using Alarm_Project.Models;
using AutoMapper;

namespace Alarm_Project.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Users, UserCreateDto>().ReverseMap();
        CreateMap<Users, UserLoginDto>().ReverseMap();
        CreateMap<Users, LoginResponseDto>().ReverseMap();
        CreateMap<Users, Payment>().ReverseMap();
        CreateMap<Users, MakePayment>().ReverseMap();
        CreateMap<Users, UserMakePaymentDto>().ReverseMap();
        CreateMap<LoginResponseDto, UserLoginDto>().ReverseMap();
        CreateMap<Products, ProductCreateDto>().ReverseMap();
        CreateMap<Products, UserMakePaymentDto>().ReverseMap();
        CreateMap<Products, MakePayment>().ReverseMap();
        CreateMap<Products, IdPaymentDto>().ReverseMap();
        CreateMap<Payment, UserMakePaymentDto>().ReverseMap();
        CreateMap<Payment, MakePayment>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Payment, UserMakePaymentDto>().ReverseMap();
        CreateMap<Payment, IdPaymentDto>().ReverseMap();
        CreateMap<Alarm, UserMakePaymentDto>().ReverseMap();
        CreateMap<Alarm, MakePayment>().ReverseMap();
        CreateMap<Alarm, AlarmDto>().ReverseMap();
        CreateMap<AlarmSettings, UserMakePaymentDto>().ReverseMap();
        CreateMap<AlarmSettings, MakePayment>().ReverseMap();
        CreateMap<AlarmSettings, AlarmSettingsDto>().ReverseMap();
        CreateMap<UserMakePaymentDto, MakePayment>().ReverseMap();
        
    }
}