using Domain;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {

        private readonly DBContext _context;
        private readonly IInvoiceService _invoicesService;
        private readonly ISerialService _serialService;


        public InvoicesRepository(DBContext context, IInvoiceService invoicesService, ISerialService serialService)
        {
            _context = context;
            _invoicesService = invoicesService;
            _serialService = serialService;
        }

        public async Task<InvoicesDB> Add(InvoicesDB invoice, List<SerialsDB> serials)
        {
            var existingInvoice = _invoicesService.GetInvoiceById(invoice.Id);

            if (existingInvoice.Result == null)
            {
                EntityEntry<InvoicesDB> result;
                try
                {
                    result = await _context.Invoices.AddAsync(invoice);
                }
                catch 
                {
                    throw new Exception("Error al crear la factura");
                }
                
                int added;
                try
                {
                    added = AddSerials(serials, invoice.Id).Result;
                }
                catch
                {
                    throw new Exception("Error insertando los seriales");
                }

                if (added == serials.Count)
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                        return result.Entity;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw new Exception("Algunos seriales no se han insertado, se ha cancelado la operacion");
                }
            }
            else
            {
                throw new Exception("La factura ya existe");
            }
        }

        private async Task<int> AddSerials(List<SerialsDB> serials, int id)
        {
            int adds = 0;
            int idSerial = _invoicesService.GetLastIdInvoiceSerials().Result;
            idSerial++;
            foreach (var s in serials)
            {
                var existingSerial = _serialService.GetSerialBySerial(s.Serial);
                var aux = existingSerial.Result != null ? existingSerial.Result.Id : 0;
                if (aux != 0)
                {
                    var newItem = new InvoiceSerials
                    {
                        Id = idSerial,
                        Serial = s.Serial.ToString(),
                        InvoiceID = id
                    };
                    idSerial++;

                    var res = await _invoicesService.UpdateInvoiceSerialsTable(newItem);
                    adds += res;
                }
            }
            return adds;
        }
    }
}
