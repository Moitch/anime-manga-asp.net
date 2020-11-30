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
        // create db reference that will point to our in-memory db
        private ApplicationDbContext _context;

        // create empty product list to hold mock product data
        List<Anime> animes = new List<Anime>();

        // declare controller we are going to test
        AnimesController controller;

        [TestInitialize]
        // this method runs automatically before each unit test to streamline the arranging
        public void TestInitialize()
        {
            // instantiate in-memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // create mock data inside the in-memory db
            var genre = new Genre { ID = 505, Name = "Some Genre" };

            animes.Add(new Anime { ID = 87, Name = "Anime 1", Episodes = 8, AirStart = DateTime.Now, Genres = genre });
            animes.Add(new Anime { ID = 92, Name = "Anime 2", Episodes = 9, AirStart = DateTime.Now, Genres = genre });
            animes.Add(new Anime { ID = 95, Name = "Anime 3", Episodes = 10, AirStart = DateTime.Now, Genres = genre });

            foreach (var a in animes)
            {
                _context.Animes.Add(a);
            }

            _context.SaveChanges();

            // instantiate the products controller and pass it the mock db object (dependency injection)
            controller = new AnimesController(_context);
        }

        //INDEX

        [TestMethod]
        public void IndexViewLoads()
        {
            // no arrange needed as all setup done first in TestInitialize()
            // act, casting the Result property to a ViewResult
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;

            // assert
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsAnimeData()
        {
            // act
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            // cast the result's data Model to a list of products so we can check it
            List<Anime> model = (List<Anime>)viewResult.Model;

            // assert
            CollectionAssert.AreEqual(animes.OrderBy(a => a.Name).ToList(), model);
        }

        //POST::EDIT

        [TestMethod]
        public void EditReturnsId()
        {

            var result = controller.Edit(34, animes[0]);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void EditSaved()
        {
            var anime = animes[0];
            anime.Episodes = 10;
            var result = controller.Edit(anime.ID, anime);
            var redirectResult = (RedirectToActionResult)result.Result;
            // assert
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void EditIdInDatabase()
        {
            var checkAnime = new Anime { ID = 1, Name = "Anime 1", Episodes = 8, AirStart = DateTime.Now, Genres = new Genre { ID = 595, Name = "Some Genre" } };

            var result = controller.Edit(1, checkAnime);
            var viewResult = (ViewResult)result.Result;
            // assert
            Assert.AreEqual("Error", viewResult.ViewName);
        }

        //GET::EDIT
        [TestMethod]
        public void AnimesGetEditNullId() //Invalid id, valid id loads edit view
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
            var result = controller.Edit(87);

            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Edit", viewResult.ViewName);

        }

        [TestMethod]
        public void EditLoadsCorrectModel()
        {
            var result = controller.Edit(87);
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
            var result = controller.Edit(87);
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
            var tempAnime = new Anime { ID = 1, Name = "Anime 1", Episodes = 8, AirStart = DateTime.Now, Genres = new Genre { ID = 595, Name = "Some Genre" } };

            var result = controller.Create(tempAnime);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void PostCreateSavedToDatabase()
        {
            var tempAnime = new Anime { ID = 1, Name = "Anime 1", Episodes = 8, AirStart = DateTime.Now, Genres = new Genre { ID = 595, Name = "Some Genre" } };

            _context.Animes.Add(tempAnime);
            _context.SaveChanges();

            Assert.AreEqual(tempAnime, _context.Animes.ToArray()[3]);
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
            var result = controller.Delete(99); //invalid ID
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteReturnsDeleteView()
        {
            var id = 87;
            var result = controller.Delete(id); // valid ID
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        [TestMethod]
        public void GetDeleteValidProduct()
        {
            var id = 87;
            var result = controller.Delete(id); // valid ID
            var viewResult = (ViewResult)result.Result;
            Anime tempAnime = (Anime)viewResult.Model;
            Assert.AreEqual(animes[0], tempAnime);
        }

        //POST::DELETE
        [TestMethod]
        public void PostDeleteSuccess()
        {
            var id = 87;
            var result = controller.DeleteConfirmed(id); // valid ID
            var product = _context.Animes.Find(id);
            Assert.AreEqual(product, null);
        }

        [TestMethod]
        public void PostDeleteReturnsIndex()
        {
            var id = 87;
            var result = controller.DeleteConfirmed(id); // valid ID
            var actionResult = (RedirectToActionResult)result.Result;
            Assert.AreEqual("Index", actionResult.ActionName);
        }
        //DETAILS
        [TestMethod]
        public void DetailsNoId()
        {
            // Act
            var result = controller.Details(id: null);
            var viewResult = (ViewResult)result.Result;
            // Assert
            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void DetailsInvalidId()
        {

            // Act
            var result = controller.Details(23);
            var viewResult = (ViewResult)result.Result;

            // Assert
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
            //Arrange
            int id = animes[0].ID;
            //Act
            var result = controller.Details(id);
            var viewResult = (ViewResult)result.Result;
            // Assert       
            Assert.AreEqual(animes[0], viewResult.Model);
        }

        
    }
}
