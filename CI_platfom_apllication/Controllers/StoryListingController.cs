﻿using CI_platform.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_platfom_apllication.Controllers
{
    public class StoryListingController : Controller
    {
        private readonly ILogger<StoryListingController> _logger;
        private readonly IStoryRepository _storyRepository;

        public StoryListingController(ILogger<StoryListingController> logger, IStoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;

        }
        public IActionResult storylisting(string? SearchInputdata = "", int pageindex = 1, int pageSize = 9)
        {
            var entity = _storyRepository.getstories(SearchInputdata,pageindex,pageSize);
            if (entity == null)
            {
                return NotFound(); // or some other error response
            }
            return View(entity);
        }
       
        [HttpGet]
        public IActionResult addstory(string missionid) 
        
        {
            var user_id = long.Parse(HttpContext.Session.GetString("userid"));
            if (user_id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var entity = _storyRepository.addstorydetail(user_id,missionid);
                return View(entity);
            }
          
        }
        public IActionResult loadaddstory(string missionid)
        {
            return Json(new { redirectUrl = Url.Action("addstory", "StoryListing", new { missionid = missionid })});

        }
        [HttpPost]
        public IActionResult storydatabse(string missionid, string title, string description, string status, string[] images, string videos,DateTime date)
        {
            long mission_id = long.Parse(missionid);
            var user_id = long.Parse(HttpContext.Session.GetString("userid"));

            var entity = _storyRepository.storydatabase(mission_id, title, description, status, images, user_id,date);
            var entity1 = _storyRepository.storymedia(mission_id, user_id, images, videos);

            return Json(new { redirectUrl = Url.Action("addstory", "StoryListing", new { missionid = missionid }) });

        }
        public IActionResult editdatabase(string missionid, string title, string description, string status, string[] images, string videos, DateTime date)
        {
            long mission_id = long.Parse(missionid);
            var user_id = long.Parse(HttpContext.Session.GetString("userid"));

            var entity = _storyRepository.editstorydatabase(mission_id, title, description, status, user_id,date);
            var entity1 = _storyRepository.editstorymedia(mission_id, user_id, images, videos);

            return Json(new { redirectUrl = Url.Action("storylisting", "StoryListing") });

        }
        public IActionResult submit(long storyId)
        {
            _storyRepository.submit(storyId);
            return Json(new { redirectUrl = Url.Action("storylisting", "StoryListing") });
        }
        /*  public IActionResult addstorydetail(int story_id)
          {
              return View();
          }*/

        public IActionResult storydetail(int story_id)
        {
            var entity = _storyRepository.getstorydetail(story_id);
            return View(entity);
        }

       

        public JsonResult getmissions()
        {
            var user_id = long.Parse(HttpContext.Session.GetString("userid"));
            var entity = _storyRepository.missions(user_id);
            return Json(entity);

        }
    }
}