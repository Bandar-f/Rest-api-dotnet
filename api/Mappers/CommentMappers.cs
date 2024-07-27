using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel){
            
            return new CommentDto {
                Id= commentModel.Id,
                Title=commentModel.Title,
                Content=commentModel.Content,
                CreatedOn=commentModel.CreatedOn,
                StockId=commentModel.StockId
            };

        }


        public static Comment toCommentFromRequest(this CreateComment commentModel, int stockId){



            return new Comment{
                Title=commentModel.Title,
                Content=commentModel.Content,
                StockId=stockId

            };




        }


        public static Comment toCommentFromUpdateRequest(this UpdateRequestDto commentModel, int id){


            return new Comment{
                Title=commentModel.Title,
                Content=commentModel.Content,
                Id=id
            };
        }
    }
}