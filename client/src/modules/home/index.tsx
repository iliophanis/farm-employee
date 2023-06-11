import React from 'react';
// import RequestMap from '@/modules/home/components/requestMap/RequestMap';
import dynamic from 'next/dynamic';
import Skeleton from '../../shared/components/Skeleton';
import { useRouter } from 'next/router';

const getDynamicComponent = (c: string | string[] | undefined) =>
  dynamic(() => import('./components/requestMap/RequestMap'), {
    loading: () => <Skeleton />,
    ssr: false,
  });

const Home = () => {
  const router = useRouter();
  const { component } = router.query;

  const RequestMap = getDynamicComponent(component);
  return <RequestMap />;
};

export default Home;
