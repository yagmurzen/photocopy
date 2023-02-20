using AutoMapper;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Entities.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Customer, Photocopy.Entities.Dto.WebUI.CustomerDto>();
        CreateMap<Photocopy.Entities.Dto.WebUI.CustomerDto, Customer>();


        CreateMap<CustomerAddress, AddressDto>();
        CreateMap<AddressDto, CustomerAddress>();
		CreateMap<Faq, FaqDto>();

		CreateMap<BlogPage, BlogListDetailDto>();
        CreateMap<BlogListDetailDto, BlogPage>();

        CreateMap<BlogPage, BlogListDto>();
        CreateMap<BlogListDto, BlogPage>();


        CreateMap<Slider, SliderDto>();
        CreateMap<SliderDto, Slider>();

        CreateMap<Order, CreatedOrderDto>();
        CreateMap<CreatedOrderDto, Order>();

        CreateMap<OrderDetail, CalculateDto>();
        CreateMap<CalculateDto, OrderDetail>();


        CreateMap<City, CityDto>();
        CreateMap<CityDto, City>();

        CreateMap<District, DistrictDto>();
        CreateMap<DistrictDto, District>();
    }
}