using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }

        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /Species/Add
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            string query = "insert into Species(Name) values (@SpeciesName)";
            SqlParameter[] sqlparams = new SqlParameter[1]; 

            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult Add()
        {

            return View();
        }

        // GET: Species/Details/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.SqlQuery("select * from Species where SpeciesID=@SpeciesID", new SqlParameter("@SpeciesID", id)).FirstOrDefault();
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        public ActionResult Delete(int id)
        {
            //need information about a particular pet

            Species selectedspecies = db.Species.SqlQuery("select * from species where SpeciesID = @id", new SqlParameter("@id", id)).FirstOrDefault();

            return View(selectedspecies);
        }

        public ActionResult DeleteSpecies(int? id)
        {
            //need information about a particular pet
            string query = "delete from species where SpeciesID = @id";
            SqlParameter[] sqlparams = new SqlParameter[1]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            //need information about a particular pet
            Species selectedspecies = db.Species.SqlQuery("select * from species where SpeciesID = @id", new SqlParameter("@id", id)).FirstOrDefault();

            return View(selectedspecies);
        }

        [HttpPost]
        public ActionResult Update(int id, string SpeciesName)
        {
            string query = "update Species set Name = @SpeciesName where SpeciesID = @SpeciesID";
            SqlParameter[] sqlparams = new SqlParameter[2];

            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);
            sqlparams[1] = new SqlParameter("@SpeciesID", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
    }
}