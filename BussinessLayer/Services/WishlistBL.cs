using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;

        }

        public bool AddToWishList(WishlistModel model)
        {
            try
            {
                return this.wishlistRL.AddToWishList(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool DeleteWishlist(int wishlistId)
        {
            try
            {
                return this.wishlistRL.DeleteWishlist(wishlistId);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
