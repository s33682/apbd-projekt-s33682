using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Models;

namespace WebApp.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddIndividualClient(PostNewIndividualClientDto dto)
    {
        var checkAddress = await _context.Addresses.FirstOrDefaultAsync(a => 
            a.City == dto.Address.City &&
            a.Street == dto.Address.Street &&
            a.Number == dto.Address.Number &&
            a.ZipCode == dto.Address.ZipCode &&
            a.Apartment == dto.Address.Apartment);

        var checkIndividual = await _context.Individuals.FirstOrDefaultAsync(i => i.Pesel == dto.Pesel);
        
        if (checkIndividual != null)
        {
            throw new AlreadyDoneException("Individual client with this Pesel already exists!");
        }
        
        var transaction  = await _context.Database.BeginTransactionAsync();

        try
        {
            checkAddress ??= new Address
            {
                City = dto.Address.City,
                Street = dto.Address.Street,
                Number = dto.Address.Number,
                ZipCode = dto.Address.ZipCode,
                Apartment = dto.Address.Apartment
            };

            var individualClient = new Individual
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address =  checkAddress,
                Email = dto.Email,
                PhoneNumber =  dto.PhoneNumber,
                Pesel =  dto.Pesel
            };

            var mainClient = new Client
            {
                Individual =  individualClient,
                Company = null
            };
            await _context.Clients.AddAsync(mainClient);
            await _context.SaveChangesAsync();
            
            await _context.Database.CommitTransactionAsync();
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    
    public async Task AddCompanyClient(PostNewCompanyClientDto dto)
    {
        var checkAddress = await _context.Addresses.FirstOrDefaultAsync(a => 
            a.City == dto.Address.City &&
            a.Street == dto.Address.Street &&
            a.Number == dto.Address.Number &&
            a.ZipCode == dto.Address.ZipCode &&
            a.Apartment == dto.Address.Apartment);

        var checkCompany = await _context.Companies.FirstOrDefaultAsync(i => i.KrsNumber == dto.KrsNumber);
        
        if (checkCompany != null)
        {
            throw new AlreadyDoneException("Company client with this Krs number already exists!");
        }
        
        var transaction  = await _context.Database.BeginTransactionAsync();

        try
        {
            checkAddress ??= new Address
            {
                City = dto.Address.City,
                Street = dto.Address.Street,
                Number = dto.Address.Number,
                ZipCode = dto.Address.ZipCode,
                Apartment = dto.Address.Apartment
            };

            var companyClient = new Company
            {
                Name = dto.Name,
                Address =  checkAddress,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                KrsNumber = dto.KrsNumber
            };

            var mainClient = new Client
            {
                Individual =  null,
                Company = companyClient
            };
            await _context.Clients.AddAsync(mainClient);
            await _context.SaveChangesAsync();
            
            await _context.Database.CommitTransactionAsync();
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteClient(int clientId)
    {
        var client = await _context.Clients
            .Include(i => i.Individual)
            .FirstOrDefaultAsync(c=> c.ClientId == clientId);

        if (client == null)
        {
            throw new NotFoundException("Client does not exist!");
        }

        if (client.Individual != null)
        {
            if (client.Individual.IsActive == false)
            {
                throw new AlreadyDoneException("This individual client is already deleted!");
            }
            client.Individual.IsActive = false;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NotPossibleException("Cannot delete company client!");
        }
    }

    public async Task UpdateIndividualClient(int clientId, PutUpdateIndividualClientDto dto)
    {
        var checkClient = await _context.Clients
            .Include(i => i.Individual).ThenInclude(i => i!.Address)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (checkClient == null)
        {
            throw new NotFoundException("Client does not exist!");
        }

        if (checkClient.Individual == null || checkClient.CompanyId != null)
        {
            throw new NotPossibleException("This ID belongs to company client!!");
        }
        
        var transaction  = await _context.Database.BeginTransactionAsync();
        try
        {
            
            var individual = checkClient.Individual!;
            
            var checkAddress = individual.Address;
            
            if (dto.Address != null)
            {
                checkAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.City == dto.Address.City &&
                    a.Street == dto.Address.Street && a.ZipCode == dto.Address.ZipCode && a.Apartment == dto.Address.Apartment && a.Number == dto.Address.Number);

                if (checkAddress == null)
                {
                
                    checkAddress = new Address
                    {
                        City = dto.Address.City,
                        ZipCode = dto.Address.ZipCode,
                        Street = dto.Address.Street,
                        Number = dto.Address.Number,
                        Apartment = dto.Address.Apartment
                    };
                }
            }
            
            individual.FirstName = dto.FirstName ??  individual.FirstName;
            individual.LastName = dto.LastName ??  individual.LastName;
            individual.Address = checkAddress;
            individual.Email = dto.Email ??  individual.Email;
            individual.PhoneNumber = dto.PhoneNumber ??  individual.PhoneNumber;
            
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
            
        }catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    public async Task UpdateCompanyClient(int clientId, PutUpdateCompanyClientDto dto)
    {
        var checkClient = await _context.Clients
            .Include(i => i.Company).ThenInclude(i => i!.Address)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (checkClient == null)
        {
            throw new NotFoundException("Client does not exist!");
        }

        if (checkClient.Company == null || checkClient.IndividualId != null)
        {
            throw new NotPossibleException("This ID belongs to individual client!!");
        }
        
        var transaction  = await _context.Database.BeginTransactionAsync();
        try
        {
            
            var company = checkClient.Company!;
            
            var checkAddress = company.Address;
            
            if (dto.Address != null)
            {
                checkAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.City == dto.Address.City &&
                    a.Street == dto.Address.Street && a.ZipCode == dto.Address.ZipCode && a.Apartment == dto.Address.Apartment && a.Number == dto.Address.Number);

                if (checkAddress == null)
                {
                    checkAddress = new Address
                    {
                        City = dto.Address.City,
                        ZipCode = dto.Address.ZipCode,
                        Street = dto.Address.Street,
                        Number = dto.Address.Number,
                        Apartment = dto.Address.Apartment
                    };
                }
            }
            
            company.Name = dto.Name ??  company.Name;
            company.Address = checkAddress;
            company.Email = dto.Email ??  company.Email;
            company.PhoneNumber = dto.PhoneNumber ??  company.PhoneNumber;
            
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
            
        }catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}