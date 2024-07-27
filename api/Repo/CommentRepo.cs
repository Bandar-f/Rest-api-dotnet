using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repo
{
    public class CommentRepo : ICommentRepo
    {

        private readonly ApplicationDBContext _context;

        public CommentRepo(ApplicationDBContext context)
        {

            _context=context;
            
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comment.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> Delete(int id)
        {

            var comment= await _context.Comment.FirstOrDefaultAsync(c=>c.Id==id);

            if(comment==null)
               return null;



             _context.Comment.Remove(comment);
             await _context.SaveChangesAsync();



             return comment;   



            
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            

            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(Comment commentModel,int id)
        {


            var comment= await _context.Comment.FirstOrDefaultAsync(c=>c.Id==id);


            if(comment==null)
               return null;

             

             comment.Title=commentModel.Title;
             comment.Content=commentModel.Content;

             await _context.SaveChangesAsync();





            return comment;
        }
    }
}