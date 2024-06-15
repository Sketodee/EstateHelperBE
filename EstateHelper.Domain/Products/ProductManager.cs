using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Products;
using EstateHelper.Domain.ConsultantGroups;
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


        public async Task<Product> Create(CreateProductDto input)
        {
            //check if product name exists 
            _ = await _productRepository.SingleOrDefaultAsync(x => x.Name == input.Name) == null ? true : throw new Exception("Product name already exist"); 
            var product = _mapper.Map<Product>(input);
            var result= await _productRepository.CreateAsync(product);
            return result;
        }

        public async Task<bool> Delete(string Id)
        {
            var product = await _productRepository.SingleOrDefaultAsync(x => x.Id == Id) ?? throw new Exception("Product not found");
            return await _productRepository.DeleteAsync(product);   
        }

        public async Task<List<Product>> GetAllProducts(string? Id, string? Name, PaginationParamaters pagination)
        {
            var product = await _productRepository.GetAllAsync(Id, Name, pagination) ?? throw new Exception("No product found");
            return product; 
        }


        public async Task<Product> Update(EditProductDto input)
        {
            //check if product name exists 
            var product = await _productRepository.SingleOrDefaultAsync(x => x.Id == input.Id) ?? throw new Exception("Product not found");
            //check if name and email exist
            bool nameExist = await _productRepository.SingleOrDefaultAsync(x => x.Name == input.Name) == null && product.Name != input.Name ? true : throw new Exception("Name already exist");
            var newProduct = _mapper.Map(input, product); 
            var result = await _productRepository.UpdateAsync(newProduct);
            return result;
        }
    }
}
