import React, { useState } from 'react';
import dynamic from 'next/dynamic';
import { icon as LeafletIcon } from 'leaflet';
import { Marker } from 'react-leaflet';

import Skeleton from '@/shared/components/Skeleton';
import useRequestMap from '@/modules/home/components/requestMap/useRequestMap';
import { useAuth } from '@/shared/contexts/AuthProvider';
import LoginModal from '@/modules/auth/login/components/loginModal';
import RequestDetailsModal from '../requestDetailsModal';
import { UserRequest } from '../../models/IUserRequest';
import SearchSelect from '@/shared/components/formControls/SearchSelect';
import useDebounce from '@/shared/hooks/useDebounce';
import Map from '@/shared/components/map/Map';

const RequestMap = () => {
  const auth = useAuth();
  const { debounce } = useDebounce();
  const {
    userRequests,
    loading,
    handleGetRequestById,
    handleGetLocationOptions,
    setSearchLocation,
  } = useRequestMap();
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
      //TODO Update Data only if authUser Change
      // const jobRequest = userRequests.find((x) => x.id == id);
      // if (jobRequest?.farmer !== undefined) {
      //   setModalData(jobRequest);
      //   setOpenDetailsModal(true);
      //   return;
      // }

      handleGetRequestById(id, setModalData, setOpenDetailsModal);
    }
  };

  if (loading) return <Skeleton />;

  return (
    <>
      <Map userRequests={userRequests}>
        <div className='mt-5 flex justify-center'>
          <SearchSelect
            loadOptions={debounce(handleGetLocationOptions, 500)}
            defaultOptions
            cacheOptions
            getOptionLabel={(option: any) => option['label']}
            getOptionValue={(option: any) => option['value']}
            onChange={(newValue: any) => setSearchLocation(newValue)}
          />
        </div>

        {userRequests?.map((d, idx) => (
          <Marker
            key={d.id}
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
