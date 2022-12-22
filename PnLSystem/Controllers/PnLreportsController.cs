using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PnLSystem.Models;
using PnLSystem.ResponseDTOs.PagingModel;
using PnLSystem.ResponseDTOs.SearchModel;

namespace PnLSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PnLreportsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public PnLreportsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/PnLreports
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<PnLreport>>>> GetPnLreports([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.PnLreports.Where(o => o.CreationDate.ToString().Contains(searchModel.SearchTerm)
                || o.StartDate.ToString().Contains(searchModel.SearchTerm) ||
                o.StoreId.ToString().Contains(searchModel.SearchTerm) ||
                o.BrandId.ToString().Contains(searchModel.SearchTerm) ||
                o.EndDate.ToString().Contains(searchModel.SearchTerm) ||
                o.TotalProfit.ToString().Contains(searchModel.SearchTerm) ||
                o.TotalLost.ToString().Contains(searchModel.SearchTerm) ||
                o.Id.ToString().Equals(searchModel.SearchTerm) ||
                o.UpdateDate.ToString().Equals(searchModel.SearchTerm)).OrderByDescending(o => o.UpdateDate).ToListAsync();
                if (searchModel.SearchTerm == "" || searchModel.SearchTerm == null)
                {
                    list = await _context.PnLreports.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.PnLreport>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.PnLreport()
                    {
                        Id = i.Id,
                        StoreId= i.StoreId,
                        StartDate= i.StartDate,
                        BrandId=i.BrandId,
                        CreationDate= i.CreationDate,
                        EndDate= i.EndDate,
                        TotalLost= i.TotalLost,
                        TotalProfit= i.TotalProfit,
                        UpdateDate= i.UpdateDate
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.PnLreport>()
                {
                    PageIndex = paging.PageIndex,
                    PageSize = paging.PageSize,
                    TotalItem = totalItem,
                    TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                    Data = list1
                };
                return Ok(groupUserResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(error: ex.Message);
            }
        }

        // GET: api/PnLreports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PnLreport>> GetPnLreport(int id)
        {
            var pnLreport = await _context.PnLreports.FindAsync(id);

            if (pnLreport == null)
            {
                return NotFound();
            }

            return pnLreport;
        }

        // PUT: api/PnLreports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPnLreport(int id, PnLreport pnLreport)
        {
            if (id != pnLreport.Id)
            {
                return BadRequest();
            }

            _context.Entry(pnLreport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PnLreportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PnLreports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PnLreport>> PostPnLreport(PnLreport pnLreport)
        {
            _context.PnLreports.Add(pnLreport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPnLreport", new { id = pnLreport.Id }, pnLreport);
        }

        // DELETE: api/PnLreports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePnLreport(int id)
        {
            var pnLreport = await _context.PnLreports.FindAsync(id);
            if (pnLreport == null)
            {
                return NotFound();
            }

            _context.PnLreports.Remove(pnLreport);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var testdata = await _context.PnLreports.ToListAsync();
            var testdata1 = await _context.ReportExpenses.ToListAsync();
            var testdata2 = await _context.ReportRevenues.ToListAsync();

            //using System.Data;
            DataTable dt = new DataTable("PnL Report");
            dt.Columns.AddRange(new DataColumn[9] { 
                                     new DataColumn("ID"),
                                     new DataColumn("StoreID"),
                                     new DataColumn("BrandID"),
                                     new DataColumn("CreationDate"),
                                     new DataColumn("StartDate"),
                                     new DataColumn("EndDate"),
                                     new DataColumn("UpdateDate"),
                                     new DataColumn("TotalProfit"),
                                     new DataColumn("TotalLost") });

            foreach (var emp in testdata)
            {
                dt.Rows.Add(emp.Id, emp.StoreId, emp.BrandId, emp.CreationDate, emp.StartDate, emp.EndDate, emp.UpdateDate, emp.TotalProfit, emp.TotalLost );
            }

            //using System.Data;
            DataTable dt1 = new DataTable("Expense");
            dt1.Columns.AddRange(new DataColumn[6] {
                                     new DataColumn("ID"),
                                     new DataColumn("SheetID"),
                                     new DataColumn("Name"),
                                     new DataColumn("CreationDate"),
                                     new DataColumn("Description"),
                                     new DataColumn("Value") });

            foreach (var emp in testdata1)
            {
                dt1.Rows.Add(emp.Id, emp.SheetId, emp.Name, emp.CreationDate, emp.Description, emp.Value);
            }
            //using System.Data;
            DataTable dt2 = new DataTable("Revenue");
            dt2.Columns.AddRange(new DataColumn[6] {
                                     new DataColumn("ID"),
                                     new DataColumn("SheetID"),
                                     new DataColumn("Name"),
                                     new DataColumn("CreationDate"),
                                     new DataColumn("Description"),
                                     new DataColumn("Value") });

            foreach (var emp in testdata2)
            {
                dt2.Rows.Add(emp.Id, emp.SheetId, emp.Name, emp.CreationDate, emp.Description, emp.Value);
            }
            //using ClosedXML.Excel;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Worksheets.Add(dt1);
                wb.Worksheets.Add(dt2);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PnLReport.xlsx");
                }
            }
        }

        [HttpGet("ImportPOS")]
        public async Task<IActionResult> ImportPOS()
        {
            var testdata = await _context.PnLreports.ToListAsync();
            var testdata1 = await _context.ReportExpenses.ToListAsync();
            var testdata2 = await _context.ReportRevenues.ToListAsync();

            //using System.Data;
            DataTable dt = new DataTable("PnL Report");
            dt.Columns.AddRange(new DataColumn[9] {
                                     new DataColumn("ID"),
                                     new DataColumn("StoreID"),
                                     new DataColumn("BrandID"),
                                     new DataColumn("CreationDate"),
                                     new DataColumn("StartDate"),
                                     new DataColumn("EndDate"),
                                     new DataColumn("UpdateDate"),
                                     new DataColumn("TotalProfit"),
                                     new DataColumn("TotalLost") });

            foreach (var emp in testdata)
            {
                dt.Rows.Add(emp.Id, emp.StoreId, emp.BrandId, emp.CreationDate, emp.StartDate, emp.EndDate, emp.UpdateDate, emp.TotalProfit, emp.TotalLost);
            }

            //using System.Data;
            DataTable dt1 = new DataTable("Expense");
            dt1.Columns.AddRange(new DataColumn[6] {
                                     new DataColumn("ID"),
                                     new DataColumn("SheetID"),
                                     new DataColumn("Name"),
                                     new DataColumn("CreationDate"),
                                     new DataColumn("Description"),
                                     new DataColumn("Value") });

            foreach (var emp in testdata1)
            {
                dt1.Rows.Add(emp.Id, emp.SheetId, emp.Name, emp.CreationDate, emp.Description, emp.Value);
            }
            //using System.Data;
            DataTable dt2 = new DataTable("Revenue");
            dt2.Columns.AddRange(new DataColumn[6] {
                                     new DataColumn("ID"),
                                     new DataColumn("SheetID"),
                                     new DataColumn("Name"),
                                     new DataColumn("CreationDate"),
                                     new DataColumn("Description"),
                                     new DataColumn("Value") });

            foreach (var emp in testdata2)
            {
                dt2.Rows.Add(emp.Id, emp.SheetId, emp.Name, emp.CreationDate, emp.Description, emp.Value);
            }
            //using ClosedXML.Excel;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Worksheets.Add(dt1);
                wb.Worksheets.Add(dt2);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PnLReport.xlsx");
                }
            }
        }
        private bool PnLreportExists(int id)
        {
            return _context.PnLreports.Any(e => e.Id == id);
        }
    }
}
