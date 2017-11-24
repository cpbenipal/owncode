using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IO;
using Abp.UI;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Exporting;
using PanelMasterMVC5Separate.Vehicle;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Manufacturing
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Host_SystemDefaults)]
    public class ManufactureAppService : PanelMasterMVC5SeparateAppServiceBase, IManufactureAppService
    {
        private readonly IRepository<VehicleMake> _VehicleMakeRepository;
        private readonly IRepository<VehicleModels> _VehicleModelsRepository;
        private readonly IAppFolders _appFolders;
        private readonly IRepository<VehicleModelLogos, int> _binaryObjectRepository;
        private readonly IMExporter _IMListExcelExporter;
        private readonly IRepository<VehicleMake, int> _binaryObjectMakeRepository;
        private readonly IRepository<VehicleModels, int> _binaryObjectMadeRepository;
        public ManufactureAppService(
            IAppFolders appFolders,
           IMExporter IMListExcelExporter,
           IRepository<VehicleModelLogos, int> binaryObjectRepository,
           IRepository<VehicleMake> VehicleMakeRepository,
           IRepository<VehicleModels> vehiclemodels,
            IRepository<VehicleMake, int> BinaryObjectMakeRepository,
            IRepository<VehicleModels, int> BinaryObjectMadeRepository
            )
        {
            _binaryObjectRepository = binaryObjectRepository;
            _appFolders = appFolders;
            _VehicleMakeRepository = VehicleMakeRepository;
            _IMListExcelExporter = IMListExcelExporter;
            _VehicleModelsRepository = vehiclemodels;
            _binaryObjectMakeRepository = BinaryObjectMakeRepository;
            _binaryObjectMadeRepository = BinaryObjectMadeRepository;
        }

        public ListResultDto<MakelListDto> GetVehicleInformation(GetVehicleInput input)
        {
            var make = _VehicleMakeRepository.GetAll()
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.Description.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var finalQuery = (from mk in make
                              select new MakelListDto
                              {
                                  MakeID = mk.Id,
                                  CreationTime = mk.CreationTime,
                                  Description = mk.Description,
                                  IsActive = mk.IsActive
                              }).ToList();

            return new ListResultDto<MakelListDto>(ObjectMapper.Map<List<MakelListDto>>(finalQuery));
        }

        public ListResultDto<ModelMadeListDto> GetVehicleMades(GetVehicleInput input)
        {
            var made = _VehicleModelsRepository.GetAll()
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.MMCode.Contains(input.Filter) ||
                     u.Model.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();


            var finalQuery = (from mk in made

                              select new ModelMadeListDto
                              {
                                  MadeID = mk.Id,
                                  CreationTime = mk.CreationTime,
                                  Make = _VehicleMakeRepository.FirstOrDefault(mk.VehicleMakeID).Description,
                                  MMCode = mk.MMCode,
                                  Model = mk.Model
                              })
                              .OrderByDescending(p => p.Make)
                              .ToList();

            return new ListResultDto<ModelMadeListDto>(ObjectMapper.Map<List<ModelMadeListDto>>(finalQuery));
        }

        public Task<VehicleModelLogos> GetOrNullAsync(int id)
        {
            return _binaryObjectRepository.GetAll().Where(c => c.VehicleMakeID == id)
              .FirstOrDefaultAsync();
        }

        private byte[] FetchByteArray(string FileName)
        {
            var tempProfilePicturePath = Path.Combine(_appFolders.TempFileDownloadFolder, FileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tempProfilePicturePath, FileMode.Open))
            {
                byteArray = new byte[fsTempProfilePicture.Length];
                fsTempProfilePicture.Read(byteArray, 0, (int)fsTempProfilePicture.Length);
                fsTempProfilePicture.Close();
            }

            if (byteArray.LongLength > 102400) //100 KB
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit"));
            }
            FileHelper.DeleteIfExists(tempProfilePicturePath);
            return byteArray;
        }

        public async Task CreateMake(VehicleMakeDto input)
        {
            if (!_binaryObjectMakeRepository.GetAll().Any(c => c.Description == input.Description))
            {
                var NewMake = new VehicleMake(input.Description, input.LogoPicture, input.Id);

                int NewMakeId = await _VehicleMakeRepository.InsertAndGetIdAsync(NewMake);

                byte[] byteArray = FetchByteArray(input.LogoPicture);

                var NewPicForBroker = new VehicleModelLogos(byteArray, NewMakeId);

                await _binaryObjectRepository.InsertAsync(NewPicForBroker);
            }
            else
            {
                throw new UserFriendlyException(L("VehicleMakeAlreadyExists"));
            }
        }


        public ListResultDto<VehicleMakeDto> GetMakes()
        {
            var makes = _VehicleMakeRepository
                .GetAll().Where(c => c.IsActive == true)
                .OrderBy(p => p.Description)
                .ToList();

            return new ListResultDto<VehicleMakeDto>(ObjectMapper.Map<List<VehicleMakeDto>>(makes));
        }

        public VehicleMakeDto GetThisMake(GetVehicleInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var query = _VehicleMakeRepository
              .GetAll().Where(c => c.Id == Id && c.IsActive == true)
              .FirstOrDefault();

            return query.MapTo<VehicleMakeDto>();
        }

        public async Task UpdateMake(MakeUpDto input)
        {
            if (!_binaryObjectMakeRepository.GetAll().Any(c => c.Description == input.Description && c.Id != input.Id))
            {
                if (input.NewFileName != null)
                {
                    byte[] byteArray = FetchByteArray(input.NewFileName);
                    var pics = _binaryObjectRepository.GetAll().Where(c => c.VehicleMakeID == input.Id).FirstOrDefault();

                    pics.Bytes = byteArray;
                    await _binaryObjectRepository.UpdateAsync(pics);
                    input.LogoPicture = input.NewFileName;
                }
                var master = _VehicleMakeRepository.FirstOrDefault(input.Id);
                master.Description = input.Description;
                master.LogoPicture = input.LogoPicture;
                await _VehicleMakeRepository.UpdateAsync(master);
            }
            else
            {
                throw new UserFriendlyException(L("VehicleMakeAlreadyExists"));
            }
        }

        public void ChangeStatus(StatusVehicleDto input)
        {
            var query = _VehicleMakeRepository.Get(input.Id);
            query.IsActive = input.Status;
            _VehicleMakeRepository.Update(query);
        }

        public void CreateOrUpdateMade(VehicleModelsDto input)
        {
            var query = input.MapTo<VehicleModels>();

            if (!_binaryObjectMadeRepository.GetAll().Any(c => c.Model == input.Model && (input.Id == 0 || input.Id != c.Id)))
            {
                _VehicleModelsRepository.InsertOrUpdate(query);
            }
            else
                throw new UserFriendlyException(L("VehicleMadeAlreadyExists"));
        }
        public VehicleFromModelsDto GetThisMade(GetVehicleInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var query = _VehicleModelsRepository
              .GetAll().Where(c => c.Id == Id)
              .FirstOrDefault();

            return query.MapTo<VehicleFromModelsDto>();
        }

        public async Task<FileDto> GetMakesToExcel()
        {
            var made = await _VehicleMakeRepository.GetAll().ToListAsync();

            var finalQuery = (from mk in made

                              select new VehicleMakeDto
                              { 
                                  CreationTime = mk.CreationTime,
                                  Description = mk.Description ,
                                  IsActive = mk.IsActive
                              })
                              .OrderByDescending(p => p.LastModificationTime)
                              .ToList();
            var ListDtos = finalQuery.MapTo<List<VehicleMakeDto>>();
            return _IMListExcelExporter.ExportToFile(ListDtos);
        }

        public async Task<FileDto> GetMadesToExcel()
        {
            var made = await _VehicleModelsRepository.GetAll().ToListAsync();

            var finalQuery = (from mk in made
                              select new VehicleModelsFDto
                              { 
                                  CreationTime = mk.CreationTime,
                                  Make = _VehicleMakeRepository.FirstOrDefault(mk.Id).Description,
                                  MMCode = mk.MMCode,
                                  Model = mk.Model
                              })
                              .OrderByDescending(p => p.Make)
                              .ToList();

            var ListDtos = finalQuery.MapTo<List<VehicleModelsFDto>>();
            return _IMListExcelExporter.ExportMadesToFile(ListDtos);
        }
    }
}
