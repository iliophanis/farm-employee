import { Dispatch, useEffect, useState } from 'react';
import { useQueryClient } from '@tanstack/react-query';

import customAxios from '@/shared/api/agent';
import { errorNotify } from '@/shared/components/toast';
import { useCommand, useQuery } from '@/shared/hooks/useQuery';
import { useAuth } from '@/shared/contexts/AuthProvider';
import { UserRequest } from '../../models/IUserRequest';
import { getQueryParams } from '../../../../shared/api/utilities';

const useRequestMap = () => {
  const { auth } = useAuth();
  const queryClient = useQueryClient();
  const [searchLocation, setSearchLocation] = useState(null);
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
    if (response.error) {
      errorNotify(
        'Σφάλμα',
        'Κατι πήγε στραβά κατα το άνοιγμα της αίτησης με id ' + id
      );
      throw new Error(response.error);
    }
    return response;
  });

  const locationsCommand = async (filter: string) => {
    if (!filter) return [];
    const response = await customAxios.get(
      `/?accept-language=gr-GR&countrycodes=[gr]&addressdetails=1&country=Ελλάδα&city=${filter}&format=json`,
      undefined,
      {
        baseURL: 'https://nominatim.openstreetmap.org/',
        headers: {
          Accept: 'application/json',
        },
      }
    );
    if (response.error) {
      errorNotify(
        'Σφάλμα',
        'Κατι πήγε στραβά κατα την ανάκτηση της λίστας τοποθεσιών '
      );
      throw new Error(response.error);
    }
    return response;
  };

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

  useEffect(() => {
    const getSearchRequests = async () => {
      if (searchLocation === null) return;
      const searchQueryParams = getQueryParams(searchLocation['value']);
      const response = await customAxios.get(
        `/requests/user?${searchQueryParams}`
      );
      if (response.error) {
        errorNotify(
          'Σφάλμα',
          'Κατι πήγε στραβά κατα την παραλαβή των αιτήσεων'
        );
        throw new Error(response.error);
      }
      queryClient.setQueryData(['user.requests'], () => response);
    };
    getSearchRequests();
  }, [searchLocation]);

  const handleGetLocationOptions = async (
    inputValue: string,
    callback: (options: any[]) => void
  ) => {
    const data = await locationsCommand(inputValue);
    const options = data.map((d: any) => ({
      label: d.display_name,
      value: {
        minLat: d.boundingbox[0],
        maxLat: d.boundingbox[1],
        minLon: d.boundingbox[2],
        maxLon: d.boundingbox[3],
      },
    }));
    callback(options || []);
  };

  return {
    userRequests: userRequestsQuery.data as UserRequest[],
    loading: userRequestsQuery.isLoading,
    handleGetRequestById,
    handleGetLocationOptions,
    setSearchLocation,
  };
};

export default useRequestMap;
