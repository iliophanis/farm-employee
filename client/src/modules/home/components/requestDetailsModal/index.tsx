import React, { Dispatch } from 'react';
import { MdOutlineAgriculture } from 'react-icons/md';

import Modal from '@/shared/components/modal/Modal';
import { UserRequest } from '@/modules/home/components/requestMap/request.models';
import DescriptionList from '@/shared/components/list/DescriptionList';
import logger from '@/shared/lib/logger';

type IRequestDetailsModalProps = {
  openModal: boolean;
  setOpenModal: Dispatch<boolean>;
  data: UserRequest | null;
};

const RequestDetailsModal = ({
  openModal,
  setOpenModal,
  data,
}: IRequestDetailsModalProps) => {
  if (data == null) return <></>;
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
      value: `${data.farmer.name} (${data.farmer.email})`,
    },
    {
      caption: 'Ημ. Έναρξης Εργασίας',
      value: data.startJobDate,
    },
    {
      caption: 'Εκτιμώμενη Διάρκεια Εργασίας',
      value: `${data.estimatedDuration} ημέρες` || '-',
    },

    {
      caption: 'Ποσό Πληρωμής',
      value: data.price || '-',
    },
  ];
  return (
    <Modal
      openModal={openModal}
      setOpenModal={setOpenModal}
      buttonName='Αίτηση'
      onClick={() => logger('submitApplication')}
      icon={<MdOutlineAgriculture />}
    >
      <DescriptionList items={items} />
    </Modal>
  );
};

export default RequestDetailsModal;
