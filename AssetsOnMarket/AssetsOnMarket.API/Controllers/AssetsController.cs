using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsOnMarket.Application.Interfaces;
using AssetsOnMarket.Application.ViewModels;
using AssetsOnMarket.Domain.Models;
using AssetsOnMarket.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetsOnMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly ILogger _logger;
        private readonly BatchConfiguration _batchConfiguration;

        public AssetsController(IAssetService assetService,
                                ILogger logger,
                                BatchConfiguration batchConfiguration)
        {
            _assetService = assetService;
            _logger = logger;
            _batchConfiguration = batchConfiguration;
        }

        /// <summary>
        /// 1) Endpoint for reading Assets from internal CSV file and update to DB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReadAssets()
        {
            string message;
            try
            {
                await _assetService.ReadAssetsFromFile(_batchConfiguration.MaxBatchSize);
                message = "Assets read from file successfully";
                _logger.Information(message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                _logger.Error(message);
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return Ok(message);
        }

        /// <summary>
        /// 2) Endpoint for obtaining the Assets IDs for property set to specific value
        /// </summary>
        /// <returns></returns>
        [HttpPost("ListAssets")]
        public async Task<IActionResult> GetAssetsIdsAsync([FromBody]PropertyValueViewModel propertyValueViewModel)
        {
            string message;
            if (string.IsNullOrWhiteSpace(propertyValueViewModel.Property) || string.IsNullOrWhiteSpace(propertyValueViewModel.Value))
                return new BadRequestResult();
            try
            {
                var listIds = await _assetService.GetAssetsIdsByPropertyValue(propertyValueViewModel);

                return Ok(listIds);
            }
            catch (Exception ex)
            {
                message = ex.Message;

                _logger.Error(message);

                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
        }

        /// <summary>
        /// 3) Endpoint for set Property for Asset individually
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> SetPropertyToAsset([FromBody] AssetPropertyViewModel assetPropertyViewModel)
        {
            string message;
            try
            {
                await _assetService.AddOrUpdateAsync(assetPropertyViewModel);
                message = "Property set to Asset successfully";

                _logger.Information(message);

                return Ok(message);
            }
            catch (Exception ex)
            {
                message = ex.Message;

                _logger.Error(message);

                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
        }

       
    }
}
