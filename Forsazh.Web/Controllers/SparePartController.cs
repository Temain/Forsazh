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
    public class SparePartController : BaseApiController
    {
        public SparePartController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET: api/Product
        public IEnumerable<SparePartViewModel> GetSpareParts()
        {
            var spareParts = UnitOfWork.Repository<SparePart>()
                .GetQ(orderBy: o => o.OrderBy(p => p.CreatedAt));

            var sparePartViewModels = Mapper.Map<IEnumerable<SparePart>, IEnumerable<SparePartViewModel>>(spareParts);

            return sparePartViewModels;
        }

        // GET: api/Product
        public ListViewModel<SparePartViewModel> GetSpareParts(int page, int pageSize = 10)
        {
            var sparePartsList = UnitOfWork.Repository<SparePart>()
                .GetQ(orderBy: o => o.OrderBy(p => p.CreatedAt));

            var spareParts = sparePartsList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var sparePartViewModels = Mapper.Map<IEnumerable<SparePart>, IEnumerable<SparePartViewModel>>(spareParts);
            var viewModel = new ListViewModel<SparePartViewModel>
            {
                Items = sparePartViewModels,
                ItemsCount = sparePartsList.Count(),
                PagesCount = (int)Math.Ceiling((double)sparePartsList.Count() / pageSize),
                SelectedPage = page
            };

            return viewModel;
        }

        // GET: api/Product/?query=#{query}
        public IHttpActionResult GetSpareParts(string query)
        {
            var spareParts = UnitOfWork.Repository<SparePart>()
                .GetQ();

            if (query != null)
            {
                spareParts = spareParts.Where(x => x.SparePartName.StartsWith(query));
            }

            var sparePartViewModels = spareParts.Select(x => new
            {
                Id = x.SparePartId,
                Name = x.SparePartName,
                Cost = x.Cost
            });

            return Ok(sparePartViewModels);
        }

        // GET: api/Product/5
        [ResponseType(typeof(SparePart))]
        public IHttpActionResult GetSparePart(int id)
        {
            var sparePart = UnitOfWork.Repository<SparePart>()
                .Get(x => x.SparePartId == id)
                .SingleOrDefault();
            if (sparePart == null)
            {
                return NotFound();
            }

            var sparePartViewModel = Mapper.Map<SparePart, SparePartViewModel>(sparePart);

            return Ok(sparePartViewModel);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSparePart(SparePartViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sparePart = UnitOfWork.Repository<SparePart>()
                .Get(x => x.SparePartId == viewModel.SparePartId)
                .SingleOrDefault();
            if (sparePart == null)
            {
                return BadRequest();
            }

            Mapper.Map<SparePartViewModel, SparePart>(viewModel, sparePart);
            sparePart.UpdatedAt = DateTime.Now;

            UnitOfWork.Repository<SparePart>().Update(sparePart);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SparePartExists(viewModel.SparePartId))
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
        [ResponseType(typeof(SparePart))]
        public IHttpActionResult PostSparePart(SparePartViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sparePart = Mapper.Map<SparePartViewModel, SparePart>(viewModel);
            sparePart.CreatedAt = DateTime.Now;          

            UnitOfWork.Repository<SparePart>().Insert(sparePart);
            UnitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(SparePart))]
        public IHttpActionResult DeleteSparePart(int id)
        {
            SparePart sparePart = UnitOfWork.Repository<SparePart>().GetById(id);
            if (sparePart == null)
            {
                return NotFound();
            }

            UnitOfWork.Repository<SparePart>().Delete(sparePart);
            UnitOfWork.Save();

            return Ok(sparePart);
        }

        private bool SparePartExists(int id)
        {
            return UnitOfWork.Repository<SparePart>().GetQ().Count(e => e.SparePartId == id) > 0;
        }
    }
}