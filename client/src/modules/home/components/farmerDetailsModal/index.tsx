import React, { Dispatch, useMemo } from 'react';
import { GiFarmer } from 'react-icons/gi';

import Modal from '@/shared/components/modal/Modal';
import { UserRequestFarmer } from '@/modules/home/components/requestMap/request.models';
import DescriptionList from '@/shared/components/list/DescriptionList';
import { useAuth } from '@/shared/contexts/AuthProvider';
import StarRating from '@/shared/components/rating/StarRating';

type IFarmerDetailsModalProps = {
  openModal: boolean;
  setOpenModal: Dispatch<boolean>;
  data: UserRequestFarmer;
};

const FarmerDetailsModal = ({
  openModal,
  setOpenModal,
  data,
}: IFarmerDetailsModalProps) => {
  const { auth } = useAuth();
  const items = useMemo(
    () => [
      {
        caption: 'Όνομα',
        value: data.name || '-',
      },
      {
        caption: 'Email',
        value: data.email || '-',
      },
      {
        caption: 'Διεύθυνση Κατοικίας',
        value: data.contactInfo || '-',
      },
      {
        caption: 'Αξιολόγηση',
        value: <StarRating ratingValue={data.avgRate || 0} />,
      },
      {
        caption: 'Περιβάλλον Εργασίας',
        value: <StarRating ratingValue={data.avgWorkPlaceRate || 0} />,
      },
      {
        caption: 'Συνέπεια Πληρωμής',
        value: <StarRating ratingValue={data.avgPaymentConsequenceRate || 0} />,
      },
    ],
    [data]
  );
  return (
    <Modal
      openModal={openModal}
      setOpenModal={setOpenModal}
      icon={<GiFarmer />}
    >
      <DescriptionList items={items} />
    </Modal>
  );
};

export default FarmerDetailsModal;
