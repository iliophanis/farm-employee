import SearchInput from '@/modules/farmer/requests/components/SearchInput';
import FarmerRequestsList from '@/modules/farmer/requests/components/farmerRequestsList/FarmerRequestsList';
import useFarmerRequests from '@/modules/farmer/requests/useFarmerRequests';
import RequestDetailsModal from '@/modules/home/components/requestDetailsModal';

import { UserRequest } from '@/modules/home/models/IUserRequest';
import Breadcrumb from '@/shared/components/breadcrumb/Breadcrumb';
import Button from '@/shared/components/buttons/Button';
import DeleteContent from '@/shared/components/modal/DeleteContent';
import Modal from '@/shared/components/modal/Modal';
import { useAuth } from '@/shared/contexts/AuthProvider';
import React, { useState } from 'react';
import { HiExclamationTriangle, HiOutlineHome } from 'react-icons/hi2';

const FarmerRequests = () => {
  const auth = useAuth();
  const [openDelModal, setOpenDelModal] = useState(false);
  const [selectedEmpReqId, setSelectedEmpReqId] = useState<number | null>(null);
  const [openDetailsModal, setOpenDetailsModal] = useState(false);
  const [modalData, setModalData] = useState<UserRequest | null>(null);
  const {
    queryParams,
    data,
    setData,
    userRequests,
    loading,
    handleChangePage,
    handleChangeFilter,
    handleChangeType,
    onDeleteRequest,
    deleteLoading,
    handleGetRequestById,
  } = useFarmerRequests();

  const handleDelRequest = (empReqId: number) => {
    setSelectedEmpReqId(empReqId);
    setOpenDelModal(true);
  };

  const handleOpenDetails = (id: number) => {
    handleGetRequestById(id, setModalData, setOpenDetailsModal);
  };

  if (
    !auth.isAuthenticated() ||
    (auth.isAuthenticated() && auth.auth?.user.isEmployee)
  )
    return <></>;
  return (
    <>
      <div className='mt-5 inline-block flex min-w-full flex-row flex-wrap items-center justify-between px-6 align-middle'>
        <div className='select-none text-gray-900'>
          <Breadcrumb
            data={[
              { Icon: HiOutlineHome, label: 'Κεντρική', href: '/' },
              { label: 'Αιτήσεις' },
            ]}
          />
        </div>
      </div>
      <div className='mt-3 flex min-w-full flex-row flex-wrap items-center justify-between py-2 px-6  align-middle'>
        <div className='select-none'>
          <div className='inline-flex rounded-md shadow-sm' role='group'>
            <Button
              variant={queryParams.type === 'personal' ? 'primary' : 'light'}
              size='sm'
              // disabled={queryParams.type === 'personal'}
              onClick={() => handleChangeType('personal')}
              className='rounded-l-lg border  border-gray-200'
            >
              Δικές μου
            </Button>
            <Button
              variant={queryParams.type === 'all' ? 'primary' : 'light'}
              size='sm'
              // disabled={queryParams.type === 'all'}
              onClick={() => handleChangeType('all')}
              className='-ml-0.5 border-t border-b border-gray-200'
            >
              {' '}
              Όλες
            </Button>
          </div>
        </div>
        <div className='mt-2 w-full sm:mt-0 md:w-5/12'>
          <SearchInput
            type={queryParams.type}
            filter={queryParams.filter}
            handleChangeFilter={handleChangeFilter}
          />
        </div>
      </div>
      <div className='mt-2'>
        <FarmerRequestsList
          data={data}
          type={queryParams.type}
          setData={setData}
          loading={loading}
          userRequests={userRequests}
          isPersonalRequests={queryParams.type === 'personal'}
          handleChangePage={handleChangePage}
          handleDelRequest={handleDelRequest}
          handleOpenDetails={handleOpenDetails}
        />
      </div>
      <Modal
        openModal={openDelModal}
        setOpenModal={setOpenDelModal}
        icon={<HiExclamationTriangle />}
        buttons={[
          {
            variant: 'red',
            label: 'Διαγραφή',
            onClick: () => onDeleteRequest(selectedEmpReqId!, setOpenDelModal),
            isDisabled: deleteLoading,
            isLoading: deleteLoading, //TODO setDynamically
          },
        ]}
      >
        <DeleteContent />
      </Modal>

      {modalData && openDetailsModal && (
        <RequestDetailsModal
          openModal={openDetailsModal}
          setOpenModal={setOpenDetailsModal}
          data={modalData}
        />
      )}
    </>
  );
};

export default FarmerRequests;
