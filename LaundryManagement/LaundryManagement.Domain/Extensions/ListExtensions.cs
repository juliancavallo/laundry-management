using LaundryManagement.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Extensions
{
    public static class ListExtensions
    {
        public static void AddOrUpdate(this IList<ShippingDetailViewDTO> list, ShippingDetailViewDTO item)
        {
            var addedItem = list.FirstOrDefault(x => x.ArticleId == item.ArticleId);
            if (addedItem == null)
                list.Add(item);
            else
                addedItem.Quantity += item.Quantity;
        }
    }
}
