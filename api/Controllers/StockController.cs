using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{    
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly IStockRepo _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepo stockRepo)
        {
            _context=context; 
            _stockRepo=stockRepo;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {

            if(!ModelState.IsValid)
                   return BadRequest(ModelState);
            var stocks = await _stockRepo.GetAllAsync(query);
            
            var stocksDto=stocks.Select(s=>s.toStockDto());


            return Ok(stocksDto);

        }
        
        [HttpGet("{id:int}")]
        public async  Task<IActionResult> GetById([FromRoute] int id){

            if(!ModelState.IsValid)
                   return BadRequest(ModelState);
               
               var stock= await _stockRepo.GetByIdAsync(id);


               if(stock== null){
                return NotFound();
               }



               return Ok(stock.toStockDto());


        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto){

            if(!ModelState.IsValid)
                   return BadRequest(ModelState);

            var stockModel=stockDto.ToStockFromRequest();
            await _stockRepo.CreateAsync(stockModel);

            

            return CreatedAtAction(nameof(GetById), new{id=stockModel.id},stockModel.toStockDto());



        }


        [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDTO body){

        if(!ModelState.IsValid)
                   return BadRequest(ModelState);


        var stockModel=await _stockRepo.UpdateAsync(id,body);
        
        if(stockModel==null){
            return NotFound();
        }


    

        return Ok(stockModel.toStockDto());

    }


    [HttpDelete]
    [Route("{id:int}")]

    public async  Task<IActionResult> Delete ([FromRoute] int id){

        if(!ModelState.IsValid)
                   return BadRequest(ModelState);


        var stockModel= await _stockRepo.DeleteAsync(id);
        if(stockModel==null){
            return NotFound();

        }


        


        return NoContent();

    }



        
    }

    


}