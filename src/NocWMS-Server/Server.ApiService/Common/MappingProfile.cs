using AutoMapper;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // AutoMapper 映射配置需要再详细填写
            CreateMap<UserInfo, UserSimpleViewModel>()
                .ForMember(model => model.IsCompleted, opt => opt.Ignore());
            CreateMap<UserInfo, UserInfoViewModel>()
                .ForMember(model => model.IsCompleted, opt => opt.Ignore());
            CreateMap<UserInfo, UserStoreViewModel>();

            CreateMap<Staff, StaffFormViewModel>();
            CreateMap<Staff, StaffAddFormViewModel>();
            CreateMap<Staff, StaffDisplayViewModel>();
            CreateMap<Staff, StaffSimpleViewModel>();

            CreateMap<Customer, CustomerFormViewModel>();
            CreateMap<Customer, CustomerDisplayViewModel>();

            CreateMap<Vendor, VendorFormViewModel>();
            CreateMap<Vendor, VendorDisplayViewModel>();

            CreateMap<Warehouse, WarehouseFormViewModel>()
                .ForMember(model => model.ManagerName, opt => opt.MapFrom(x => x.Staff.StaffName));
            CreateMap<Warehouse, WarehouseDisplayViewModel>()
                .ForMember(model => model.ManagerName, opt => opt.MapFrom(x => x.Staff.StaffName));
            CreateMap<Warehouse, WarehouseSimpleViewModel>();
            CreateMap<Warehouse, WarehouseProvinceViewModel>();

            CreateMap<WarehouseCell, WarehouseCellDisplayViewModel>();
            CreateMap<WarehouseCell, WarehouseCellFormViewModel>();
            CreateMap<WarehouseCell, WarehouseCellSimpleViewModel>();

            CreateMap<Category, CategoryFormViewModel>();
            CreateMap<Category, CategoryDisplayViewModel>();
            CreateMap<Category, CategorySimpleViewModel>();

            CreateMap<Product, ProductFormViewModel>();
            CreateMap<Product, ProductDisplayViewModel>();

            CreateMap<Inventory, InventoryDisplayViewModel>()
                .ForMember(model => model.ProductName, opt => opt.MapFrom(x => x.Product.ProductName))
                .ForMember(model => model.ProductUnit, opt => opt.MapFrom(x => x.Product.Unit))
                .ForMember(model=>model.ProductPrice, opt=>opt.MapFrom(x=>x.Product.SellPrice))
                .ForMember(model => model.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(model => model.WarehouseName, opt => opt.MapFrom(x => x.Warehouse.WarehouseName));
            CreateMap<Inventory, InventoryDetailViewModel>()
                .ForMember(model => model.ProductName, opt => opt.MapFrom(x => x.Product.ProductName))
                .ForMember(model => model.ProductSpec, opt => opt.MapFrom(x => x.Product.Spec))
                .ForMember(model => model.ProductUnit, opt => opt.MapFrom(x => x.Product.Unit))
                .ForMember(model => model.ProductModel, opt => opt.MapFrom(x => x.Product.Model))
                .ForMember(model => model.ProductPrice, opt => opt.MapFrom(x => x.Product.SellPrice))
                .ForMember(model => model.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(model => model.WarehouseName, opt => opt.MapFrom(x => x.Warehouse.WarehouseName))
                .ForMember(model => model.InventoryCells, opt => opt.MapFrom(x => x.InventoryCells));

            CreateMap<InventoryCell, InventoryCellDisplayViewModel>()
                .ForMember(model => model.CellName, opt => opt.MapFrom(x => x.WarehouseCell.CellName))
                .ForMember(model => model.UpdateTime, opt => opt.MapFrom(x => x.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<InboundReceipt, InboundReceiptDisplayViewModel>()
                .ForMember(model => model.VendorName, opt => opt.MapFrom(x => x.Vendor.VendorName))
                .ForMember(model=>model.WarehouseName, opt=>opt.MapFrom(x=>x.Warehouse.WarehouseName))
                .ForMember(model => model.CreateTime, opt => opt.MapFrom(x => x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));
            CreateMap<InboundReceipt, InboundReceiptFormViewModel>()
                .ForMember(model => model.VendorName, opt => opt.MapFrom(x => x.Vendor.VendorName));
            CreateMap<InboundReceipt, InboundReceiptDetailViewModel>()
                .ForMember(model => model.InboundReceiptItems, opt => opt.MapFrom(x => x.InboundReceiptItems))
                .ForMember(model => model.WarehouseName, opt => opt.MapFrom(x => x.Warehouse.WarehouseName))
                .ForMember(model => model.VendorName, opt => opt.MapFrom(x => x.Vendor.VendorName))
                .ForMember(model => model.CreateTime, opt => opt.MapFrom(x => x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<InboundReceiptItem, InboundReceiptItemDisplayViewModel>()
                .ForMember(model => model.ProductName, opt => opt.MapFrom(x => x.Product.ProductName))
                .ForMember(model => model.ProductSpec, opt => opt.MapFrom(x => x.Product.Spec))
                .ForMember(model => model.ProductUnit, opt => opt.MapFrom(x => x.Product.Unit))
                .ForMember(model => model.ProductModel, opt => opt.MapFrom(x => x.Product.Model))
                .ForMember(model=>model.ProductPrice, opt=>opt.MapFrom(x=>x.Product.SellPrice))
                .ForMember(model => model.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(model => model.StoreCell, opt => opt.MapFrom(x => x.WarehouseCell.CellName));
            CreateMap<InboundReceiptItem, InboundReceiptItemAddFormViewModel>();



            CreateMap<OutboundReceipt, OutboundReceiptDisplayViewModel>()
                .ForMember(model => model.CustomerName, opt => opt.MapFrom(x => x.Customer.CustomerName))
                .ForMember(model => model.WarehouseName, opt => opt.MapFrom(x => x.Warehouse.WarehouseName))
                .ForMember(model => model.CreateTime, opt => opt.MapFrom(x => x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));
            CreateMap<OutboundReceipt, OutboundReceiptFormViewModel>()
                .ForMember(model => model.CustomerName, opt => opt.MapFrom(x => x.Customer.CustomerName));
            CreateMap<OutboundReceipt, OutboundReceiptDetailViewModel>()
                .ForMember(model => model.OutboundReceiptItems, opt => opt.MapFrom(x => x.OutboundReceiptItems))
                .ForMember(model => model.WarehouseName, opt => opt.MapFrom(x => x.Warehouse.WarehouseName))
                .ForMember(model => model.CustomerName, opt => opt.MapFrom(x => x.Customer.CustomerName))
                .ForMember(model => model.CreateTime, opt => opt.MapFrom(x => x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<OutboundReceiptItem, OutboundReceiptItemDisplayViewModel>()
                .ForMember(model => model.ProductName, opt => opt.MapFrom(x => x.Product.ProductName))
                .ForMember(model => model.ProductSpec, opt => opt.MapFrom(x => x.Product.Spec))
                .ForMember(model => model.ProductUnit, opt => opt.MapFrom(x => x.Product.Unit))
                .ForMember(model => model.ProductModel, opt => opt.MapFrom(x => x.Product.Model))
                .ForMember(model => model.ProductPrice, opt => opt.MapFrom(x => x.Product.SellPrice))
                .ForMember(model => model.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(model => model.StoreCell, opt => opt.MapFrom(x => x.WarehouseCell.CellName));
            CreateMap<OutboundReceiptItem, OutboundReceiptItemAddFormViewModel>();
        }
    }
}
