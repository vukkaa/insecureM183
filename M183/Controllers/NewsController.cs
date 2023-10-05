﻿using M183.Controllers.Dto;
using M183.Data;
using M183.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M183.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
        private readonly NewsAppContext _context;

        public NewsController(NewsAppContext context)
        {
            _context = context;
        }

        private News SetTimezone(News news)
        {
            news.PostedDate = TimeZoneInfo.ConvertTimeFromUtc(news.PostedDate, tzi);
            return news;
        }

        /// <summary>
        /// Retrieve all news entries ordered by PostedDate descending
        /// </summary>
        /// <response code="200">All news entries</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<News>> GetAll()
        {
            return Ok(_context.News
                .Include(n => n.Author)
                .OrderByDescending(n => n.PostedDate)
                .ToList()
                .Select(SetTimezone));
        }

        /// <summary>
        /// Retrieve a specific news entry by id
        /// </summary>
        /// <param name="id" example="123">The news id</param>
        /// <response code="200">News retrieved</response>
        /// <response code="404">News not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<News> GetById(int id)
        {
            News? news = _context.News
                .Include(n => n.Author)
                .FirstOrDefault(n => n.Id == id);

            if (news == null)
            {
                return NotFound();
            }
            return Ok(SetTimezone(news));
        }

        /// <summary>
        /// Create a news entry
        /// </summary>
        /// <response code="201">News successfully created</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Create(NewsDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var newNews = new News();

            newNews.Header = request.Header;
            newNews.Detail = request.Detail;
            newNews.AuthorId = request.AuthorId;
            newNews.PostedDate = DateTime.UtcNow;
            newNews.IsAdminNews = request.IsAdminNews;

            _context.News.Add(newNews);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newNews.Id}, newNews);
        }

        /// <summary>
        /// Update a specific news by id
        /// </summary>
        /// <param name="id" example="123">The news id</param>
        /// <response code="200">News retrieved</response>
        /// <response code="404">News not found</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Update(int id, NewsDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var news = _context.News.Find(id);
            if (news == null)
            {
                return NotFound(string.Format("News {0} not found", id));
            }
            
            news.Header = request.Header;
            news.Detail = request.Detail;
            news.AuthorId = request.AuthorId;
            news.IsAdminNews = request.IsAdminNews;

            _context.News.Update(news);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Delete a specific news by id
        /// </summary>
        /// <param name="id" example="123">The news id</param>
        /// <response code="200">News deleted</response>
        /// <response code="404">News not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            var news = _context.News.Find(id);
            if (news == null)
            {
                return NotFound(string.Format("News {0} not found", id));
            }

            _context.News.Remove(news);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
