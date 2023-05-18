import customAxios from '@/shared/api/agent';
import { errorNotify } from '@/shared/components/toast';
import { useQuery } from '@/shared/hooks/useQuery';

const useRequestMap = () => {
  const userRequestsQuery = useQuery(['user.requests'], async () => {
    const response = await customAxios.get(`/requests/user`);
    if (response.error) {
      errorNotify('Σφάλμα', 'Κατι πήγε στραβά κατα την παραλαβή των αιτήσεων');
      return [];
    }
    return response;
  });
  return {
    userRequests: userRequestsQuery.data as {
      longtitude: number;
      latitude: number;
    }[],
    loading: userRequestsQuery.isLoading,
  };
};

export default useRequestMap;
