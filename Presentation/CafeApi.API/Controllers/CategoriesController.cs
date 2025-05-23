﻿using CafeApi.Application.Dtos.CategoryDtos;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryServices _categoryServices;
        
        
        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryServices.GetAllCategories();
           return CreateResponse(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryServices.GetByIdCategory(id);
            return CreateResponse(result);
        }

        [Authorize] //koruma altına aldık 
        [HttpPost]
        public async Task<IActionResult> AddCategory( CreateCategoryDto dto)
        {
            var result = await _categoryServices.AddCategory(dto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var result = await _categoryServices.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategory(id);
            return CreateResponse(result);
        }






    }
}
