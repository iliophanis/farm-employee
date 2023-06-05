import { Dispatch, useEffect, useState } from 'react';
import { useQueryClient } from '@tanstack/react-query';

import customAxios from '@/shared/api/agent';
import { errorNotify, successNotify } from '@/shared/components/toast';
import { useCommand, useQuery } from '@/shared/hooks/useQuery';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { UserRequest } from '../../models/IUserRequest';
import { getQueryParams } from '../../../../shared/api/utilities';
import { ISubmitRequest } from '@/modules/home/models/ISubmitRequest';

const useRequestDetailsModal = () => {
  const { auth } = useAuth();
  const command = useCommand([], async (data) => {
    const response = await customAxios.post(
      `/employeeRequests`,
      data,
      auth!.token
    );

    if (response.error) {
      errorNotify('Σφάλμα', 'Κατι πήγε στραβά κατα το υποβολή της αίτησης');
      throw new Error(response.error);
    }
    return response;
  });

  const onSubmitRequest = (
    data: ISubmitRequest,
    setOpenModal: Dispatch<boolean>
  ) => {
    command.execute(data, {
      onSuccess: async (response: string) => {
        //TODO UDPATE My Requests from queryCache
        setOpenModal(false);
        successNotify(
          'Επιτυχία',
          'Η υποβολή της αίτησης πραγματοποιήθηκε με επιτυχία'
        );
      },
    });
  };
  return { onSubmitRequest, loading: command.isLoading };
};

export default useRequestDetailsModal;
