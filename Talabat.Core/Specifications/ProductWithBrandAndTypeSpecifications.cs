using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams Params) 
        : base(P => (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId) 
                 && (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
                 && (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search)) )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
