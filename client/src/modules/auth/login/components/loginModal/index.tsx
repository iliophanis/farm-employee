import React, { useEffect, useState } from 'react';

import useLogin from '@/modules/auth/login/useLogin';

import GoogleButton from '@/modules/auth/login/components/externalAuth/GoogleButton';

import AddUserRole from '@/modules/auth/login/components/addUserRole';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { HiArrowRightOnRectangle } from 'react-icons/hi2';
import Modal from '@/shared/components/modal/Modal';
import useLoginModal from '@/modules/auth/login/components/loginModal/useLoginModal';

type IProps = {
  onGoogleLogin: () => void;
  profile: any;
  showAddRoleModal: boolean;
  setShowAddRoleModal: React.Dispatch<React.SetStateAction<boolean>>;
};

const LoginModal = ({ showLoginModal, setShowLoginModal }: any) => {
  const {
    onGoogleLogin,
    profile,
    showAddRoleModal,
    setShowAddRoleModal,
  }: IProps = useLoginModal(setShowLoginModal);
  if (showAddRoleModal) {
    return (
      <AddUserRole
        profile={profile}
        shoModal={showAddRoleModal}
        setShowModal={setShowAddRoleModal}
      />
    );
  }
  return (
    <Modal
      openModal={showLoginModal}
      setOpenModal={setShowLoginModal}
      buttonName='Ολoκλήρωση Εγγραφής'
      icon={<HiArrowRightOnRectangle />}
    >
      <GoogleButton handleGoogleLogin={onGoogleLogin} />
    </Modal>
  );
};

export default LoginModal;
