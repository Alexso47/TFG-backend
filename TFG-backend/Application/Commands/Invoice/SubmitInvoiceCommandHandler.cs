using Application.Commands.Extensions;
using Domain;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Invoice
{
    class SubmitInvoiceCommandHandler : IRequestHandler<SubmitInvoiceCommand, string>
    {
        private readonly IFacilityService _facilityService;
        private readonly IInvoicesRepository _invoicesRepository;
        private readonly ISerialsRepository _serialsRepository;


        public SubmitInvoiceCommandHandler(IFacilityService facilityService, IInvoicesRepository invoicesRepository, ISerialsRepository serialsRepository)
        {
            _facilityService = facilityService;
            _invoicesRepository = invoicesRepository;
        }

        public async Task<string> Handle(SubmitInvoiceCommand request, CancellationToken cancellationToken)
        {
            InvoiceRequestDTO dto = request.Request;
            try
            {
                dto.ValidateObject("La request no puede ser null");
                LocalValidations(dto);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            Facilities buyer = new Facilities();

            if (dto.BuyerEU.Value == 1)
            {
                var res = _facilityService.GetFacilityById(dto.BuyerID);
                if (res.Result != null)
                {
                    buyer = res.Result;
                }
                else
                {
                    throw new Exception("No existe este FID europeo");
                }
            }
            else
            {
                var res = _facilityService.GetFacilityByFacility(dto.BuyerName , dto.BuyerCountry, dto.BuyerCity, dto.BuyerAddress, dto.BuyerZipCode);
                if (res.Result != null)
                {
                    buyer = res.Result;
                }
                else
                {
                    throw new Exception("No existe este Facility");
                }
            }

            if(buyer.Id != null)
            {
                List<SerialsDB> serials = new List<SerialsDB>();

                foreach(var s in dto.Serials)
                {
                    if(s != "" && s != null && s !="[]")
                    {
                        serials.Add(new SerialsDB
                        {
                            Serial = s
                        });
                    }
                }

                var invoice = new InvoicesDB
                {
                    Id = Int32.Parse(dto.Id),
                    InvoiceDate = dto.InvoiceDate.DateTime,
                    Price = Convert.ToDouble(dto.Price),
                    Currency = dto.Currency,
                    BuyerId = buyer.Id,
                    BuyerEU = Convert.ToBoolean(dto.BuyerEU.Value)
                };
                try
                {
                    var added = _invoicesRepository.Add(invoice, serials).Result;
                    return added.Id.ToString();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Error al crer la factura");
            }
        }

        private void LocalValidations(InvoiceRequestDTO dto)
        {
            dto.Id.ValidateString("El Id de la factura es necesario");
            dto.InvoiceDate.ValidateObject("La fecha de factura es necesaria");
            dto.Currency.ValidateString("La moneda es necesaria");
            dto.BuyerEU.ValidateObject("Comprador en EU es necesario");
            if (dto.BuyerEU.Value == 1)
            {
                dto.BuyerID.ValidateString("Si el comprador es europeo, el ID es necesario");
            }
            else
            {
                dto.BuyerName.ValidateString("El nombre del comprador es necesario para compradores extraeuropeos");
                dto.BuyerCountry.ValidateString("El pais del comprador es necesario para compradores extraeuropeos");
                dto.BuyerCity.ValidateString("La ciudad del comprador es necesario para compradores extraeuropeos");
                dto.BuyerAddress.ValidateString("La dirección del comprador es necesario para compradores extraeuropeos");
                dto.BuyerZipCode.ValidateString("El codigo postal del comprador es necesario para compradores extraeuropeos");
            }
        }
    }
}
