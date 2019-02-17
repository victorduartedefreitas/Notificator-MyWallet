using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Business.Services;
using AutoMapper;
using MyWallet.Presentation.WebApi.Dto;
using MyWallet.Presentation.WebApi.Models;
using MyWallet.Business.Entities;
using System.Text;

namespace MyWallet.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        }

        [HttpGet("GetWalletById")]
        public async Task<IActionResult> GetWalletById([FromQuery]Guid walletId)
        {
            var wallet = await walletService.GetWallet(walletId);
            return Ok(Mapper.Map<WalletDto>(wallet));
        }

        [HttpGet("GetWalletByName")]
        public async Task<IActionResult> GetWalletByName([FromQuery]string walletName)
        {
            var wallet = await walletService.GetWallet(walletName);
            return Ok(Mapper.Map<WalletDto>(wallet));
        }

        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet([FromQuery]string walletName)
        {
            var wallet = await walletService.CreateWallet(walletName);

            wallet.Validate();
            if (!wallet.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var n in wallet.Notifications)
                {
                    sb.Append(n.Message);
                    sb.Append("\n");
                }

                return BadRequest(sb.ToString());
            }

            await walletService.Save(wallet);

            return Ok(Mapper.Map<WalletDto>(wallet));
        }

        [HttpPost("AddTransactionToWallet")]
        public async Task<IActionResult> AddTransactionToWallet([FromBody]AddTransactionPost model)
        {
            await walletService.AddTransaction(model.WalletId, Mapper.Map<Transaction>(model.Transaction));
            return Ok();
        }

        [HttpPost("SaveWallet")]
        public async Task<IActionResult> SaveWallet([FromBody]WalletDto wallet)
        {
            await walletService.Save(Mapper.Map<Wallet>(wallet));
            return Ok();
        }
    }
}