import React from 'react';

import useLogin from './useLogin';

import GoogleButton from './components/externalAuth/GoogleButton';

import NextImage from '@/shared/components/NextImage';
import AddUserRole from '@/modules/auth/login/components/addUserRole';
import { useAuth } from '@/shared/contexts/AuthProvider';
import IconButton from '@/shared/components/buttons/IconButton';
import { HiArrowLeftOnRectangle } from 'react-icons/hi2';
import Button from '../../../shared/components/buttons/Button';

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
  const authState = useAuth();
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
    <div>
      {authState.auth !== null ? (
        <div className='flex flex-col justify-center'>
          <div className='mb-2 flex flex-row items-center'>
            <NextImage
              imgClassName='ml-0 h-12 w-12 rounded-full'
              src={authState.auth?.user.picture}
              width='80'
              height='80'
              alt={`${authState.auth?.user.displayName} image`}
            />
            {authState.auth?.user.displayName}
          </div>
          <hr />
          <br />
          <Button
            variant='outline'
            leftIcon={HiArrowLeftOnRectangle}
            className='inline-flex w-full justify-center border border-red-500 bg-red-500 text-gray-100 hover:bg-red-300'
            onClick={() => authState.logout()}
          >
            Αποσύνδεση
          </Button>
        </div>
      ) : (
        <GoogleButton handleGoogleLogin={onGoogleLogin} />
      )}
    </div>
  );
};

export default Login;
