import { useCommand, useQuery } from '@/shared/hooks/useQuery';
import customAxios from '@/shared/api/agent';
import { IAddRole } from '@/modules/auth/models/IUser';
import { handleValidateForm } from '@/shared/utils/validateForm';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { errorNotify } from '@/shared/components/toast';

const useAddUserRole = (profile: any, setShowModal: any) => {
  const authState = useAuth();
  const rolesQuery = useQuery(['user.roles'], async () => {
    const response = await customAxios.get(`/users/roles`);
    if (response.error)
      return errorNotify(
        'Σφάλμα',
        'Κάτι πήγε στραβά κατα την παραλαβή των ρόλων'
      );
    return response;
  });
  const command = useCommand([], async (data) => {
    const response = await customAxios.post(`users/role`, data);
    if (response.error) throw new Error(response.error);
    return response;
  });
  function handleAddRole({ roleId, contactInfo }: IAddRole) {
    const isValidContactInfo = handleValidateForm(contactInfo);
    const dto = {
      roleId: Number(roleId),
      userName: profile.email,
      contactInfo: isValidContactInfo ? contactInfo : null,
    };
    command.execute(dto, {
      onSuccess: async (response: any) => {
        console.log(response['data']);
        const tokenRes = await customAxios.get(
          `/users/token/${profile.email}?authProvider=Google`
        );
        console.log({ tokenRes });
        if (tokenRes.error)
          return errorNotify(
            'Σφάλμα',
            'Κατι πήγε στραβά κατα την δημιουργία του token'
          );
        authState.login({ ...tokenRes, picture: profile.picture });
        setShowModal(false);
        return tokenRes;
      },
    });
  }

  return { roles: rolesQuery.data, handleAddRole, loading: command.isLoading };
};

export default useAddUserRole;
