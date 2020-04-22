using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nonogram.Entity;
using nonogram.Service;

namespace NonogramWeb.APIControllers
{
    [Produces("application/json")]
    [Route("api/Comment")]
    public class CommentController : Controller
    {
        private ICommentService commentService = new CommentServiceEF();

        // GET: api/Comment
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return commentService.GetLatestComments();
        }

        // POST: api/Comment
        [HttpPost]
        public void Post([FromBody]Comment comment)
        {
            commentService.AddComment(comment);
        }
    }
}