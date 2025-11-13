using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Entities.UserEntities;
using Bookstore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(ClaimsPrincipal claimsPrincipal, ReviewCreateRequestDto reviewDto)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null) throw new ArgumentNullException(nameof(user));

            var review = _mapper.Map<Review>(reviewDto);
            review.UserId = user.Id;

            var book = await _unitOfWork.Books.GetOneAsync(review.BookId);
            if (book == null) throw new NotFoundException("Book not found");

            var allReviews = await _unitOfWork.Reviews.GetAllAsync();
            var ratingsList = allReviews.Select(r => r.Rating).ToList();
            ratingsList.Add(review.Rating);

            double newAvgRating = ratingsList.Average();

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Reviews.AddAsync(review);
                await _unitOfWork.Books.UpdateAvgRating(review.BookId, newAvgRating);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
