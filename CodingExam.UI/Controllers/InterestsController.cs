using AutoMapper;
using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExam.UI.Controllers
{
    [Authorize]
    public class InterestsController : Controller
    {
        private readonly IInterestService _interestService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public InterestsController(IInterestService interestService, IUserService userService, IMapper mapper)
        {
            _interestService = interestService;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult GenerateInterestDetails(InterestViewModel interestVm)
        {
            var interest = _mapper.Map<Interest>(interestVm);

            if (ModelState.IsValid)
                _interestService.GenerateInterestDetails(interest);
            else
                interest.InterestDetails = new List<InterestDetails>();

            return PartialView("InterestDetails/_InterestDetails", _mapper.Map<List<InterestDetailsViewModel>>(interest.InterestDetails));
        }

        public async Task<IActionResult> Index(int id)
        {
            var interest = await _interestService.GetById(id);

            if (interest == null) return NotFound();

            return View(_mapper.Map<InterestViewModel>(interest));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var interest = await _interestService.GetById(id);

            return View(_mapper.Map<InterestViewModel>(interest));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresentValue,LowerBoundInterestRate,UpperBoundInterestRate,IncrementalRate,MaturityYears,UserId,Id")] InterestViewModel interestVm)
        {
            if (!ModelState.IsValid)
                return View(interestVm);

            var interest = _mapper.Map<Interest>(interestVm);

            _interestService.GenerateInterestDetails(interest);

            await _interestService.Update(interest);

            return RedirectToAction("Index", new { id });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}
