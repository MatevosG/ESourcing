using ESourcing.Core.Repositories;
using ESourcing.UI.Clients;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ProductClient _productClient;
        private readonly AuctionClient _auctionClient;

        public AuctionController(/*ProductClient productClient, IUserRepository userRepository, AuctionClient auctionClient*/)
        {
            //_productClient = productClient;
            //_userRepository = userRepository;
            //_auctionClient = auctionClient;
        }

        public async Task<IActionResult> Index()
        {
            //var auctionList = await _auctionClient.GetAuctions();
            //if (auctionList.IsSuccess)
            //    return View(auctionList.Data);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    //TODO:Product GetAll
        //    var productList = await _productClient.GetProducts();
        //    if (productList.IsSuccess)
        //        ViewBag.ProductList = productList.Data;

        //    var userList = await _userRepository.GetAllAsync();
        //    ViewBag.UserList = userList;

        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Create(AuctionViewModel model)
        {
            //model.Status = 0;
            //model.CreatedAt = DateTime.Now;
            //model.IncludedSellers.Add(model.SellerId);
            //var createAuction = await _auctionClient.CreateAuction(model);
            //if (createAuction.IsSuccess)
            //    return RedirectToAction("Index");
            return View(model);
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View();
        }

        //[HttpPost]
        //public async Task<Result<string>> SendBid(BidViewModel model)
        //{
        //}
        //[HttpPost]
        //public async Task<Result<string>> CompleteBid(string id)
        //{
        //}
    }
}
