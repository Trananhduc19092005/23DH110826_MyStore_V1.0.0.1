using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Đồ_Án_BackUp.Models.ViewModel
{
    public class CartService
    {
        private readonly HttpSessionStateBase session;

        public CartService(HttpSessionStateBase session)
        {
            this.session = session;
        }

        public Category GetCart()
        {
            var cart = (Category)session["Cart"];
            if (cart == null) 
            {
                cart = new Category();
                session["Cart"] = cart;
            }
            return cart;
        }

        public void ClearCart()
        {
            session["Cart"] = null;
        }
    }
}