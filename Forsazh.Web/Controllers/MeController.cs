using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using SaleOfDetails.Domain.DataAccess.Interfaces;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models;

namespace SaleOfDetails.Web.Controllers
{
    [Authorize]
    public class MeController : BaseApiController
    {
        public MeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET api/Me
        public MeViewModel Get()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var viewModel = Mapper.Map<ApplicationUser, MeViewModel>(user);

            return viewModel;
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(MeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return BadRequest();
            }

            // Mapper.Map<MeViewModel, ApplicationUser>(viewModel, user);
            user.Person.LastName = viewModel.LastName;
            user.Person.FirstName = viewModel.FirstName;
            user.Person.MiddleName = viewModel.MiddleName;
            user.Person.Birthday = viewModel.Birthday;
            UserManager.Update(user);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}