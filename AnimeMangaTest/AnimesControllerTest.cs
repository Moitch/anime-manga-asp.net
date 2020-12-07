using COMP2084_Assignment1.Data;
using COMP2084_Assignment1.Models;
using COMP2084_Assignment1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeMangaTest
{
    [TestClass]
    public class AnimesControllerTest
    {
        private ApplicationDbContext _context;

        List<Anime> animes = new List<Anime>();
        AnimesController controller;

        [TestInitialize]
        public void AnimeControllerTestsInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            var genre = new Genre { ID = 25, Name = "Sports" };
            animes.Add(new Anime { ID = 12, Name = "Haikyuu!!", Episodes = 8, AirStart = DateTime.Now, Genres = genre });
            animes.Add(new Anime { ID = 103, Name = "Naruto", Episodes = 9, AirStart = DateTime.Now, Genres = genre });
            animes.Add(new Anime { ID = 64, Name = "Bleach", Episodes = 10, AirStart = DateTime.Now, Genres = genre });

            foreach (var a in animes)
            {
                _context.Animes.Add(a);
            }

            _context.SaveChanges();
            controller = new AnimesController(_context);
        }

        //INDEX

        [TestMethod]
        public void IndexViewLoads()
        {
            
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsAnimeData()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;

            List<Anime> model = (List<Anime>)viewResult.Model;

            
            CollectionAssert.AreEqual(animes.OrderBy(a => a.Name).ToList(), model);
        }

        //POST::EDIT

        [TestMethod]
        public void EditReturnsId()
        {

            var result = controller.Edit(12, animes[0]);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void EditSaved()
        {
            var anime = animes[0];
            anime.Episodes = 54;
            var result = controller.Edit(anime.ID, anime);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void EditIdInDatabase()
        {
            var checkAnime = new Anime { ID = 1, Name = "fake", Episodes = 12, AirStart = DateTime.Now, Genres = new Genre { ID = 595, Name = "random" } };

            var result = controller.Edit(1, checkAnime);
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Error", viewResult.ViewName);
        }

        //GET::EDIT
        [TestMethod]
        public void AnimesGetEditNullId() 
        {
            var result = controller.Edit(null);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void AnimesGetEditInvalidId()
        {
            var result = controller.Edit(5);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void AnimesGetEditValidId()
        {
            var result = controller.Edit(12);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Edit", viewResult.ViewName);

        }

        [TestMethod]
        public void EditLoadsCorrectModel()
        {
            var result = controller.Edit(12);
            var viewResult = (ViewResult)result.Result;
            Anime model = (Anime)viewResult.Model;
            //List<Category> categoryModel= (List<Category>)viewResult.Model;
            //var viewData =viewResult.ViewData;

            Assert.AreEqual(_context.Animes.Find(87), model);
            //Assert.AreEqual(viewData,viewResult.ViewData);
            //Assert.IsTrue(viewData["CategoryId"]!=null);
        }

        [TestMethod]
        public void EditLoadsViewData()
        {
            var result = controller.Edit(12);
            var viewResult = (ViewResult)result.Result;
            var viewData = viewResult.ViewData;

            Assert.AreEqual(viewData, viewResult.ViewData);
        }

        [TestMethod]
        public void EditLoadsErrorViewForInvalidModel()
        {
            var result = controller.Edit(10);
            var viewResult = (ViewResult)result.Result;
            Anime model = (Anime)viewResult.Model;

            Assert.AreNotEqual(_context.Animes.FindAsync(10), model);
        }

        //GET::CREATE

        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            var result = controller.Create();
            var viewResult = (ViewResult)result;

            //Assert 
            Assert.AreEqual("Create", viewResult.ViewName);

        }

        [TestMethod]
        public void CreateReturnsValidList()
        {

            var result = controller.Create();
            var viewData = controller.ViewData["GenreID"];

            Assert.IsNotNull(viewData);
        }

        

        //POST::CREATE
        [TestMethod]
        public void PostCreateReturnsToList()
        {
            var checkAnime = new Anime { ID = 1, Name = "fake", Episodes = 12, AirStart = DateTime.Now, Genres = new Genre { ID = 595, Name = "random" } };

            var result = controller.Create(checkAnime);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void PostCreateSavedToDatabase()
        {
            var checkAnime = new Anime { ID = 1, Name = "fake", Episodes = 12, AirStart = DateTime.Now, Genres = new Genre { ID = 595, Name = "random" } };

            _context.Animes.Add(checkAnime);
            _context.SaveChanges();

            Assert.AreEqual(checkAnime, _context.Animes.ToArray()[3]);
        }

        [TestMethod]
        public void PostCreateReturnsCreate()
        {           
            var tempAnime = new Anime { };
            controller.ModelState.AddModelError("put a descriptive key name here", "add an appropriate key value here");
            var result = controller.Create(tempAnime);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void PostCreateValidData()
        {
            var tempAnime = new Anime { };

            controller.ModelState.AddModelError("put a descriptive key name here", "add an appropriate key value here");
            var result = controller.Create(tempAnime);
            var viewResult = (ViewResult)result.Result;

            Assert.IsNotNull(viewResult.ViewData);
        }

        //GET::DELETE
        [TestMethod]
        public void GetDeleteNullID()
        {
            var result = controller.Delete(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteInvalidID()
        {
            var result = controller.Delete(99); 
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteReturnsDeleteView()
        {
            var id = 12;
            var result = controller.Delete(id);
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteValidProduct()
        {
            var id = 12;
            var result = controller.Delete(id); 
            var viewResult = (ViewResult)result.Result;
            Anime tempAnime = (Anime)viewResult.Model;
            Assert.AreEqual(animes[0], tempAnime);
        }

        //POST::DELETE
        [TestMethod]
        public void PostDeleteSuccess()
        {
            var id = 12;
            var result = controller.DeleteConfirmed(id); 
            var product = _context.Animes.Find(id);
            Assert.AreEqual(product, null);
        }

        [TestMethod]
        public void PostDeleteReturnsIndex()
        {
            var id = 12;
            var result = controller.DeleteConfirmed(id);
            var actionResult = (RedirectToActionResult)result.Result;
            Assert.AreEqual("Index", actionResult.ActionName);
        }
        //DETAILS
        [TestMethod]
        public void DetailsNoId()
        {
            
            var result = controller.Details(id: null);
            var viewResult = (ViewResult)result.Result;    
            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailsInvalidId()
        {

            
            var result = controller.Details(23);
            var viewResult = (ViewResult)result.Result;

            
            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailsViewLoads()
        {
            
            var result = controller.Details(87);
            var viewResult = (ViewResult)result.Result;
      
            Assert.AreEqual("Details", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailObjectReturnsMatches()
        {
            
            int id = animes[0].ID;
            var result = controller.Details(id);
            var viewResult = (ViewResult)result.Result;
                
            Assert.AreEqual(animes[0], viewResult.Model);
        }

        
    }
}
