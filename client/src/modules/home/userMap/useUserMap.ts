import customAxios from '@/shared/api/agent';
import { useQuery } from '@/shared/hooks/useQuery';

const useUserMap = () => {
  const userRequestsQuery = useQuery(['user.requests'], async () => {
    const response = await customAxios.get(`/requests/user`);
    if (response.error) return console.log('error');
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

export default useUserMap;
