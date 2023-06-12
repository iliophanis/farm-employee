import { Dispatch, useState } from 'react';
import { useQueryClient } from '@tanstack/react-query';
import { useRouter } from 'next/navigation';
import customAxios from '@/shared/api/agent';
import { errorNotify, successNotify } from '@/shared/components/toast';
import { useCommand, useQuery } from '@/shared/hooks/useQuery';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { getQueryParams } from '@/shared/api/utilities';
import {
  EmptyEmployeeRequestRes,
  IEmployeeRequestResponse,
} from '@/modules/employee/requests/models/IEmployeeRequestList';
import { UserRequest } from '@/modules/home/models/IUserRequest';
import {
  IFarmerRequestItem,
  IFarmerRequestResponse,
} from '@/modules/farmer/requests/models/IFarmerRequestList';

const useFarmerRequests = () => {
  const { auth, isAuthenticated } = useAuth();
  const { push } = useRouter();
  const [data, setData] = useState<IFarmerRequestItem[]>([]);
  const queryClient = useQueryClient();
  const [queryParams, setQueryParams] = useState({
    currentPage: 1,
    pageSize: 3,
    filter: '',
    type: 'personal',
  });

  const userRequestsQuery = useQuery(
    ['farmer.requests.list', queryParams],
    async () => {
      if (!isAuthenticated()) push('/');

      const searchQueryParams = getQueryParams(queryParams);
      const response = await customAxios.get(
        `/farmerRequests/list?${searchQueryParams}`,
        auth?.token
      );

      if (response.error && response.error !== 'Network Error')
        errorNotify(
          'Σφάλμα',
          'Κατι πήγε στραβά κατα την παραλαβή των αιτήσεων'
        );

      if (response.error) return EmptyEmployeeRequestRes;

      return response;
    }
  );
  const command = useCommand([], async (id) => {
    const response = await customAxios.del(`/requests/${id}`, auth!.token);
    if (response.error && response.error !== 'Network Error') {
      errorNotify('Σφάλμα', 'Κατι πήγε στραβά κατα την παραλαβή των αιτήσεων');
      throw new Error(response.error);
    }

    if (response.error) {
      errorNotify('Σφάλμα', 'Κατι πήγε στραβά κατα το διαγραφή της αίτησης');
      throw new Error(response.error);
    }

    return response;
  });

  const onDeleteRequest = (id: number, setOpenModal: Dispatch<boolean>) => {
    command.execute(id, {
      onSuccess: async (response: string) => {
        //TODO UDPATE My Requests from queryCache
        setOpenModal(false);
        successNotify(
          'Επιτυχία',
          'Η διαγραφή της αίτησης πραγματοποιήθηκε με επιτυχία'
        );
        setData((state) => {
          const items = state.slice();
          const findIdx = items.findIndex((x) => x.requestId === id);
          if (findIdx !== -1) items.splice(findIdx, 1);
          return items;
        });
      },
    });
  };

  const getReqDetailsCommand = useCommand([], async (id) => {
    const response = await customAxios.get(`/requests/user/${id}`, auth?.token);
    if (response.error) {
      errorNotify(
        'Σφάλμα',
        'Κατι πήγε στραβά κατα το άνοιγμα της αίτησης με id ' + id
      );
      throw new Error(response.error);
    }
    return response;
  });

  const handleGetRequestById = (
    id: number,
    setModalData: Dispatch<UserRequest | null>,
    setOpenDetailsModal: Dispatch<boolean>
  ) => {
    getReqDetailsCommand.execute(id, {
      onSuccess: async (response: UserRequest) => {
        setModalData(response);
        setOpenDetailsModal(true);
      },
    });
  };

  const handleChangePage = () => {
    setQueryParams((state) => ({
      ...state,
      currentPage: state.currentPage + 1,
    }));
  };

  const handleChangeFilter = (filter: string, clear = false) => {
    if (!filter && !clear) return;
    setQueryParams((state) => ({
      currentPage: 1,
      pageSize: 3,
      filter: clear ? '' : filter,
      type: state.type,
    }));

    setData([]);
  };
  const handleChangeType = (type = 'personal') => {
    if (!['personal', 'all'].includes(type)) return;
    if (type === queryParams.type) return;
    setData([]);
    const newQueryParams = {
      currentPage: 1,
      pageSize: 3,
      filter: '',
      type: type,
    };
    queryClient.resetQueries(['farmer.requests.list', newQueryParams]);
    setQueryParams(newQueryParams);
  };

  return {
    queryParams,
    data,
    setData,
    userRequests: userRequestsQuery.data as IFarmerRequestResponse,
    loading: userRequestsQuery.isLoading,
    deleteLoading: command.isLoading,
    handleChangePage,
    handleChangeFilter,
    handleChangeType,
    onDeleteRequest,
    handleGetRequestById,
  };
};

export default useFarmerRequests;
