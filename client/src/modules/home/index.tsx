import React from 'react';
// import RequestMap from '@/modules/home/components/requestMap/RequestMap';
import dynamic from 'next/dynamic';
import Skeleton from '../../shared/components/Skeleton';

const RequestMap = dynamic(() => import('./components/requestMap/RequestMap'), {
  loading: () => <Skeleton />,
  ssr: false,
});
const Home = () => {
  return <RequestMap />;
};

export default Home;
