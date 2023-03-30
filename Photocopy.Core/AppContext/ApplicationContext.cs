using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Photocopy.Entities;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.Domain;
using Photocopy.Entities.Domain.FixType;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.AppContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ContentNode> ContentNodes { get; set; }
        public DbSet<ContentPage> ContentPages { get; set; }

        public DbSet<BlogNode> BlogNodes { get; set; }
        public DbSet<BlogPage> BlogPages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Galery> Galeries { get; set; }
        public DbSet<GaleryFile> GaleryFiles { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

        public DbSet<CustomerAddress> CustomerAddress { get; set; }

        public DbSet<LookupPrice> LookupPrice { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderState> OrderState { get; set; }
        public DbSet<CargoFirm> CargoFirms { get; set; }


        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketDetail> BasketDetail { get; set; }

        public DbSet<UploadData> UploadData { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                entity.HasOne(ur => ur.Role).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.RoleId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });            

            modelBuilder.Entity<ContentNode>(entity => 
            {            
                entity.HasOne(ur => ur.Menu).WithOne(ur => ur.ContentNode).HasForeignKey<Menu>(b => b.ContentNodeId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.Name).HasMaxLength(128);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
                entity.HasDiscriminator<ContentPageType>("ContentPageType").HasValue<ContentNode>(value: ContentPageType.Static);
                entity.HasDiscriminator<ContentPageType>("ContentPageType").HasValue<BlogNode>(value: ContentPageType.Blog);



            });
           
            modelBuilder.Entity<ContentPage>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.ContentNode).WithMany(u => u.Pages).HasForeignKey(ur => ur.ContentNodeId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
                entity.Property(x => x.Subtitle).HasMaxLength(512);
                entity.Property(x => x.PageTitle).HasMaxLength(512);
                entity.Property(x => x.Description).HasMaxLength(512);
                entity.Property(x => x.HtmlContent).HasMaxLength(int.MaxValue);
                entity.Property(x => x.MetaDescription).HasMaxLength(512);
                entity.Property(x => x.MetaKeyword).HasMaxLength(512);
				entity.Property(x => x.MainImagePath).HasMaxLength(int.MaxValue);
				entity.Property(x => x.ThumbImagePath).HasMaxLength(int.MaxValue);
                entity.Property(x => x.CustomCss).IsRequired(false);
                entity.Property(x => x.CustomJs).IsRequired(false);



                entity.HasDiscriminator<ContentPageType>("ContentPageType").HasValue<ContentPage>(value: ContentPageType.Static);
                entity.HasDiscriminator<ContentPageType>("ContentPageType").HasValue<BlogPage>(value: ContentPageType.Blog);

            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(ur => ur.Menu)
                .WithMany(ur => ur.MenuItems).HasForeignKey(ur => ur.MenuId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.ParentItem)
                    .WithMany(x => x.ParentItems)
                    .HasForeignKey(x => x.MenuItemId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });

            modelBuilder.Entity<GaleryFile>(entity => 
            { 
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.Galery).WithMany(u => u.GaleryFiles).HasForeignKey(ur => ur.GaleryId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });


            modelBuilder.Entity<Galery>(entity =>
            {
                entity.HasOne(ur => ur.ContentNode).WithMany(u => u.GaleryList).HasForeignKey(ur => ur.ContentNodeId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });

            modelBuilder.Entity<LookupPrice>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
            });


            modelBuilder.Entity<UploadData>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.OrderDetail).WithOne(u => u.UploadData).HasForeignKey<OrderDetail>(ur => ur.UploadDataId);
                entity.Property(x => x.FilePath).HasMaxLength(int.MaxValue);
                entity.HasOne(s => s.BasketDetail)
                    .WithOne(i => i.UploadData)
                    .HasForeignKey<BasketDetail>(s => s.UploadDataId);
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
                entity.Property(x => x.MobileImagePath).HasMaxLength(int.MaxValue);
                entity.Property(x => x.ImagePath).HasMaxLength(int.MaxValue);


            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.Property(x => x.Question).HasMaxLength(256);
                entity.Property(x => x.SubQuestion).HasMaxLength(2048);
                entity.Property(x => x.Answer).HasMaxLength(512);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.Property(x => x.Name).HasMaxLength(128);
                entity.Property(x => x.LastName).HasMaxLength(128);
                entity.Property(x => x.Email).HasMaxLength(128);
                entity.Property(x => x.PhoneNumber).HasMaxLength(64);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.Customer).WithMany(u => u.CustomerAddress).HasForeignKey(ur => ur.CustomerId);
                entity.Property(x => x.Address).HasMaxLength(512);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });
            modelBuilder.Entity<BasketDetail>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.Basket).WithMany(u => u.BasketDetails).HasForeignKey(ur => ur.BasketId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.Customer).WithMany(u => u.Orders).HasForeignKey(ur => ur.CustomerId);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
                entity.Property(x => x.OrderInvoiceDetailId).IsRequired(false);
                entity.Property(x => x.OrderInvoiceId).IsRequired(false);
                entity.Property(x => x.ShipperBranchCode).IsRequired(false);

            });
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.Order).WithMany(u => u.OrderDetails).HasForeignKey(ur => ur.OrderId);
                //entity.HasOne(ur => ur.UploadData).WithOne(u => u.OrderDetail).HasForeignKey<UploadData>(ur => ur.Id);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now.Date);
            });


            modelBuilder.Entity<OrderState>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });

                entity.HasOne(s => s.Order)
                      .WithOne(i => i.OrderState)
                      .HasForeignKey<Order>(s => s.OrderStateId);
            });

            modelBuilder.Entity<CargoFirm>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });

                entity.HasOne(s => s.Order)
                      .WithOne(i => i.CargoCompany)
                      .HasForeignKey<Order>(s => s.CargoCompanyId);
            });
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.Property(x => x.Id).HasMaxLength(3);


                entity.HasOne(s => s.CustomerAddress)
                      .WithOne(i => i.City)
                      .HasForeignKey<CustomerAddress>(s => s.CityId);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(ur => new { ur.Id });
                entity.HasOne(ur => ur.City).WithMany(u => u.Districts).HasForeignKey(ur => ur.CityId);
                entity.Property(x => x.Id).HasMaxLength(3);
            });
         
        }
    }
}
