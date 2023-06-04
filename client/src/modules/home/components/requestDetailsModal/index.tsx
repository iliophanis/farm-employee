import React, { Dispatch, useMemo, useState } from 'react';
import { MdOutlineAgriculture } from 'react-icons/md';

import Modal from '@/shared/components/modal/Modal';
import { UserRequest } from '@/modules/home/components/requestMap/request.models';
import DescriptionList from '@/shared/components/list/DescriptionList';
import logger from '@/shared/lib/logger';
import { useAuth } from '@/shared/contexts/AuthProvider';
import FarmerDetailsModal from '../farmerDetailsModal';
import clsxm from '@/shared/lib/clsxm';

type IRequestDetailsModalProps = {
  openModal: boolean;
  setOpenModal: Dispatch<boolean>;
  data: UserRequest;
};

const RequestDetailsModal = ({
  openModal,
  setOpenModal,
  data,
}: IRequestDetailsModalProps) => {
  const { auth } = useAuth();
  const [openFarmerModal, setOpenFarmerModal] = useState(false);
  const button = useMemo(() => {
    let buttonName = undefined;
    let onClick = undefined;
    let isDisabled = false;
    if (auth?.user.isEmployee) {
      buttonName = 'Αίτηση';
      onClick = () => logger('submitApplication');
      isDisabled = !data.actions.includes('Submit');
    }
    return { buttonName, onClick, isDisabled };
  }, [auth]);
  const items = [
    {
      caption: 'Τύπος Εργασίας',
      value: data.jobType,
    },
    {
      caption: 'Καλλιέργεια',
      value: data.cultivationName,
    },
    {
      caption: 'Τοποθεσία',
      value: data.location.displayName,
    },
    {
      caption: 'Αγρότης',
      value: (
        <div
          className={clsxm(
            'animated-underline custom-link cursor-pointer ',
            'text-white hover:text-primary-200 active:text-primary-700',
            'border-b border-dotted border-primary-300 hover:border-black/0'
          )}
          onClick={() => setOpenFarmerModal(true)}
        >
          {data.farmer.name} ({data.farmer.email})
        </div>
      ),
    },
    {
      caption: 'Ημ. Έναρξης Εργασίας',
      value: data.startJobDate,
    },
    {
      caption: 'Εκτιμώμενη Διάρκεια Εργασίας',
      value:
        `${data.estimatedDuration} ${
          data.estimatedDuration == 1 ? 'ημέρα' : 'ημέρες'
        }` || '-',
    },
    {
      caption: 'Ποσό Πληρωμής',
      value: `${data.price}€` || '-',
    },
  ];
  return (
    <>
      <Modal
        openModal={openModal}
        setOpenModal={setOpenModal}
        buttonName={button.buttonName}
        onClick={button.onClick}
        isDisabled={button.isDisabled}
        icon={<MdOutlineAgriculture />}
      >
        <DescriptionList items={items} />
      </Modal>
      <FarmerDetailsModal
        openModal={openFarmerModal}
        setOpenModal={setOpenFarmerModal}
        data={data.farmer}
      />
    </>
  );
};

export default RequestDetailsModal;
