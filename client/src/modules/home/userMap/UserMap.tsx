import useUserMap from '@/modules/home/userMap/useUserMap';
import Skeleton from '@/shared/components/Skeleton';
import dynamic from 'next/dynamic';
import React from 'react';

const UserMap = () => {
  const Map = dynamic(() => import('@/shared/components/map/Map'), {
    loading: () => <Skeleton />,
    ssr: false, // This line is important. It's what prevents server-side render
  });
  const { userRequests, loading } = useUserMap();
  if (loading) return <Skeleton />;

  return <Map data={userRequests} />;
};

export default UserMap;
