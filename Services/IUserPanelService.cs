using CinemaGo.DataModels.CustomModels;
using CinemaGo.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaGo.Web.Services
{
    public interface IUserPanelService
    {
        Task<bool> IsUserLoggedIn();
        Task<List<CategoryModel>> GetCategories();

        Task<List<ProductModel>> GetProducts();
        Task<List<ProductModel>> GetProductByCategoryId(int categoryId);

        Task<ResponseModel> RegisterUser(RegisterModel registerModel);
        Task<ResponseModel> LoginUser(LoginModel loginModel);

        Task<ResponseModel> Checkout(List<CartModel> cartItems);

        Task<List<CustomerOrder>> GetOrdersByCustomerId(int customerId);

        Task<List<CartModel>> GetOrderDetailForCustomer(int customerId, string order_number);

        Task<List<string>> GetShippingStatusForOrder(string order_number);

        Task<ResponseModel> ChangePassword(PasswordModel passwordModel);

        Task<bool> DeleteContactUs(ContactUsModel contactusToDelete);

        Task<List<ContactUsModel>> GetContactUss();

        Task<ContactUsModel> SaveContactUs(ContactUsModel newContactUs);


    }
}