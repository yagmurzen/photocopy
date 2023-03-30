using AutoMapper;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Entities.Dto;
using Photocopy.Entities.Domain.Domain;

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


        CreateMap<Contact, ContactDto>();
        CreateMap<ContactDto, Contact>();

        //CMS


        // Add as many of these lines as you need to map your objects
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<ContentNode, ContentNodeDto>();
        CreateMap<ContentNodeDto, ContentNode>();
        CreateMap<ContentPage, ContentPageDto>();
        CreateMap<ContentPageDto, ContentPage>();


        CreateMap<BlogNode, BlogNodeDto>();
        CreateMap<BlogNodeDto, BlogNode>();
        CreateMap<BlogPage, BlogPageDto>();
        CreateMap<BlogPageDto, BlogPage>();

        CreateMap<Slider, SliderDto>();
        CreateMap<SliderDto, Slider>();

        CreateMap<Faq, FaqDto>();
        CreateMap<FaqDto, Faq>();


        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();



        CreateMap<UserRole, UserRoleDto>();
        CreateMap<UserRoleDto, UserRole>();


        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();


        CreateMap<Customer, Photocopy.Entities.Dto.CustomerListDto>();
        CreateMap<Photocopy.Entities.Dto.CustomerListDto, Customer>();

        CreateMap<CustomerAddress, Photocopy.Entities.Dto.CustomerAddressDto>();
        CreateMap<Photocopy.Entities.Dto.CustomerAddressDto, CustomerAddress>();

        CreateMap<Customer, Photocopy.Entities.Dto.CustomerDto>();
        CreateMap<Photocopy.Entities.Dto.CustomerDto, Customer>();

        CreateMap<CustomerAddress, AddressDto>();
        CreateMap<AddressDto, CustomerAddress>();



        CreateMap<Order, OrderListDto>();
        CreateMap<OrderListDto, Order>();


        CreateMap<OrderDetail, Photocopy.Entities.Dto.OrderDetailDto>();
        CreateMap<Photocopy.Entities.Dto.OrderDetailDto, OrderDetail>();



        CreateMap<OrderState, OrderStateItemDto>();
        CreateMap<OrderStateItemDto, OrderState>();

        CreateMap<OrderState, OrderStateDto>();
        CreateMap<OrderStateDto, OrderState>();

        CreateMap<UploadData, Photocopy.Entities.Dto.UploadDataDto>();
        CreateMap<Photocopy.Entities.Dto.UploadDataDto, UploadData>();

        CreateMap<LookupPrice, LookupPriceListDto>();
        CreateMap<LookupPriceListDto, LookupPrice>();
    }
}