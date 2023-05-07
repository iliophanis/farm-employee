import React, { Suspense } from 'react';
import { ImSpinner2 } from 'react-icons/im';

const Loader = () => {
  return (
    <div className='flex items-center justify-center text-center text-lg'>
      <ImSpinner2 className='h-10 w-10 animate-spin text-indigo-800' />
    </div>
  );
};

const withSuspense = (Container: any) => {
  return <Suspense fallback={<Loader />}>{Container}</Suspense>;
};

export default withSuspense;
