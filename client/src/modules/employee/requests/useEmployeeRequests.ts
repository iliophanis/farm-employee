import { Dispatch, useEffect, useState } from 'react';
import { useQueryClient } from '@tanstack/react-query';
import { redirect, useRouter } from 'next/navigation';
import customAxios from '@/shared/api/agent';
import { errorNotify } from '@/shared/components/toast';
import { useCommand, useQuery } from '@/shared/hooks/useQuery';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { getQueryParams } from '@/shared/api/utilities';
import {
  EmptyEmployeeRequestRes,
  IEmployeeRequestItem,
  IEmployeeRequestResponse,
} from '@/modules/employee/requests/models/IEmployeeRequestList';

const useEmployeeRequests = () => {
  const { auth, isAuthenticated } = useAuth();
  const { push } = useRouter();
  const [data, setData] = useState<IEmployeeRequestItem[]>([]);

  const [queryParams, setQueryParams] = useState({
    currentPage: 1,
    pageSize: 3,
    filter: '',
    type: 'personal',
  });

  const userRequestsQuery = useQuery(
    ['employee.requests.list', queryParams],
    async () => {
      if (!isAuthenticated()) push('/');

      const searchQueryParams = getQueryParams(queryParams);
      const response = await customAxios.get(
        `/employeeRequests/list?${searchQueryParams}`,
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
      pageSize: 10,
      filter: clear ? '' : filter,
      type: state.type,
    }));
    setData([]);
  };

  const handleChangeType = (type = 'personal') => {
    if (!['personal', 'all'].includes(type)) return;
    if (type === queryParams.type) return;
    setQueryParams((state) => ({
      currentPage: 1,
      pageSize: 10,
      filter: '',
      type: type,
    }));
    setData([]);
  };

  return {
    queryParams,
    data,
    setData,
    userRequests: userRequestsQuery.data as IEmployeeRequestResponse,
    loading: userRequestsQuery.isLoading,
    handleChangePage,
    handleChangeFilter,
    handleChangeType,
  };
};

export default useEmployeeRequests;
