import * as React from 'react';
import { useGoogleLogin } from '@react-oauth/google';

import { useAuth } from '@/shared/contexts/AuthProvider';
import customAxios from '@/shared/api/agent';
import logger from '@/shared/lib/logger';
import { errorNotify } from '@/shared/components/toast';

type IError = { error: any };
const useLoginModal = (setShowLoginModal: any) => {
  const authState = useAuth();
  const [profile, setProfile]: any = React.useState(null);
  const [showAddRoleModal, setShowAddRoleModal] = React.useState(false);
  const onGoogleLogin = useGoogleLogin({
    onSuccess: async (codeResponse) => {
      const googleResponse = await customAxios.get(
        `/oauth2/v1/userinfo?alt=json&access_token=${codeResponse.access_token}`,
        undefined,
        {
          baseURL: 'https://www.googleapis.com',
          headers: {
            Authorization: `Bearer ${codeResponse.access_token}`,
            Accept: 'application/json',
          },
        }
      );
      if (googleResponse.error) return logger(googleResponse.error);
      const data = {
        userName: googleResponse.email,
        firstName: googleResponse.given_name || '',
        lastName: googleResponse.family_name || '',
      };
      setProfile(googleResponse);
      const authResponse = await customAxios.post('/users/google-auth', data);
      if (authResponse.error)
        return errorNotify(
          'Σφάλμα',
          'Κατι πήγε στραβά κατα την δημιουργία του χρήστη'
        );
      if (authResponse.isNewUser) {
        setShowAddRoleModal(true);
        setShowLoginModal(false);
      } else {
        const tokenRes = await customAxios.get(
          `/users/token/${googleResponse.email}?authProvider=Google`
        );

        if (tokenRes.error)
          return errorNotify(
            'Σφάλμα',
            'Κατι πήγε στραβά κατα την δημιουργία του token'
          );
        authState.login({ ...tokenRes, picture: googleResponse.picture });
      }
      setShowLoginModal(false);
    },
    onError: (error) => console.log('Login Failed:', error),
  });
  return { onGoogleLogin, profile, showAddRoleModal, setShowAddRoleModal };
};

export default useLoginModal;
