using System.Runtime.CompilerServices;
using Play.Catalog.Service.DTO;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service
{
    public static  class Extension
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}
