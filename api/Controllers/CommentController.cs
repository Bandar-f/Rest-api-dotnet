using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;

        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo)
        {
            _commentRepo=commentRepo;
            _stockRepo=stockRepo;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll(){


            if(!ModelState.IsValid)
                   return BadRequest(ModelState);


            var comment= await _commentRepo.GetAllAsync();

            var commentDto=comment.Select(c=>c.ToCommentDto());


            return Ok(commentDto);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){


            if(!ModelState.IsValid)
                   return BadRequest(ModelState);
             var comment= await _commentRepo.GetByIdAsync(id);

             if(comment==null)
             return NotFound();


        return Ok(comment.ToCommentDto());     

        }



        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateComment body){

            if(!ModelState.IsValid)
                   return BadRequest(ModelState);
             

             if(! await _stockRepo.StockExist(stockId)){

                return BadRequest("Stock does not exist");
             }


             var commentModel=body.toCommentFromRequest(stockId);
             await _commentRepo.CreateAsync(commentModel);


             return CreatedAtAction(nameof(GetById),new {id=commentModel},commentModel);





        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateRequestDto req){

            if(!ModelState.IsValid)
                   return BadRequest(ModelState);
            
            var comm= req.toCommentFromUpdateRequest(id);
            var comment=await _commentRepo.UpdateAsync(comm,id);


            if(comment==null){

                return NotFound();
            }



            return Ok(comment);


                



        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){

            if(!ModelState.IsValid)
                   return BadRequest(ModelState);

             var commentModel=await _commentRepo.Delete(id);

             if(commentModel==null)
                 return NotFound();


            return  NoContent();   




        }





    }
}