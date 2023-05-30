import React from 'react';
import { FaRegStar, FaStar, FaStarHalfAlt } from 'react-icons/fa';

function StarRating({ ratingValue }: any) {
  const filledStars = Math.floor(ratingValue);
  const remainingValue = ratingValue - filledStars;

  const starIcons = Array.from({ length: 5 }, (_, index) => {
    const starNumber = index + 1;
    if (starNumber <= filledStars) {
      return (
        <FaStar
          className='h-5 w-5 text-yellow-400'
          key={`filled-star-${starNumber}`}
        />
      );
    } else if (
      starNumber === Math.ceil(ratingValue) &&
      remainingValue > 0 &&
      remainingValue < 1
    ) {
      return (
        <FaStarHalfAlt
          key={`half-star-${starNumber}`}
          className='h-5 w-5 text-yellow-400'
        />
      );
    } else {
      return (
        <FaRegStar
          className='h-5 w-5 text-gray-300 dark:text-gray-500'
          key={`empty-star-${starNumber}`}
        />
      );
    }
  });

  return (
    <div className='flex items-center'>
      {starIcons}
      <p className='ml-2 text-sm font-medium text-gray-300 dark:text-gray-400'>
        {ratingValue} στα 5
      </p>
    </div>
  );
}

export default StarRating;
