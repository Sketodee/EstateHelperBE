using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Products;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Products
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly Helpers _helpers;
        private AppUser _loggedInUser;

        public ProductManager(IProductRepository productRepository, Helpers helpers, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _helpers = helpers;
            InitializeLoggedInUser().GetAwaiter().GetResult();
        }

        private async Task InitializeLoggedInUser()
        {
            _loggedInUser = await _helpers.ReturnLoggedInUser();
        }


        public async Task<Product> CreateAsync(CreateProductDto input)
        {
            //check if product name exists 
            _ = await _productRepository.SingleOrDefaultAsync(x => x.Name == input.Name) == null ? true : throw new Exception("Product name already exist"); 
            var product = _mapper.Map<Product>(input);
            var result= await _productRepository.CreateAsync(product);
            return result;
        }

        public Task<bool> DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync(string? Id, string? Name, PaginationParamaters pagination)
        {
            throw new NotImplementedException();
        }

        public Task<Product> SingleOrDefaultAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(EditProductDto input)
        {
            throw new NotImplementedException();
        }
    }
}
