using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishlistRL
    {
        public bool AddToWishList(WishlistModel model);
        public bool DeleteWishlist(int wishlistId);
    }
}
