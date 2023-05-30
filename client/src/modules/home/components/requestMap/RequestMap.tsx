import React, { useState } from 'react';
import dynamic from 'next/dynamic';
import { icon as LeafletIcon } from 'leaflet';
import { Marker } from 'react-leaflet';

import Skeleton from '@/shared/components/Skeleton';
import useRequestMap from '@/modules/home/components/requestMap/useRequestMap';
import { useAuth } from '@/shared/contexts/AuthProvider';
import LoginModal from '@/modules/auth/login/components/loginModal';
import RequestDetailsModal from '../requestDetailsModal';
import { UserRequest } from './request.models';

const Map = dynamic(() => import('@/shared/components/map/Map'), {
  loading: () => <Skeleton />,
  ssr: false,
});

const RequestMap = () => {
  const auth = useAuth();
  const { userRequests, loading, handleGetRequestById } = useRequestMap();
  const [showLoginModal, setShowLoginModal] = useState(false);
  const [openDetailsModal, setOpenDetailsModal] = useState(false);
  const [modalData, setModalData] = useState<UserRequest | null>(null);
  const customMarker = LeafletIcon({
    iconUrl: '/images/agirculture.png',
    iconSize: [50, 45],
    iconAnchor: [25, 0],
  });

  const handleOpenDetails = (id: number) => {
    if (!auth.isAuthenticated()) {
      setShowLoginModal(true);
    } else {
      setShowLoginModal(false);
      const jobRequest = userRequests.find((x) => x.id == id);
      if (jobRequest?.farmer !== undefined) {
        setModalData(jobRequest);
        setOpenDetailsModal(true);
        return;
      }

      handleGetRequestById(id, setModalData, setOpenDetailsModal);
    }
  };

  if (loading) return <Skeleton />;

  return (
    <>
      <Map>
        {userRequests?.map((d, idx) => (
          <Marker
            key={idx}
            position={[d.location.latitude, d.location.longitude]}
            icon={customMarker}
            eventHandlers={{ click: () => handleOpenDetails(d.id) }}
          ></Marker>
        ))}
      </Map>
      <LoginModal
        showLoginModal={showLoginModal}
        setShowLoginModal={setShowLoginModal}
      />
      {auth.isAuthenticated() && modalData && (
        <RequestDetailsModal
          openModal={openDetailsModal}
          setOpenModal={setOpenDetailsModal}
          data={modalData}
        />
      )}
    </>
  );
};

export default RequestMap;
