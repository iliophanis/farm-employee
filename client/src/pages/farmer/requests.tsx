import * as React from 'react';
import Seo from '@/shared/components/Seo';
import dynamic from 'next/dynamic';
import Skeleton from '@/shared/components/Skeleton';
import { useRouter } from 'next/router';

const getDynamicComponent = (c: string | string[] | undefined) =>
  dynamic(() => import('@/modules/farmer/requests'), {
    loading: () => <Skeleton />,
    ssr: false,
  });

export default function FarmerRequests() {
  const router = useRouter();
  const { component } = router.query;

  const DynamicComponent = getDynamicComponent(component);

  return (
    <>
      <Seo templateTitle='Employee-Requests' />
      <main>
        <section className='bg-white'>
          <DynamicComponent />
        </section>
      </main>
    </>
  );
}
