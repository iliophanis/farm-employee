import { Dispatch } from 'react';
import { useQueryClient } from '@tanstack/react-query';

import customAxios from '@/shared/api/agent';
import { errorNotify } from '@/shared/components/toast';
import { useCommand, useQuery } from '@/shared/hooks/useQuery';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { UserRequest } from './request.models';

const useRequestMap = () => {
  const { auth } = useAuth();
  const queryClient = useQueryClient();
  const userRequestsQuery = useQuery(['user.requests'], async () => {
    const response = await customAxios.get(`/requests/user`);
    if (response.error) {
      errorNotify('Σφάλμα', 'Κατι πήγε στραβά κατα την παραλαβή των αιτήσεων');
      return [];
    }
    return response;
  });
  const command = useCommand([], async (id) => {
    const response = await customAxios.get(`/requests/user/${id}`, auth?.token);
    if (response.error)
      errorNotify(
        'Σφάλμα',
        'Κατι πήγε στραβά κατα το άνοιγμα της αίτησης με id ' + id
      );
    return response;
  });
  const handleGetRequestById = (
    id: number,
    setModalData: Dispatch<UserRequest | null>,
    setOpenDetailsModal: Dispatch<boolean>
  ) => {
    command.execute(id, {
      onSuccess: async (response: UserRequest) => {
        queryClient.setQueryData(
          ['user.requests'],
          (data: UserRequest[] | undefined) => {
            const udpatedData = [...data!];
            const requestIdx = udpatedData.findIndex((x) => x.id === id);
            udpatedData[requestIdx] = response;
            setModalData(response);
            setOpenDetailsModal(true);
            return udpatedData;
          }
        );
      },
    });
  };

  return {
    userRequests: userRequestsQuery.data as UserRequest[],
    loading: userRequestsQuery.isLoading,
    handleGetRequestById,
  };
};

export default useRequestMap;
