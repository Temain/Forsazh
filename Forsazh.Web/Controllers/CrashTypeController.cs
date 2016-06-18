using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using SaleOfDetails.Domain.Context;
using SaleOfDetails.Domain.DataAccess.Interfaces;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models;

namespace SaleOfDetails.Web.Controllers
{
    public class CrashTypeController : BaseApiController
    {
        public CrashTypeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET: api/Product
        public IEnumerable<CrashTypeViewModel> GetCrashTypes()
        {
            var crashTypes = UnitOfWork.Repository<CrashType>()
                .GetQ(orderBy: o => o.OrderBy(p => p.CrashTypeName));

            var crashTypeViewModels = Mapper.Map<IEnumerable<CrashType>, IEnumerable<CrashTypeViewModel>>(crashTypes);

            return crashTypeViewModels;
        }

        // GET: api/Product
        public ListViewModel<CrashTypeViewModel> GetCrashTypes(int page, int pageSize = 10)
        {
            var crashTypesList = UnitOfWork.Repository<CrashType>()
                .GetQ(orderBy: o => o.OrderBy(p => p.CrashTypeName));

            var crashTypes = crashTypesList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var crashTypeViewModels = Mapper.Map<IEnumerable<CrashType>, IEnumerable<CrashTypeViewModel>>(crashTypes);
            var viewModel = new ListViewModel<CrashTypeViewModel>
            {
                Items = crashTypeViewModels,
                ItemsCount = crashTypesList.Count(),
                PagesCount = (int)Math.Ceiling((double)crashTypesList.Count() / pageSize),
                SelectedPage = page
            };

            return viewModel;
        }

        // GET: api/Product/?query=#{query}
        public IHttpActionResult GetCrashTypes(string query)
        {
            var crashTypes = UnitOfWork.Repository<CrashType>()
                .GetQ();

            if (query != null)
            {
                crashTypes = crashTypes.Where(x => x.CrashTypeName.StartsWith(query));
            }

            var crashTypeViewModels = crashTypes.Select(x => new
            {
                Id = x.CrashTypeId,
                Name = x.CrashTypeName,
                Cost = x.RepairCost
            });

            return Ok(crashTypeViewModels);
        }

        // GET: api/Product/5
        [ResponseType(typeof(CrashType))]
        public IHttpActionResult GetCrashType(int id)
        {
            var crashType = UnitOfWork.Repository<CrashType>()
                .Get(x => x.CrashTypeId == id)
                .SingleOrDefault();
            if (crashType == null)
            {
                return NotFound();
            }

            var crashTypeViewModel = Mapper.Map<CrashType, CrashTypeViewModel>(crashType);

            return Ok(crashTypeViewModel);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCrashType(CrashTypeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var crashType = UnitOfWork.Repository<CrashType>()
                .Get(x => x.CrashTypeId == viewModel.CrashTypeId)
                .SingleOrDefault();
            if (crashType == null)
            {
                return BadRequest();
            }

            Mapper.Map<CrashTypeViewModel, CrashType>(viewModel, crashType);
            crashType.UpdatedAt = DateTime.Now;

            UnitOfWork.Repository<CrashType>().Update(crashType);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrashTypeExists(viewModel.CrashTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Product
        [ResponseType(typeof(CrashType))]
        public IHttpActionResult PostCrashType(CrashTypeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var crashType = Mapper.Map<CrashTypeViewModel, CrashType>(viewModel);
            crashType.CreatedAt = DateTime.Now;          

            UnitOfWork.Repository<CrashType>().Insert(crashType);
            UnitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(CrashType))]
        public IHttpActionResult DeleteCrashType(int id)
        {
            CrashType crashType = UnitOfWork.Repository<CrashType>().GetById(id);
            if (crashType == null)
            {
                return NotFound();
            }

            UnitOfWork.Repository<CrashType>().Delete(crashType);
            UnitOfWork.Save();

            return Ok(crashType);
        }

        private bool CrashTypeExists(int id)
        {
            return UnitOfWork.Repository<CrashType>().GetQ().Count(e => e.CrashTypeId == id) > 0;
        }
    }
}