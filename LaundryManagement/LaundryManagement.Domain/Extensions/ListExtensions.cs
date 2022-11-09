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
        public static void AddOrUpdate(this IList<ProcessDetailViewDTO> list, ProcessDetailViewDTO item)
        {
            var addedItem = list.FirstOrDefault(x => x.ArticleId == item.ArticleId);
            if (addedItem == null)
                list.Add(item);
            else
                addedItem.Quantity += item.Quantity;
        }

        public static void AddOrUpdate(this IList<ReceptionDetailViewDTO> list, ItemDTO item)
        {
            var addedItem = list.FirstOrDefault(x => x.ArticleId == item.Article.Id);
            if (addedItem == null)
                list.Add(new ReceptionDetailViewDTO()
                {
                    ArticleId = item.Article.Id,
                    Color = item.Article.Color.Name,
                    ItemType = item.Article.Type.Name,
                    Size = item.Article.Size.Name,
                    Quantity = 1,
                    ExpectedQuantity = 0
                });
            else
            {
                addedItem.Quantity += 1;
            }
        }
    }
}
