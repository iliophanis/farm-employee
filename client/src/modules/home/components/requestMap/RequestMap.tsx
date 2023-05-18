import React, { useEffect, useState } from 'react';
import dynamic from 'next/dynamic';
import { icon as LeafletIcon } from 'leaflet';
import { Marker, Popup } from 'react-leaflet';

import Skeleton from '@/shared/components/Skeleton';
import useRequestMap from '@/modules/home/components/requestMap/useRequestMap';
import { useAuth } from '@/shared/contexts/AuthProvider';
import LoginModal from '@/modules/auth/login/components/loginModal';

const Map = dynamic(() => import('@/shared/components/map/Map'), {
  loading: () => <Skeleton />,
  ssr: false,
});

const RequestMap = () => {
  const authState = useAuth();
  const { userRequests, loading } = useRequestMap();
  const [showLoginModal, setShowLoginModal] = useState(false);
  const customMarker = LeafletIcon({
    iconUrl: '/images/agirculture.png',
    iconSize: [50, 45],
    iconAnchor: [25, 0],
  });

  const handleOpenDetails = () => {
    if (!authState.isAuthenticated()) setShowLoginModal(true);
    else setShowLoginModal(false);
  };

  if (loading) return <Skeleton />;

  return (
    <>
      <Map>
        {userRequests?.map((d, idx) => (
          <Marker
            key={idx}
            position={[d.latitude, d.longtitude]}
            icon={customMarker}
            eventHandlers={{ click: handleOpenDetails }}
          >
            {/* @TODO */}
            {authState.isAuthenticated() && (
              <Popup>Καλλιέργεια : ΑΓΓΟΥΡΑΚΙ Αγρότης :</Popup>
            )}
          </Marker>
        ))}
      </Map>
      <LoginModal
        showLoginModal={showLoginModal}
        setShowLoginModal={setShowLoginModal}
      />
    </>
  );
};

export default RequestMap;
