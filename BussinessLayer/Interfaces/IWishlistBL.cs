using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IWishlistBL
    {
        public bool AddToWishList(WishlistModel model);
        public bool DeleteWishlist(int wishlistId);
    }
}
