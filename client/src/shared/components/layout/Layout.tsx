import * as React from 'react';

import Footer from '@/shared/components/layout/Footer';
import Sidebar from '@/shared/components/layout/Sidebar';
import Header from '@/shared/components/layout/Sidebar';
import Skeleton from '@/shared/components/Skeleton';

export default function Layout({ children }: { children: React.ReactNode }) {
  // Put Header or Footer Here
  return (
    <>
      <Sidebar />
      <React.Suspense fallback={<Skeleton className='w-100 h-100' />}>
        {children}
      </React.Suspense>
      <div className='flex items-center justify-center text-center'>
        <Footer />
      </div>
    </>
  );
}
