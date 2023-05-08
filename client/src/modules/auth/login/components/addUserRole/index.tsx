import React from 'react';
import { HiUserPlus } from 'react-icons/hi2';
import { useForm } from 'react-hook-form';

import logger from '@/shared/lib/logger';
import useAddUserRole from './useAddUserRole';
import { IAddRole } from '@/modules/auth/models/IUser';
import {
  UnControlledInput as TextInput,
  UncontrolledSelect as SelectInput,
} from '@/shared/components/formControls';
import Modal from '@/shared/components/modal/Modal';

type IProps = {
  profile: any;
  shoModal: boolean;
  setShowModal: React.Dispatch<React.SetStateAction<boolean>>;
};

const AddUserRole = ({ profile, shoModal, setShowModal }: IProps) => {
  const { roles, handleAddRole, loading } = useAddUserRole(
    profile,
    setShowModal
  );
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<IAddRole>();
  return (
    <Modal
      openModal={shoModal}
      setOpenModal={setShowModal}
      buttonName='Ολoκλήρωση Εγγραφής'
      icon={<HiUserPlus />}
      onClick={handleSubmit(handleAddRole)}
    >
      <div className='grid grid-cols-1 gap-4'>
        <SelectInput
          label='Ιδιότητα'
          options={roles}
          name='roleId'
          optionKey='id'
          optionValue='description'
          register={register('roleId', {
            required: 'Η ιδιότητα είναι υποχρεωτική',
          })}
          errorName={errors.roleId}
          placeholder='Επιλέξτε ιδιότητα...'
        />
        <TextInput
          label='Διεύθυνση'
          type='text'
          register={register('contactInfo.address')}
          errorName={errors.contactInfo?.address}
        />
        <TextInput
          label='Πόλη'
          type='text'
          register={register('contactInfo.city')}
          errorName={errors.contactInfo?.city}
        />
        <TextInput
          label='ΤΚ'
          type='number'
          register={register('contactInfo.tk')}
          errorName={errors.contactInfo?.tk}
        />
        <TextInput
          label='Σταθερό Τηλ.'
          type='number'
          register={register('contactInfo.phoneNo')}
          errorName={errors.contactInfo?.phoneNo}
        />
        <TextInput
          label='Κινητό Τηλ.'
          type='number'
          register={register('contactInfo.mobilePhoneNo')}
          errorName={errors.contactInfo?.mobilePhoneNo}
        />
      </div>
    </Modal>
  );
};

export default AddUserRole;
