import React from 'react';
import { HiUserPlus } from 'react-icons/hi2';

import useLogin from './useLogin';
import Modal from '@/shared/components/modal/Modal';
import GoogleButton from './components/externalAuth/GoogleButton';
import logger from '@/shared/lib/logger';
import NextImage from '@/shared/components/NextImage';

type IProps = {
  onGoogleLogin: () => void;
  profile: any;
  showAddRoleModal: boolean;
  setShowAddRoleModal: React.Dispatch<React.SetStateAction<boolean>>;
};

const Login = () => {
  const {
    onGoogleLogin,
    profile,
    showAddRoleModal,
    setShowAddRoleModal,
  }: IProps = useLogin();
  if (showAddRoleModal) {
    return (
      <Modal
        openModal={showAddRoleModal}
        setOpenModal={setShowAddRoleModal}
        buttonName='Ολωκλήρωση Εγγραφής'
        icon={<HiUserPlus />}
        onClick={() => logger('submit role')}
      >
        <div>Επιλέξτε ένα απο τους διαθέσιμους ρόλους</div>
      </Modal>
    );
  }
  return (
    <div>
      {profile ? (
        <div className='flex flex-col justify-center'>
          <div className='mb-2 flex flex-row items-center'>
            <NextImage
              imgClassName='ml-0 h-12 w-12 rounded-full'
              src={profile.picture}
              width='80'
              height='80'
              alt={`${profile.name} image`}
            />
            {profile.name}
          </div>
          <hr />
          <br />
          <button>Log out</button>
        </div>
      ) : (
        <GoogleButton handleGoogleLogin={onGoogleLogin} />
      )}
    </div>
  );
};

export default Login;
