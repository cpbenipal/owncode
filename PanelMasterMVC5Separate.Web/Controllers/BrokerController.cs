using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp;
using Abp.Auditing;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using PanelMasterMVC5Separate.Tenants.Brokers;
using PanelMasterMVC5Separate.IO;
using PanelMasterMVC5Separate.Net.MimeTypes;

namespace PanelMasterMVC5Separate.Web.Controllers
{ 
    public class BrokerController : PanelMasterMVC5SeparateControllerBase
    { 
        private readonly IBrokerAppService _binaryObjectManager;
        private readonly IAppFolders _appFolders;       

        public BrokerController(
            IBrokerAppService binaryObjectManager,
            IAppFolders appFolders )
        { 
            _binaryObjectManager = binaryObjectManager;
            _appFolders = appFolders;           
        }
         

        public JsonResult UploadProfilePicture()
        {
            try
            {
                //Check input
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.InputStream);
                var acceptedFormats = new List<ImageFormat>
                {
                    ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif
                };

                if (!acceptedFormats.Contains(fileImage.RawFormat))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "BrokerImage_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                var tempFileName = System.Guid.NewGuid().ToString() + AbpSession.GetUserId() + fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                file.SaveAs(tempFilePath);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
        public async Task<FileResult> GetProfilePictureById(int Id)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(Id);
            if (file == null)
            {
                return GetDefaultProfilePicture();
            }

            return File(file.Bytes, MimeTypeNames.ImageJpeg);
        }
        private FileResult GetDefaultProfilePicture()
        {
            return File(Server.MapPath("~/Common/Images/default-profile-picture.png"), MimeTypeNames.ImagePng);
        }
    }
}